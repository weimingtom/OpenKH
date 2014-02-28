using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace khiiMapv.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("khiiMapv.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}
		internal static Bitmap ActualSizeHS
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("ActualSizeHS", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static string EVTjp0
		{
			get
			{
				return Resources.ResourceManager.GetString("EVTjp0", Resources.resourceCulture);
			}
		}
		internal static string EVTjp1
		{
			get
			{
				return Resources.ResourceManager.GetString("EVTjp1", Resources.resourceCulture);
			}
		}
		internal static string EVTjp2
		{
			get
			{
				return Resources.ResourceManager.GetString("EVTjp2", Resources.resourceCulture);
			}
		}
		internal static string EVTjp3
		{
			get
			{
				return Resources.ResourceManager.GetString("EVTjp3", Resources.resourceCulture);
			}
		}
		internal static Bitmap ROCKET
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("ROCKET", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap search4files
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("search4files", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static string SYSjp
		{
			get
			{
				return Resources.ResourceManager.GetString("SYSjp", Resources.resourceCulture);
			}
		}
		internal Resources()
		{
		}
	}
}
