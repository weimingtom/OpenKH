using System;
using System.Collections.Generic;
namespace khiiMapv.Parse02
{
	public class StrCodeCollection : List<StrCode>
	{
		public override string ToString()
		{
			string text = "";
			foreach (StrCode current in this)
			{
				text += current.ToString();
			}
			return text;
		}
	}
}
