using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Core
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		private string title = string.Empty;
		public const string TitlePropertyName = "Title";

		/// <summary>
		/// Gets or sets the "Title" property
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get { return title; }
			set { ChangeAndNotify (ref title, value); }
		}


		private string subTitle = string.Empty;
		/// <summary>
		/// Gets or sets the "Subtitle" property
		/// </summary>
		public const string SubtitlePropertyName = "Subtitle";


		public string Subtitle {
			get { return subTitle; }
			set { ChangeAndNotify (ref subTitle, value); }
		}


		private string icon = null;
		/// <summary>
		/// Gets or sets the "Icon" of the viewmodel
		/// </summary>
		public const string IconPropertyName = "Icon";


		public string Icon {
			get { return icon; }
			set { ChangeAndNotify (ref icon, value); }
		}


		protected bool ChangeAndNotify<T> (ref T property, T value, [CallerMemberName] string propertyName = "")
		{
			if (!EqualityComparer<T>.Default.Equals (property, value)) {
				property = value;
				NotifyPropertyChanged (propertyName);
				return true;
			}


			return false;
		}


		protected void NotifyPropertyChanged ([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}