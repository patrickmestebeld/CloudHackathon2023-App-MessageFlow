using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.ValueObjects;

namespace MessageFlow.Infra.Messaging.Services
{
    internal class PersonalDataFetcher : IPersonalDataFetcher
    {
        // Todo: needs fetch information from MessageOracle later.
        public PersonalData GetPersoonsGegevens(Guid burgerKey)
            => new PersonalData(new Bsn("123456782"),
                            new Naam("H.", "de Vries", "Henk"),
                            new Adres("1234AB", 1, "AMSTERDAM", "Kerkstraat"));
    }
}
