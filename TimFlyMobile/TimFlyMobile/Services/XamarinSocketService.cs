using Sockets.Plugin;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MobilePrototype.Services
{
    public class XamarinSocketService
    {
        private bool _isConnected;
        TcpSocketClient _client;
        StreamWriter _writer;
        public event EventHandler<string> OnMessageReceived;
        public event EventHandler OnServerDisconnected;

        public XamarinSocketService()
        {
            _client = new TcpSocketClient();
        }

        public async Task<bool> Connect(string adresse, int port)
        {
            if (_isConnected)
                return true;

            bool success = false;
            try
            {
                await _client.ConnectAsync(adresse, port);

                _writer = new StreamWriter(_client.WriteStream);

                Task taskReceive = Task.Run(() => { WaitForData(_client.ReadStream); });

                _isConnected = true;

                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _isConnected;
        }

        private void Disconnection()
        {

        }

        private void WaitForData(Stream inputStream)
        {
            var dr = new StreamReader(inputStream);

            while (true)
            {
                try
                {
                    string serverResponse = dr.ReadLine();

                    if (OnMessageReceived != null && !string.IsNullOrEmpty(serverResponse))
                    {
                        Debug.WriteLine("Message : " + serverResponse);
                        OnMessageReceived(this, serverResponse);
                    }
                }
                catch (Exception ex)
                {
                    Disconnect();
                    Debug.WriteLine(ex);
                }
            }
        }

        private void Disconnect()
        {
            _client.DisconnectAsync();
            OnServerDisconnected?.Invoke(this, new EventArgs());
        }

        public async Task SendMessage(string message)
        {
            if (!_isConnected)
                return;

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Le message ne peut pas être vide ou null");

            try
            {
                await _writer.WriteLineAsync(message);
                await _writer.FlushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
