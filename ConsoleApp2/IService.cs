using log4net;

namespace ConsoleApp2
{
    internal interface IService
    {
        void Execute();
    }

    internal class Service
    (
        IGenerator generator,
        int num
    )
        : IService
    {
        private static volatile ILog log = LogManager.GetLogger(typeof(Service));

        public void Execute()
        {
            string[] randoms = new string[5];
            for (int i = 0; i < num; i++)
            {
                randoms[i] = RandomString(i + 1);
                log.Info($"{randoms[i]} -> {generator.Generate(randoms[i])}");
            }

            for (int i = num - 1; i >= 0; i--)
            {
                log.Info($"{randoms[i]} -> {generator.Generate(randoms[i])}");
            }
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[index: new Random().Next(s.Length)]).ToArray());
        }
    }
}
