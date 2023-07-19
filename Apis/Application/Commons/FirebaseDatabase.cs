using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FirebaseAdmin.Messaging;
using Application.ViewModels.CartDTO;

namespace Application.Commons
{
    public class FcmToken
    {
        public String fcmToken { get; set; } = default!;
    }
    public static class FirebaseDatabase
    {
        //public static async Task SendNotification(string clientToken, string title, string body)
        //{
        //    var message = new Message()
        //    {
        //        Notification = new Notification()
        //        {
        //            Title = title,
        //            Body = body
        //        },
        //        Token = clientToken
        //    };

        //    // Send the message
        //    var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        //    Console.WriteLine($"Successfully sent message: {response}");
        //}
        public static async Task SendNotification(IFirebaseClient client,Guid userId, string title, string body)
        {
            FirebaseResponse firebaseResponse = await client.GetAsync($"TourGuideId/" + userId);
            FcmToken clientToken = JsonConvert.DeserializeObject<FcmToken>(firebaseResponse.Body);

            if(clientToken==null)
            {
                return;
            }
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                },
                Token = clientToken.fcmToken
            };

            // Send the message
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine($"Successfully sent message:{response}");
        }

        
    }
}
