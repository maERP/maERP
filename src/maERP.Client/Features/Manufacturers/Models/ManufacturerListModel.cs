namespace maERP.Client.Features.Manufacturers.Models;

public partial record ManufacturerListModel
{
    private readonly IManufacturersApiClient _manufacturersApiClient;

    public ManufacturerListModel(IManufacturersApiClient manufacturersApiClient)
    {
        _manufacturersApiClient = manufacturersApiClient;
    }
}
