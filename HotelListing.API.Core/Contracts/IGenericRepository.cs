using HotelListing.API.Core.Models;
using Microsoft.Build.Execution;

namespace HotelListing.API.Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        //new method to allow auto mapper in the Generic Repository
        Task<TResult> GetAsync<TResult>(int? id);
        Task<List<T>> GetAllAsync();
        //new method to allow auto mapper in the Generic Repository
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<QueryPaged<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        //new method to allow auto mapper in the Generic Repository
        Task<TResult> AddAsync<TSource, TResult>(TSource source);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        //new method to allow auto mapper in the Generic Repository
        Task UpdateAsync<TSource>(int id, TSource source);
        Task<bool> Exists(int id);
    }
}
