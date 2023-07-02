using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for ChatRoom.xaml
    /// </summary>
    public partial class ChatRoom : UserControl
    {
        HubConnection hubConnection; // connection to our blazor server hub

        public ChatRoom()
        {
            InitializeComponent();

            InitHubConnection();
            SubscribeEvents();
        }

        private void InitHubConnection()
        {
            hubConnection = new HubConnectionBuilder()
               .WithUrl("https://localhost:7120/ChatHub")
               .WithAutomaticReconnect()
               .Build();
        }

        private void SubscribeEvents()
        {
            hubConnection.Reconnecting += (sender) =>
            {
                //we use dispatcher as Reconnecting is an event so it can be fired on/from different thread.
                this.Dispatcher.Invoke(() =>
                {
                    string newMessage = "Attempting to reconnect ..";
                    messages.Items.Add(newMessage);
                });

                //this will wait for  this.Dispatcher.Invoke to complete its working then return completed task.
                //this will not wait for  this.Dispatcher.InvokeAsync to complete its working then return completed task.
                return Task.CompletedTask;
            };

            hubConnection.Reconnected += (sender) =>
            {
                //we use dispatcher as Reconnected is an event so it can be fired on/from different thread.
                this.Dispatcher.Invoke(() =>
                {
                    string newMessage = "Reconnected to the server ..";
                    messages.Items.Clear();
                    messages.Items.Add(newMessage);
                });

                //this will wait for  this.Dispatcher.Invoke to complete its working then return completed task.
                //this will not wait for  this.Dispatcher.InvokeAsync to complete its working then return completed task.
                return Task.CompletedTask;
            };

            hubConnection.Closed += (sender) =>
            {
                //we use dispatcher as Closed is an event so it can be fired on/from different thread.
                this.Dispatcher.Invoke(() =>
                {
                    string newMessage = "Connection Closed";
                    messages.Items.Add(newMessage);
                    openConnection.IsEnabled = true;
                    sendMessage.IsEnabled = false;
                });

                //this will wait for  this.Dispatcher.Invoke to complete its working then return completed task.
                //this will not wait for  this.Dispatcher.InvokeAsync to complete its working then return completed task.
                return Task.CompletedTask;
            };

            hubConnection.On<string, string>("ReceiveMessage", (user, Message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    string newMessage = $"{user}: {Message}";
                    messages.Items.Add(newMessage);
                });
            });
        }

        private async void openConnection_Click(object sender, RoutedEventArgs e)
        {
            //Start the connection
            try
            {
                await hubConnection.StartAsync();
                messages.Items.Add("Connection Started");
                openConnection.IsEnabled = false;
                sendMessage.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        private async void sendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessageAsync", "WPF-Client", messageInput.Text);
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }
    }
}
