using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.GroupDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork,IClaimsService claimsService,IMapper mapper)
        {
            _claimsService = claimsService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Create Group
        public async Task<GroupViewDTO> CreateGroupAsync(GroupCreateDTO createDTO)
        {
            var groupDTO = _mapper.Map<Group>(createDTO);
            groupDTO.TourGuideId=_claimsService.GetCurrentUser;
            if (createDTO.StartDate <= DateTime.Now)
            {
                groupDTO.Status=GroupEnums.InProgress.ToString();
            }
            var result= await _unitOfWork.GroupRepository.AddAsync(groupDTO);
            if (await _unitOfWork.SaveChangeAsync() > 0)
            {
                var groupView = _mapper.Map<GroupViewDTO>(result);
                return groupView;
            }
            throw new BadRequestException("Create fail!");
        }

        #endregion

        #region GetAll Group
        public async Task<List<GroupViewDTO>> GetAllGroupAsync()
        {
            var listGroup = (await _unitOfWork.GroupRepository.GetAllAsync(x=>x.Menu!)).OrderByDescending(x=>x.CreationDate).Where(x=>x.TourGuideId==_claimsService.GetCurrentUser).ToList();
            if (listGroup.Count==0) throw new NotFoundException("No Group!");
            var mapper = _mapper.Map<List<GroupViewDTO>>(listGroup);
            return mapper;
        }
        #endregion

        #region Get Group By Id
        public async Task<GroupViewDTO> GetGroupByIdAsyn(Guid groupId)
        {
            var group= await _unitOfWork.GroupRepository.FindByField(x=>x.Id==groupId,x=>x.Menu!);
            if (group == null) throw new NotFoundException("There is no group " + groupId + " in system!");
            var result= _mapper.Map<GroupViewDTO>(group);
            return result;
        }
        #endregion

        #region Update Group
        public async Task<bool> UpdateGroupAsync(GroupUpdateDTO updateDTO)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(updateDTO.Id,x=>x.Menu!);
            if(group == null) throw new NotFoundException("There is no group " + updateDTO.Id + " in system!");
            return await Update(updateDTO, group);
        }


        private async Task<bool> Update(GroupUpdateDTO updateDTO,Group group)
        {
            var mapper = _mapper.Map(updateDTO, group);
            if (updateDTO.StartDate <= DateTime.Now) { mapper.Status=GroupEnums.InProgress.ToString(); }
            mapper.Menu = await _unitOfWork.MenuRepository.GetByIdAsync(updateDTO.MenuId);
            _unitOfWork.GroupRepository.Update(group);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }

        #endregion

        #region Add Menu for Group
        public async Task AddMenu(GroupMenuDTO updateDTO)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(updateDTO.Id,x=>x.Menu!);
            if (group == null) throw new NotFoundException("There is no group " + updateDTO.Id + " in system!");
            if (group.Menu==null) throw new BadRequestException("Already existing menu for group");
            group.MenuId = updateDTO.MenuId;
            _unitOfWork.GroupRepository.Update(group);
            await _unitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Get All Order By Group Id
        public async Task<List<OrderViewDTO>> GetAllOrdersAsync(Guid groupId)
        {
            var orders=await _unitOfWork.OrderRepository.FindListByField(x=>x.GroupId==groupId,x=>x.OrderDetails,x=>x.Group,x=>x.Payments);
            if (orders.Count == 0) throw new NotFoundException("There is no order for group!");
            orders=orders.OrderByDescending(x=>x.CreationDate).ToList();
            var result=GetOrdersViewDTO(orders);
            return result;
        }

        private List<OrderViewDTO> GetOrdersViewDTO(List<Order> orders)
        {
            var result = new List<OrderViewDTO>();
            orders.ForEach(x =>
            {
                var addDTO= _mapper.Map<OrderViewDTO>(x);
                addDTO.OrderDetails = _mapper.Map<List<OrderDetailViewDTO>>(x.OrderDetails);
                addDTO.PaymentName=x.Payments.First().PaymentName;
                result.Add(addDTO);
            });
            return result;
        }
        #endregion

        #region Delete group
        public async Task<bool> DeleteGroup(Guid groupId)
        {
            var group= await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (group == null) throw new NotFoundException($"Group {groupId} is not exist in system");
            _unitOfWork.GroupRepository.SoftRemove(group);
            var result = await _unitOfWork.SaveChangeAsync();
            return result>0;
        }
        #endregion

        #region Update group Status
        public async Task UpdateGroupStatus()
        {
            var group = await _unitOfWork.GroupRepository.GetAllAsync();
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].EndDate < DateTime.Now)
                {
                    group[i].Status=GroupEnums.Commplete.ToString();
                    _unitOfWork.GroupRepository.Update(group[i]);
                    await _unitOfWork.SaveChangeAsync();
                }
                if (group[i].StartDate <= DateTime.Now && group[i].EndDate >= DateTime.Now)
                {
                    group[i].Status = GroupEnums.Commplete.ToString();
                    _unitOfWork.GroupRepository.Update(group[i]);
                    await _unitOfWork.SaveChangeAsync();
                }

            };
        }
        #endregion
    }
}
