using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Community.Repository.Repos
{
    public class UserRepo : IUser

    {
        // Private field to hold the connection string for the database...
        private readonly string _connString;
        public UserRepo(IConfiguration config)
        {
            _connString = config.GetConnectionString("Community");
        }


        // Create a new user
        public User CreateUser(User user)
        {
            using(IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username);
                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.Email);
                db.Execute("CreateUser", parameters,
                    commandType: CommandType.StoredProcedure);
                return user;

            }
        }
        //Delete a user
        public User DeleteUser(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                var user = GetUserById(userId); // Retrieve the user before deleting
                if (user != null)
                {
                    db.Execute("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
                }

                return user;
            }
        }

        public List<User> GetAllUsers()
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                return db.Query<User>("GetAllUsers", commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public User GetUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                return db.QuerySingleOrDefault<User>("GetUserById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public  User UpdateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", user.UserId);
                parameters.Add("@Username", user.Username);
                parameters.Add("@Password",user.Password);
                parameters.Add("@Email", user.Email);

                db.Execute("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        //Authenticates a user by executing the AuthenticateUser stored procedure 
        //with @Username and @Password parameters.
        //Returns the user details if the credentials match, or null if authentication fails.
        // if the username and password are correct then the user can log in ,
        public User AuthenticateUser(string username, string password)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@Password", password);

                return db.QuerySingleOrDefault<User>("AuthenticateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
