﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ExceptionsManager;

public static class Thrower
{
    [DoesNotReturn]
    public static void InvalidOpEx(string message = "") => throw new InvalidOperationException(message);

    [DoesNotReturn]
    public static void AssertationFail(string errorMessage = "") => InvalidOpEx($"Assertion failed: {errorMessage}");

    [DoesNotReturn]
    public static T InvalidOpEx<T>(string? message = "") => throw new InvalidOperationException(message);

    public static void AssertAlways(
        [DoesNotReturnIf(false)] bool cond,
        string errorMessage = "",
        [CallerArgumentExpression(nameof(cond))]
        string expression = ""
    )
    {
        if (!cond) AssertationFail(errorMessage == "" ? expression : errorMessage);
    }

    public static void NotImplementedException(string msg = "") => throw new NotImplementedException(msg);

    [DoesNotReturn]
    public static T NullException<T>(string message = "") => throw new NullReferenceException(message);

    [DoesNotReturn]
    public static void Unreachable(string? message = null) =>
        throw new InvalidOperationException(message ?? "Current point is unreachable");
}