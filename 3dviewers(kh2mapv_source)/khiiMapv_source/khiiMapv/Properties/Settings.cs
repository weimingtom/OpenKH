﻿namespace khiiMapv.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance;

        static Settings()
        {
            defaultInstance = (Settings) SettingsBase.Synchronized(new Settings());
            return;
        }

        public Settings()
        {
            base..ctor();
            return;
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}
