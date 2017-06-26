using System;
using System.Threading;

namespace ihaiu.sockets
{
	public class ServerCommand
	{
		public const string CMD_HELP = "help";
		public const string CMD_STOP = "stop";

		private bool 	runing;
		private string 	cmd;
		private Thread  thread;

		public ServerCommand()
		{
		}

		public void Start()
		{
			runing = true;

			thread = new Thread(Loop);
			thread.Start();
		}

		public void Loop()
		{
			while (runing)
			{
				cmd = Console.ReadLine();
				switch (cmd)
				{
					case CMD_HELP:
						Console.WriteLine("h");
						break;
					case CMD_STOP:
						MainClass.Stop();
						break;
					default:
						Console.WriteLine("无效命名！！");
						break;
				}
			}
		}

		public void Stop()
		{
			runing = false;
			if (thread != null)
			{
				thread.Abort();
				thread = null;
			}
		}
	}
}
