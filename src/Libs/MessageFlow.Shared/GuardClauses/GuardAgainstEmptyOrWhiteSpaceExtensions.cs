using MessageFlow.SharedKernel.GuardClauses;

namespace Ardalis.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static ReadOnlySpan<char> Empty(this IGuardClause guardClause,
        ReadOnlySpan<char> input,
        string parameterName,
        string? message = null)
    {
        if (input.Length == 0 || input == string.Empty)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }
        return input;
    }

    public static ReadOnlySpan<char> WhiteSpace(this IGuardClause guardClause,
        ReadOnlySpan<char> input,
        string parameterName,
        string? message = null)
    {
        if (MemoryExtensions.IsWhiteSpace(input))
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }
}