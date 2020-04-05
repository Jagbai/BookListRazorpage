using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new {data = await _db.Books.ToListAsync() } );
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var bookfromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookfromDb == null)
            {
                return Json(new {success = false, Message = "Error while Deleting"});

            }

            _db.Books.Remove(bookfromDb);
            await _db.SaveChangesAsync();
            return Json(new {success = true, Message = "Sucessfully Deleted"});


        }
    }
}