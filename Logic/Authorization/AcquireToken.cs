using CrossCutting.DataClasses;
using LogicAuthorization.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Authorization
{
    internal class AcquireToken : IAcquireToken
    {
        private readonly string RequestTokenUrl = @"https://id.twitch.tv/oauth2/authorize
    ?client_id=#ClientID#
    &redirect_uri=#RedirectUrl#
    &response_type=token
    &scope=#Scope#";
        private readonly string RedirectUrl = "http://127.0.0.1:62324/token/";

        private readonly string FullRequestTokenUrl;
        private string UrlAccessed = "";

        private readonly string html = @"<html>
<script type=""text/javascript"">
function parseHash()
{
    var urlhash = window.location.hash;
    urlhash = urlhash.replace(""#"", """");
    document.location.replace(""http://127.0.0.1:62324/token/""+urlhash);
}
</script>
<body onload=""parseHash()"">
</body>
</html>";

        public AcquireToken(ICredentialsManagement credentialsManagement)
        {
            Credentials credentials = credentialsManagement.Load();
            FullRequestTokenUrl = RequestTokenUrl.Replace("#ClientID#", credentials.CliendId).Replace("#RedirectUrl#", Uri.EscapeUriString(RedirectUrl)).Replace("#Scope#", Uri.EscapeUriString(string.Join(" ", Scope.List.Where(x => x != Scope.CHAT).Select(x => x.Name))));
            FullRequestTokenUrl = FullRequestTokenUrl.Replace(" ", "").Replace(Environment.NewLine, "");
        }

        public string GetToken() 
        {
            WebServer webServer = null;
            string token = "";
            try
            {
                UrlAccessed = "";
                webServer = new WebServer(62324);
                webServer.UrlAccessed += WebServer_UrlAccessed;
                Process.Start(FullRequestTokenUrl);
                while (string.IsNullOrEmpty(UrlAccessed))
                {
                    Thread.Sleep(1000);
                }
                Regex tokenRegex = new Regex(@"access_token=(?<token>\w+)");
                Match match = tokenRegex.Match(UrlAccessed);
                token = match.Groups["token"].Value;
            }
            finally 
            {
                webServer.UrlAccessed -= WebServer_UrlAccessed;
                webServer?.Stop();
            }
            return token;
        }

        private void WebServer_UrlAccessed(object sender, UrlAccessedParams e)
        {
            if (e.Url.EndsWith("/token/"))
            {
                e.HttpListenerContext.Response.ContentType = "text/html";
                int length = 0;
                foreach (byte value in new UTF8Encoding().GetBytes(this.html))
                {
                    e.HttpListenerContext.Response.OutputStream.WriteByte(value);
                    length++;
                }
                e.HttpListenerContext.Response.OutputStream.Flush();
                e.HttpListenerContext.Response.OutputStream.Close();
            }
            else if(e.Url.Contains("access_token"))
            {
                this.UrlAccessed = e.Url;
            }
        }
    }
}
