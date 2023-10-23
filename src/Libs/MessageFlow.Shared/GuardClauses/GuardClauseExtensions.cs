using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using MessageFlow.SharedKernel.GuardClauses;

namespace Ardalis.GuardClauses;


public static partial class GuardClauseExtensions
{

    public static T Null<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName);
            }
            throw new ArgumentNullException(parameterName, message);
        }

        return input;
    }

    public static T Null<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null) where T : struct
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName);
            }
            throw new ArgumentNullException(parameterName, message);
        }

        return input.Value;
    }

    public static string NullOrEmpty(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)

    {
        Guard.Against.Null(input, parameterName, message);
        if (input == string.Empty)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    public static Guid NullOrEmpty(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] Guid? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);
        if (input == Guid.Empty)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input.Value;
    }

    public static IEnumerable<T> NullOrEmpty<T>(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] IEnumerable<T>? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);
        if (!input.Any())
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    public static string NullOrWhiteSpace(this IGuardClause guardClause,
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        Guard.Against.NullOrEmpty(input, parameterName, message);
        if (String.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }



    public static T Default<T>(this IGuardClause guardClause,
        [AllowNull, NotNull] T input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
        {
            throw new ArgumentException(message ?? $"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
        }

        return input;
    }

    public static T NullOrInvalidInput<T>(this IGuardClause guardClause,
        [NotNull] T? input,
        string parameterName,
        Func<T, bool> predicate,
        string? message = null)
    {
        Guard.Against.Null(input, parameterName, message);

        return Guard.Against.InvalidInput(input, parameterName, predicate, message);
    }
}