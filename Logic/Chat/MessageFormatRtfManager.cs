using CrossCutting.DataClasses;
using Logic.Chat.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Chat
{
    public class MessageFormatRtfManager : IMessageFormatManager
    {
        public void Format(Message message) 
        {
            StringBuilder stringBuilder = new StringBuilder(message.PlainText);
            foreach (EmotePosition emotePosition in message.EmotePositionList.OrderByDescending(x => x.StartIndex))
            {
                Emote emote = emotePosition.Emote;
                string str = BitConverter.ToString(emote.Data, 0).Replace("-", string.Empty);
                
                string mpic = @"{\pict\pngblip\picw" +
                                emote.Bitmap.Width.ToString() + @"\pich" + emote.Bitmap.Height.ToString() +
                                @"\picwgoal" + emote.Bitmap.Width.ToString() + @"\pichgoal" + emote.Bitmap.Height.ToString() +
                                @"\bin " + str + "}";
                stringBuilder.Remove(emotePosition.StartIndex, emotePosition.EndIndex - emotePosition.StartIndex + 1);
                stringBuilder.Insert(emotePosition.StartIndex, mpic);
            }
            message.EmoteText = stringBuilder.ToString();
        }
    }
}
