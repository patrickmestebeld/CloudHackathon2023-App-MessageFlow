using System.Text.RegularExpressions;
using MessageFlow.SharedKernel.GuardClauses;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static string InvalidFormat(this IGuardClause guardClause,
        string input,
        string parameterName,
        string regexPattern,
        string? message = null)
    {
        var m = Regex.Match(input, regexPattern);
        if (!m.Success || input != m.Value)
        {
            throw new ArgumentException(message ?? $"Input {parameterName} was not in required format", parameterName);
        }

        return input;
    }

    public static T InvalidInput<T>(this IGuardClause guardClause, T input, string parameterName, Func<T, bool> predicate, string? message = null)
    {
        if (!predicate(input))
        {
            throw new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
        }

        return input;
    }


    public static async Task<T> InvalidInputAsync<T>(this IGuardClause guardClause,
        T input,
        string parameterName,
        Func<T, Task<bool>> predicate,
        string? message = null)
    {
        if (!await predicate(input))
        {
            throw new ArgumentException(message ?? $"Input {parameterName} did not satisfy the options", parameterName);
        }

        return input;
    }
}