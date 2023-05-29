// <copyright file="TimeOnlyConverter.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CBCanteen.Server.Data.Models.ValueConverters;

/// <summary>
/// Converts TimeOnly struct to TimeSpan represented in the database and vice versa.
/// </summary>
public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeOnlyConverter"/> class.
    /// </summary>
    public TimeOnlyConverter()
        : base(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    {
    }
}

/// <summary>
/// Compares instances of TimeOnly struct.
/// </summary>
public class TimeOnlyComparer : ValueComparer<TimeOnly>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeOnlyComparer"/> class.
    /// </summary>
    public TimeOnlyComparer()
        : base(
        (t1, t2) => t1.Ticks == t2.Ticks,
        t => t.GetHashCode())
    {
    }
}