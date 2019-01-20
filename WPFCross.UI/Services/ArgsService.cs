using Fclp;

namespace WPFCross.UI.Services
{
    public class ArgsService : IArgsService
    {
        private readonly FluentCommandLineParser parser;

        public bool Mock { get; private set; }

        public bool DbInitialize { get; private set; }

        public bool Clear { get; private set; }

        public ArgsService()
        {
            parser = new FluentCommandLineParser();
            parser.Setup<bool>('m', "mock")
                .Callback(m => Mock = m)
                .SetDefault(false);

            parser.Setup<bool>('i', "dbInitialize")
                .Callback(i => DbInitialize = i)
                .SetDefault(false);

            parser.Setup<bool>('c', "dbClear")
                .Callback(c => Clear = c)
                .SetDefault(false);
        }

        public bool Parse(string[] args)
        {
            return !parser.Parse(args).HasErrors;
        }
    }
}
