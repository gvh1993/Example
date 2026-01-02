using System;
using System.Collections.Generic;
using System.Text;

namespace Padel.Domain.Player;

public sealed class Player
{
    public Guid Id { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public EmailAddress Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public Player(
        Guid id,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        EmailAddress email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
