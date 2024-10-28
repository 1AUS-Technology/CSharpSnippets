using EcommerceWithDefaultIdentity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceWithDefaultIdentity.Models;

public class StoreModel : PageModel
{
    public StoreModel(ProductDbContext ctx) => DbContext = ctx;
    public ProductDbContext DbContext { get; set; }
}