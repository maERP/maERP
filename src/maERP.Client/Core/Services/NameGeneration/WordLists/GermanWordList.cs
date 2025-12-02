namespace maERP.Client.Core.Services.NameGeneration.WordLists;

/// <summary>
/// German word lists for name generation.
/// </summary>
public class GermanWordList : IWordList
{
    public string LanguageCode => "de";

    public IReadOnlyList<string> ProductAdjectives { get; } =
    [
        "Premium", "Ergonomisch", "Professional", "Smart", "Kompakt",
        "Wireless", "Digital", "Ultra", "Eco", "Modern",
        "Klassisch", "Robust", "Leicht", "Schnell", "Leistungsstark",
        "Tragbar", "Flexibel", "Multifunktional", "Hochwertig", "Innovativ"
    ];

    public IReadOnlyList<string> ProductCategories { get; } =
    [
        // Electronics
        "Laptop", "Monitor", "Tastatur", "Maus", "Headset",
        "Webcam", "Drucker", "Scanner", "Router", "Festplatte",
        "USB-Hub", "Dockingstation", "Ladegerät", "Powerbank", "Tablet",
        // Office
        "Schreibtisch", "Bürostuhl", "Aktenschrank", "Whiteboard", "Schreibtischlampe",
        "Ordner", "Notizbuch", "Kalender", "Stifthalter", "Papierkorb",
        "Rollcontainer", "Regal", "Konferenztisch", "Flipchart", "Pinnwand",
        // Audio
        "Lautsprecher", "Mikrofon", "Kopfhörer", "Soundbar", "Verstärker",
        "Bluetooth-Box", "Audiointerface", "Mischpult"
    ];

    public IReadOnlyList<string> ProductVariants { get; } =
    [
        "Pro", "Plus", "Max", "Lite", "Mini", "XL", "SE",
        "2024", "2025", "V2", "V3", "MK II", "MK III",
        "Elite", "Basic", "Advanced", "Expert", "Home", "Business",
        "15", "17", "21", "24", "27", "32", "4K", "HD", "UHD"
    ];

    public IReadOnlyList<string> FirstNames { get; } =
    [
        "Max", "Anna", "Thomas", "Maria", "Michael", "Julia",
        "Stefan", "Laura", "Andreas", "Sandra", "Christian", "Nicole",
        "Markus", "Sabine", "Daniel", "Petra", "Martin", "Claudia",
        "Sebastian", "Katharina", "Tobias", "Monika", "Patrick", "Stefanie",
        "Jan", "Lisa", "Felix", "Sarah", "Lukas", "Lena",
        "Tim", "Hannah", "David", "Emma", "Florian", "Sophie"
    ];

    public IReadOnlyList<string> LastNames { get; } =
    [
        "Müller", "Schmidt", "Weber", "Wagner", "Becker", "Hoffmann",
        "Schulz", "Koch", "Richter", "Klein", "Wolf", "Schröder",
        "Neumann", "Schwarz", "Zimmermann", "Braun", "Krüger", "Hofmann",
        "Hartmann", "Lange", "Schmitt", "Werner", "Schmitz", "Krause",
        "Meier", "Lehmann", "Schmid", "Schulze", "Maier", "Köhler",
        "Herrmann", "König", "Walter", "Mayer", "Huber", "Kaiser"
    ];

    public IReadOnlyList<string> Streets { get; } =
    [
        "Hauptstraße", "Bahnhofstraße", "Gartenstraße", "Schulstraße", "Bergstraße",
        "Dorfstraße", "Kirchstraße", "Waldstraße", "Ringstraße", "Lindenstraße",
        "Birkenweg", "Eichenweg", "Rosenweg", "Tulpenweg", "Sonnenweg",
        "Am Markt", "Am Park", "Am Bach", "Am Hang", "Am Wald",
        "Mühlenweg", "Industriestraße", "Gewerbestraße", "Parkstraße", "Seestraße",
        "Blumenstraße", "Feldstraße", "Wiesenstraße", "Buchenweg", "Ahornweg",
        "Mozartstraße", "Beethovenstraße", "Goethestraße", "Schillerstraße", "Lessingstraße"
    ];

    public IReadOnlyList<string> Cities { get; } =
    [
        "Berlin", "Hamburg", "München", "Köln", "Frankfurt am Main",
        "Stuttgart", "Düsseldorf", "Leipzig", "Dortmund", "Essen",
        "Bremen", "Dresden", "Hannover", "Nürnberg", "Duisburg",
        "Bochum", "Wuppertal", "Bielefeld", "Bonn", "Münster",
        "Mannheim", "Karlsruhe", "Augsburg", "Wiesbaden", "Mönchengladbach",
        "Gelsenkirchen", "Aachen", "Braunschweig", "Kiel", "Chemnitz",
        "Halle", "Magdeburg", "Freiburg", "Krefeld", "Mainz"
    ];

    public IReadOnlyList<string> ZipCodes { get; } =
    [
        "10115", "10178", "10435", "12043", "13353",
        "20095", "20457", "22041", "22765", "23552",
        "30159", "30449", "33098", "34117", "35037",
        "40210", "40476", "44135", "45127", "48143",
        "50667", "53111", "55116", "60311", "63065",
        "70173", "72070", "76131", "79098", "80331",
        "81541", "85049", "86150", "90402", "99084"
    ];
}
