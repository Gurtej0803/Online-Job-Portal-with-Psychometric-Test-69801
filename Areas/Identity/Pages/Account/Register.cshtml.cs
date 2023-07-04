// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using JobPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace JobPortal.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser> _userStore;
        private readonly Microsoft.AspNetCore.Identity.IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager; 

        public RegisterModel(
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
            Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (Microsoft.AspNetCore.Identity.IUserEmailStore<ApplicationUser>)GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string Firstname { get; set; }

            [Required]
            public string Lastname{ get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public string CompanyName { get; set; }
            public string CompanyLocation { get; set; }
            public string CompanyType { get; set; }
            public string CompanyIndustry { get; set; }
            public DateTime JobSeekerDateofBirth { get; set; }
            public string JobSeekerGender { get; set; }
            public byte[] ProfilePicture { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Required]
            public string Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> Rolelist { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null, ApplicationUser user =null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new InputModel()
            {
                Rolelist = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                CompanyName = user.CompanyName,
                CompanyLocation = user.CompanyLocation,
                CompanyType = user.CompanyType,
                CompanyIndustry = user.CompanyIndustry,
                JobSeekerDateofBirth = (DateTime)user.JobSeekerDateofBirth,
                JobSeekerGender = user.JobSeekerGender,
                ProfilePicture = user.ProfilePicture,
            };
        
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.FirstName = Input.Firstname;
                user.LastName = Input.Lastname;
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, Input.Role);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                var CompanyName = user.CompanyName;
                var CompanyLocation = user.CompanyLocation;
                var CompanyType = user.CompanyType;
                var CompanyIndustry = user.CompanyIndustry;
                 var JobSeekerDateofBirth = user.JobSeekerDateofBirth;
                var JobSeekerGender = user.JobSeekerGender;
                if (Input.CompanyName != null)
                {
                    if (Input.CompanyName != CompanyName)
                    {
                        user.CompanyName = Input.CompanyName;
                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    Input.CompanyName = "Insert Company Name";
                    user.CompanyName = Input.CompanyName;
                    await _userManager.UpdateAsync(user);
                }
                if (Input.CompanyLocation != null)
                {
                    if (Input.CompanyLocation != CompanyLocation)
                    {
                        user.CompanyLocation = Input.CompanyLocation;
                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    Input.CompanyLocation = "Insert Company Location";
                    user.CompanyLocation = Input.CompanyLocation;
                    await _userManager.UpdateAsync(user);
                }
                if (Input.CompanyType != null)
                {
                    if (Input.CompanyType != CompanyType)
                    {
                        user.CompanyType = Input.CompanyType;
                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    Input.CompanyType = "Insert Company Type";
                    user.CompanyType = Input.CompanyType;
                    await _userManager.UpdateAsync(user);
                }
                if (Input.CompanyIndustry != null)
                {
                    if (Input.CompanyIndustry != CompanyIndustry)
                    {
                        user.CompanyIndustry = Input.CompanyIndustry;
                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    Input.CompanyIndustry = "Insert Company Industry";
                    user.CompanyIndustry = Input.CompanyIndustry;
                    await _userManager.UpdateAsync(user);
                }
                if (Input.JobSeekerDateofBirth != JobSeekerDateofBirth)
                {
                    user.JobSeekerDateofBirth = Input.JobSeekerDateofBirth;
                    await _userManager.UpdateAsync(user);
                }
                if (Input.JobSeekerGender != null)
                {
                    if (Input.JobSeekerGender != JobSeekerGender)
                    {
                        user.JobSeekerGender = Input.JobSeekerGender;
                        await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    Input.JobSeekerGender = "--ChooseGender--";
                    user.JobSeekerGender = Input.JobSeekerGender;
                    await _userManager.UpdateAsync(user);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private Microsoft.AspNetCore.Identity.IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (Microsoft.AspNetCore.Identity.IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
