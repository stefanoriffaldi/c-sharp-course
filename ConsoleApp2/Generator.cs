using log4net;

namespace ConsoleApp2
{
    internal interface IGenerator
    {
        string Generate(string source);
    }
    internal class GeneratorUUID : IGenerator
    {
        private static volatile ILog log = LogManager.GetLogger(typeof(GeneratorUUID));
        public string Generate(string source)
        {
            log.Debug("New generation: ");
            return Guid.NewGuid().ToString();
        }
    }

    internal class GeneratorReverse : IGenerator
    {
        private static volatile ILog log = LogManager.GetLogger(typeof(GeneratorReverse));

        public string Generate(string source)
        {
            log.Debug($"Reversing: ");
            char[] charArray = source.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    internal class BufferedGenerator
    (
        IGenerator generator
    )
        : IGenerator
    {

        private readonly IDictionary<string, string> map = new Dictionary<string, string>();

        public string Generate(string source)
        {

            if (!map.ContainsKey(source))
            {
                map[source] = generator.Generate(source);
            }
            return map[source];
        }
    }

    internal class FilteredGenerator
        (
            int lengthThreshold,
            IGenerator generator
        )
        : IGenerator
    {
        private static volatile ILog log = LogManager.GetLogger(typeof(FilteredGenerator));

        public string Generate(string source)
        {
            if (source.Length <= lengthThreshold)
            {
                log.Debug("Filtered: ");
                return source;
            }
            return generator.Generate(source);
        }
    }
}
