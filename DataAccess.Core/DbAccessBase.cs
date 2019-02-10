using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public class DbAccessBase
    {
        protected readonly IDataMapperService dataMapperService;
        protected readonly IDbConnectionProvider dbConnectionProvider;

        public DbAccessBase(IDataMapperService dataMapperService, IDbConnectionProvider dbConnectionProvider)
        {
            this.dataMapperService = dataMapperService;
            this.dbConnectionProvider = dbConnectionProvider;
        }
    }
}
