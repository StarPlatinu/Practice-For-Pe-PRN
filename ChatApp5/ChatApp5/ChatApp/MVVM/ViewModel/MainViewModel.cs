using ChatClient.Core;
using ChatClient.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChatClient.MVVM.ViewModel
{
    class MainViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }
        private Server _server;
        public string username { get; set; }
        public MainViewModel()
        {
            _server= new Server();
            ConnectToServerCommand = new RelayCommand(o => _server.connectToServer(username), o => !string.IsNullOrEmpty(username));
        }
    }
}
