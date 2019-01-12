using Fclp;

namespace WPFCross.UI.Services
{
    public class ArgsService : IArgsService
    {
        private readonly FluentCommandLineParser parser;

        public bool Mock { get; set; }

        public bool DbInitialize { get; set; }

        public ArgsService()
        {
            parser = new FluentCommandLineParser();
            parser.Setup<bool>('m', "mock")
                .Callback(m => Mock = m)
                .SetDefault(false);

            parser.Setup<bool>('i', "dbInitialize")
                .Callback(i => DbInitialize = i)
                .SetDefault(false);
        }

        public bool Parse(string[] args)
        {
            return !parser.Parse(args).HasErrors;
        }
    }
}
