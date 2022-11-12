using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IService<T>
    {
        Task CreateAsync(T post, CancellationToken cancellationToken);

        Task<List<T>> GetAsync(CancellationToken cancellationToken);

        Task<T> GetByIdAsync(string postId, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(T postToUpdate, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(string postId, CancellationToken cancellationToken);
    }
}
