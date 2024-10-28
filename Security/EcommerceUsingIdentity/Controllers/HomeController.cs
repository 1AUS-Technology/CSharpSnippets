using EcommerceUsingIdentity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUsingIdentity.Controllers;

public class HomeController
{
    private ProductDbContext DbContext;
    public HomeController(ProductDbContext ctx) => DbContext = ctx;
    public IActionResult Index() => View(DbContext.Products);
}