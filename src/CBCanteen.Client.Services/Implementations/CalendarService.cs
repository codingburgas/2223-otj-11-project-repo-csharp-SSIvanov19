using CBCanteen.Client.Services.Contracts;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Implementations;

internal class CalendarService : ICalendarService
{
    private readonly GraphServiceClient graphClient;

    public CalendarService(GraphServiceClient graphClient)
    {
        this.graphClient = graphClient;
    }
    
    public async Task<EventCollectionResponse?> GetEventsBetweenTwoDatesAsync(DateTime StartDate, DateTime EndDate)
    {
        return await this.graphClient.Me.CalendarView.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Top = 100;
            requestConfiguration.QueryParameters.StartDateTime = StartDate.ToString("o");
            requestConfiguration.QueryParameters.EndDateTime = EndDate.ToString("o");
            requestConfiguration.QueryParameters.Select = new string[] { "subject", "attendees", "start", "end" };
        });
    }
}
