using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronusSocketClient
{
    public static void start()
    {
        byte[] bytes = new byte[1024];
        try
        {
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
            Socket sender = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Connesso con {0}",sender.RemoteEndPoint.ToString());
                Console.WriteLine("Scegli\n" + "1. Sasso\n" + "2.Carta\n" + "3.Forbice\n");
                var risp = Console.ReadLine();
                byte[] msg = Encoding.ASCII.GetBytes("Sasso,Carta,Forbice<EOF>");
                int bytesSent = sender.Send(msg);
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("L'avversario ha scelto {0}",Encoding.ASCII.GetString(bytes, 0, bytesRec));
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                switch(bytes)
                {
                    case 1:

                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public static int Main(String[] args)
    {
        start();
        Console.WriteLine("Premere un tasto per continuare ");
        Console.ReadLine();
        return 0;
    }
}