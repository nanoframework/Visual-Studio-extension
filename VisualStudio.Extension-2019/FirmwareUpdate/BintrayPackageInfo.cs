﻿//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Newtonsoft.Json;
using System;

namespace nanoFramework.Tools.VisualStudio.Extension.FirmwareUpdate
{
    [Serializable]
    internal class BintrayPackageInfo
    {
        [JsonProperty("latest_version")]
        public string LatestVersion { get; set; }
    }
}
