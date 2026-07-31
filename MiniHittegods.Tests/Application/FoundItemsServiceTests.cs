using MiniHittegods.Domain.Entities;
using MiniHittegods.Application.Services;
using MiniHittegods.Tests.Fakes;

namespace MiniHittegods.Tests.Application;

public class FoundItemsServiceTests
{
    [Fact]
    public void Service_can_be_created()
    {
        var repository = new FakeFoundItemRepository();

        var Services = new FoundItemsService(repository);

        Assert.NotNull(Services);
    }

[Fact]
public async Task Create_item_should_add_item_to_repository()
    {
        var repository = new FakeFoundItemRepository();
        var Service = new FoundItemsService(repository);

        var item = new FoundItem(
            "Blå jakke",
            "Norge skrevet på ryggen",
            "Clothing",
            "Scene 2");
           
         await Service.CreateAsync(item);

        var items = await repository.GetAllAsync();

        Assert.Single(items);
        
    }


}