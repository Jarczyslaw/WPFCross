using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public class DbAccessBase
    {
        protected readonly IDataMapperService dataMapperService;
        protected readonly IDbConnectionProvider connectionProvider;

        public DbAccessBase(IDataMapperService dataMapperService, IDbConnectionProvider connectionProvider)
        {
            this.dataMapperService = dataMapperService;
            this.connectionProvider = connectionProvider;
        }
    }
}
