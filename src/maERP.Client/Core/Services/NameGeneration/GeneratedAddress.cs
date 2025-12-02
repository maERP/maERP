namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Represents a generated address with all components.
/// </summary>
/// <param name="Firstname">The first name of the address holder.</param>
/// <param name="Lastname">The last name of the address holder.</param>
/// <param name="Street">The street name.</param>
/// <param name="HouseNr">The house number.</param>
/// <param name="Zip">The postal/zip code.</param>
/// <param name="City">The city name.</param>
public record GeneratedAddress(
    string Firstname,
    string Lastname,
    string Street,
    string HouseNr,
    string Zip,
    string City);
