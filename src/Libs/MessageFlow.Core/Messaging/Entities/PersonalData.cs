using MessageFlow.Core.Messaging.ValueObjects;

namespace MessageFlow.Core.Messaging.Entities
{
    public class PersonalData
    {
        public string Key => Bsn.ToString(); // Do something with pseudonymization here later
        public Bsn Bsn { get; private set; }
        public Adres Adres { get; private set; }
        public Naam Naam { get; private set; }

        public PersonalData(Bsn bsn, Naam naam, Adres adres)
        {
            Bsn = bsn ?? throw new ArgumentNullException(nameof(bsn));
            Naam = naam ?? throw new ArgumentNullException(nameof(naam));
            Adres = adres ?? throw new ArgumentNullException(nameof(adres));
        }

        public override string ToString() => $"{Naam} ({Bsn})"; // Do something with pseudonymization here later
        public override bool Equals(object? obj) => obj is PersonalData persoonsGegevens && persoonsGegevens.Bsn == Bsn;
        public override int GetHashCode() => Bsn.GetHashCode();

        public static implicit operator Bsn(PersonalData persoonsGegevens) => persoonsGegevens.Bsn;
        public static implicit operator Adres(PersonalData persoonsGegevens) => persoonsGegevens.Adres;
        public static implicit operator Naam(PersonalData persoonsGegevens) => persoonsGegevens.Naam;

        public static bool operator ==(PersonalData left, PersonalData right) => left.Bsn == right.Bsn;
        public static bool operator !=(PersonalData left, PersonalData right) => left.Bsn != right.Bsn;
    }
}
