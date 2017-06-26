using System;

namespace ihaiu.sockets
{
	class MainClass
	{
		public static ServerSocket 	server;
		public static ServerCommand cmd;
		public static void Main(string[] args)
		{
			Loger.Log(ServerSocket.logIsopen, ServerSocket.logTag, !ServerSocket.logIsopen ? null : "ServerSocket Main");

			cmd = new ServerCommand();
			cmd.Start();

			server = new ServerSocket();
			server.Start();
		}

		public static void Stop()
		{
			if (server != null)
			{
				server.Stop();
				server = null;
			}

			if (cmd != null)
			{
				cmd.Stop();
				cmd = null;
			}
		}
	}
}
