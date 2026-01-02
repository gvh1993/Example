using System.Globalization;

namespace Padel.Domain.Tests.Unit;

public class EmailAddressTests
{
    [Theory]
    [InlineData("simple@example.com")]
    [InlineData("very.common@example.com")]
    [InlineData("disposable.style.email.with+symbol@example.com")]
    [InlineData("other.email-with-hyphen@example.com")]
    [InlineData("fully-qualified-domain@example.com")]
    [InlineData("user.name+tag+sorting@example.com")]
    [InlineData("x@example.com")]
    [InlineData("example-indeed@strange-example.com")]
    [InlineData("example@s.example")]
    [InlineData("\"john..doe\"@example.org")]
    public void Create_WithValidEmailAddress_ShouldReturnEmailAddressInstance(string validEmail)
    {
        // Act
        var result = EmailAddress.Create(validEmail);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Create_WithUpperCase_ShouldCreateWithLowerCase()
    {
        // Arrange
        const string validEmail = "TeST@eXAmpLe.COM";

        // Act
        var result = EmailAddress.Create(validEmail);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Value.ShouldBe(validEmail.ToLower(new CultureInfo("en-NL")));
    }

    [Fact]
    public void Create_WithEmptyEmailAddress_ShouldReturnError()
    {
        // Arrange
        const string emptyEmail = "";
        // Act
        var result = EmailAddress.Create(emptyEmail);
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(EmailAddress.Invalid);
    }

    [Theory]
    [InlineData("Abc.example.com")] // no @ character
    [InlineData("A@b@c@example.com")] // only one @ is allowed outside quotation marks
    [InlineData("a\"b(c)d,e:f;g<h>i[j\\k]l@example.com")] // none of the special characters in this local-part are allowed outside quotation marks
    [InlineData("just\"not\"right@example.com")] // quoted strings must be dot separated or the only element making up the local-part
    [InlineData("this is\"not\\allowed@example.com")] // spaces, quotes, and backslashes may only exist when within quoted strings and preceded by a backslash
    [InlineData("this\\ still\\\"not\\\\allowed@example.com")] // even if escaped (preceded by a backslash), spaces, quotes, and backslashes must still be contained by quotes
    [InlineData("1234567890123456789012345678901234567890123456789012345678901234+x@example.com")] // local part is longer than 64 characters
    public void Create_WithInvalidEmailAddress_ShouldReturnError(string invalidEmail)
    {
        // Act
        var result = EmailAddress.Create(invalidEmail);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(EmailAddress.Invalid);
    }
}
