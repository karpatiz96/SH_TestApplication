using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SH_TestApplication.Models;
using SH_TestApplication.Services;

namespace SH_TestApplication.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly IXmlService xmlService;

		public UserController(IUserService UserService, IXmlService XmlService)
		{
			userService = UserService;
			xmlService = XmlService;
		}

		[HttpGet("Index")]
		public async Task<ActionResult<IEnumerable<UserListViewModel>>> Index()
		{
            var users = await userService.GetUserList();
			return View(users);
		}

		[HttpGet("Search")]
		public async Task<IEnumerable<UserListViewModel>> Search(string search = "")
		{
			var users = await userService.GetUserList();
			if (!string.IsNullOrEmpty(search)) {
				search = search.ToLower();
				var result = users.Where(x => x.UserName.ToLower().Contains(search) ||
					x.FirstName.ToLower().Contains(search) ||
                    x.LastName.ToLower().Contains(search) ||
                    x.BirthPlace.ToLower().Contains(search) ||
					x.Address.ToLower().Contains(search) ||
					x.BirthDate.ToString("yyyy-MM-dd").Contains(search)
                    ).ToList();

				return result;
			}
			return users;
		}

		[HttpGet("Edit/{id}")]
		public async Task<ActionResult<UserViewModel>> Edit(int id)
		{
			var user = await userService.GetUser(id);

			if(user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		[HttpPost("Edit/{id}")]
		public async Task<IActionResult> Edit(int id, UserViewModel userViewModel)
		{
			if (ModelState.IsValid)
			{
				var users = await userService.GetUsers();
				var userNameExists = users.Where(x => x.Id != id)
					.Any(x => x.UserName.ToLower()
						.Equals(userViewModel.UserName.ToLower()));

				if (userNameExists)
				{
					ViewBag.Message = "User name already exists!";
					return View(userViewModel);
				}

                await userService.UpdateUser(userViewModel);
                return LocalRedirect("/User/Index");
            }

			return View(userViewModel);
		}

		[HttpGet("Download")]
		public async Task<IActionResult> Download()
		{
			var users = await userService.GetUsersForXML();
			byte[] content = xmlService.GetTableXml(users);

			return File(content, "application/xml", "User_Table.xml");
		}
	}
}
