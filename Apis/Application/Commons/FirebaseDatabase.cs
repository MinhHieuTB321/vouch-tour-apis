using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FirebaseAdmin.Messaging;

namespace Application.Commons
{
    public static class FirebaseDatabase
    {

        public static async Task SendNotification(string title, string body)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Test Notification",
                    Body = "This is a test notification"
                },
                Token = ""
            };

            // Send the message
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine($"Successfully sent message: {response}");
        }
    }
}
