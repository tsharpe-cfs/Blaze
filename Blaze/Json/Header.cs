namespace Blaze.Json
{
	internal class Header
	{
		public string schema_hash { get; set; }
		public string node_name { get; set; }
		public long timestamp { get; set; }
		public List<string> fields { get; set; }
		public bool is_stream { get; set; }
	}
}
