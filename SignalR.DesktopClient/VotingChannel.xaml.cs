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
    /// Interaction logic for VotingChannel.xaml
    /// </summary>
    public partial class VotingChannel : UserControl
    {
        HubConnection counterConnection; // connection to our blazor server hub
        public VotingChannel()
        {
            InitializeComponent();

            InitHubConnection();
            SubscribeEvents();
        }

        private void InitHubConnection()
        {
            counterConnection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:7120/CounterHub")
                 .WithAutomaticReconnect()
                 .Build();
        }
        private void SubscribeEvents()
        {
            counterConnection.Closed += (sender) =>
            {
                //we use dispatcher as Closed is an event so it can be fired on/from different thread.
                this.Dispatcher.Invoke(() =>
                {
                    string newMessage = "Connection Closed";
                });

                //this will wait for  this.Dispatcher.Invoke to complete its working then return completed task.
                //this will not wait for  this.Dispatcher.InvokeAsync to complete its working then return completed task.
                return Task.CompletedTask;
            };
        }

        private async void VoteCounterConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Creating counter connection to SignalR hub.
                await counterConnection.StartAsync();
                VoteCounterConnection.IsEnabled = false;
                Vote.IsEnabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private async void Vote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await counterConnection.InvokeAsync("AddToTotal", "WPFClient", 1);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
