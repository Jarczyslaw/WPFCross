﻿using DataAccess.Core;
using Fclp;

namespace WPFCross.Startup.Services
{
    public class ArgsService : IArgsService
    {
        private readonly FluentCommandLineParser parser;

        public ArgsService()
        {
            parser = new FluentCommandLineParser();
            parser.Setup<bool>('t', "test")
                .Callback(t => Test = t)
                .SetDefault(false);

            parser.Setup<DbAccessType>('d', "db")
                .Callback(m => DbAccessType = m)
                .SetDefault(DbAccessType.Mock);

            parser.Setup<bool>('i', "dbInitialize")
                .Callback(i => DbInitialize = i)
                .SetDefault(false);

            parser.Setup<bool>('c', "dbClear")
                .Callback(c => Clear = c)
                .SetDefault(false);
        }

        public bool Test { get; private set; }

        public DbAccessType DbAccessType { get; private set; }

        public bool DbInitialize { get; private set; }

        public bool Clear { get; private set; }

        public bool Parse(string[] args)
        {
            return !parser.Parse(args).HasErrors;
        }
    }
}
