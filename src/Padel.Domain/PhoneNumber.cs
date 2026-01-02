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
            || !ValidPhoneNumberRegex().IsMatch(value))
        {
            return Result.Failure<PhoneNumber>(Invalid);
        }

        return new PhoneNumber(value);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^\+?[0-9]*$")]
    private static partial System.Text.RegularExpressions.Regex ValidPhoneNumberRegex();

    public static Error Invalid = new Error($"{nameof(PhoneNumber)}.{nameof(Invalid)}", "Invalid phone number format.", ErrorType.Validation);
}
