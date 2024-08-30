using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upload_download.Entity;

namespace upload_download.Repository
{
    public interface IUserRepositories
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByTokenAndEmail(string token, string email);
        Task ActiveUser(string email);
        Task AddUser(UserModel user);
    }
}