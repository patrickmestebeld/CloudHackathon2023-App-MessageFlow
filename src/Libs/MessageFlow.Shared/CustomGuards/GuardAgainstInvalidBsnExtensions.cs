using System.Runtime.CompilerServices;

namespace MessageFlow.SharedKernel.GuardClauses
{
    public static class GuardAgainstInvalidBsnExtensions
    {
        public static void InvalidBsn(this IGuardClause _, string input, [CallerArgumentExpression("input")] string? parameterName = null, string? message = null)
        {
            if (input.Length != 9)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} is not a valid BSN", parameterName);
            }

            var sum = 0;
            for (var i = 0; i < 8; i++)
            {
                sum += (9 - i) * (input[i] - '0');
            }

            sum %= 11;
            if (sum == 10)
            {
                sum = 0;
            }

            if (sum != input[8] - '0')
            {
                throw new ArgumentException(message ?? $"Input {parameterName} is not a valid BSN", parameterName);
            }
        }
    }
}
