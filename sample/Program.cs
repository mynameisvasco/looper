using System;

namespace Looper.Sample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new AppBuilder()
                .Configure()
                .Build()
                .Start();
        }
    }
}
