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

namespace ChatClient.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }

        public string Username { get; set; }

        private Server _server;

        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
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

    }
}
