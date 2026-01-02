using System;
using System.Collections.Generic;
using System.Text;
using Padel.Domain.Shared;

namespace Padel.Domain;

public sealed partial class EmailAddress
{
    public string Value { get; init; }

    private EmailAddress(string value)
    {
        Value = value.ToLower(System.Globalization.CultureInfo.CurrentCulture);
    }

    public static Result<EmailAddress> Create(string value)
    {       
        var regex = EmailAddressRegex();

        if (!regex.IsMatch(value))
        {
            return Result.Failure<EmailAddress>(new Error($"{nameof(EmailAddress)}.Invalid", "Invalid email address format.", ErrorType.Validation));
        }
            
        return new EmailAddress(value);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", System.Text.RegularExpressions.RegexOptions.IgnoreCase, "en-NL")]
    private static partial System.Text.RegularExpressions.Regex EmailAddressRegex();
}

