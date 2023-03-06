using ChatClient.Core;
using ChatClient.MVVM.Model;
using ChatClient.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> users { get; set; }
        public ObservableCollection<string> messages { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        private Server _server;
        public string username { get; set; }
        public string message { get; set; }
        public MainViewModel()
        {
            users = new ObservableCollection<UserModel>();
            messages = new ObservableCollection<string>();
            _server= new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            ConnectToServerCommand = new RelayCommand(o => _server.connectToServer(username), o => !string.IsNullOrEmpty(username));
            SendMessageCommand = new RelayCommand(o => _server.sendMessageToServer(message), o => !string.IsNullOrEmpty(message));

        }

        private void MessageReceived()
        {
            var msg = _server.packetReader.readMessage();
            Application.Current.Dispatcher.Invoke(() => messages.Add(msg));
        }

        private void UserConnected()
        {
            var user = new UserModel()
            {
                username = _server.packetReader.readMessage(),
                UID = _server.packetReader.readMessage(),
            };

            if(!users.Any(x => x.UID == user.UID)) 
            {
                Application.Current.Dispatcher.Invoke(() => users.Add(user));
            }
        }
    }
}
