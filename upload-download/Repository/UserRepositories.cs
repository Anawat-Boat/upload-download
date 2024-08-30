
using Newtonsoft.Json;
using upload_download.Entity;
using upload_download.Repository;


namespace upload_download.Repository
{
    public class UserRepositories : IUserRepositories
    {
        private readonly string _filePath = "users.json";

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        public async Task AddUser(UserModel user)
        {
            var users = await GetAllUsers();
            users.Add(user);
            SaveAllUsers(users);
        }

        private async Task<List<UserModel>> GetAllUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<UserModel>();
            }
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<UserModel>>(jsonData) ?? new List<UserModel>();
        }

        private async Task SaveAllUsers(List<UserModel> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        public async Task<UserModel> GetUserByTokenAndEmail(string token, string email)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Token.Equals(token, StringComparison.OrdinalIgnoreCase));
        }

        public async Task ActiveUser(string email)
        {
            var users = await GetAllUsers();
            users.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).IsActive = true;
            SaveAllUsers(users);
        }
    }
}
