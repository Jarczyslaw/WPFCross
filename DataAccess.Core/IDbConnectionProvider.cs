using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public interface IDbConnectionProvider
    {
        string DbConnection { get; }
    }
}
