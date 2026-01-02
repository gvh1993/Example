using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Padel.Domain;

namespace Padel.Infrastructure.Data.Shared;

internal sealed class PhoneNumberConversion() : ValueConverter<PhoneNumber, string>(
    phoneNumber => phoneNumber.Value,
    value => PhoneNumber.Create(value).Value);
