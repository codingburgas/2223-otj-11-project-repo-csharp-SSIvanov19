// <copyright file="LunchHoursService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Data.Models.User;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Service for managing user lunch hours.
/// </summary>
internal class LunchHoursService : ILunchHoursService
{
    private readonly ApplicationDBContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LunchHoursService"/> class.
    /// </summary>
    /// <param name="context">The DB Context.</param>
    public LunchHoursService(ApplicationDBContext context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<UserLunchHoursVM?> GetUserLunchHoursAsync(string userId)
    {
        var returnVal = new UserLunchHoursVM();

        var userLunchHours = await this.context.UserLunchHours.Where(ulh => ulh.UserId == userId)
            .Include(o => o.MondayLunchHours)
            .Include(o => o.TuesdayLunchTime)
            .Include(o => o.WednesdayLunchTime)
            .Include(o => o.ThursdayLunchTime)
            .Include(o => o.FridayLunchTime)
            .FirstOrDefaultAsync();

        if (userLunchHours is null)
        {
            return null;
        }

        returnVal.HasSameLunchHours = userLunchHours.HasSameLunchHours;
        returnVal.MondayLunchTimeStart = userLunchHours.MondayLunchHours.StartTime;
        returnVal.MondayLunchTimeEnd = userLunchHours.MondayLunchHours.EndTime;
        returnVal.TuesdayLunchTimeStart = userLunchHours.TuesdayLunchTime.StartTime;
        returnVal.TuesdayLunchTimeEnd = userLunchHours.TuesdayLunchTime.EndTime;
        returnVal.WednesdayLunchTimeStart = userLunchHours.WednesdayLunchTime.StartTime;
        returnVal.WednesdayLunchTimeEnd = userLunchHours.WednesdayLunchTime.EndTime;
        returnVal.ThursdayLunchTimeStart = userLunchHours.ThursdayLunchTime.StartTime;
        returnVal.ThursdayLunchTimeEnd = userLunchHours.ThursdayLunchTime.EndTime;
        returnVal.FridayLunchTimeStart = userLunchHours.FridayLunchTime.StartTime;
        returnVal.FridayLunchTimeEnd = userLunchHours.FridayLunchTime.EndTime;

        return returnVal;
    }

    /// <inheritdoc/>
    public async Task SetUserLunchHoursAsync(UserLunchHoursIM userLunchHoursIM, string userId)
    {
        var userLunchHour = this.context.UserLunchHours.Where(ulh => ulh.UserId == userId).FirstOrDefault();

        userLunchHour ??= new UserLunchHours
            {
                UserId = userId,
            };

        userLunchHour.HasSameLunchHours = userLunchHoursIM.HasSameLunchHours;

        if (userLunchHoursIM.HasSameLunchHours)
        {
            var dayLunchHours = this.context.LunchHours.Where(lh => lh.StartTime == userLunchHoursIM.MondayLunchTimeStart && lh.EndTime == userLunchHoursIM.MondayLunchTimeEnd).FirstOrDefault();

            if (dayLunchHours is not null)
            {
                userLunchHour.MondayLunchHoursId = dayLunchHours.Id;
                userLunchHour.TuesdayLunchTimeId = dayLunchHours.Id;
                userLunchHour.WednesdayLunchTimeId = dayLunchHours.Id;
                userLunchHour.ThursdayLunchTimeId = dayLunchHours.Id;
                userLunchHour.FridayLunchTimeId = dayLunchHours.Id;
            }
            else
            {
                var id = Guid.NewGuid().ToString();
                this.context.LunchHours.Add(new LunchHours
                {
                    Id = id,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = userLunchHoursIM.MondayLunchTimeStart,
                    EndTime = userLunchHoursIM.MondayLunchTimeEnd,
                });

                userLunchHour.MondayLunchHoursId = id;
                userLunchHour.TuesdayLunchTimeId = id;
                userLunchHour.WednesdayLunchTimeId = id;
                userLunchHour.ThursdayLunchTimeId = id;
                userLunchHour.FridayLunchTimeId = id;
            }

            await this.SaveUserLunchHours(userLunchHour);
            return;
        }

        // For each day of the work week check if there are already people with the same lunch hours
        foreach (var day in Enum.GetValues(typeof(DayOfWeek)))
        {
            var dayLunchHours = this.context.LunchHours.Where(lh => lh.DayOfWeek == (DayOfWeek)day);

            TimeOnly startTime;
            TimeOnly endTime;

            switch ((DayOfWeek)day)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                default:
                    continue;
                case DayOfWeek.Monday:
                    startTime = userLunchHoursIM.MondayLunchTimeStart;
                    endTime = userLunchHoursIM.MondayLunchTimeEnd;
                    break;
                case DayOfWeek.Tuesday:
                    startTime = userLunchHoursIM.TuesdayLunchTimeStart;
                    endTime = userLunchHoursIM.TuesdayLunchTimeEnd;
                    break;
                case DayOfWeek.Wednesday:
                    startTime = userLunchHoursIM.WednesdayLunchTimeStart;
                    endTime = userLunchHoursIM.WednesdayLunchTimeEnd;
                    break;
                case DayOfWeek.Thursday:
                    startTime = userLunchHoursIM.ThursdayLunchTimeStart;
                    endTime = userLunchHoursIM.ThursdayLunchTimeEnd;
                    break;
                case DayOfWeek.Friday:
                    startTime = userLunchHoursIM.FridayLunchTimeStart;
                    endTime = userLunchHoursIM.FridayLunchTimeEnd;
                    break;
            }

            var dlh = dayLunchHours.Where(lh => lh.StartTime == startTime && lh.EndTime == endTime).FirstOrDefault();

            if (dlh is not null)
            {
                switch ((DayOfWeek)day)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                    default:
                        continue;
                    case DayOfWeek.Monday:
                        userLunchHour.MondayLunchHoursId = dlh.Id;
                        break;
                    case DayOfWeek.Tuesday:
                        userLunchHour.TuesdayLunchTimeId = dlh.Id;
                        break;
                    case DayOfWeek.Wednesday:
                        userLunchHour.WednesdayLunchTimeId = dlh.Id;
                        break;
                    case DayOfWeek.Thursday:
                        userLunchHour.ThursdayLunchTimeId = dlh.Id;
                        break;
                    case DayOfWeek.Friday:
                        userLunchHour.FridayLunchTimeId = dlh.Id;
                        break;
                }
            }
        }

        if (string.IsNullOrEmpty(userLunchHour.MondayLunchHoursId))
        {
            var lunchHour = new LunchHours();
            lunchHour.Id = Guid.NewGuid().ToString();
            lunchHour.DayOfWeek = DayOfWeek.Monday;
            lunchHour.StartTime = userLunchHoursIM.MondayLunchTimeStart;
            lunchHour.EndTime = userLunchHoursIM.MondayLunchTimeEnd;

            this.context.LunchHours.Add(lunchHour);
            await this.context.SaveChangesAsync();

            userLunchHour.MondayLunchHoursId = lunchHour.Id;
        }

        if (string.IsNullOrEmpty(userLunchHour.TuesdayLunchTimeId))
        {
            var lunchHour = new LunchHours();
            lunchHour.Id = Guid.NewGuid().ToString();
            lunchHour.DayOfWeek = DayOfWeek.Tuesday;
            lunchHour.StartTime = userLunchHoursIM.TuesdayLunchTimeStart;
            lunchHour.EndTime = userLunchHoursIM.TuesdayLunchTimeEnd;

            this.context.LunchHours.Add(lunchHour);
            await this.context.SaveChangesAsync();

            userLunchHour.TuesdayLunchTimeId = lunchHour.Id;
        }

        if (string.IsNullOrEmpty(userLunchHour.WednesdayLunchTimeId))
        {
            var lunchHour = new LunchHours();
            lunchHour.Id = Guid.NewGuid().ToString();
            lunchHour.DayOfWeek = DayOfWeek.Wednesday;
            lunchHour.StartTime = userLunchHoursIM.WednesdayLunchTimeStart;
            lunchHour.EndTime = userLunchHoursIM.WednesdayLunchTimeEnd;

            this.context.LunchHours.Add(lunchHour);
            await this.context.SaveChangesAsync();

            userLunchHour.WednesdayLunchTimeId = lunchHour.Id;
        }

        if (string.IsNullOrEmpty(userLunchHour.ThursdayLunchTimeId))
        {
            var lunchHour = new LunchHours();
            lunchHour.Id = Guid.NewGuid().ToString();
            lunchHour.DayOfWeek = DayOfWeek.Thursday;
            lunchHour.StartTime = userLunchHoursIM.ThursdayLunchTimeStart;
            lunchHour.EndTime = userLunchHoursIM.ThursdayLunchTimeEnd;

            this.context.LunchHours.Add(lunchHour);
            await this.context.SaveChangesAsync();

            userLunchHour.ThursdayLunchTimeId = lunchHour.Id;
        }

        if (string.IsNullOrEmpty(userLunchHour.FridayLunchTimeId))
        {
            var lunchHour = new LunchHours();
            lunchHour.Id = Guid.NewGuid().ToString();
            lunchHour.DayOfWeek = DayOfWeek.Friday;
            lunchHour.StartTime = userLunchHoursIM.FridayLunchTimeStart;
            lunchHour.EndTime = userLunchHoursIM.FridayLunchTimeEnd;

            this.context.LunchHours.Add(lunchHour);
            await this.context.SaveChangesAsync();

            userLunchHour.FridayLunchTimeId = lunchHour.Id;
        }

        await this.SaveUserLunchHours(userLunchHour);
    }

    private async Task SaveUserLunchHours(UserLunchHours userLunchHours)
    {
        if (this.context.UserLunchHours!.Any(o => o.UserId == userLunchHours.UserId))
        {
            this.context.Update(userLunchHours);
        }
        else
        {
            this.context.Add(userLunchHours);
        }

        await this.context.SaveChangesAsync();
    }
}
