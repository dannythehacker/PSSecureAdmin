using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;

namespace papershredder432.PSSecureAdmin
{
    public class PSSecureAdmin : RocketPlugin<SecureAdminConfiguration>
    {
        public static PSSecureAdmin Instance;

        protected override void Load()
        {
            Instance = this;
            Logger.LogWarning("[PSSecureAdmin] Loaded, made by papershredder432, join the Discord for support: https://discord.gg/ydjYVJ2");
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;

            Logger.Log($"User Count: {Configuration.Instance.Users.Count}");
            Logger.Log($"");

            foreach (User user in Configuration.Instance.Users)
            {
                Logger.Log($"User SteamID64: {user.SteamID64}");
                Logger.Log($"User Password: {user.Password}");
            }
        }

        protected override void Unload()
        {
            Instance = null;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (Instance.Configuration.Instance.unadminAllOnDisconnect == true)
            {
                if (player.IsAdmin)
                {
                    player.Admin(false);
                    Logger.LogWarning($"Unadmined ({player}) {player.SteamName} because they disconnected.");
                }
                else return;
            } else if (Instance.Configuration.Instance.unadminAllOnDisconnect == false)
            {
                if (Instance.Configuration.Instance.unadminUsersOnDisconnect == true)
                {
                    var FindMe = Instance.Configuration.Instance.Users.Find(x => x.SteamID64 == player.CSteamID.m_SteamID);

                    if (FindMe.SteamID64 == player.CSteamID.m_SteamID)
                    {
                        if (player.IsAdmin)
                        {
                            player.Admin(false);
                            Logger.LogWarning($"Unadmined user ({player}) {player.SteamName} because they disconnected.");
                        }
                        else return;
                    }
                    else return;
                }
            }
        }
    }
}
