using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Unturned.Events;
using Steamworks;
using Rocket.API;
using SDG.Unturned;


namespace BanOnDeath
{
    public class BanOnDeath : RocketPlugin<ConfigurationBanOnDeath>
    {
        public static BanOnDeath Instance { get; private set; }

        protected override void Load()
        {
            base.Load();
            Instance = this;

            Logger.LogWarning(" Loading BanOnDeath by Mr.Kwabs.");

            Logger.LogWarning($"\n Ban Length: {Configuration.Instance.BanLengthSeconds} Seconds");
            Logger.LogWarning($"\n Ban Reason: '{Configuration.Instance.BanReason}' \n");

            if (Configuration.Instance.OnlyBanOnSuicide)
            {
                Logger.LogWarning(" Only Ban on Suicide: Enabled");
            }
            else
            {
                Logger.LogError(" Only Ban on Suicide: Disabled");
            }
            Logger.LogWarning("\n BanOnDeath by Mr.Kwabs has successfully loaded.");

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            base.Unload();
            Instance = null;
        }


        private void OnPlayerDeath(UnturnedPlayer Player, EDeathCause Cause, ELimb Limb, CSteamID Murderer)
        {

            if (Configuration.Instance.OnlyBanOnSuicide)
            {
                if (Cause.ToString() == "SUICIDE" || UnturnedPlayer.FromCSteamID(Murderer) == Player)
                {
                    DeathBan(Player);
                } else
                {
                    Logger.LogWarning("Not banning Player, as it wasn't suicide.");
                }
            }
            else
            {
                DeathBan(Player);
            }

        }

        private void DeathBan(UnturnedPlayer player)
        {
            if (!player.HasPermission("banondeath.ignore"))
            {
                player.Ban(Configuration.Instance.BanReason, Configuration.Instance.BanLengthSeconds);
                Logger.LogWarning($"{player.DisplayName} [{player.Id}] has been banned for {Configuration.Instance.BanLengthSeconds} seconds for dying!");
            } else
            {
                Logger.LogWarning("Ignoring Death...");
            }


        
        }

    }

  

}
