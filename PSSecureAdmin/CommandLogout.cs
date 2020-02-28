using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSSecureAdmin
{
    class CommandLogout : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "logout";

        public string Help => "Logs you out of Bluehammer.";

        public string Syntax => "/logout";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ps.logout" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var SID = caller as UnturnedPlayer;

            if (!SID.IsAdmin)
            {
                UnturnedChat.Say(caller, "You are not logged in.");
                return;
            } else
            {
                SID.Admin(false);
                UnturnedChat.Say(caller, "Logged out of Bluehammer.");
            }
        }
    }
}
