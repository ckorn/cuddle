using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class Scope
    {
        public static Scope CHAT { get; } = new Scope("chat_login", "chat");
        public static Scope USERINFO { get; } = new Scope("user_read", "user");
        public static Scope EDITOR { get; } = new Scope("channel_editor", "editor");
        public static Scope EDIT_BROADCAST { get; } = new Scope("user:edit:broadcast", "broadcast");
        public static Scope COMMERICALS { get; } = new Scope("channel_commercial", "commercials");
        public static Scope SUBSCRIBERS { get; } = new Scope("channel_subscriptions", "subscribers");
        public static Scope FOLLOW { get; } = new Scope("user_follows_edit", "follow");
        public static Scope SUBSCRIPTIONS { get; } = new Scope("user_subscriptions", "subscriptions");
        public static IEnumerable<Scope> List = GetList();
        public string Name { get; }
        public string Label { get; }

        private Scope(string name, string label)
        {
            Name = name;
            Label = label;
        }

        private static IEnumerable<Scope> GetList() 
        {
            yield return CHAT;
            yield return USERINFO;
            yield return EDITOR;
            yield return EDIT_BROADCAST;
            yield return COMMERICALS;
            yield return SUBSCRIBERS;
            yield return FOLLOW;
            yield return SUBSCRIPTIONS;
        }
    }
}
