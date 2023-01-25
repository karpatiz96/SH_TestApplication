using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SH_TestApplication.Models
{
	public class User
	{
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public string UserName { get; set; }
        [XmlElement]
        public string FirstName { get; set; }
		[XmlElement]
		public string LastName { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime BirthDate { get; set; }
        [XmlElement]
        public string BirthPlace { get; set; }
        [XmlElement]
        public string Address { get; set; }
	}
}
