using Domain.Models.Cart;
using System;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        public void Add(Cart cart);
        public Cart Find(Guid id);
        public Cart FindByUserId(string userId);
        public void Update(Cart cart);
    }
}
