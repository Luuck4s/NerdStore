using NerdStore.Core.Interfaces;

namespace NerdStore.Core.Data;

public interface IRepository<T>: IDisposable where T: IAggregateRoot 
{
    IUnitOfWork UnitOfWork { get; }
}