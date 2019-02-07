using System.Data;

namespace DataAccess.Core.DbLib
{
    public interface IDbFactory
    {
        IDbConnection CreateConnection();
    }
}
