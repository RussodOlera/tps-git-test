using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class SynchronousSocketListener
{
    public static string data = null;

    public static void start()
    {
        byte[] bytes = new Byte[1024];
        IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);
            while(true)
            {
                Console.WriteLine("In attesa di connessione...");
                Socket handler = listener.Accept();
                data = null;
                while(true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                Console.WriteLine("Messaggio del Client: ", data);
                Random r = new Random();
                var risp = r.Next(0, 3) + 1;
                byte[] msg = Encoding.ASCII.GetBytes(risp.ToString());

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();
    }
    public static int Main(String[] args)
    {
        start();
        return 0;
    }
}