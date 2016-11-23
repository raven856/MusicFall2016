using System;
using System.Collections.Generic;
using System.Linq;
using MusicFall2016.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class GenreController : Controller
    {
        private readonly MusicDbContext _db;

        public GenreController(MusicDbContext context)
        {
           _db = context;
        }
        // GET: /<controller>/
        public IActionResult IndexGenre()
        {
            var genre = _db.Genres.ToList();
            return View(genre);
        }

        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult CreateGenre(Genre genre)
        {
            if (ModelState.IsValid && (genre.Name != "") && genre.Name != null)
            {
                if (_db.Genres.Any(a => a.Name == genre.Name))
                 {
                    return View(genre);
                }
                else
                {
                    _db.Add(genre);
                    _db.SaveChanges();
                    return RedirectToAction("IndexGenre");
                }
                
            }

            return View(genre);
        }

    }
}
