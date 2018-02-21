using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPM.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;




        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(nameof(GeneralSettings), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(GeneralSettings), value);

        }

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }

        public static string PassWord
        {
            get => AppSettings.GetValueOrDefault(nameof(PassWord), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PassWord), value);
        }

        public static string Group
        {
            get => AppSettings.GetValueOrDefault(nameof(Group), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Group), value);
        }

        public static string UserId
        {
            get => AppSettings.GetValueOrDefault(nameof(UserId), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserId), value);
        }

        public static void ClearAcctout()
        {
            Settings.UserName = string.Empty;
            Settings.PassWord = string.Empty;
            Settings.UserId = string.Empty;
            Settings.Group = string.Empty;

        }

    }
}
