// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Contracts;
using Data;
using System.Collections;

namespace SkillBox.App.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<SkillBoxUser> _userManager;
        private readonly SignInManager<SkillBoxUser> _signInManager;
        private readonly SkillBoxDbContext _dbContext;
        private readonly IUserService _userService;

        public IndexModel(
            UserManager<SkillBoxUser> userManager,
            SignInManager<SkillBoxUser> signInManager,
            SkillBoxDbContext dbContext,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userService = userService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public string ProfilePhoto { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public List<City> Cities { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            [Display(Name = "Име")]
            public string FirstName { get; set; }

            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [EmailAddress]
            [Display(Name = "Имейл адрес")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "City")]
            public int City { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public int Gender { get; set; }

            [Phone]
            [Display(Name = "Телефонен номер")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Уебсайт")]
            public string WebsiteName { get; set; }

            [Display(Name = "Професия")]
            public string Career { get; set; }

            [Display(Name = "Автобиография")]
            public string Bio { get; set; }
            public string NewSkillName { get; set; }
            public int NewSkillPoints { get; set; }
            public IList<Skill> Skills { get; set; }

            [Display(Name = "Стара парола")]
            public string OldPassword { get; set; }
            [Display(Name = "Нова парола")]
            public string NewPassword { get; set; }
            [Display(Name = "Потвърди парола")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Въведи парола")]
            public string DangerPassword { get; set; }
        }

        private async Task LoadAsync(SkillBoxUser user)
        {
            user = _userService.GetUserByUsername(user.UserName);
            Username = user.UserName;
            ProfilePhoto = user.ProfilePhoto;
            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                WebsiteName = user.WebsiteName,
                Gender = user.Gender.Id,
                City = user.City.Id,
                Career = user.Career,
                Bio = user.Bio,
                Skills = user.Skills.ToList()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Cities = new List<City>();
            Cities = await _dbContext.Cities.ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.FirstName != null)
            {
                user.FirstName = Input.FirstName;
            }
            if (Input.LastName != null)
            {
                user.LastName = Input.LastName;
            }
            if (Input.Email != null)
            {
                user.Email = Input.Email;
            }
            if (Input.PhoneNumber != null)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }
            if (Input.WebsiteName != null)
            {
                user.WebsiteName = Input.WebsiteName;
            }
            if (Input.Bio != null)
            {
                user.Bio = Input.Bio;
            }
            if (Input.Career != null)
            {
                user.Career = Input.Career;
            }
            if (Input.NewSkillName != null
                && Input.NewSkillPoints != 0)
            {
                var allSkillLevels = _userService.GetAllSkillLevels();
                var level = allSkillLevels.FirstOrDefault(sl => sl.BGName.TrimEnd('%') == Input.NewSkillPoints.ToString());
                if (level != null)
                {
                    user.Skills.Add(new Skill
                    {
                        Name = Input.NewSkillName,
                        Level = level
                    });
                }
            }

            user.City = _dbContext.Cities.FirstOrDefault(c => c.Id == Input.City);
            user.Gender = _dbContext.Genders.FirstOrDefault(c => c.Id == Input.Gender);

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                Cities = new List<City>();
                Cities = await _dbContext.Cities.ToListAsync();
                return Page();
            }

            if (Input.NewPassword != null && Input.ConfirmPassword != null)
            {
                if (await _userManager.CheckPasswordAsync(user, Input.OldPassword))
                {
                    if (Input.NewPassword == Input.ConfirmPassword
                     && Input.NewPassword.Length > 5
                     && Input.ConfirmPassword.Length > 5)
                    {
                        await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
                        await _signInManager.RefreshSignInAsync(user);
                        StatusMessage = "Вашата парола беше променена!";
                        return RedirectToPage();
                    }
                }
                await LoadAsync(user);
                Cities = new List<City>();
                Cities = await _dbContext.Cities.ToListAsync();
                StatusMessage = "Неуспешен опит за смяна на паролата!";
                return Page();
            }

            if (Input.DangerPassword != null)
            {
                if (await _userManager.CheckPasswordAsync(user, Input.DangerPassword))
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddYears(200));
                    await _signInManager.SignOutAsync();
                    return RedirectToPage();
                }
                await LoadAsync(user);
                Cities = new List<City>();
                Cities = await _dbContext.Cities.ToListAsync();
                StatusMessage = "Паролата, която сте въвели е невалидна!";
                return Page();
            }

            await _userService.UpdateUserProps(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Вашият профил беше подновен!";
            return RedirectToPage();
        }
    }
}
