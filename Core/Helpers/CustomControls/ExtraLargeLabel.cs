using System;
using Xamarin.Forms;

namespace Core
{
	public class ExtraLargeLabel : Label
	{
		public ExtraLargeLabel ()
		{
			Font = Font.BoldSystemFontOfSize (100);
			TextColor = Color.FromHex ("#e67e22");
		}
	}
}

