using CommandLine;

namespace ssh_flag_grabber
{
	public class Options
	{
		[Option('v', "verbose", Default = false, Required = false, HelpText = "Set extended output")]
		public bool Verbose { get; set; }
		[Option('h', "host", Required = false, HelpText = "Input file with host:port and ssh credentials")]
		public string HostFilePath { get; set; } = string.Empty;
	}
}