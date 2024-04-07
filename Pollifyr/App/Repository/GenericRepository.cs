using Microsoft.EntityFrameworkCore;
using MoonCore.Abstractions;
using Pollifyr.App.Database;

namespace Pollifyr.App.Repository;

public class GenericRepository<TEntity> : Repository<TEntity> where TEntity : class
{
    private readonly DatabaseContext DataContext;
    private readonly DbSet<TEntity> DbSet;

    public GenericRepository(DatabaseContext dbContext)
    {
        DataContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = DataContext.Set<TEntity>();
    }

    public override DbSet<TEntity> Get()
    {
        return DbSet;
    }

    public override TEntity Add(TEntity entity)
    {
        var x = DbSet.Add(entity);
        DataContext.SaveChanges();
        return x.Entity;
    }

    public override void Update(TEntity entity)
    {
        DbSet.Update(entity);
        DataContext.SaveChanges();
    }
    
    public override void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
        DataContext.SaveChanges();
    }
}