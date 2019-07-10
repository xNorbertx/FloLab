using FloLab.Core.Entities;
using FloLab.Core.Interfaces;
using FloLab.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FloLab.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private FloLabContext _context;
        private IRepository<UserDay> _repository;

        public UserService(FloLabContext context, IRepository<UserDay> repository)
        {
            _context = context;
            _repository = repository;
        }

        public IList<int> GetUserDays(string userId)
        {
            return _context.UserDays.Where(x => x.UserId.Equals(userId)).Select(x => x.DayNumber).ToList();
        }

        public void Persist()
        {
            _repository.Persist();
        }
    }
}
