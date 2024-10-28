using EcommerceWithDefaultIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWithDefaultIdentity.Controllers;

// restrict access to authenticated user only
[Authorize]
public class StoreController : Controller
{
    private ProductDbContext DbContext;
    public StoreController(ProductDbContext ctx) => DbContext = ctx;
    public IActionResult Index() => View(DbContext.Products);
}