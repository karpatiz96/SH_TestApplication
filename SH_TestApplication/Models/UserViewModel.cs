using System.ComponentModel.DataAnnotations;

namespace SH_TestApplication.Models
{
	public class UserViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
		[Required]
		public string BirthPlace { get; set; }
		[Required]
		public string Address { get; set; }
	}
}
