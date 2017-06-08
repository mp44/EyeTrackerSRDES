using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;

namespace PipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("[NamedPipeClient]: Need pipe name.");
                return;
            }
            Console.WriteLine("1");
            NamedPipeClientStream PipeClient = new NamedPipeClientStream(args[0], args[1], System.IO.Pipes.PipeDirection.In);
            Console.WriteLine("2");
            PipeClient.Connect();
            Console.WriteLine("3");
            StreamReader PipeReader = new StreamReader(PipeClient);
            Console.WriteLine("4");
            string tempRead;
            Console.WriteLine("5");
            while ((tempRead = PipeReader.ReadLine()) != null)
            {
                Console.WriteLine(tempRead);
            }
            Console.WriteLine("6");
            PipeReader.Close();
            PipeClient.Close();
        }
    }
}
