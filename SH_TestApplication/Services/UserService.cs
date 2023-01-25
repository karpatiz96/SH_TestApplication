using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using SH_TestApplication.Configuration;
using SH_TestApplication.Models;
using System.Globalization;

namespace SH_TestApplication.Services
{
	public class UserService : IUserService
	{
        private readonly InputFileConfiguration fileConfiguration;

        public UserService(IOptions<InputFileConfiguration> options)
        {
            fileConfiguration = options.Value;
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var users = await GetUsers();
            var user = users.Where(x => x.Id == id)
                .FirstOrDefault();

            return user;
        }

        public async Task<List<UserListViewModel>> GetUserList()
		{
			List<UserListViewModel> users = new List<UserListViewModel>();
			List<string> lines = new List<string>();
            lines = await ReadFile();

            foreach (var line in lines)
            {
                string[] entries = line.Split(';');
                users.Add(new UserListViewModel
                {
                    Id = Int32.Parse(entries[0]),
                    UserName = entries[1],
                    FirstName = entries[3],
                    LastName = entries[4],
                    BirthDate = DateTime.ParseExact(entries[5], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    BirthPlace = entries[6],
                    Address = entries[7]
                });
            }

            return users;
		}

		public async Task<List<UserViewModel>> GetUsers()
		{
			List<UserViewModel> users = new List<UserViewModel>();
			List<string> lines = new List<string>();
            lines = await ReadFile();

            foreach (var line in lines)
            {
                string[] entries = line.Split(';');
                users.Add(new UserViewModel
                {
                    Id = Int32.Parse(entries[0]),
                    UserName = entries[1],
                    Password = entries[2],
                    FirstName = entries[3],
                    LastName = entries[4],
                    BirthDate = DateTime.ParseExact(entries[5], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    BirthPlace = entries[6],
                    Address = entries[7]
                });
            }

            return users;
		}

        public async Task<List<User>> GetUsersForXML()
        {
            List<User> users = new List<User>();
            List<string> lines = new List<string>();
            lines = await ReadFile();

            foreach (var line in lines)
            {
                string[] entries = line.Split(';');
                users.Add(new User
                {
                    Id = Int32.Parse(entries[0]),
                    UserName = entries[1],
                    FirstName = entries[3],
                    LastName = entries[4],
                    BirthDate = DateTime.ParseExact(entries[5], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    BirthPlace = entries[6],
                    Address = entries[7]
                });
            }

			return users;
        }

        public async Task UpdateUser(UserViewModel userViewModel)
        {
            var users = await GetUsers();
            var userUpdate = users.FirstOrDefault(x => x.Id == userViewModel.Id);
            if (userUpdate != null)
            {
                userUpdate.UserName = userViewModel.UserName;
                if (!string.IsNullOrEmpty(userViewModel.Password))
                {
                    userUpdate.Password = userViewModel.Password;
                }
                userUpdate.FirstName = userViewModel.FirstName;
                userUpdate.LastName = userViewModel.LastName;
                userUpdate.BirthDate = userViewModel.BirthDate;
                userUpdate.BirthPlace = userViewModel.BirthPlace;
                userUpdate.Address = userViewModel.Address;
            

            List<string> lines = new List<string>();

            foreach (var user in users)
            {
                var line = user.Id + ";" + user.UserName + ";" +
                           user.Password + ";" + user.FirstName + ";" +
                           user.LastName + ";" + user.BirthDate.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ";" +
                           user.BirthPlace + ";" + user.Address;
                lines.Add(line);
            }

            try
            {
                await File.WriteAllLinesAsync(fileConfiguration.Path, lines);
            } catch (Exception e) {

            }

            }
		}

        private async Task<List<string>> ReadFile()
        {
            List<string> lines = new List<string>();
            try
            {
                var result = await File.ReadAllLinesAsync(fileConfiguration.Path);
                if (result != null)
                {
                    lines = result.ToList();
                }
            } catch (Exception e) {

            }

            return lines;
        }
	}
}
