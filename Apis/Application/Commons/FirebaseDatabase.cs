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

        public static async Task SendNotification(List<string> clientToken, string title, string body)
        {
            for (int i = 0; i < clientToken.Count; i++)
            {
                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body
                    },
                    Token = clientToken[i]
                };

                // Send the message
                var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"Successfully sent message: {response}");
            }     
        }
    }
}
