using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Core.Messaging.Models;
using MessageFlow.SharedKernel.GuardClauses;
using Scriban;

namespace MessageFlow.Infra.Messaging.Services
{
    public class ScribanMessageRenderer : IMessageRenderer
    {
        public Message Render(RenderableMessage renderable)
        {

            Guard.Against.Null(renderable.TemplateData.Aanvrager);
            var resourceKeyValues = MessageTemplatesResources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);
            ValidateTemplateName(resourceKeyValues, renderable.TemplateName);

            var temp = MessageTemplatesResources.ResourceManager.GetString(renderable.TemplateName);
            var template = Template.ParseLiquid(temp);
            var result = template.Render(renderable.TemplateData);
            var recipient = renderable.TemplateData.Aanvrager!.Naam.ToString()!;

            if (TryExtractSubject(result, out var subject))
            {
                return new Message(recipient, subject, result);
            }

            return new Message(recipient, "", result);
        }

        private static void ValidateTemplateName(IEnumerable? entries, string key)
        {
            if (entries is null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            bool isTemplateNameValid = false;
            foreach (DictionaryEntry resourceEntry in entries!)
            {
                if (resourceEntry.Key.Equals(key))
                {
                    isTemplateNameValid = true;
                }
            }

            if (!isTemplateNameValid)
            {
                throw new ArgumentException($"Template name {key} is not valid");
            }
        }

        private static bool TryExtractSubject(string result, [NotNullWhen(true)] out string? subject)
        {
            subject = null;
            var regex = new Regex(@"(?<=[Oo]nderwerp:)(.*)(?=\n)");
            var match = regex.Match(result);

            if (match.Success)
            {
                subject = match.Value.Trim();
                return true;
            }

            return false;
        }

    }
}
