using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoAppUsingIdentity.Data;

namespace TodoAppUsingIdentity.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleteItems { get; set; }

        public bool HasDoneAllItems => TodoItems.All(x => x.Complete);
        public IEnumerable<TodoItem> TodoItems { get; set; }

        public void OnGet()
        {
            TodoItems = _context.TodoItems.Where(x => (!ShowCompleteItems || x.Complete) && x.Owner == User.Identity.Name).ToList();
        }

        public IActionResult OnPostShowCompleteItems()
        {
            return RedirectToPage(new { ShowCompleteItems });
        }

        public async Task<IActionResult> OnPostAddItemAsync(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                TodoItem item = new TodoItem
                {
                    Task = task,
                    Owner = User.Identity.Name,
                    Complete = false
                };
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { ShowCompleteItems });
        }

        public async Task<IActionResult> OnPostMarkItemAsync(long id)
        {
            TodoItem item = _context.TodoItems.Find(id);
            if (item != null)
            {
                item.Complete = !item.Complete;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { ShowCompleteItems });
        }


    }
}
