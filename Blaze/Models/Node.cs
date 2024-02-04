namespace Blaze.Models
{
	internal class Node
	{
		public DateTime LastSeen { get; set; }
		public Dictionary<string, Device> Devices { get; private set; }
		public string Console { get; set; }

		internal Node()
		{
			Devices = new Dictionary<string, Device>();
		}
	}
}
