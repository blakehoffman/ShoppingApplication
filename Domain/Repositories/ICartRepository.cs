using Domain.Models.Cart;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        public Task Add(Cart cart);
        public Task<Cart> Find(Guid id);
        public Task<Cart> FindByUserId(string userId);
        public Task Update(Cart cart);
    }
}
