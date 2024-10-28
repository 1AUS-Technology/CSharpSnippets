using EcommerceWithDefaultIdentity.Data;
using EcommerceWithDefaultIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWithDefaultIdentity.Controllers;

// restrict Administrators only
[Authorize(Roles = "Admin")]
public class AdminController(ProductDbContext ctx) : Controller
{
    public IActionResult Index() => View(ctx.Products);
    [HttpGet]
    public IActionResult Create() => View("Edit", new Product());
    [HttpGet]
    public IActionResult Edit(long id)
    {
        Product? p = ctx.Find<Product>(id);
        if (p != null)
        {
            return View("Edit", p);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Save(Product p)
    {
        ctx.Update(p);
        ctx.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(long id)
    {
        Product? p = ctx.Find<Product>(id);
        if (p != null)
        {
            ctx.Remove(p);
            ctx.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}