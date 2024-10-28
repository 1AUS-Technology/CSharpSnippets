using EcommerceWithDefaultIdentity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceWithDefaultIdentity.Models;

public class LandingModel(ProductDbContext ctx) : PageModel
{
    public ProductDbContext DbContext { get; set; } = ctx;
}