using Shouldly;

namespace Padel.Domain.Unit;

public class EmailAddressTests
{
    [Fact]
    public void Create_WithValidEmailAddress_ShouldReturnEmailAddressInstance()
    {
        // Arrange
        var validEmail = "test@example.com";

        // Act
        var result = EmailAddress.Create(validEmail);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}
