using Community.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Community.Repository
{
    // Define a class that implements the ICommunityContext interface.
    public class CommunityContext : ICommunityContext
    {
        // Private field to hold the connection string for the database.
        private readonly string _connString;
        
        public CommunityContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("Community");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
