using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TwitterClone.Data.Models;
using TwitterClone.Data.UnitOfWork;

namespace TwitterClone.Core.Areas.Identity.Pages.Account
{
    public class DeleteAccountModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ILoggerManager _logger;
        private IUnitOfWork _unitofWork;

        public DeleteAccountModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerManager logger,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitofWork = unitOfWork;
        }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);


            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Password not correct.");
                    return Page();
                }
            }

            //Delete Tweets
            List<Tweet> AllTweets = await _unitofWork.TweetRepository.FindByConditionAync(p => p.UserId.Equals(user.Id)) as List<Tweet>;
            if (AllTweets.Count > 0)
            {
                foreach (var tweet in AllTweets)
                {
                    _unitofWork.TweetRepository.Delete(tweet);
                }
                
            }

            //Delete followers
            List<UserFollower> follow = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.FollowerId.Equals(user.Id)) as List<UserFollower>;
            if (follow.Count > 0)
            {
                foreach (var temp in follow)
                {
                    _unitofWork.UserFollowerRepository.Delete(temp);
                }
               
            }

            //Delete following
            List<UserFollower> follower = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.UserId.Equals(user.Id)) as List<UserFollower>;
            if (follower.Count > 0)
            {
                foreach (var temp in follower)
                {
                    _unitofWork.UserFollowerRepository.Delete(temp);
                }
                
            }


            await _unitofWork.SaveAsync();

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInfo($"User with ID {userId} deleted themselves.");

            return Redirect("~/");
        }


    }
}