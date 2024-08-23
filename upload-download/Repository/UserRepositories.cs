
using Newtonsoft.Json;
using upload_download.Entity;
using upload_download.Repository;


namespace upload_download.Repository
{
    public class UserRepositories : IUserRepositories
    {
        private readonly string _filePath = "users.json";

        public UserModel GetUserByUsername(string username)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public void AddUser(UserModel user)
        {
            var users = GetAllUsers();
            users.Add(user);
            SaveAllUsers(users);
        }

        private List<UserModel> GetAllUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<UserModel>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<UserModel>>(jsonData) ?? new List<UserModel>();
        }

        private void SaveAllUsers(List<UserModel> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }

}
