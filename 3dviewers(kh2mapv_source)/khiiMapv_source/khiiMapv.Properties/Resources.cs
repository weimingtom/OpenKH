using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace khiiMapv.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode,
     CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    var resourceManager = new ResourceManager("khiiMapv.Properties.Resources",
                        typeof (Resources).Assembly);
                    resourceMan = resourceManager;
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static Bitmap ActualSizeHS
        {
            get
            {
                object @object = ResourceManager.GetObject("ActualSizeHS", resourceCulture);
                return (Bitmap) @object;
            }
        }

        internal static string EVTjp0
        {
            get { return ResourceManager.GetString("EVTjp0", resourceCulture); }
        }

        internal static string EVTjp1
        {
            get { return ResourceManager.GetString("EVTjp1", resourceCulture); }
        }

        internal static string EVTjp2
        {
            get { return ResourceManager.GetString("EVTjp2", resourceCulture); }
        }

        internal static string EVTjp3
        {
            get { return ResourceManager.GetString("EVTjp3", resourceCulture); }
        }

        internal static Bitmap ROCKET
        {
            get
            {
                object @object = ResourceManager.GetObject("ROCKET", resourceCulture);
                return (Bitmap) @object;
            }
        }

        internal static Bitmap search4files
        {
            get
            {
                object @object = ResourceManager.GetObject("search4files", resourceCulture);
                return (Bitmap) @object;
            }
        }

        internal static string SYSjp
        {
            get { return ResourceManager.GetString("SYSjp", resourceCulture); }
        }
    }
}