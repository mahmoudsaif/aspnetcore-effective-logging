﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookClub.Logic.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BookClub.Infrastructure;
using BookClub.Infrastructure.BaseClasses;

namespace BookClub.UI.Pages
{
    public class BookListBadModel : BasePageModel
    {
        private readonly ILogger _logger;        
        public List<BookModel> Books;

        public BookListBadModel(ILogger<BookListModel> logger, IScopeInformation scope) : base(logger, scope)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {            
            using (var http = new HttpClient(new StandardHttpMessageHandler(HttpContext, _logger)))
            {
                var response = await http.GetAsync("https://localhost:44322/api/Book?throwException=true");
                Books = JsonConvert.DeserializeObject<List<BookModel>>(
                    await response.Content.ReadAsStringAsync()).OrderByDescending(a=> a.Id).ToList();                
            }
        }       
    }
}