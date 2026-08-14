using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Domain.Entities;

public class Participant
{
    public string Name { get; }
    public int Age { get; }
    public string City { get; }

    public Participant(string name)
    {
        ValidateName(name);

        Name = name;
        City = string.Empty;
    }

    public Participant(string name, int age)
    {
        ValidateName(name);
        ValidateAge(age);

        Name = name;
        Age = age;
        City = string.Empty;
    }

    public Participant(string name, int age, string city)
    {
        ValidateName(name);
        ValidateAge(age);
        ValidateCity(city);

        Name = name;
        Age = age;
        City = city;
    }

    public bool CanDelete(IEnumerable<Prize> prizes)
    {
        return !prizes.Any(prize => prize.Status == PrizeStatus.Assigned);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Participant name cannot be empty.", nameof(name));

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException("Participant name must be between 2 and 80 characters.", nameof(name));
    }

    private static void ValidateAge(int age)
    {
        if (age < 0 || age > 120)
            throw new ArgumentException("Age must be between 0 and 120.", nameof(age));
    }

    private static void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty.", nameof(city));

        if (city.Length < 2 || city.Length > 80)
            throw new ArgumentException("City must be between 2 and 80 characters.", nameof(city));
    }
}