using MiniHittegods.Application.Services;
using MiniHittegods.Domain.Entities;
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

[Fact]
public async Task Get_item_by_id_should_return_item()
    {
        var repository= new FakeFoundItemRepository();
        var Service = new FoundItemsService(repository);

        var item = new FoundItem(
            "Svart lommebok",
            "Fant ved inngang",
            "Wallet",
            "Inngang A");
        
        await repository.AddAsync(item);

        var result = await Service.GetByIdAsync(item.Id);

        Assert.NotNull(result);
        Assert.Equal("Svart lommebok", result.Title);
    }

}