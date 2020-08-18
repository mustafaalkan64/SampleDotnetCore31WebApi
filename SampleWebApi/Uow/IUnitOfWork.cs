using SampleWebApi.Repository;
using SampleWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<OrderItems> OrderItemsRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }

        Task Commit();
    }
}
