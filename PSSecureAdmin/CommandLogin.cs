using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSSecureAdmin
{
    class CommandLogin : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "login";

        public string Help => "Allows users to login to Bluehammer. Verifys with SteamID64";

        public string Syntax => "/login <password>";

        public List<string> Aliases => new List<string> { };

        public List<string> Permissions => new List<string>() { "ps.login" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command[0].Length < 0)
            {
                UnturnedChat.Say("No password input.");
                return;
            }

            var SID = caller as UnturnedPlayer;

            var FindMe = PSSecureAdmin.Instance.Configuration.Instance.Users.Find(x => x.SteamID64 == SID.CSteamID.m_SteamID);
            var FOUND = FindMe.Password;

            if (FindMe.SteamID64 != SID.CSteamID.m_SteamID)
            {
                UnturnedChat.Say(caller, "You are not a permitted user!");
                return;
            }
            else
            {
                if (FOUND != command[0])
                {
                    UnturnedChat.Say(caller, "Incorrect password.");
                    return;
                } else
                {
                    if (SID.IsAdmin)
                    {
                        UnturnedChat.Say(caller, "Already in admin mode.");
                        return;
                    } else
                    {
                        SID.Admin(true);
                        UnturnedChat.Say(caller, "Successfully logged into Bluehammer!");
                    }
                }
            }
        }
    }
}
