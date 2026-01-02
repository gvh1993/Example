using System;
using System.Collections.Generic;
using System.Text;
using Padel.Domain.Shared;

namespace Padel.Domain;

public sealed partial class PhoneNumber
{
    public string Value { get; init; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) 
            || value.Length < 7 
            || value.Length > 15 
            || !IsValidPHoneNumber().IsMatch(value))
        {
            return Result.Failure<PhoneNumber>(new Error($"{nameof(PhoneNumber)}.Invalid", "Invalid phone number format.", ErrorType.Validation));
        }

        return new PhoneNumber(value);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^\+?[0-9]*$")]
    private static partial System.Text.RegularExpressions.Regex IsValidPHoneNumber();
}
