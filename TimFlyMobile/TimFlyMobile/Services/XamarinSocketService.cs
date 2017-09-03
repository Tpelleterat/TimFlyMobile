using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePrototype.Services
{
    public class XamarinSocketService
    {
        TcpSocketClient _client;
        StreamWriter _writer;
        public event EventHandler<string> OnMessageReceived;

        public async Task<bool> Connect(string adresse, int port)
        {
            bool success = false;
            try
            {
                _client = new TcpSocketClient();
                await _client.ConnectAsync(adresse, port);

                _writer = new StreamWriter(_client.WriteStream);

                Task taskReceive = Task.Run(() => { WaitForData(_client.ReadStream); });
                await taskReceive.ConfigureAwait(false);

                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
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
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Le message ne peut pas être vide ou null");
            
            await _writer.WriteLineAsync(message);
            await _writer.FlushAsync();
        }
    }
}
