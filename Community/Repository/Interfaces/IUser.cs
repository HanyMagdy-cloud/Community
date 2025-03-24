using Community.Repository.Entities;

namespace Community.Repository.Interfaces
{
    public interface IUser
    {
        // Create a new user
        public User CreateUser(User user);

        // Get a user by their ID
        public User GetUserById(int userId);

        // Get all users 
        public List<User> GetAllUsers();

        // Update a user's information
        public User UpdateUser(User user);

        // Delete a user by their ID
        public User DeleteUser(int userId);

        // Authenticate a user (e.g., for login)
        public User AuthenticateUser(string username, string password);



    }
}
