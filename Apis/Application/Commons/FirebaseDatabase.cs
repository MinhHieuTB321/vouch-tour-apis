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
        //public static async Task<int> SendNotification(string title, string body)
        //{
        //    var registrationTokens = new List<string>
        //    {
        //        "eF01z7UkRjyQ-mqKLMSpNK:APA91bHuL4R5hiGxWfvAc4Hq8usmO2fbAjY2FAzywOBBTWjM3hoh1-sTHBw_LZXTgLqzAW_X3i1D2xfmasO9QDzjFG709x__4pvWsjO73eTy9BqLd2DqvB928qThUQguNRbp-p1LyuPX"
        //    };
        //    var message = new MulticastMessage()
        //    {
        //        Tokens = registrationTokens,
        //        Data = new Dictionary<string, string>()
        //        {
        //            {"title", title},
        //            {"body", body},
        //        }
        //    };
        //    var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        //    if (response.FailureCount > 0)
        //    {
        //        var failedTokens = new List<string>();
        //        for (var i = 0; i < response.Responses.Count; i++)
        //        {
        //            if (!response.Responses[i].IsSuccess)
        //            {
        //                // The order of responses corresponds to the order of the registration tokens.
        //                failedTokens.Add(registrationTokens[i]);
        //            }
        //        }

        //        Console.WriteLine($"List of tokens that caused failures: {failedTokens}");
        //    }
        //    return response.SuccessCount;
        //}

        public static async Task SendNotification(string title, string body)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Test Notification",
                    Body = "This is a test notification"
                },
                Token = "eF01z7UkRjyQ-mqKLMSpNK:APA91bHuL4R5hiGxWfvAc4Hq8usmO2fbAjY2FAzywOBBTWjM3hoh1-sTHBw_LZXTgLqzAW_X3i1D2xfmasO9QDzjFG709x__4pvWsjO73eTy9BqLd2DqvB928qThUQguNRbp-p1LyuPX"
            };

            // Send the message
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine($"Successfully sent message: {response}");
        }
    }
}
