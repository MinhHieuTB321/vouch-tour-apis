using Application.Commons;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;

        public NotificationService(IUnitOfWork unitOfWork,
                            IConfiguration config,
                            IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        public async Task PushNotiBeforeStartDate()
        {
            var groups = await _unitOfWork.GroupRepository
                .FindListByField(x => 
               DateTime.Now.AddDays(2)>x.StartDate &&
               x.StartDate > DateTime.Now &&
                x.Status==GroupEnums.UpComming.ToString() &&
                x.IsDeleted == false);

            for (int i = 0; i < groups.Count; i++)
            {
                await FirebaseDatabase.SendNotification(_client, groups[i].TourGuideId, $"Group {groups[i].GroupName}", $"This group will start on {groups[i].StartDate.ToShortDateString()}!");
            }

            }

        public async Task PushNotiOnStartDate ()
        {
            var groups = await _unitOfWork.GroupRepository.FindListByField(x => x.IsDeleted == false);

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].StartDate.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    await FirebaseDatabase.SendNotification(_client, groups[i].TourGuideId, $"Group {groups[i].GroupName}", $"This group will start on {groups[i].StartDate.ToShortDateString()}!");
                }
            }
        }

        public async Task PushNotiAfterEndDate ()
        {
            var groups = await _unitOfWork.GroupRepository
             .FindListByField(x =>x.IsDeleted == false);
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].EndDate.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    await FirebaseDatabase.SendNotification(_client, groups[i].TourGuideId, $"Group {groups[i].GroupName}", $"This group will start on {groups[i].StartDate.ToShortDateString()}!");
                }
            }
        }

    }
}
