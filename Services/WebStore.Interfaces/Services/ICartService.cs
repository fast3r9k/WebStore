using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void AddToCart(int id);

        void DecrementFromCart(int id);

        void DeleteFromCart(int id);

        void Clear();

        CartViewModel TransformFromCart();
    }
}
