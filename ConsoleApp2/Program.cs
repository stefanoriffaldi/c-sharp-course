using log4net.Config;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("conf/log4net.xml"));

            IGenerator generator = new BufferedGenerator(new GeneratorUUID());
            IService service = new Service(generator, 5);

            service.Execute();
        }
    }
}
