using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ihaiu.sockets
{
	public class ServerSocket
	{
		public static bool 		logIsopen 	= true;
		public static string 	logTag 	= "ServerSocket";

		public string 		ip 			= "127.0.0.1";
		public int 			port 		= 9900;
		public int 			listenNum 	= 1000;
		public Socket 		socket;

		private bool 	runing;
		private Thread 	listenThread;

		private List<ServerClient> clientList = new List<ServerClient>();

		public ServerSocket()
		{
		}

		public void Start()
		{
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
			socket.Listen(listenNum);

			runing = true;
			listenThread = new Thread(ListenClientConnect);
			listenThread.Start();

			Loger.Log(ServerSocket.logIsopen, ServerSocket.logTag, !ServerSocket.logIsopen ? null : "Start");

		}


		protected void ListenClientConnect()
		{
			while (runing)
			{
				Socket clientSocket = socket.Accept();
				clientList.Add(new ServerClient(this, clientSocket).Start());

				Loger.Log(ServerSocket.logIsopen, ServerSocket.logTag, !ServerSocket.logIsopen ? null : "ListenClientConnect " + clientSocket.RemoteEndPoint.ToString());

			}
		}


		public void Stop()
		{
			if (!runing) return;

			runing = false;
			socket.Disconnect(true);
			socket.Close();
			socket.Dispose();
			socket = null;

			listenThread.Abort();
			listenThread = null;

			int count = clientList.Count;
			for (int i = count - 1; i >= 0; i --)
			{
				if (i < clientList.Count)
					clientList[i].Stop();
			}

			clientList.Clear();
		}


	}
}
