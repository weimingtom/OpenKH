using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace khkh_xldMii.Properties
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
                    var resourceManager = new ResourceManager("khkh_xldMii.Properties.Resources",
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

        internal static Bitmap DFH
        {
            get
            {
                object @object = ResourceManager.GetObject("DFH", resourceCulture);
                return (Bitmap) @object;
            }
        }

        internal static Bitmap Happy
        {
            get
            {
                object @object = ResourceManager.GetObject("Happy", resourceCulture);
                return (Bitmap) @object;
            }
        }

        internal static Bitmap NG
        {
            get
            {
                object @object = ResourceManager.GetObject("NG", resourceCulture);
                return (Bitmap) @object;
            }
        }
    }
}