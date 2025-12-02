namespace maERP.Client.Core.Services.NameGeneration.WordLists;

/// <summary>
/// English word lists for name generation.
/// </summary>
public class EnglishWordList : IWordList
{
    public string LanguageCode => "en";

    public IReadOnlyList<string> ProductAdjectives { get; } =
    [
        "Premium", "Ergonomic", "Professional", "Smart", "Compact",
        "Wireless", "Digital", "Ultra", "Eco", "Modern",
        "Classic", "Robust", "Lightweight", "Fast", "Powerful",
        "Portable", "Flexible", "Multifunctional", "High-Quality", "Innovative"
    ];

    public IReadOnlyList<string> ProductCategories { get; } =
    [
        // Electronics
        "Laptop", "Monitor", "Keyboard", "Mouse", "Headset",
        "Webcam", "Printer", "Scanner", "Router", "Hard Drive",
        "USB Hub", "Docking Station", "Charger", "Power Bank", "Tablet",
        // Office
        "Desk", "Office Chair", "Filing Cabinet", "Whiteboard", "Desk Lamp",
        "Binder", "Notebook", "Calendar", "Pen Holder", "Waste Basket",
        "Mobile Pedestal", "Shelf", "Conference Table", "Flip Chart", "Pin Board",
        // Audio
        "Speaker", "Microphone", "Headphones", "Soundbar", "Amplifier",
        "Bluetooth Speaker", "Audio Interface", "Mixer"
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
        "John", "Sarah", "Michael", "Emily", "David", "Emma",
        "James", "Olivia", "William", "Sophia", "Robert", "Isabella",
        "Thomas", "Mia", "Daniel", "Charlotte", "Matthew", "Amelia",
        "Christopher", "Harper", "Andrew", "Evelyn", "Joshua", "Abigail",
        "Ryan", "Elizabeth", "Nathan", "Avery", "Kevin", "Ella",
        "Brian", "Grace", "Steven", "Chloe", "Mark", "Victoria"
    ];

    public IReadOnlyList<string> LastNames { get; } =
    [
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia",
        "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
        "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore",
        "Jackson", "Martin", "Lee", "Thompson", "White", "Harris",
        "Clark", "Lewis", "Robinson", "Walker", "Young", "Allen",
        "King", "Wright", "Scott", "Green", "Baker", "Adams"
    ];

    public IReadOnlyList<string> Streets { get; } =
    [
        "Main Street", "Oak Avenue", "Maple Drive", "Cedar Lane", "Pine Road",
        "Elm Street", "Park Avenue", "Lake Drive", "River Road", "Hill Street",
        "Washington Street", "Lincoln Avenue", "Jefferson Drive", "Madison Lane", "Jackson Road",
        "Broadway", "Church Street", "School Street", "Mill Road", "High Street",
        "Center Street", "Market Street", "Spring Street", "Union Street", "Water Street",
        "Cherry Lane", "Willow Way", "Birch Court", "Sunset Boulevard", "Forest Drive",
        "Meadow Lane", "Valley Road", "Mountain View", "Ocean Drive", "Harbor Street"
    ];

    public IReadOnlyList<string> Cities { get; } =
    [
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
        "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
        "Austin", "Jacksonville", "Fort Worth", "Columbus", "Charlotte",
        "Seattle", "Denver", "Boston", "Nashville", "Portland",
        "Las Vegas", "Detroit", "Memphis", "Louisville", "Baltimore",
        "Milwaukee", "Albuquerque", "Tucson", "Fresno", "Sacramento",
        "Atlanta", "Miami", "Oakland", "Minneapolis", "Cleveland"
    ];

    public IReadOnlyList<string> ZipCodes { get; } =
    [
        "10001", "10010", "10019", "10022", "10036",
        "20001", "20005", "20037", "21201", "22030",
        "30301", "30303", "33101", "33139", "33180",
        "40202", "43004", "44101", "48201", "49001",
        "60601", "60614", "63101", "70112", "75201",
        "77001", "80202", "85001", "90001", "90210",
        "94102", "95101", "97201", "98101", "99501"
    ];
}
