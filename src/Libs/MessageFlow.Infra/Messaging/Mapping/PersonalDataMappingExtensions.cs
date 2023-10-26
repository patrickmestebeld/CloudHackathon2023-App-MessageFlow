using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.ValueObjects;
using MessageOracle;

namespace MessageFlow.Infra.Messaging.Mapping;

public static class PersonalDataMappingExtensions
{
    public static PersonalData ToPersonalData(this PersonalDataDto dto)
    {
        var naam = new Naam(dto.Naam.Voorletters, dto.Naam.Achternaam, dto.Naam.Voornaam);
        var adres = new Adres(dto.Adres.Postcode, dto.Adres.Huisnummer, dto.Adres.Woonplaats, dto.Adres.Straatnaam);
        return new PersonalData(dto.Key, dto.Bsn, naam, adres);
    }
}