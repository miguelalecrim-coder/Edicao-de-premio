

using Domain.Interfaces;

namespace Domain.Models;

public class Tipo : ITipo
{
    public Guid Id { get; private set; }

    public Tipo(Guid id)
    {
        Id = id;
    }

    

    

}