namespace Blaze.Models
{
	internal class Device
	{
		//NOTE(tsharpe): This is also the devices key, dupe?
		public string Name { get; set; }
		public DateTime LastSeen { get; set; }
		public object Value { get; set; }
	}
}
