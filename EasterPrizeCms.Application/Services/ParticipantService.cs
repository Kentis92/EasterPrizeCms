using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Services;

public class ParticipantService
{
    public Participant Create(string name, int age, string city)
    {
        if (age < 0 || age > 120)
            throw new ArgumentException("Age must be between 0 and 120.");

        if (name.Length < 2)
            throw new ArgumentException("Name must be between 2 and 80 characters.");

        return new Participant(name, age, city);
    }
}