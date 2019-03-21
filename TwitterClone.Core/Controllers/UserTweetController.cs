using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Core.Models.ViewModels;
using TwitterClone.Data.Models;
using TwitterClone.Data.UnitOfWork;
using LoggerService;
using System.Net;

namespace TwitterClone.Core.Controllers
{
    public class UserTweetController : Controller
    {
        private IUnitOfWork _unitofWork;
        private readonly UserManager<User> _userManager;
        private ILoggerManager _logger;

        public UserTweetController(IUnitOfWork unitOfWork,
                                   UserManager<User> userManager,
                                   ILoggerManager logger)
        {
            _unitofWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> DisplayTweet()
        {


            //Get Logged in user details
            User loggedUser = await GetLoggedInUserDetails();

            
            _logger.LogInfo($"Get all tweets home page for user: {loggedUser.UserName}");
          
            _logger.LogInfo($"Controller : UserTweet | Action : DisplayTweet | User : {loggedUser.Id}");

            List<TweetViewModel> TweetVMList = await GetTweetsForDisplaying(loggedUser);

            List<UserFollower> tempFollowerCount = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.FollowerId.Equals(loggedUser.Id) && p.FollowAction.Equals(FollowActions.Following)) as List<UserFollower>;

            List<UserFollower> tempFollowingCount = await _unitofWork.UserFollowerRepository.FindByConditionAync(p => p.UserId.Equals(loggedUser.Id) && p.FollowAction.Equals(FollowActions.Following)) as List<UserFollower>;


            UserTweetViewModel userViewModel = new UserTweetViewModel()
            {
                UserId = loggedUser.Id,
                UserFullName = loggedUser.FirstName + " " + loggedUser.LastName,
                FollowersCount = tempFollowerCount.Count,
                FollowingCount = tempFollowingCount.Count,
                //TotalTweetCount = TweetVMList == null ? 0 : TweetVMList.Count(),
                UserAccountCreationDate = $"{loggedUser.AccountCreationDate.ToString("MMMM")} {loggedUser.AccountCreationDate.Year}",
                UserBirthDay = $"{loggedUser.BirthDay.Value.ToString("MMMM")} {loggedUser.BirthDay.Value.Day}",
                UserAvatarUrl = loggedUser.AvatarUrl,
                UserBiography = loggedUser.Biography,
                UserLocation = loggedUser.Location,
                TwitterHandle = loggedUser.UserName,
                TweetVM = new TweetViewModel()
            };

            userViewModel.OwnTweets = TweetVMList;

            _logger.LogInfo($"Returning data: {userViewModel.TotalTweetCount}");

            return View(userViewModel);
        }


        private async Task<List<TweetViewModel>> GetTweetsForDisplaying(User loggedUser)
        {
            
            _logger.LogInfo($"Controller : UserTweet | Action : GetTweetsForDisplaying | User : {loggedUser.Id}");

            List<TweetViewModel> OwnTweetListVM = new List<TweetViewModel>(), 
                FollowedUserTweetListVM = new List<TweetViewModel>();
            
            if (await _unitofWork.TweetRepository
                                 .FindByConditionAync(p => p.UserId == loggedUser.Id) is List<Tweet> userTweets && userTweets.Count > 0)
            {
                OwnTweetListVM = MapTweetsToVM(userTweets, loggedUser, true);
                ViewBag.OwnTweetCount = OwnTweetListVM.Count != 0 ? OwnTweetListVM.Count : 0;
            }

            //Get followed users tweet
            //Get Followed Users
            List<UserFollower> followedUsers = await _unitofWork.UserFollowerRepository
                                                    .FindByConditionAync(p => p.UserId.Equals(loggedUser.Id) && p.FollowAction.Equals(FollowActions.Following)) as List<UserFollower>;
            if(followedUsers.Count > 0)
            {
                foreach (var followedUser in followedUsers)
                {
                    if (await _unitofWork.TweetRepository.FindByConditionAync(p => p.UserId == followedUser.FollowerId) is List<Tweet> followedUserTweets && followedUserTweets.Count > 0)
                    {
                        var tempUser = await _userManager.FindByIdAsync(followedUser.FollowerId);
                        FollowedUserTweetListVM.AddRange(MapTweetsToVM(followedUserTweets, tempUser,false));
                    }
                }
            }

            return OwnTweetListVM.Concat(FollowedUserTweetListVM)
                .OrderByDescending (p => p.ModifyDate > p.CreateDate ? p.ModifyDate : p.CreateDate).ToList();

        }

        private List<TweetViewModel> MapTweetsToVM(List<Tweet> userTweets, User tweetuser, bool isOwner)
        {
            List<TweetViewModel> tweetList = new List<TweetViewModel>();

            TweetViewModel TweetVM;
            foreach (var tweet in userTweets.OrderByDescending(p => p.ModifiedDate > p.CreatedDate ? p.ModifiedDate : p.CreatedDate))
            {
                TweetVM = new TweetViewModel()
                {
                    Content = tweet.Content,
                    Id = tweet.Id,
                    DisplayCreateDate = $"{tweet.CreatedDate.Date.ToString("MMMM")},{tweet.CreatedDate.Day} {tweet.CreatedDate.Year} at {tweet.CreatedDate.ToString("hh:mm tt")}",
                    DisplayModifedDate = tweet.ModifiedDate == DateTime.MinValue ? null :
                                           $"{tweet.ModifiedDate.Date.ToString("MMMM")},{tweet.ModifiedDate.Day} {tweet.ModifiedDate.Year} at {tweet.ModifiedDate.ToString("hh:mm tt")}",
                    UserId = tweet.UserId,
                    UserName = $"{tweetuser.FirstName} {tweetuser.LastName}",
                    CreateDate = tweet.CreatedDate,
                    ModifyDate = tweet.ModifiedDate,
                    OwnTweet = isOwner
                };

                tweetList.Add(TweetVM);
            }

            return tweetList;
        }

        private async Task<User> GetLoggedInUserDetails()
        {
            return await _userManager.GetUserAsync(User);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateTweet([Bind("Content")] TweetViewModel tweetVM)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false });
               // return PartialView("_CreateTweet", tweetVM);
            }

            User loggedUser = await GetLoggedInUserDetails();

            _logger.LogInfo($"Controller : UserTweet | Action : CreateTweet | User : {loggedUser.Id}");
            
            Tweet tweet = new Tweet
            {
                UserId = loggedUser.Id,
                Content = tweetVM.Content,
                CreatedDate = DateTime.Now
            };
            

            _unitofWork.TweetRepository.Add(tweet);
            if (await _unitofWork.SaveAsync())
            {
                _logger.LogInfo($"{tweet.UserId} added new tweet.");

                Response.StatusCode = (int)HttpStatusCode.OK;
                ModelState.Clear();
                return Json(new { success = true });
            }
            
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { success = false });
        }



        public async Task<IActionResult> PostedTweets()
        {
           
            User loggedUser = await GetLoggedInUserDetails();

            _logger.LogInfo($"Controller : UserTweet | Action : PostedTweets | User : {loggedUser.Id}");

            //Get tweets posted by logged in user
            List<TweetViewModel> TweetVMList = await GetTweetsForDisplaying(loggedUser);

            return PartialView("_PostedTweets", TweetVMList);
        }

        public async Task<JsonResult> GetTweetCount()
        {
            User loggedUser = await GetLoggedInUserDetails();

            //Get tweets posted by logged in user
            List<Tweet> TweetVMList = await _unitofWork.TweetRepository.FindByConditionAync(p => p.UserId == loggedUser.Id) as List<Tweet>;
            return Json(TweetVMList.Count);
        }

        public async Task<IActionResult> ModalEdit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
          
            Tweet tweet = await _unitofWork.TweetRepository.GetByIdAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }
            
            TweetViewModel tweetVM = new TweetViewModel
            {
                Content = tweet.Content,
                Id = tweet.Id,
                UserId = tweet.UserId,
                CreateDate = tweet.CreatedDate
            };

            _logger.LogInfo($"Controller : UserTweet | Action : ModalEdit GET | User : {tweetVM.UserId}");

            return PartialView("_ModalEditTweet", tweetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ModalEdit(int id, [Bind("Id,Content,UserId,CreateDate")] TweetViewModel tweetVM)
        {
            if (id != tweetVM.Id)
            {
                ModelState.AddModelError("","Wrong Tweet Id");
                _logger.LogInfo("Controller : UserTweet | Action : ModalEdit Post | tweetId mismatch");

                return Json(new { success = false });
            }

            if (ModelState.IsValid)
            {
                Tweet tweet = new Tweet()
                {
                    Content = tweetVM.Content,
                    ModifiedDate = DateTime.Now,
                    Id = tweetVM.Id,
                    UserId = tweetVM.UserId,
                    CreatedDate = tweetVM.CreateDate

                };

                _logger.LogInfo($"Controller : UserTweet | Action : ModalEdit Post | User : {tweetVM.UserId}");

                _unitofWork.TweetRepository.Update(tweet);

                if (await _unitofWork.SaveAsync()) //Success
                {
                    //TempData["updateTweetMsg"] = "Success";
                    return Json(new { success = true });
                }
                

            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false });

            }
            _logger.LogError("Controller : UserTweet | Action : ModalEdit Post | Error during update");
            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if (id == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "Id cannot be null" });

            }
            _logger.LogInfo("In Controller UserTweet, Action Delete");

            Tweet tweet = await _unitofWork.TweetRepository.GetByIdAsync(id);

            if (tweet != null)
            {
                _logger.LogInfo($"Controller : UserTweet | Action : DELETE | User : {tweet.UserId}");

                _unitofWork.TweetRepository.Delete(tweet);

                if (await _unitofWork.SaveAsync()) //success operation
                {
                    //TempData["deleteTweetMsg"] = "Success";
                    _logger.LogInfo("Delete SuccessFull");
                    return Json(new { success = true, responseText = "Delete Successfull" });
                }
                else { }
                    //TempData["deleteTweetMsg"] = "Fail";
            }

            //Log error message during EF delete operation
            _logger.LogError($"Controller : UserTweet | Action : DELETE |Error during delete");

            return Json(new { success = false, responseText = "Error during Delete" });

        }

    }
}