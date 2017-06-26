using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ihaiu.sockets
{
	public class ServerClient
	{
		public ServerSocket 	server;
		public Socket 			socket;
		private Thread 			receiveThread;
		private bool 			runing;
		private byte[] 			result = new byte[1024];

		public ServerClient(ServerSocket server, Socket socket)
		{
			this.server = server;
			this.socket = socket;
		}

		public ServerClient Start()
		{
			runing = true;
			receiveThread = new Thread(ReceiveLoop);
			receiveThread.Start();
			return this;
		}

		public void ReceiveLoop()
		{
			while (runing)
			{
				Receive();
			}
		}

		public void Receive()
		{
			int receiveNumber = socket.Receive(result);
			Loger.LogFormat("接收客户端{0}消息{1}", socket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
		}

		public void Stop()
		{
			if (runing)
			{
				runing = false;
				receiveThread.Abort();
				receiveThread = null;

				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
				socket = null;
			}
		}

	}
}
