using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//@inject SignInManager<ApplicationUser> SignInManager
//@inject UserManager<ApplicationUser> UserManager

namespace MusicFall2016.Controllers
{
    public class PlaylistController : Controller
    {
        public SignInManager<ApplicationUser> _signInManager;
        public UserManager<ApplicationUser> _userManager;

        private readonly MusicDbContext _db;

        public PlaylistController(MusicDbContext context)
        {
            _db = context;
        }

        // GET: /<controller>/
        public IActionResult IndexPlaylist()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var playlist = _db.Playlist.Where(p => p.Owner == User.Identity.Name);

                return View(playlist);
            }
            return RedirectToAction("Login","Account");
        }
        //public IActionResult Playlist
        public IActionResult AddToPlaylist(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Album album = _db.Albums.Where(i => i.AlbumID == id).Single();
                //unsure about PlaylistTags here..
                ViewBag.PlaylistList = new SelectList(_db.Playlist, "PlaylistId", "Name", album.PlaylistTags);

                return View(album);
            }
            else
            {
                return RedirectToAction("login","Account");
            }
            //ViewBag.ArtistPlaylists = new SelectList(_db., "PlaylistId", "Name", album.PlaylistTags);
            //album.PlaylistTags.Add
        }
        [HttpPost]
        public IActionResult AddToPlaylist(int AlbumID, int? PlaylistId/*, Playlist playlist*/)
        {
           // Album album = _db.Albums.Where(a => a.AlbumID == AlbumID).Single();
            Playlist playlist = _db.Playlist.Where(p => p.PlaylistId == PlaylistId).Single();
            PlaylistTag tag = new PlaylistTag
            {
                AlbumId = AlbumID,
                PlaylistId = playlist.PlaylistId,
            };

            _db.PlaylistTag.Add(tag);
            _db.SaveChanges();

            //album.PlaylistTags.Add(tag);
            //_db.SaveChanges();
                      
            return RedirectToAction("IndexAlbum","Albums");
        }

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
                playlist.Owner = User.Identity.Name;
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

        public IActionResult DetailsPlaylist(int id)
        {
            var playlist = _db.Playlist.Include(p => p.PlaylistTags).Where(p => p.PlaylistId == id).Single();

            var albums = _db.PlaylistTag
                .Include(a => a.aAlbum)
                .Where(pt => pt.PlaylistId == id)
                .ToList();

            //var albums = _db.Albums
            //    .Include(a => a.PlaylistTags/*.Include(pt => pt.Playlist)*/)
            //    .Where(a => a.PlaylistTags == playlist.PlaylistTags)
            //    .ToList();

            return View(albums);
        }

        public IActionResult DeletePlaylist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Playlist playlist = _db.Playlist.FirstOrDefault(i => i.PlaylistId == id);
            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        [HttpPost, ActionName("DeletePlaylist")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = _db.Playlist.FirstOrDefault(i => i.PlaylistId == id);
            _db.Playlist.Remove(playlist);
            _db.SaveChanges();
            return RedirectToAction("IndexPlaylist");
        }
    }
}
