using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChatClient.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }


        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageComand { get; set; }

        public string Username { get; set; }
        public string Message { get; set; }
        private Server _server;

        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();

            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.messageRecievedEvent += MessageRecieved;
            _server.userDisconnectEvent += RemoveUser;

            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));

            SendMessageComand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.packetReader.ReadMessage(),
                UID = _server.packetReader.ReadMessage(),

            };
            if(!Users.Any(x => x.UID == user.UID)) //if the Users collection doesnt already contain any user that already has that id then we can add that to the collection
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Users.Add(user);
                });
            }
        }
        private void MessageRecieved()
        {
            var message = _server.packetReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(Message));
        }
        private void RemoveUser()
        {
            var uid = _server.packetReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

    }
}
