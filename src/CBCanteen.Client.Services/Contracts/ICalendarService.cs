using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Contracts;

public interface ICalendarService
{
    Task<EventCollectionResponse?> GetEventsBetweenTwoDatesAsync(DateTime StartDate, DateTime EndDate);
}
