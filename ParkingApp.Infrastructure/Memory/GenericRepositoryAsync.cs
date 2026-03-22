using ParkingApp.Core.Common;
using ParkingApp.Core.Interfaces;

namespace ParkingApp.Infrastructure.Memory;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : EntityBase
{
    protected readonly Dictionary<Guid, T> _data = new();

    public Task<T> AddAsync(T entity)
    {
        if(entity is null) throw new ArgumentNullException(nameof(entity));

        if(entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();

        if (_data.ContainsKey(entity.Id))
            throw new InvalidOperationException("obiekt o tym id już istnieje");

        _data.Add(entity.Id, entity);

        return Task.FromResult(entity);
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        IEnumerable<T> items = _data.Values.ToList();
        return Task.FromResult(items);
    }

    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        if (page <= 0)
            throw new ArgumentException("Page must be greater than zero.", nameof(page));

        if (pageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

        var items = _data.Values.ToList();
        var count = items.Count;
        var pagedItems = items
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(new PagedResult<T>(pagedItems, count, page, pageSize));
    }

    public Task RemoveByIdAsync(Guid id)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id nie może być puste");

        if (!_data.Remove(id))
            throw new KeyNotFoundException("Nie ma encji o tym id");

        return Task.CompletedTask;
    }

    public Task<T> UpdateAsync(T entity)
    {
     
   
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (entity.Id == Guid.Empty)
        {
            throw new ArgumentException("Id nie może być puste", nameof(entity));
        }

        if (!_data.ContainsKey(entity.Id))
        {
            throw new KeyNotFoundException("Nie ma encji o tym id");
        }
       
        _data[entity.Id] = entity;

        return Task.FromResult(entity);
  


    }
}
