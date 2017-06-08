using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;

namespace PipeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("[NamedPipeServer]: Need pipe name.");
                return;
            }
            Console.WriteLine("1");
            PipeSecurity pipeSecurity = new PipeSecurity();
            pipeSecurity.AddAccessRule(new PipeAccessRule("Everyone",
            PipeAccessRights.ReadWrite,
            System.Security.AccessControl.AccessControlType.Allow));
            using (NamedPipeServerStream pipe = new
            NamedPipeServerStream("pipetest", PipeDirection.InOut, 1,
            PipeTransmissionMode.Message, PipeOptions.None, 512,
            512, pipeSecurity, HandleInheritability.None));
            NamedPipeServerStream PipeServer = new NamedPipeServerStream(args[0], System.IO.Pipes.PipeDirection.Out);
            Console.WriteLine("2");
            PipeServer.WaitForConnection();
            Console.WriteLine("3");
            StreamWriter PipeWriter = new StreamWriter(PipeServer);
            Console.WriteLine("4");
            PipeWriter.AutoFlush = true;
            Console.WriteLine("5");
            string tempWrite;

            while ((tempWrite = Console.ReadLine()) != null)
            {
                try
                {
                    PipeWriter.WriteLine(tempWrite);
                }
                catch (IOException ex)
                {
                    if (ex.Message == "Pipe is broken.")
                    {
                        Console.Error.WriteLine("[NamedPipeServer]: NamedPipeClient was closed, exiting");
                        return;
                    }
                }
            }

            PipeWriter.Close();
            PipeServer.Close();
        }
    }
}
