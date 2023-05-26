// <copyright file="MappingProfile.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Globalization;
using AutoMapper;
using CBCanteen.Shared.Models;
using CBCanteen.Shared.Models.Canteen;
using Microsoft.Graph.Models;

namespace CBCanteen.Client.Web.Models;

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
        this.CreateMap<Event, CalendarEvent>()
            .ForMember(d => d.StartTime, cfg => cfg.MapFrom(s => DateTime.ParseExact(s.Start!.DateTime!, "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture)))
            .ForMember(d => d.StartTimezone, cfg => cfg.MapFrom(s => s.Start!.TimeZone))
            .ForMember(d => d.EndTime, cfg => cfg.MapFrom(s => DateTime.ParseExact(s.End!.DateTime!, "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture)))
            .ForMember(d => d.EndTimezone, cfg => cfg.MapFrom(s => s.End!.TimeZone))
            .ForMember(d => d.Description, cfg => cfg.MapFrom(s => s.Attendees!.Where(attendee => attendee.Type == AttendeeType.Required).LastOrDefault()!.EmailAddress!.Name))
            .ForMember(d => d.Location, cfg => cfg.MapFrom(s => s.Location!.DisplayName))
            .ForMember(d => d.IsAllDay, cfg => cfg.MapFrom(s => false))
            .ForMember(d => d.RecurrenceID, cfg => cfg.MapFrom(s => string.Empty))
            .ForMember(d => d.RecurrenceRule, cfg => cfg.MapFrom(s => string.Empty))
            .ForMember(d => d.RecurrenceException, cfg => cfg.MapFrom(s => string.Empty))
            .ForMember(d => d.IsReadonly, cfg => cfg.MapFrom(s => true))
            .ForMember(d => d.IsBlock, cfg => cfg.MapFrom(s => false))
            .ForMember(d => d.CssClass, cfg => cfg.MapFrom(s => string.Empty));
        this.CreateMap<MealVM, MealIM>()
            .ForMember(d => d.Category, cfg => cfg.MapFrom(s => CategoryStringToEnum(s.Category)));
    }

    private static MealCategories CategoryStringToEnum(string category)
    {
        return category switch
        {
            "Пред." => MealCategories.Appetizer,
            "Осн." => MealCategories.MainDish,
            "Десерт" => MealCategories.Dessert,
            _ => MealCategories.Appetizer,
        };
    }
}