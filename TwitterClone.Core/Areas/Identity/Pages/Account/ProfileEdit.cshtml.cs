using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClone.Core.CustomValidators;
using TwitterClone.Data.Models;

namespace TwitterClone.Core.Areas.Identity.Pages.Account
{
    public class ProfileEditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        [TempData]
        public string StatusMessage { get; set; }

        public ProfileEditModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [MaxLength(20)]
            [MinLength(3, ErrorMessage = "Minimum 3 chars")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [MaxLength(20)]
            [MinLength(3, ErrorMessage = "Minimum 3 chars")]
            public string LastName { get; set; }

            [Display(Name = "Biography")]
            [MaxLength(250)]
            public string Biography { get; set; }

            [Required]
            [Display(Name = "Location")]
            [MaxLength(20)]
            public string Location { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            [MinimumAgeAllowed(18)]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            public DateTime BirthDay { get; set; }

            [Display(Name = "Avatar URL")]
            [MaxLength(3000)]
            public string AvatarUrl { get; set; }

            [Display(Name = "User Name")]
            public string UserName { get; set; }


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            

            Input = new InputModel
            {
                UserName = userName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUrl = user.AvatarUrl,
                Biography = user.Biography,
                BirthDay = user.BirthDay.Value,
                Location = user.Location,
            };

            //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                user.Email = Input.Email;
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.AvatarUrl = Input.AvatarUrl;
                user.BirthDay = Input.BirthDay;
                user.Biography = Input.Biography;
                user.Location = Input.Location;
                user.SecurityStamp = Guid.NewGuid().ToString();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    StatusMessage = "Your profile has been updated";
                    TempData["UpdateProfileMsg"] = StatusMessage;
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return Page();
        }
    }
}