using System.Text.Json;
using System.Text;
using System.Net.WebSockets;
using System.Diagnostics;
using Blaze.Json;
using Blaze.Models;

namespace Blaze
{
	internal class RelayClient : IDisposable
	{

		internal EventHandler StateChanged;
		internal EventHandler<Telem> TelemReceived;

		internal WebSocketState State => _socket.State;

		private CancellationTokenSource _token;
		private ClientWebSocket _socket;
		internal RelayClient()
		{
			_token = new CancellationTokenSource();
			_socket = new ClientWebSocket();
		}

		internal async Task Connect()
		{
			Uri url = new Uri("ws://dev-08.sw.cfsenergy.com:8765/ws");
			await _socket.ConnectAsync(url, _token.Token);
			StateChanged?.Invoke(this, new EventArgs());
		}

		internal async Task ReceiveLoop()
		{
			var buffer = new ArraySegment<byte>(new byte[4096]);
			while (!_token.IsCancellationRequested)
			{
				var received = await _socket.ReceiveAsync(buffer, _token.Token);
				Debug.Assert(received.EndOfMessage);

				string text = Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
				HandleMessage(text);
			}
		}

		internal void Stop()
		{
			_token.Cancel();
		}

		public void Dispose()
		{
			_socket.Dispose();
			_token.Dispose();
		}

		private void HandleMessage(string json)
		{
			Telem message = JsonSerializer.Deserialize<Telem>(json);
			TelemReceived?.Invoke(this, message);
		}
	}
}
