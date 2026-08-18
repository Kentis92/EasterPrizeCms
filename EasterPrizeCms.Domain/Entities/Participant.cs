using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    public string FullName { get; private set; } = string.Empty;
    public string Name => FullName;
    public int Age { get; private set; }
    public string City { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    public Participant()
    {
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public Participant(string name)
    {
        ValidateName(name);

        FullName = name;
        City = string.Empty;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public Participant(string name, int age)
    {
        ValidateName(name);
        ValidateAge(age);

        FullName = name;
        Age = age;
        City = string.Empty;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public Participant(string name, int age, string city)
    {
        ValidateName(name);
        ValidateAge(age);
        ValidateCity(city);

        FullName = name;
        Age = age;
        City = city;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public void Update(string fullName, int age, string city)
    {
        ValidateName(fullName);
        ValidateAge(age);
        ValidateCity(city);

        FullName = fullName;
        Age = age;
        City = city;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public bool CanDelete(IEnumerable<Prize> prizes)
    {
        return !prizes.Any(prize =>
            prize.ParticipantId == Id && prize.Status == PrizeStatus.Assigned
        );
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Participant name cannot be empty.", nameof(name));

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException(
                "Participant name must be between 2 and 80 characters.",
                nameof(name)
            );
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
