using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSSecureAdmin
{
    class CommandChangePassword : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "changepassword";

        public string Help => "Changes your Bluehammer password";

        public string Syntax => "/changepassword <Old Password> <New Password>";

        public List<string> Aliases => new List<string>() { "cpw", "changepw" };

        public List<string> Permissions => new List<string>() { "ps.changepassword" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command[0].Length < 1)
            {
                UnturnedChat.Say("No password input.");
                return;
            }

            var SID = caller as UnturnedPlayer;

            var FindMe = PSSecureAdmin.Instance.Configuration.Instance.Users.Find(x => x.SteamID64 == SID.CSteamID.m_SteamID);

            var OLDPASS = FindMe.Password;

            if (FindMe.SteamID64 != SID.CSteamID.m_SteamID)
            {
                UnturnedChat.Say(caller, "You are not a permitted user!");
                return;
            } else
            {
                if (FindMe.Password != command[0])
                {
                    UnturnedChat.Say(caller, "Incorrect password.");
                    return;
                } else
                {
                    if (command[1].Length < 1)
                    {
                        UnturnedChat.Say(caller, "You did not input a new password");
                        return;
                    } else
                    {
                        var NEWPASS = command[1];

                        FindMe.Password = NEWPASS;
                        UnturnedChat.Say(caller, $"Changed password from {OLDPASS} to {NEWPASS}");
                        PSSecureAdmin.Instance.Configuration.Save();
                    }
                }
            }
        }
    }
}
