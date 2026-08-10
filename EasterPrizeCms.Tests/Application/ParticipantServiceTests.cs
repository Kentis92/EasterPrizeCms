using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Services;

public class ParticipantService
{
    public Participant Create(string name, int age, string city)
    {
        return new Participant(name, age, city);
    }
}