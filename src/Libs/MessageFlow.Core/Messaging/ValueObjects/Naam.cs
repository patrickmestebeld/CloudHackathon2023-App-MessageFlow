using Ardalis.GuardClauses;
using MessageFlow.SharedKernel.GuardClauses;

namespace MessageFlow.Core.Messaging.ValueObjects
{
    public class Naam
    {
        public string Voorletters { get; }
        public string Achternaam { get; }
        public string Voornaam { get; }

        public Naam(string voorletters, string achternaam, string voornaam = "")
        {
            Guard.Against.NullOrEmpty(voorletters);
            Guard.Against.NullOrEmpty(achternaam);
            Voorletters = voorletters;
            Achternaam = achternaam;
            Voornaam = voornaam;
        }

        public Naam(string name)
        {
            var parts = name.Split(' ');
            if (parts.Length < 2)
            {
                throw new ArgumentException("Naam moet bestaan uit voorletters en achternaam");
            }
            Voorletters = parts[0];
            Achternaam = parts[^1];
            Voornaam = "";
        }

        public override string? ToString() => $"{Voorletters} {Achternaam}";
        public override bool Equals(object? obj) => obj is Naam naam && naam.Voorletters == Voorletters && naam.Achternaam == Achternaam;
        public override int GetHashCode() => Voorletters.GetHashCode() ^ Achternaam.GetHashCode();

        public static bool operator ==(Naam left, Naam right) => left.Voorletters == right.Voorletters && left.Achternaam == right.Achternaam;
        public static bool operator !=(Naam left, Naam right) => left.Voorletters != right.Voorletters || left.Achternaam != right.Achternaam;

        public static implicit operator string(Naam naam) => naam.ToString()!;
        public static implicit operator Naam(string naam) => new(naam);
    }
}
