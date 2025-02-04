﻿using System;
using System.IO;
using System.Net;
using VRChatAPI_New.Modules;
using VRChatAPI_New.Modules.Game;

namespace VRChatAPI_New
{
    //"2019.4.40f1"
    public static class StartUp
    {
        public static void SetupDownloader()
        {
            Random rnd = new Random();
            string macAddress = EasyHash.GetSHA1String(new byte[] { (byte)rnd.Next(254), (byte)rnd.Next(254), (byte)rnd.Next(254), (byte)rnd.Next(254), (byte)rnd.Next(254) });
            Console.WriteLine($"Don't use this method everytime, Send the macaddress or it'll likely cause a ban, you can use this macaddress{Environment.NewLine}{macAddress}");
            SetupDownloader(macAddress);
        }

        public static void SetupDownloader(string macAddress)
        {
            StaticGameValues.ApiUrl = new Uri("https://api.vrchat.cloud/api/1/", UriKind.Absolute);
            StaticGameValues.GameVersion = SetupGameVersion();
            StaticGameValues.UnityVersion = SetupUnityGameVersion();
            StaticGameValues.Store = SetupStore();
            StaticGameValues.ServerVersion = SetupServerVersion();
            StaticGameValues.MacAddress = macAddress;
            StaticGameValues.CookieContainer = new CookieContainer();
            SARSGameClient.SetupClient();
            StaticGameValues.LoggedInOnce = false;
        }

        private static string SetupGameVersion()
        {
            string gameVersion = "2023.4.2p2-1390-53164fa82e-Release";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://avatarrecovery.com/Version.txt");
                StreamReader reader = new StreamReader(stream);
                gameVersion = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                client.Dispose();
            }
            catch { }
            return gameVersion;
        }

        private static string SetupServerVersion()
        {
            string serverVersion = "Release_1343";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://avatarrecovery.com/ServerVersion.txt");
                StreamReader reader = new StreamReader(stream);
                serverVersion = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                client.Dispose();
            }
            catch { }
            return serverVersion;
        }

        private static string SetupStore()
        {
            string serverVersion = "steam";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://avatarrecovery.com/Store.txt");
                StreamReader reader = new StreamReader(stream);
                serverVersion = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                client.Dispose();
            }
            catch { }
            return serverVersion;
        }

        public static bool IsLoginGameInitialised()
        {
            if(StaticGameValues.AuthKey != null)
            {
                return true;
            }
            return false;
        }

        private static string SetupUnityGameVersion()
        {
            string unityVersion = "2022.3.6f1-DWR";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://avatarrecovery.com/UnityGameVersion.txt");
                StreamReader reader = new StreamReader(stream);
                unityVersion = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                client.Dispose();
            }
            catch { }
            return unityVersion;
        }
    }
}