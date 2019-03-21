using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Core.Models.ViewModels;
using TwitterClone.Data.Models;
using TwitterClone.Data.UnitOfWork;

namespace TwitterClone.Core.Controllers
{
    public class UserFollowersController : Controller
    {
        private IUnitOfWork _unitofWork;
        private readonly UserManager<User> _userManager;
        private ILoggerManager _logger;


        public UserFollowersController(IUnitOfWork unitOfWork,
                                   UserManager<User> userManager,
                                   ILoggerManager logger)
        {
            _unitofWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<PartialViewResult> UpdatedFollowers()
        {
            List<UserTweetViewModel> AllUsersVM = await GetFollowingUsersAndFollowers(string.Empty);
            return PartialView("_ManageFollowers",AllUsersVM);
        }

        // GET: UserFollowers
        public async Task<IActionResult> Search(string searchString)
        {
            List<UserTweetViewModel> AllUsersVM = await GetFollowingUsersAndFollowers(searchString);
            return View(AllUsersVM);
        }

        private async Task<List<UserTweetViewModel>> GetFollowingUsersAndFollowers(string searchString)
        {
            User loggedUser = await _userManager.GetUserAsync(User);

            _logger.LogInfo($"Controller : ManageFollowers | Action : Search | User : {loggedUser.Id} ");

            IEnumerable<User> USERS = new List<User>();

            if (string.IsNullOrWhiteSpace(searchString))
            {
                USERS = await _unitofWork.UserRepository.FindByConditionAync(p => !p.Id.Equals(loggedUser.Id));
            }
            else
            {

                USERS = await _unitofWork.UserRepository
                    .FindByConditionAync(p => (p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Location.Contains(searchString)) && p.Id != loggedUser.Id);
            }

            List<UserTweetViewModel> AllUsersVM = new List<UserTweetViewModel>();

            if (USERS != null)
            {
                foreach (var followedUser in USERS)
                {
                    List<Tweet> userTweets = await _unitofWork.TweetRepository
                                 .FindByConditionAync(p => p.UserId == followedUser.Id) as List<Tweet>;

                    //fr each user in AspNet users find if logged in user is following him/her. This will return one single user only
                    List<UserFollower> ListFollowedUsers = await GetExistingRecordForUserFollowed(loggedUser.Id, followedUser.Id);

                    List<UserFollower> tempFollowerCount = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.FollowerId.Equals(followedUser.Id) && p.FollowAction.Equals(FollowActions.Following)) as List<UserFollower>;

                    List<UserFollower> tempFollowingCount = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.UserId.Equals(followedUser.Id) && p.FollowAction.Equals(FollowActions.Following)) as List<UserFollower>;


                    UserTweetViewModel userViewModel = new UserTweetViewModel()
                    {
                        UserId = followedUser.Id,
                        UserFullName = followedUser.FirstName + " " + followedUser.LastName,
                        FollowersCount = tempFollowerCount.Count,
                        FollowingCount = tempFollowingCount.Count,
                        TotalTweetCount = userTweets.Count,
                        UserAccountCreationDate = $"{followedUser.AccountCreationDate.ToString("MMMM")} {followedUser.AccountCreationDate.Year}",
                        UserBirthDay = $"{followedUser.BirthDay.Value.ToString("MMMM")} {followedUser.BirthDay.Value.Day}",
                        UserAvatarUrl = followedUser.AvatarUrl,
                        UserBiography = followedUser.Biography,
                        UserLocation = followedUser.Location,
                        TwitterHandle = followedUser.UserName,
                        FollowFlag = ListFollowedUsers.Where(p => p.FollowAction == FollowActions.Following).Count() > 0 ? true : false
                    };

                    AllUsersVM.Add(userViewModel);
                }

            }

            return AllUsersVM;
        }


        // POST: UserFollowers/Create
        [HttpPost]
        public async Task<JsonResult> Follow(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "Error ! Id cannot be null" });
            }

            //success
            if (await FollowOrUnFollowByAction(FollowActions.Following, id))
            {
                return Json(new { success = true, responseText = "Success" });
            }

            return Json(new { success = false, responseText = "Error ! Operation Failed." });

        }

        public async Task<JsonResult> UnFollow(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "Id cannot be null" });

            }

            //success
            if (await FollowOrUnFollowByAction(FollowActions.NotFollowing, id))
            {
                return Json(new { success = true, responseText = "Success" });
            }


            return Json(new { success = false, responseText = "Error" });
        }
        
        private async Task<List<UserFollower>> GetExistingRecordForUserFollowed(string loggedUserId, string followedUserId)
        {
            return await _unitofWork.UserFollowerRepository
                                             .FindByConditionAync(p => p.UserId.Equals(loggedUserId) && p.FollowerId.Equals(followedUserId)) as List<UserFollower>;
        }

        private async Task<bool> FollowOrUnFollowByAction(FollowActions followAction, string userToFollowOrUnFollowId)
        {

            User loggedUser = await _userManager.GetUserAsync(User);

            _logger.LogInfo($"Controller : ManageFollowers | Action : Follow | User : {loggedUser.Id} ");

            List<UserFollower> followedUsers = await GetExistingRecordForUserFollowed(loggedUser.Id, userToFollowOrUnFollowId);

            UserFollower followedUser = new UserFollower()
            {
                UserId = loggedUser.Id,
                FollowerId = userToFollowOrUnFollowId,
                FollowAction = followAction
            };

            if (followedUsers.Count == 0)
            {
                _unitofWork.UserFollowerRepository.Add(followedUser);
            }
            else
            {
                followedUser.Id = followedUsers.Find(p => p.UserId == loggedUser.Id && p.FollowerId == userToFollowOrUnFollowId).Id;

                _unitofWork.UserFollowerRepository.Update(followedUser);
            }

            if (await _unitofWork.SaveAsync())
            {
                var following = await _userManager.FindByIdAsync(userToFollowOrUnFollowId);

                if (followAction == FollowActions.Following)
                {
                    TempData["FollowerActionMsg"] = $"You are now following {following.FirstName} {following.LastName}.";
                }
                else
                    TempData["FollowerActionMsg"] = $"You have un-followed {following.FirstName} {following.LastName}.";

                return true;
            }
            else
                _logger.LogError("Error in operation");

            return false;

        }

    }
}