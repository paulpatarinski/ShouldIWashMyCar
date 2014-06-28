using System;
using Xamarin.Forms;

namespace Core
{
	public class OptionItem
	{
		public virtual string Title {
			get {
				var n = GetType ().Name;
				return n.Substring (0, n.Length - 10);
			}
		}

		public virtual int Count { get; set; }

		public virtual bool Selected { get; set; }

		public virtual string Icon { get { return 
				Title.ToLower ().TrimEnd ('s') + ".png"; } }

		public ImageSource IconSource { get { return ImageSource.FromFile (Icon); } }
	}
}
