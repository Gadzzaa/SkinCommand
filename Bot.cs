using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SkinCommand
{
    internal class Bot
    {
        public readonly TwitchClient _client;
        Startup startup = new Startup();

        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Startup.ApiSettings.botUser, Startup.ApiSettings.botOAuth);
            ClientOptions clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            _client = new TwitchClient(customClient);
            _client.Initialize(credentials, Startup.ApiSettings.channel);

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnConnected += Client_OnConnected;

        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("gadzzaaBot ONLINE!");
            _client.SendMessage(e.Channel, "gadzzaaBot online!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (!e.ChatMessage.Message.Contains("!skin")) return;
            _client.SendMessage(e.ChatMessage.Channel, "The current skin i am using is: " + Program.BaseAddresses.Skin.Folder);
         //   _client.SendMessage(e.ChatMessage.Channel, "You can find it inside this google drive folder: " + Startup.ApiSettings.folderLink);
        }
    }
}