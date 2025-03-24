using Microsoft.Data.SqlClient;

namespace Community.Repository.Interfaces
{
    public interface ICommunityContext
    {
        public SqlConnection GetConnection();

    }
}
