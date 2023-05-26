// <copyright file="ReflectionHelpers.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace CBCanteen.Shared.Models.ReflectionHelpers;

/// <summary>
/// Utility class for reflection related operations.
/// </summary>
public static class ReflectionHelpers
{
    /// <summary>
    /// Gets custom description of an Enum object.
    /// </summary>
    /// <param name="objEnum">Enum object to get description of.</param>
    /// <returns>Description of the Enum object, or the object's name if no description was found.</returns>
    public static string GetCustomDescription(object objEnum)
    {
        var fi = objEnum.GetType().GetField(objEnum.ToString());
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
    }

    /// <summary>
    /// Gets description of an Enum object using its Extension Method.
    /// </summary>
    /// <param name="value">Enum object to get description of.</param>
    /// <returns>Description of the Enum object, or the object's name if no description was found.</returns>
    public static string GetDescription(this Enum value)
    {
        return GetCustomDescription(value);
    }
}
