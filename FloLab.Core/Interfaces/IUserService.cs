using System;
using System.Collections.Generic;
using System.Text;

namespace FloLab.Core.Interfaces
{
    public interface IUserService
    {
        IList<int> GetUserDays(string userId);
    }
}
