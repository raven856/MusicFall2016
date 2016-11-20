using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System;

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _db;

        public AlbumsController(MusicDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> IndexAlbum(string sortOrder, string searchString)
        {

            ViewData["ArtistSortParm"] = sortOrder == "Artist" ? "artist_desc" : "Artist";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["LikeSortParm"] = sortOrder == "Likes" ? "likes_desc" : "Likes";
            var albums = from s in _db.Albums.Include(a => a.Artist).Include(a => a.Genre)
                         select s;
            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
                albums = from s in _db.Albums.Include(a => a.Artist).Include(a => a.Genre)
                         .Where(s => s.Artist.Name.Contains(searchString) || s.Genre.Name.Contains(searchString))
                         select s;

            }
            //end search
            switch (sortOrder)
            {
                case "artist_desc":
                    albums = albums.OrderByDescending(s => s.Artist.Name);
                    break;
                case "Genre":
                    albums = albums.OrderBy(s => s.Genre.Name);
                    break;
                case "genre_desc":
                    albums = albums.OrderByDescending(s => s.Genre.Name);
                    break;
                case "Price":
                    albums = albums.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(s => s.Price);
                    break;
                case "Likes":
                    albums = albums.OrderBy(s => s.Likes);
                    break;
                case "likes_desc":
                    albums = albums.OrderByDescending(s => s.Likes);
                    break;
                default:
                    albums = albums.OrderBy(s => s.Artist.Name);
                    break; 
            }
            // return View(await albums.AsNoTracking().ToList());
            return View(albums.ToList());
        }        

        public IActionResult DetailsAlbum(int? id)
        {
            
            var details = _db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(i => i.AlbumID == id).Single();
            ViewBag.Suggestions = _db.Albums.Where(a => (a.GenreID == details.GenreID) || (a.ArtistID == details.AlbumID)).ToList().Take(3);
    
            return View(details);
        }

        // GET: Albums/CreateC:\Users\Ralph\Documents\Visual Studio 2015\MusicFall2016-master\src\MusicFall2016\Views\Albums\CreateAlbum.cshtml
        public IActionResult CreateAlbum()
        {
            ViewBag.ArtistList = new SelectList(_db.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_db.Genres, "GenreID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
                return RedirectToAction("IndexAlbum");
            }
            if (!string.IsNullOrEmpty(album.Artist.Name))
            {
                Artist aArtist = new Artist();
                aArtist.Name = album.Artist.Name;
                album.ArtistID = aArtist.ArtistID;
            }
            if (!string.IsNullOrEmpty(album.Genre.Name))
            {
               Genre aGenre = new Genre();
                aGenre.Name = album.Genre.Name;
                album.GenreID = aGenre.GenreID;
            }
            return View(album);
        }
        public IActionResult EditAlbum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = _db.Albums.Include(a => a.Artist).Include(a => a.Genre).FirstOrDefault(i => i.AlbumID == id);
            if (album == null)
            {
               return NotFound();
            }
            ViewBag.ArtistList = new SelectList(_db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreList = new SelectList(_db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAlbum([Bind("AlbumID,Title,GenreID,Price,ArtistID,Likes")] Album album)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(album).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("IndexAlbum");
            }
            return View(album);
        }

        
        //[HttpPost]
        //public IActionResult LikeAlbum([Bind("AlbumID, Title, GenreID, Price, ArtistID, Likes")] Album album)
        public IActionResult LikeAlbum(int? id)
        {
            Album album = _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(i => i.AlbumID == id);
            if (album == null){
                return NotFound();
            }
            album.Likes++;
            _db.SaveChanges();
            return RedirectToAction("IndexAlbum");
        }

        public ActionResult DeleteAlbum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = _db.Albums.Include(a => a.Artist).Include(a => a.Genre).FirstOrDefault(i => i.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost, ActionName("DeleteAlbum")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = _db.Albums.FirstOrDefault(i => i.AlbumID == id);
            _db.Albums.Remove(album);
            _db.SaveChanges();
            return RedirectToAction("IndexAlbum");
        }
    }
}