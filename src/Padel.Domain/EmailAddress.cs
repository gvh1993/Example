using Padel.Domain.Shared;

namespace Padel.Domain;

public sealed partial record EmailAddress
{
    public string Value { get; init; }

    private EmailAddress(string value)
    {
        Value = value.ToLower(System.Globalization.CultureInfo.CurrentCulture);
    }

    public static Result<EmailAddress> Create(string value)
    {
        if (!ValidEmailAddressRegex().IsMatch(value))
        {
            return Result.Failure<EmailAddress>(Invalid);
        }

        return new EmailAddress(value);
    }

    // RFC 5322 Official Standard
    [System.Text.RegularExpressions.GeneratedRegex("^((?:[A-Za-z0-9!#$%&'*+\\-\\/=?^_`{|}~]|(?<=^|\\.)\"|\"(?=$|\\.|@)|(?<=\".*)[ .](?=.*\")|(?<!\\.)\\.){1,64})(@)((?:[A-Za-z0-9.\\-])*(?:[A-Za-z0-9])\\.(?:[A-Za-z0-9]){2,})$")]
    private static partial System.Text.RegularExpressions.Regex ValidEmailAddressRegex();

    public static Error Invalid = new Error($"{nameof(EmailAddress)}.{nameof(Invalid)}", "Invalid email address format.", ErrorType.Validation);
}

