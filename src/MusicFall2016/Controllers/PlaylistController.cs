using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicFall2016.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly MusicDbContext _db;

        public PlaylistController(MusicDbContext context)
        {
            _db = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToPlaylist(int? id)
        {
           
            //ViewBag.ArtistPlaylists = new SelectList(_db., "PlaylistId", "Name", album.PlaylistTags);
            //album.PlaylistTags.Add

            return View();
        }
        //[HttpPost]
        //public IActionResult AddToPlaylist(PlaylistTag playlistTag)
        //{
        //    Album album = _db.Albums.Include(PlaylistTag)
        //        .SingleOrDefault(i => i.AlbumID == id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.ArtistPlaylists = new SelectList(_db., "PlaylistId", "Name", album.PlaylistTags);
        //    //album.PlaylistTags.Add

        //    return View(album);
        //}

        public IActionResult CreatePlaylist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePlaylist(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _db.Playlist.Add(playlist);
                _db.SaveChanges();
                return RedirectToAction("IndexPlaylist");
            }
            if (!string.IsNullOrEmpty(playlist.Name))
            {
                Playlist aPlaylist = new Playlist();
                aPlaylist.Name = playlist.Name;
            }
            return View(playlist);
        }
    }
}
