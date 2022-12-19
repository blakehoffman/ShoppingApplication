using Domain.Models.Cart;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public CartRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Cart cart)
        {
            var cartEntity = CartMapper.MapToCartEntity(cart);
            
            foreach (var cartProduct in cart.Products)
            {
                var cartProductEntity = CartMapper.MapToCartProductEntity(cartEntity.Id, cartProduct);
                cartEntity.Products.Add(cartProductEntity);
            }

            _applicationDbContext.Carts.Add(cartEntity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Cart> Find(Guid id)
        {
            var cartEntity = await _applicationDbContext.Carts
                .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(cart => cart.Id == id);

            return CartMapper.MapToCart(cartEntity);
        }

        public async Task<Cart> FindByUserId(string userId)
        {
            var cartEntity = await _applicationDbContext.Carts
                .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(cart => cart.UserId == new Guid(userId));

            return CartMapper.MapToCart(cartEntity);
        }

        public async Task Update(Cart cart)
        {
            var cartEntity = await _applicationDbContext.Carts
                .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
                .FirstOrDefaultAsync(cart => cart.Id == cart.Id);

            cartEntity.Purchased = cart.Purchased;

            //remove any that were deleted
            foreach (var cartProductEntity in cartEntity.Products.ToList())
            {
                if (!cart.Products.Any(cartProduct => cartProduct.Id == cartProductEntity.ProductId))
                {
                    cartEntity.Products.Remove(cartProductEntity);
                }
            }


            foreach (var cartProduct in cart.Products)
            {
                var cartProductEntity = cartEntity.Products.FirstOrDefault(cpe => cpe.ProductId == cartProduct.Id);

                //add new product to cart
                if (cartProductEntity == null)
                {
                    var newCartProductEntity = CartMapper.MapToCartProductEntity(cart.Id, cartProduct);
                    cartEntity.Products.Add(newCartProductEntity);
                }
                //update product in cart
                else
                {
                    cartProductEntity.Quantity = cartProduct.Quantity;
                }
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
