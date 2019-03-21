using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Data.Models;
using TwitterClone.Data.Repositories;

namespace TwitterClone.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User,string> UserRepository { get; }

        IGenericRepository<Tweet, int> TweetRepository { get; }

        IGenericRepository<UserFollower, int> UserFollowerRepository { get; }

        Task<bool> SaveAsync();
    }
}
