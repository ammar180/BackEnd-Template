using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SharedKernel;

namespace IntegrationTests.Data;
public abstract class BaseEfRepoTestFixture : IDisposable
{
  protected ApplicationDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();

    _dbContext = new ApplicationDbContext(options);
  }

  protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
  {
    // Create a fresh service provider, and therefore a fresh
    // InMemory database instance.
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

    // Create a new options instance telling the context to use an
    // InMemory database and the new service provider.
    var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    builder.UseInMemoryDatabase("cleanarchitecture")
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  public void Dispose()
  {
    _dbContext?.Dispose();
    GC.SuppressFinalize(this);
  }

  protected EfRepository<T> GetRepository<T>() where T : class
  {
    return new EfRepository<T>(_dbContext);
  }
}
