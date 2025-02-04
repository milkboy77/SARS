﻿using System;
using System.Collections.Generic;

namespace SARS.Models
{
    public class Config
    {
        public string UserId { get; set; }
        public string AuthKey { get; set; }
        public string TwoFactor { get; set; }
        public string ReuploaderUserId { get; set; }
        public string ReuploaderAuthKey { get; set; }
        public string ReuploaderTwoFactor { get; set; }
        public bool ReuploaderEnabled { get; set; }
        public string ApiKey { get; set; }
        public string PreSelectedAvatarLocation { get; set; }
        public bool PreSelectedAvatarLocationChecked { get; set; }
        public string PreSelectedWorldLocation { get; set; }
        public bool PreSelectedWorldLocationChecked { get; set; }
        public string UnityLocation2019 { get; set; }
        public string UnityLocation2022 { get; set; }
        public string ClientVersion { get; set; }
        public string UnityVersion { get; set; }
        public DateTime ClientVersionLastUpdated { get; set; }
        public bool LightMode { get; set; }
        public string ThemeColor { get; set; }
        public string HotSwapName2022 { get; set; }
        public string HotSwapName2019 { get; set; }
        public string MacAddress { get; set; }
        public string ReuploaderMacAddress { get; set; }
        public int ViewerVersion { get; set; }
        public int HsbVersion { get; set; }
        public int HsbVersion2019 { get; set; }
        public int HsbWorldVersion { get; set; }
        public bool AltAPI { get; set; }
        public bool Tls11 { get; set; }
        public bool Tls12 { get; set; }
        public bool Tls13 { get; set; }
        public string CustomApi { get; set; }
        public bool CustomApiUse { get; set; }
        public string UserAgent { get; set; }
        public bool ReplaceUnityVersion { get; set; }
        public bool IndexAdded { get; set; }
        public int AvatarsInLocalDatabase { get; set; }
        public int AvatarsLoggedToApi { get; set; }
    }
}