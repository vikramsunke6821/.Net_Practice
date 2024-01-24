﻿using CrudApp.Data;
using CrudApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly CrudApp.Data.CrudAppContext _context;

        public IndexModel(CrudApp.Data.CrudAppContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
