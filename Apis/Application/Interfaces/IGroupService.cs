using Application.ViewModels.GroupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupViewDTO>> GetAllGroupAsync();
        Task<GroupViewDTO> GetGroupByIdAsyn(Guid groupId);
        Task<GroupViewDTO> CreateGroupAsync(GroupCreateDTO createDTO);
        Task<bool> UpdateGroupAsync(GroupUpdateDTO updateDTO);
    }
}
