using CrossCutting.DataClasses;
using LogicAuthorization.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Authorization
{
    public class CredentialsManagement : ICredentialsManagement
    {
        private readonly string ClientId = "ffhsd8pxead4mqc1cqf4y3vdp75chr";

        public Credentials Load()
        {
            return new Credentials() { AccessToken = File.ReadAllText("twitch_token.txt"), CliendId = ClientId };
        }
    }
}
