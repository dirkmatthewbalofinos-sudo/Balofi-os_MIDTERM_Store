using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Balofinos_Midterm_Store.Data;
using Balofinos_Midterm_Store.Models;

namespace Balofinos_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.CartItems.ToListAsync();
            
            ViewBag.GrandTotal = cartItems.Sum(item => item.Price * item.Quantity);
            return View(cartItems);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                _context.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"{product.Name} was added to your cart!";
            return RedirectToAction("Index", "Products");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                    _context.Update(cartItem);
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cart updated successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Item removed from cart.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}