namespace EasterPrizeCms.Domain.Entities;

public class Participant
{
    public string Name { get; }
    public int Age { get; }
    public string City { get; }

    public Participant(string name)
    {
        Name = name;
    }

    public Participant(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Participant(string name, int age, string city)
    {
        Name = name;
        Age = age;
        City = city;
    }
}