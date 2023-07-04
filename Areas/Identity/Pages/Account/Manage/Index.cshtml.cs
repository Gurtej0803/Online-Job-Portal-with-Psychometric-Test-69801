// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JobPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobPortal.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            
            public string FirstName { get; set; }
            public string LastName { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string CompanyName { get; set; }  
            public string CompanyLocation { get; set; } 
            public string CompanyType { get; set; }
            public string CompanyIndustry { get; set; } 
            public DateTime JobSeekerDateofBirth { get; set; } 
            public string JobSeekerGender { get; set; } 
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                CompanyName = user.CompanyName,  
                CompanyLocation = user.CompanyLocation, 
                CompanyType = user.CompanyType, 
                CompanyIndustry = user.CompanyIndustry, 
                FirstName = user.FirstName,
                LastName = user.LastName,
                JobSeekerDateofBirth = (DateTime)user.JobSeekerDateofBirth,
                JobSeekerGender = user.JobSeekerGender,
                ProfilePicture = user.ProfilePicture,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var CompanyName = user.CompanyName;
            var CompanyLocation = user.CompanyLocation;
            var CompanyType = user.CompanyType;
            var CompanyIndustry = user.CompanyIndustry;
            var FirstName = user.FirstName;
            var LastName = user.LastName;
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
            if (Input.FirstName != null)
            {
                if (Input.FirstName != FirstName)
                {
                    user.FirstName = Input.FirstName;
                    await _userManager.UpdateAsync(user);
                }
            }
            else
            {
                Input.FirstName = "Insert First Name";
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName != null)
            {
                if (Input.LastName != LastName)
                {
                    user.LastName = Input.LastName;
                    await _userManager.UpdateAsync(user);
                }
            }
            else
            {
                Input.LastName = "Insert Last Name";
                user.LastName = Input.LastName;
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

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if(Request.Form.Files.Count > 0)
            {
                var files = Request.Form.Files.FirstOrDefault();
                //check file size and extension
                using (var dataStream = new MemoryStream())
                {
                    await files.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
