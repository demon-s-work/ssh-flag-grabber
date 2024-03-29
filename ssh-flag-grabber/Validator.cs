namespace ssh_flag_grabber
{
	public static class Validator
	{
		public static bool FileExist(string path)
		{
			if (!File.Exists(path))
			{
				Logger.Err($"No file {path}");
				return false;
			}
			Logger.Verb($"File found {path}");
			return true;
		}
	}
}