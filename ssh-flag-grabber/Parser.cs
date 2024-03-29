namespace ssh_flag_grabber
{
	public static class Parser
	{
		public record SshUser(string Login, string Password);
		public record SshHost(string Host, int Port, List<SshUser> Users);

		public static SshHost? ParseHost(string path)
		{
			if (!Validator.FileExist(path))
				return null;

			var lines = File.ReadLines(path).ToArray();
			var header = lines[0].Split(":");
			if (header.Length < 2)
			{
				Logger.Err($"Invalid file");
				return null;
			}
			if (!int.TryParse(header[1], out var port))
			{
				Logger.Err($"Cant parse port {header[1]}");
			}

			var sshHost = new SshHost(header[0], port, []);

			for (var i = 1; i < lines.Length; i++)
			{
				var line = lines[i].Split("/");
				if (line.Length < 2)
				{
					Logger.Err($"Invalid line {i}:{line[i]}");
					return null;
				}
				sshHost.Users.Add(new SshUser(line[0], line[1]));
			}
			Logger.Verb($"Loaded {sshHost.Users.Count} credentials for {sshHost.Host}:{sshHost.Port}");
			return sshHost;
		}
	}
}