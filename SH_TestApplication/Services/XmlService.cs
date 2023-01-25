using SH_TestApplication.Models;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SH_TestApplication.Services
{
	public class XmlService : IXmlService
	{
		public byte[] GetTableXml(List<User> users)
		{
			byte[] content;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
			using(MemoryStream ms = new MemoryStream())
			{
				xmlSerializer.Serialize(ms, users);
				content = ms.ToArray();
			}

			return content;
		}
	}
}
