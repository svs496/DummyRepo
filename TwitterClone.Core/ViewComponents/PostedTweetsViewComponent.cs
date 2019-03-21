using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Core.Models.ViewModels;
using TwitterClone.Data.Models;
using TwitterClone.Data.UnitOfWork;

namespace TwitterClone.Core.ViewComponents
{
    public class PostedTweetsViewComponent: ViewComponent
    {
        private IUnitOfWork _unitofWork;
        private readonly UserManager<User> _userManager;

        public PostedTweetsViewComponent(IUnitOfWork unitOfWork,
                                   UserManager<User> userManager)
        {
            _unitofWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tweetsList = await GetTweetsAsync();
            return View(tweetsList);
        }

        private async Task<List<TweetViewModel>> GetTweetsAsync()
        {
            // Get Logged in user details
            User loggedUser = await _userManager.GetUserAsync(Request.HttpContext.User);

            //Get tweets posted by logged in user
            var results = await _unitofWork.TweetRepository
                                  .FindByConditionAync(p => p.UserId == loggedUser.Id);

            List<TweetViewModel> TweetVMList = new List<TweetViewModel>();

            if (results != null)
            {
                TweetViewModel item;
                foreach (Tweet tweet in results)
                {
                    item = new TweetViewModel
                    {
                        Content = tweet.Content,
                        Id = tweet.Id,
                        DisplayCreateDate = $"{tweet.CreatedDate.Date.ToString("MMMM")},{tweet.CreatedDate.Day} {tweet.CreatedDate.Year}",
                        DisplayModifedDate = tweet.ModifiedDate == DateTime.MinValue ? null : $"{tweet.ModifiedDate.Date.ToString("MMMM")},{tweet.ModifiedDate.Day} {tweet.ModifiedDate.Year}",
                        UserId = tweet.UserId,
                        UserName = $"{loggedUser.FirstName} {loggedUser.LastName}",
                        CreateDate = tweet.CreatedDate,
                        ModifyDate = tweet.ModifiedDate
                    };

                    TweetVMList.Add(item);
                }
            }

            //last modified tweets will be shown first
            var OrderedList = TweetVMList.OrderByDescending(p => p.ModifyDate).ThenByDescending( q => q.CreateDate).ToList();

            return OrderedList;


        }

    }
}
