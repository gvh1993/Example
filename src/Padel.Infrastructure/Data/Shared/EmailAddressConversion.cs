using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Padel.Domain;

namespace Padel.Infrastructure.Data.Shared;

internal sealed class EmailAddressConversion() : ValueConverter<EmailAddress, string>(
    emailAddress => emailAddress.Value,
    value => EmailAddress.Create(value).Value);
