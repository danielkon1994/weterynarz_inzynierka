using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Weterynarz.Domain.EntitiesDb;
using Microsoft.AspNetCore.Identity;
using Weterynarz.Web.Services;
using Microsoft.Extensions.Logging;
using Weterynarz.Basic.Const;
using Weterynarz.Web.Models.NotifyMessage;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.Users;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IUsersRepository _usersRepository;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IUsersRepository usersRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll(int page = 1)
        {
            IQueryable<UserViewModel> listUsersQueryable = _usersRepository.GetListUsersViewModel();
            var model = await PagingList.CreateAsync(listUsersQueryable.OrderBy(a => a.Name), 20, page);

            return View(model);
        }

        [HttpGet]
        public IActionResult ListDoctors(int page = 1)
        {
            IQueryable<UserViewModel> listUsersQueryable = _usersRepository.GetListUsersViewModel().Where(u => u.Roles.Contains<string>(UserRoles.Doctor));
            var model = PagingList.Create(listUsersQueryable.OrderBy(a => a.Name), 20, page);

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(string returnUrl = null)
        {
            UsersManageViewModel model = new UsersManageViewModel
            {
                RolesList = UserRoles.GetUserRolesSelectList()
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UsersManageViewModel model, string returnUrl = null)
        {
            await checkUsersManageViewModelAsync(model);

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Active = true,
                    Address = model.Address,
                    City = model.City,
                    CreationDate = DateTime.Now,
                    Deleted = false,
                    EmailConfirmed = true,
                    HouseNumber = model.HouseNumber,
                    Name = model.Name,
                    Surname = model.Surname,
                    ZipCode = model.ZipCode,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.SelectedRoles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation("User created a new account with password.");

                    return RedirectToAction("ListAll");
                }
                AddErrors(result);
            }

            model.RolesList = UserRoles.GetUserRolesSelectList();

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            UsersRegisterViewModel model = new UsersRegisterViewModel
            {
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsersRegisterViewModel model, string returnUrl = null)
        {
            await checkUsersRegisterViewModelAsync(model);

            if (ModelState.IsValid)
            {
                var client = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Active = true,
                    Address = model.Address,
                    City = model.City,
                    CreationDate = DateTime.Now,
                    Deleted = false,                    
                    Name = model.Name,
                    Surname = model.Surname,
                    ZipCode = model.ZipCode,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(client, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(client, UserRoles.Client);
                
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation("User created a new account with password.");

                    return RedirectToAction("Login", "Account");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            UsersManageViewModel model;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                base.NotifyMessage("Nie znaleziono użytkownika z takim identyfikatorem", "Upppsss !", MessageStatus.error);
                return RedirectToAction("ListAll");
            }

            model = _usersRepository.GetEditViewModel(user);

            var userRoles = await _userManager.GetRolesAsync(user);
            model.RolesList = UserRoles.GetUserRolesSelectList(userRoles);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersManageViewModel model)
        {
            await checkUsersManageViewModelAsync(model);

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _usersRepository.EditUser(model);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        string newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                        await _usersRepository.SavePassword(user, newPasswordHash);
                    }

                    var currentUserRoles = await _userManager.GetRolesAsync(user);
                    var deleteUserRoles = currentUserRoles.Except(model.SelectedRoles).ToList();
                    await _userManager.RemoveFromRolesAsync(user, deleteUserRoles);

                    var newUserRoles = model.SelectedRoles.Except(currentUserRoles).ToList();
                    await _userManager.AddToRolesAsync(user, newUserRoles);

                    Message message = new Message
                    {
                        Text = "Sukces !",
                        OptionalText = "Pomyślnie zapisano typ",
                        MessageStatus = MessageStatus.success
                    };
                    base.NotifyMessage(message);
                }
                else
                {
                    base.NotifyMessage("Wystąpił błąd podczas edycji", "Upppsss !", MessageStatus.error);
                }

                return RedirectToAction("ListAll");
            }

            model.RolesList = UserRoles.GetUserRolesSelectList();

            return View(model);
        }

        public async Task<IActionResult> UnlockAsync(string id)
        {
            Message message = null;

            try
            {
                bool result = await _usersRepository.UnlockUser(id);
                //bool result = false;

                if (result)
                {
                    message = new Message
                    {
                        Text = "Sukces !",
                        OptionalText = "Pomyślnie odblokowano użytkownika",
                        MessageStatus = MessageStatus.success
                    };
                    base.NotifyMessage(message);
                }
                else
                {
                    message = new Message
                    {
                        Text = "Uppsss !",
                        OptionalText = "Coś poszło nie tak przy odblokowywaniu użytkownika",
                        MessageStatus = MessageStatus.error
                    };
                    base.NotifyMessage(message);
                }

                return RedirectToAction("ListAll");
            }
            catch (Exception)
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Coś poszło nie tak przy odblokowywaniu użytkownika",
                    MessageStatus = MessageStatus.error
                };
                base.NotifyMessage(message);
            }

            return RedirectToAction("ListAll");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Message message = null;

            try
            {
                bool result = await _usersRepository.DeleteUser(id);
                //bool result = false;

                if (result)
                {
                    message = new Message
                    {
                        Text = "Sukces !",
                        OptionalText = "Pomyślnie usunięto użytkownika",
                        MessageStatus = MessageStatus.success
                    };
                }
                else
                {
                    message = new Message
                    {
                        Text = "Uppsss !",
                        OptionalText = "Coś poszło nie tak przy usuwaniu użytkownika",
                        MessageStatus = MessageStatus.error
                    };
                }
            }
            catch (Exception)
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Coś poszło nie tak przy usuwaniu użytkownika",
                    MessageStatus = MessageStatus.error
                };
            }

            return Json(message);
        }
        
        public async Task<IActionResult> BanAsync(string id)
        {
            Message message = null;

            try
            {
                bool result = await _usersRepository.BanUser(id);
                //bool result = false;

                if (result)
                {
                    message = new Message
                    {
                        Text = "Sukces !",
                        OptionalText = "Pomyślnie zablokowano użytkownika",
                        MessageStatus = MessageStatus.success
                    };
                    base.NotifyMessage(message);
                }
                else
                {
                    message = new Message
                    {
                        Text = "Uppsss !",
                        OptionalText = "Coś poszło nie tak przy blokowaniu użytkownika",
                        MessageStatus = MessageStatus.error
                    };
                    base.NotifyMessage(message);
                }

                return RedirectToAction("ListAll");
            }
            catch (Exception)
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Coś poszło nie tak przy blokowaniu użytkownika",
                    MessageStatus = MessageStatus.error
                };
                base.NotifyMessage(message);
            }

            return RedirectToAction("ListAll");
        }

        public async Task<IActionResult> ShowGraphic(string id)
        {
            Message message = null;

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Nie znaleziono użytkownika",
                    MessageStatus = MessageStatus.error
                };
                base.NotifyMessage(message);
            }

            if(!await _userManager.IsInRoleAsync(user, UserRoles.Doctor))
            {
                message = new Message
                {
                    Text = "Uppsss !",
                    OptionalText = "Ten użytkownik nie jest lekarzem",
                    MessageStatus = MessageStatus.error
                };
                base.NotifyMessage(message);
            }



            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task checkUsersManageViewModelAsync(UsersManageViewModel model)
        {
            var userNameExists = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExists != null)
            {
                if (userNameExists.Id != model.Id)
                {
                    ModelState.AddModelError("", "Użytkownik o takiej nazwie użytkownika już istnieje");
                }
            }

            var userEmailExists = await _userManager.FindByEmailAsync(model.Email);
            if (userEmailExists != null)
            {
                if (userEmailExists.Id != model.Id)
                {
                    ModelState.AddModelError("", "Użytkownik z takim adresem email już istnieje");
                }
            }
        }

        private async Task checkUsersRegisterViewModelAsync(UsersRegisterViewModel model)
        {
            var userNameExists = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExists != null)
            {
                if (userNameExists.Id != model.Id)
                {
                    ModelState.AddModelError("", "Użytkownik o takiej nazwie użytkownika już istnieje");
                }
            }

            var userEmailExists = await _userManager.FindByEmailAsync(model.Email);
            if (userEmailExists != null)
            {
                if (userEmailExists.Id != model.Id)
                {
                    ModelState.AddModelError("", "Użytkownik z takim adresem email już istnieje");
                }
            }
        }
    }
}