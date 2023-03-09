using UCompany.Platform.Debugging;

namespace UCompany.Platform
{
    public class PlatformConsts
    {
        public const string LocalizationSourceName = "UCompany.Platform";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "0861b223eaa8449dabd891d8be38f5f8";
    }
}
