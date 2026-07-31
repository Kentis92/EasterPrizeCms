using MiniHittegods.Application.Interfaces;

namespace MiniHittegods.Application.Services;

public class FoundItemsService
{
    
    private readonly IFoundItemRepository _repository;

    public FoundItemsService(IFoundItemRepository repository)
    {
        _repository = repository;
        
    }
}