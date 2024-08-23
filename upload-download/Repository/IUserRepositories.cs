using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upload_download.Entity;

namespace upload_download.Repository
{
    public interface IUserRepositories
    {
        UserModel GetUserByUsername(string username);
        void AddUser(UserModel user);
    }
}