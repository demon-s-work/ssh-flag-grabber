using System.Reflection;
using System.Xml.XPath;
using Renci.SshNet;

namespace ssh_flag_grabber
{
	public class Connection(ConnectionInfo connectionInfo)
	{
		private SshClient? client;

		private ConnectionInfo connectionInfo { get; set; } = connectionInfo;

		public void Process()
		{
			client = new SshClient(connectionInfo);
			var user = connectionInfo.Username;
			client.Connect();
			var lsOut = client.RunCommand($"find / -readable -type f -not -path \"/proc/*\" 2>/dev/null").Result;
			var files = lsOut.Split('\n');
			Logger.Verb($"{user} files count: {files.Length}");
			var skippedFiles = 0;
			var processedFiles = 0;
			var filesCount = files.Length;
			Console.CursorVisible = false;
			foreach (var file in files)
			{
				processedFiles++;
				Logger.WriteAtBottom($"Processing file {file}", $"{processedFiles}/{filesCount}, skipped {skippedFiles}");
				if (Program.files.ContainsKey(file) ||
				    file.Trim() == string.Empty ||
				    file == $"/pwned/{user}/flagz.txt")
				{
					skippedFiles++;
					continue;
				}
				
				var fileContent = client.RunCommand($"cat {file}").Result;
				if (fileContent.Contains("8===") && fileContent.Contains("===D~~"))
				{
					var from = fileContent.IndexOf("8===");
					var to = fileContent.IndexOf("===D~~");
					var flag = fileContent.Substring(from, to - from + 6);
					Logger.Log($"{flag} Founded at {file}");
				}
				Program.files.Add(file, "");
			}
			Logger.Verb($"{skippedFiles} files skipped for user {user}");
		}
	}
}