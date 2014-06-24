namespace Core.Models
{
	public class WeatherViewTemplate
	{
		public string WeatherCondition { get; set; }

		public string DayAbbreviation { get; set; }

		public string TempHigh { get; set; }

		public string TempLow { get; set; }

		public string Icon { get; set; }

		public string ItemTemplateTextProperty { get { return DayAbbreviation; } }

		public string ItemTemplateDetailProperty { get { return TempLow + " / " + TempHigh + " - " + WeatherCondition; } }
	}
}