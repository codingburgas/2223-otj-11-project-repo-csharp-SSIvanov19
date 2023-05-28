// <copyright file="MappingProfile.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.DailyOrder;
using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;
using CBCanteen.Shared.Models.ReflectionHelpers;

namespace CBCanteen.Server.WebHost.Models;

/// <summary>
/// Mapping profile.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        this.CreateMap<MealIM, Meal>();
        this.CreateMap<Meal, MealVM>()
            .ForMember(d => d.Category, cfg => cfg.MapFrom(s => s.Category.GetDescription()));
        this.CreateMap<MenuIM, Menu>();
        this.CreateMap<Menu, MenuVM>();
        this.CreateMap<DailyOrderIM, DailyOrder>();
        this.CreateMap<DailyOrder, DailyOrderVM>();
    }
}
