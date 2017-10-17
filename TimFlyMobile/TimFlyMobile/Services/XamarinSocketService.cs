using Sockets.Plugin;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MobilePrototype.Services
{
    public class XamarinSocketService
    {
        TcpSocketClient _client;
        StreamWriter _writer;
        public event EventHandler<string> OnMessageReceived;
        bool _isConnected;

        public XamarinSocketService()
        {
            _client = new TcpSocketClient();
        }

        public async Task<bool> Connect(string adresse, int port)
        {
            try
            {
                await _client.ConnectAsync(adresse, port);

                _writer = new StreamWriter(_client.WriteStream);

                Task taskReceive = Task.Run(() => { WaitForData(_client.ReadStream); });

                //Task taskReccceive = Task.Run(() => { CheckConnexion(_client); });

                _isConnected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _isConnected;
        }

        private async void CheckConnexion(TcpSocketClient test)
        {
            while (_isConnected)
            {
                try
                {
                    var interfaceSocket = await test.GetConnectedInterfaceAsync();

                    if (interfaceSocket.ConnectionStatus == Sockets.Plugin.Abstractions.CommsInterfaceStatus.Disconnected)
                    {
                        Disconnection();
                    }
                }
                catch (Exception ex)
                {

                }

                await Task.Delay(1000);
            }
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
                    Debug.WriteLine(ex);
                }
            }
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
