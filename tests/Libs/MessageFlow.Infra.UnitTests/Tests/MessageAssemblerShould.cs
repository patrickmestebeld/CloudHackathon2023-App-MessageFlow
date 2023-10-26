using MessageFlow.Api.Models;
using MessageFlow.Core.Messaging.Services;
using MessageFlow.Infra.Messaging.Services;
using MessageFlow.UnitTests.Fakes;
using Shouldly;

namespace MessageFlow.UnitTests.Tests;

[TestClass]
public class MessageAssemblerShould
{
    private MessageAssembler _sut = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _sut = new MessageAssembler(new FakePersoonsgegevensFetcher(), new ScribanMessageRenderer());
    }

    [TestMethod]
    public async Task AssembleWhenCorrectVraagbriefTriggerIsGiven()
    {
        var messageTrigger = new MessageTrigger()
        {
            AanvragerKey = Guid.NewGuid(),
            KgbVariant = false,
            Reactiedatum = "22 november 2023",
            DatumDagtekening = "8 april 2023",
            Toeslagjaar = "2022",
            BerichtType = "Vraagbrief_Inkomen"
        };

        var actualMessage = await _sut.AssembleAsync(messageTrigger);

        actualMessage.Recipient.ShouldBe("H. de Vries");
        actualMessage.Subject.ShouldBe("We hebben uw inkomen nodig voor uw toeslag");
        ShouldBeEqualLineForLine(actualMessage.Content, TestMessageTemplatesResources.Expected_Vraagbrief_Inkomen);
    }

    [TestMethod]
    public async Task AssembleWhenCorrectHerinneringsbriefTriggerIsGiven()
    {
        var messageTrigger = new MessageTrigger()
        {
            AanvragerKey = Guid.NewGuid(),
            KgbVariant = true,
            Reactiedatum = "22 november 2023",
            DatumDagtekening = "19 oktober 2023",
            DatumVraagbrief = "8 april 2023",
            Toeslagjaar = "2022",
            BerichtType = "Herinneringsbrief_Inkomen"
        };

        var actualMessage = await _sut.AssembleAsync(messageTrigger);

        actualMessage.Recipient.ShouldBe("H. de Vries");
        actualMessage.Subject.ShouldBe("We denken dat u kindgebonden budget kunt krijgen");
        ShouldBeEqualLineForLine(actualMessage.Content, TestMessageTemplatesResources.Expected_Herinneringsbrief_Inkomen);
    }

    private static void ShouldBeEqualLineForLine(string actualText, string expectedText)
    {
        var actualLines = actualText.Split(Environment.NewLine);
        var expectedLine = expectedText.Split(Environment.NewLine);
        for (int i = 0; i < Math.Min(actualLines.Length, expectedLine.Length); i++)
        {
            actualLines[i].ShouldBe(expectedLine[i]);
        }

    }
}