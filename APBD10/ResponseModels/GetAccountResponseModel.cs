using APBD10.DTOs;
using APBD10.Models;

namespace APBD10.ResponseModels;

public class GetAccountResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public List<SingleShoppingCartItem> ShoppingCarts { get; set; }
}