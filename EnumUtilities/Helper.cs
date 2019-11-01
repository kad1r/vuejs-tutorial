using System;

namespace EnumUtilities
{
	public static class Helper
	{
		public static int GetValueOfEnum(string name, string value)
		{
			name = replaceTurkishCharacters(name);
			var val = 0;

			try
			{
				val = Convert.ToInt32(Enum.Parse(Type.GetType(name), value));
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}

			return val;
		}

		public static string replaceTurkishCharacters(string text)
		{
			var message = text;
			var oldValue = new char[] { 'ö', 'Ö', 'ü', 'Ü', 'ç', 'Ç', 'İ', 'ı', 'Ğ', 'ğ', 'Ş', 'ş' };
			var newValue = new char[] { 'o', 'O', 'u', 'U', 'c', 'C', 'I', 'i', 'G', 'g', 'S', 's' };

			for (int i = 0; i < oldValue.Length; i++)
			{
				message = message.Replace(oldValue[i], newValue[i]);
				message = message.Replace(" ", "");
			}

			string a = message.Substring(0, 1);

			message = a.ToUpper() + message.Substring(1, message.Length - 1).ToLower();

			return message.Trim();
		}
	}
}
