using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;


        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Index() => View(new CartOrderViewModel
        {
            Cart = _CartService.TransformFromCart()
        });

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteFromCart(int id)
        {
            _CartService.DeleteFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Checkout(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _CartService.TransformFromCart(),
                    Order = OrderModel
                });

            var createOrderModel = new CreateOrderModel(
                OrderModel, _CartService.TransformFromCart()
                   .Items.Select(
                        i => new OrderItemDTO(
                            i.Product.Id,
                            i.Product.Price,
                            i.Quantity)).ToList());

            var order = await OrderService.CreateOrder(User.Identity!.Name, createOrderModel);
            //var order = await OrderService.CreateOrder(User.Identity!.Name, _CartService.TransformFromCart(), OrderModel);

            _CartService.Clear();

            return RedirectToAction("OrderConfirmed", new {order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebApi

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            _CartService.AddToCart(id);
            return Json(new { id, message = $"Product with id: {id} has been added to cart"});
        }

        public IActionResult DeleteFromCartAPI(int id)
        {
            _CartService.DeleteFromCart(id);
            return Ok();
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _CartService.DecrementFromCart(id);
            return Ok(new { id, message = $"Product quantity with id: {id} has been reduced by 1" });
        }
        public IActionResult ClearAPI()
        {
            _CartService.Clear();
            return Ok();
        }
        #endregion

    }
}
