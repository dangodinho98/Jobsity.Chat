namespace Jobsity.Chat.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public List<SelectListItem>? Users { get; set; }

        [BindProperty]
        public string? LoggedUser { get; set; }

        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public void OnGet()
        {
            Users = _userManager.Users
                .Select(a => new SelectListItem { Text = a.UserName, Value = a.UserName })
                .OrderBy(s => s.Text)
                .ToList();

            LoggedUser = User.Identity?.Name ?? string.Empty;
        }
    }
}