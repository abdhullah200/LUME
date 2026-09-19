using LumeAuth.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace LumeAuth.Controllers
{
    /// <summary>
    /// Controller responsible for the login and sign up pages.
    /// Lives in the "Auth" area so its views and layout stay isolated from the host application's own
    /// <c>_ViewStart</c> and <c>_Layout</c> (the login pages must not render the sidebar and topbar).
    /// </summary>
    [Area("Auth")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <param name="returnUrl">The local URL to return to after logging in. Ignored when it is not a local URL.</param>
        /// <returns>The login view.</returns>
        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            var viewModel = new LoginVM { ReturnUrl = returnUrl };
            SanitizeReturnUrl(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// Handles the submitted login form.
        /// UI only for now: validates the input and shows a preview notice.
        /// </summary>
        /// <param name="model">The view model containing the submitted credentials.</param>
        /// <returns>The login view.</returns>
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM model)
        {
            SanitizeReturnUrl(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Verify the credentials against the identity store, issue the auth cookie,
            // then redirect to model.ReturnUrl (re-check Url.IsLocalUrl) or to Home/Index.
            ViewData["PreviewNotice"] = "Log in is not connected yet. This is a UI preview.";

            return View(model);
        }

        /// <summary>
        /// Displays the sign up page.
        /// </summary>
        /// <returns>The sign up view.</returns>
        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            return View(new SignUpVM());
        }

        /// <summary>
        /// Handles the submitted sign up form.
        /// UI only for now: validates the input and shows a preview notice.
        /// </summary>
        /// <param name="model">The view model containing the details of the new account.</param>
        /// <returns>The sign up view.</returns>
        [HttpPost("signup")]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Reject duplicate emails, hash the password, create the credentials and the
            // matching User row in LumeData, then log the user in.
            ViewData["PreviewNotice"] = "Sign up is not connected yet. This is a UI preview.";

            return View(model);
        }

        /// <summary>
        /// Drops the return URL unless it is a local URL, otherwise it becomes an open redirect.
        /// The value is posted back by the browser, so it is checked on every request, not only on the GET.
        /// </summary>
        /// <param name="model">The login view model whose return URL is checked.</param>
        private void SanitizeReturnUrl(LoginVM model)
        {
            if (!Url.IsLocalUrl(model.ReturnUrl))
            {
                model.ReturnUrl = null;
            }
        }
    }
}
