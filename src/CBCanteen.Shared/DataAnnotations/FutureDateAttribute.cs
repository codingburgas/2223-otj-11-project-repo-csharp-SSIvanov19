using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Shared.DataAnnotations;

/// <summary>
/// Validates that a date is in the future.
/// </summary>
public class FutureDateAttribute : ValidationAttribute
{
    /// <summary>
    /// Determines if the provided value is a date in the future.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A boolean value indicating whether the provided value is a valid future date.</returns>
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not DateTime date)
        {
            return false;
        }

        return date.Date >= DateTime.Now.Date;
    }
}
