using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace papershredder432.PSSecureAdmin
{
    public class SecureAdminConfiguration : IRocketPluginConfiguration
    {
        public List<User> Users;

        public bool unadminAllOnDisconnect;
        public bool unadminUsersOnDisconnect;

        public void LoadDefaults()
        {
            unadminAllOnDisconnect = false;
            unadminUsersOnDisconnect = true;

            Users = new List<User>
            {
                new User { SteamID64 = 76561198132469161, Password = "PASSWORD" }
            };
        }
    }

    [XmlRoot("PSSecureAdminConfiguration")]
    public class User
    {
        [XmlAttribute] public ulong SteamID64 { get; set; }
        [XmlAttribute] public string Password { get; set; }
    }
}