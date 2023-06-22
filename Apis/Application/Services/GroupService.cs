﻿using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels.GroupDTOs;
using AutoMapper;
using Domain.Entities;
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

        public async Task<GroupViewDTO> CreateGroupAsync(GroupCreateDTO createDTO)
        {
            var groupDTO = _mapper.Map<Group>(createDTO);
            groupDTO.TourGuideId=_claimsService.GetCurrentUser;
            var result= await _unitOfWork.GroupRepository.AddAsync(groupDTO);
            if (await _unitOfWork.SaveChangeAsync() > 0)
            {
                var groupView = _mapper.Map<GroupViewDTO>(result);
                return groupView;
            }
            throw new BadRequestException("Create fail!");
        }

        public async Task<List<GroupViewDTO>> GetAllGroupAsync()
        {
            var listGroup = (await _unitOfWork.GroupRepository.GetAllAsync(x=>x.Menu)).Where(x=>x.TourGuideId==_claimsService.GetCurrentUser).ToList();
            if (listGroup.Count==0) throw new NotFoundException("No Group!");
            var mapper = _mapper.Map<List<GroupViewDTO>>(listGroup);
            return mapper;
        }


        public async Task<GroupViewDTO> GetGroupByIdAsyn(Guid groupId)
        {
            var group= await _unitOfWork.GroupRepository.FindByField(x=>x.Id==groupId&& x.TourGuideId==_claimsService.GetCurrentUser,x=>x.Menu);
            if (group == null) throw new NotFoundException("There is no group " + groupId + " in system!");
            var result= _mapper.Map<GroupViewDTO>(group);
            return result;
        }

        public async Task<bool> UpdateGroupAsync(GroupUpdateDTO updateDTO)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(updateDTO.Id,x=>x.Menu);
            if(group == null) throw new NotFoundException("There is no group " + updateDTO.Id + " in system!");
            if (group.Menu == null)
            {
                return await Update(updateDTO, group);
            }
            else
            {
                if (group.Menu.IsDeleted == false) throw new BadRequestException("Already existing menu for group");
                return await Update(updateDTO, group);
            }


            throw new BadRequestException("Already existing menu for group");
        }


        private async Task<bool> Update(GroupUpdateDTO updateDTO,Group group)
        {
            var mapper = _mapper.Map(updateDTO, group);
            mapper.Menu = await _unitOfWork.MenuRepository.GetByIdAsync(updateDTO.MenuId);
            _unitOfWork.GroupRepository.Update(group);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }
        public async Task AddMenu(GroupMenuDTO updateDTO)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(updateDTO.Id,x=>x.Menu);
            if (group == null) throw new NotFoundException("There is no group " + updateDTO.Id + " in system!");
            if (group.Menu==null) throw new BadRequestException("Already existing menu for group");
            group.MenuId = updateDTO.MenuId;
            _unitOfWork.GroupRepository.Update(group);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
