using Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        public void Add(Cart cart);
        public void FindCart(Guid guid);
    }
}
