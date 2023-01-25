using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SH_TestApplication.Models
{
	public class UserListViewModel
	{
		[Display(Name = "Id")]
		public int Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Display(Name ="Last name")]
        public string LastName { get; set; }
        [Display(Name ="Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime BirthDate { get; set; }
        [DisplayName("Birth place")]
        public string BirthPlace { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
	}
}
