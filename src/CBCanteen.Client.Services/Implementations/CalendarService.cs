using CBCanteen.Client.Services.Contracts;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// This class implements methods used to interact with Microsoft Graph API and retrieve calendar events information.
/// </summary>
internal class CalendarService : ICalendarService
{
    /// <summary>
    /// The GraphServiceClient instance.
    /// </summary>
    private readonly GraphServiceClient graphClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarService"/> class.
    /// </summary>
    /// <param name="graphClient">The GraphServiceClient instance</param>
    public CalendarService(GraphServiceClient graphClient)
    {
        this.graphClient = graphClient;
    }

    /// <inheritdoc/>
    public async Task<EventCollectionResponse?> GetEventsBetweenTwoDatesAsync(DateTime startDate, DateTime endDate)
    {
        return await this.graphClient.Me.CalendarView.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Top = 100;
            requestConfiguration.QueryParameters.StartDateTime = startDate.ToString("o");
            requestConfiguration.QueryParameters.EndDateTime = endDate.ToString("o");
            requestConfiguration.QueryParameters.Select = new string[] { "subject", "attendees", "start", "end" };
        });
    }
}
