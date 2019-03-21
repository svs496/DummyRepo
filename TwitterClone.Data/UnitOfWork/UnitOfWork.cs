using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Data.Models;
using TwitterClone.Data.Repositories;

namespace TwitterClone.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterCloneContext _context;
        private IGenericRepository<User,string> _userRepository;
        private IGenericRepository<Tweet, int> _tweetRepository;
        private IGenericRepository<UserFollower, int> _userFollowerRepository;

        public UnitOfWork(TwitterCloneContext Context)
        {
            _context = Context;
        }

        public IGenericRepository<User, string> UserRepository => _userRepository = _userRepository ?? new GenericRepository<User,string>(_context);

        public IGenericRepository<Tweet,int> TweetRepository => _tweetRepository = _tweetRepository ?? new GenericRepository<Tweet, int>(_context);

        public IGenericRepository<UserFollower,int> UserFollowerRepository => _userFollowerRepository = _userFollowerRepository ?? new GenericRepository<UserFollower,int>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
