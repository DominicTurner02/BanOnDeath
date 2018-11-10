using Rocket.API;

namespace BanOnDeath
{
    public class ConfigurationBanOnDeath : IRocketPluginConfiguration
    {
        public uint BanLengthSeconds;
        public bool OnlyBanOnSuicide;
        public string BanReason;

        public void LoadDefaults()
        {
            BanLengthSeconds = 86400;
            OnlyBanOnSuicide = false;
            BanReason = "You died!";
        }

    }
}
