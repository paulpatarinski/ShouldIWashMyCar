using System;
using Xamarin.Forms;

namespace Core
{
	public class ExtraLargeLabel : Label
	{
		public ExtraLargeLabel ()
		{
			Font = Font.BoldSystemFontOfSize (90);
			TextColor = Color.FromHex ("#F9690E");
		}
	}
}

