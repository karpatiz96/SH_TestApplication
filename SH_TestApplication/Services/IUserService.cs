using SH_TestApplication.Models;

namespace SH_TestApplication.Services
{
	public interface IUserService
	{
		Task<List<UserViewModel>> GetUsers();

		Task<List<UserListViewModel>> GetUserList();

		Task<UserViewModel> GetUser(int id);

		Task<List<User>> GetUsersForXML();

		Task UpdateUser(UserViewModel userViewModel);
	}
}
