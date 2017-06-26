using System;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
	public class ServerSocket
	{
		public string 		ip 			= "127.0.0.1";
		public int 			port 		= 9900;
		public int 			listenNum 	= 1000;
		public Socket 		socket;

		public ServerSocket()
		{
		}

		public void Start()
		{
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
			socket.Listen(listenNum);

		}

		public void Stop()
		{

		}
	}
}
