namespace MessageFlow.Core.Messaging.ValueObjects
{
    public class Adres
    {
        public string Straatnaam { get; } = "";
        public int Huisnummer { get; }
        public Postcode Postcode { get; }
        public string Woonplaats { get; } = "";

        public Adres(Postcode postcode, int huisnummer, string woonplaats = "", string straatnaam = "")
        {
            Postcode = postcode ?? throw new ArgumentNullException(nameof(postcode));
            Huisnummer = huisnummer;
            Woonplaats = woonplaats;
            Straatnaam = straatnaam;
        }

        public Adres(string adres)
        {
            var parts = adres.Split(' ');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Adres moet bestaan uit postcode en huisnummer");
            }
            Postcode = parts[0];
            Huisnummer = int.Parse(parts[1]);
            Woonplaats = "";
            Straatnaam = "";
        }

        public override string? ToString() => $"{Postcode} {Huisnummer}";
        public override bool Equals(object? obj) => obj is Adres adres && adres.Postcode == Postcode && adres.Huisnummer == Huisnummer;
        public override int GetHashCode() => Postcode.GetHashCode() ^ Huisnummer.GetHashCode();

        public static bool operator ==(Adres left, Adres right) => left.Postcode == right.Postcode && left.Huisnummer == right.Huisnummer;
        public static bool operator !=(Adres left, Adres right) => left.Postcode != right.Postcode || left.Huisnummer != right.Huisnummer;

        public static implicit operator string(Adres adres) => adres.ToString()!;
        public static implicit operator Adres(string adres) => new(adres);
    }
}
