using SH_TestApplication.Models;

namespace SH_TestApplication.Services
{
	public interface IXmlService
	{
		byte[] GetTableXml(List<User> users);
	}
}
