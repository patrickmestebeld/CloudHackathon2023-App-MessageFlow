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
        var messageTrigger = new MessageContext()
        {
            AanvragerKey = Guid.NewGuid(),
            KgbVariant = false,
            Reactiedatum = new DateTime(2023, 11, 22),
            DatumDagtekening = new DateTime(2023, 04, 12),
            Toeslagjaar = 2022,
            BerichtType = "vraagbrief"
        };

        var actualMessage = await _sut.AssembleAsync(messageTrigger);

        actualMessage.RecipientId.ShouldBe(messageTrigger.AanvragerKey);
        actualMessage.Recipient.ShouldBe("H. de Vries");
        actualMessage.MessageType.ShouldBe(messageTrigger.BerichtType);
        actualMessage.Subject.ShouldBe("We hebben uw inkomen nodig voor uw toeslag");
        ShouldBeEqualLineForLine(actualMessage.Content, TestMessageTemplatesResources.expected_vraagbrief);
    }

    [TestMethod]
    public async Task AssembleWhenCorrectHerinneringsbriefTriggerIsGiven()
    {
        var messageTrigger = new MessageContext()
        {
            AanvragerKey = Guid.NewGuid(),
            KgbVariant = true,
            Reactiedatum = new DateTime(2023, 11, 22),
            DatumDagtekening = new DateTime(2023, 10, 24),
            Toeslagjaar = 2022,
            BerichtType = "rappelbrief"
        };

        var actualMessage = await _sut.AssembleAsync(messageTrigger);

        actualMessage.RecipientId.ShouldBe(messageTrigger.AanvragerKey);
        actualMessage.Recipient.ShouldBe("H. de Vries");
        actualMessage.MessageType.ShouldBe(messageTrigger.BerichtType);
        actualMessage.Subject.ShouldBe("We denken dat u kindgebonden budget kunt krijgen");
        ShouldBeEqualLineForLine(actualMessage.Content, TestMessageTemplatesResources.expected_rappelbrief);
    }

    [TestMethod]
    public async Task AssembleWhenCorrectBeschikkingTriggerIsGiven()
    {
        var messageTrigger = new MessageContext()
        {
            AanvragerKey = Guid.NewGuid(),
            KgbVariant = true,
            Reactiedatum = new DateTime(2023, 11, 22),
            DatumDagtekening = new DateTime(2023, 11, 24),
            Toeslagjaar = 2022,
            BerichtType = "beschikking"
        };

        var actualMessage = await _sut.AssembleAsync(messageTrigger);

        actualMessage.RecipientId.ShouldBe(messageTrigger.AanvragerKey);
        actualMessage.Recipient.ShouldBe("H. de Vries");
        actualMessage.MessageType.ShouldBe(messageTrigger.BerichtType);
        actualMessage.Subject.ShouldBe("U krijgt kindgebonden budget");
        ShouldBeEqualLineForLine(actualMessage.Content, TestMessageTemplatesResources.expected_beschikking);
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