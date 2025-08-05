using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ExceptionsManager;

public static class ThrowerExtensions
{
    [return: NotNull]
    public static T ThrowIfNull<T>(
        [NotNull] this T? value,
        string? message = null,
        [CallerArgumentExpression(nameof(value))]
        string exprStr = ""
    ) => value ?? Thrower.NullException<T>(message ?? $"({exprStr}) was null");
}