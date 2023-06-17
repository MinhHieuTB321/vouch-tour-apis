﻿using Application.ViewModels.ProductInMenuDTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MenuDTOs
{
    public class MenuViewDTO
    {
        public Guid Id { get; set; }
        public Guid TourGuideId {get;set;}
        public int Quantity { get; set;}
        public string Status { get; set; } = default!;
        public List<ProductMenuViewDTO> Products { get; set; } = default!;
    }



    public class MenuCreateDTO
    {
        public Guid GroupId { get; set; }

    }
}