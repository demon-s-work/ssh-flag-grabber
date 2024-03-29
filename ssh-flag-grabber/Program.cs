using System.Drawing;
using CommandLine;
using Renci.SshNet;

namespace ssh_flag_grabber
{
	class Program
	{
		public static bool verbose = false;
		public static Dictionary<string, string> files = new Dictionary<string, string>();
		private static void Main(string[] args)
		{
			Logger.Ascii(";)", Color.Red);
			var hostFilePath = string.Empty;
			CommandLine.Parser.Default.ParseArguments<Options>(args)
			           .WithParsed(o => {
				           verbose = o.Verbose;
				           hostFilePath = o.HostFilePath;
			           });

			var sshHost = Parser.ParseHost(hostFilePath);
			if (sshHost is null)
				return;

			var connections = new List<Connection>(sshHost.Users.Count);

			foreach (var user in sshHost.Users)
			{
				var connInfo = new ConnectionInfo(sshHost.Host, sshHost.Port, user.Login,
					[
						new PasswordAuthenticationMethod(user.Login, user.Password)
					]
				);
				connections.Add(new Connection(connInfo));
			}

			foreach (var con in connections)
			{
				con.Process();
			}

			Logger.Log("Finished");
		}
	}
}