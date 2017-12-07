﻿
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Debugger.Interop;
using System.Runtime.InteropServices;
using nanoFramework.Tools.VisualStudio.Extension;

namespace Microsoft.VisualStudioTools
{
    class ProvideDebugLanguageAttribute : RegistrationAttribute
    {
        private readonly string _languageGuid, _languageName, _engineGuid, _eeGuid;

        public ProvideDebugLanguageAttribute(string languageName, string languageGuid, string eeGuid, string debugEngineGuid)
        {
            _languageName = languageName;
            _languageGuid = languageGuid;
            _eeGuid = eeGuid;
            _engineGuid = debugEngineGuid;
        }

        public override void Register(RegistrationContext context)
        {
            var langSvcKey = context.CreateKey("Languages\\Language Services\\" + _languageName + "\\Debugger Languages\\" + _languageGuid);
            langSvcKey.SetValue("", _languageName);
            // 994... is the vendor ID (Microsoft)
            var eeKey = context.CreateKey("AD7Metrics\\ExpressionEvaluator\\" + _languageGuid + "\\{994B45C4-E6E9-11D2-903F-00C04FA302A1}");
            eeKey.SetValue("Language", _languageName);
            eeKey.SetValue("Name", _languageName);
            eeKey.SetValue("CLSID", _eeGuid);

            var engineKey = eeKey.CreateSubkey("Engine");
            engineKey.SetValue("0", _engineGuid);
        }

        public override void Unregister(RegistrationContext context)
        {
        }
    }
}

