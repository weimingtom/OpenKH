namespace khiiMapv.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
            base..ctor();
            return;
        }

        internal static Bitmap ActualSizeHS
        {
            get
            {
                object obj2;
                return (Bitmap) ResourceManager.GetObject("ActualSizeHS", resourceCulture);
            }
        }

        [EditorBrowsable(2)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
                return;
            }
        }

        internal static string EVTjp0
        {
            get
            {
                return ResourceManager.GetString("EVTjp0", resourceCulture);
            }
        }

        internal static string EVTjp1
        {
            get
            {
                return ResourceManager.GetString("EVTjp1", resourceCulture);
            }
        }

        internal static string EVTjp2
        {
            get
            {
                return ResourceManager.GetString("EVTjp2", resourceCulture);
            }
        }

        internal static string EVTjp3
        {
            get
            {
                return ResourceManager.GetString("EVTjp3", resourceCulture);
            }
        }

        [EditorBrowsable(2)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                System.Resources.ResourceManager manager;
                if (object.ReferenceEquals(resourceMan, null) == null)
                {
                    goto Label_002D;
                }
                manager = new System.Resources.ResourceManager("khiiMapv.Properties.Resources", typeof(Resources).Assembly);
                resourceMan = manager;
            Label_002D:
                return resourceMan;
            }
        }

        internal static Bitmap ROCKET
        {
            get
            {
                object obj2;
                return (Bitmap) ResourceManager.GetObject("ROCKET", resourceCulture);
            }
        }

        internal static Bitmap search4files
        {
            get
            {
                object obj2;
                return (Bitmap) ResourceManager.GetObject("search4files", resourceCulture);
            }
        }

        internal static string SYSjp
        {
            get
            {
                return ResourceManager.GetString("SYSjp", resourceCulture);
            }
        }
    }
}

