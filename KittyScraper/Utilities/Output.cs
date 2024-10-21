using KittyScraper.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace KittyScraper.Utilities
{
	public static class Output
	{
		public static void ConsoleWriteLine(Cat obj)
		{
			Type t = obj.GetType();
			PropertyInfo[] pi = t.GetProperties();
			foreach (PropertyInfo p in pi)
			{
				if (p.GetValue(obj) != null)
				{
					if (p.PropertyType == typeof(List<string>))
					{
						Console.WriteLine($"{p.Name}: {string.Join(",", p.GetValue(obj))}");
					}
					else
					{
						Console.WriteLine($"{p.Name}: {p.GetValue(obj)}");
					}
				}
			}
		}

		//source: https://gist.github.com/lot224/e6e0398c2c62a334168a63f09ffff2bc
		public static void SendToWebhook(Cat obj, string webhookToken)
		{
            var client = new HttpClient();
            var message = new StringBuilder();

            Type t = obj.GetType();
			PropertyInfo[] pi = t.GetProperties();
			foreach (PropertyInfo p in pi)
			{
				if (p.GetValue(obj) != null)
				{
					if (p.PropertyType == typeof(List<string>))
					{
						message.AppendLine($"{p.Name}: {string.Join(",", p.GetValue(obj))}");
					}
					else
					{
						message.AppendLine($"{p.Name}: {p.GetValue(obj)}");
					}
				}
			}

			var WebhookContent = new
			{
				content = message.ToString()
			};

			var webhookContentJson = new StringContent(JsonConvert.SerializeObject(WebhookContent), Encoding.UTF8, "application/json");
			client.PostAsync(webhookToken, webhookContentJson).Wait();
		}

		public static void SendToWebhook(Exception ex, string webhookToken)
		{
            var client = new HttpClient();

			var WebhookContent = new
			{
				content = ex.ToString()
			};

			var webhookContentJson = new StringContent(JsonConvert.SerializeObject(WebhookContent), Encoding.UTF8, "application/json");
			client.PostAsync(webhookToken, webhookContentJson).Wait();
		}
	}
}