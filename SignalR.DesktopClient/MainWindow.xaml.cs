using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection hubConnection; // connection to our blazor server hub

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked_ChatRoom(object sender, RoutedEventArgs e)
        {
            votingChannel.Visibility = Visibility.Collapsed;
            chatRoom.Visibility = Visibility.Visible;
        }

        private void CheckBox_Checked_VotingChannel(object sender, RoutedEventArgs e)
        {
            votingChannel.Visibility = Visibility.Visible;
            chatRoom.Visibility = Visibility.Collapsed;
        }

        //private void CheckBox_Unchecked_ChatRoom(object sender, RoutedEventArgs e)
        //{
        //    chatRoom.Visibility = Visibility.Collapsed;
        //}

        //private void CheckBox_Unchecked_VotingChannel(object sender, RoutedEventArgs e)
        //{
        //    votingChannel.Visibility = Visibility.Collapsed;
        //}
    }
}
