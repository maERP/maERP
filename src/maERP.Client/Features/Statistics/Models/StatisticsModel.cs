namespace maERP.Client.Features.Statistics.Models;

public partial record StatisticsModel
{
    private readonly IStatisticsApiClient _statisticsApiClient;

    public StatisticsModel(IStatisticsApiClient statisticsApiClient)
    {
        _statisticsApiClient = statisticsApiClient;
    }
}
