using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicFall2016.Controllers
{
    public class ArtistController : Controller
    {
        private readonly MusicDbContext _db;

        public ArtistController(MusicDbContext context)
        {
            _db = context;
        }

        public IActionResult IndexArtist()
        {
            var artist = _db.Artists.ToList();
            return View(artist);
        }

        public IActionResult CreateArtist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateArtist([Bind("ArtistID, Name, Bio")] Artist artists)
        {
            if (ModelState.IsValid && !_db.Artists.Any(a => a.Name == artists.Name) && !(artists.Name==null))
            {
                _db.Add(artists);
                _db.SaveChanges();
                return RedirectToAction("IndexArtist");
            }

            return View(artists);
        }

        public IActionResult DetailsArtist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var albums = _db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.ArtistID == id);

            if (albums == null)
            {
                return NotFound();
            }
            return View(albums.ToList());
        }

        public IActionResult EditArtist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Artist artist = _db.Artists.FirstOrDefault(i => i.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtist([Bind("ArtistID,Name,Bio")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(artist).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("IndexArtist");
            }
            return View(artist);
        }

    }
}
