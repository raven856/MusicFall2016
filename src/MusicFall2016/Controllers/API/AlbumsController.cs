//using MusicFall2016.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

//namespace MusicFall2016.Controllers.API
//{
//    public class AlbumsController : Controller
//    {

//        private readonly MusicDbContext _db;
//        public AlbumsController(MusicDbContext context)
//        {
//            _db = context;
//        }
        
//        // GET: /<controller>/
//        [HttpGet("api/Albums")]
//        public JsonResult Get()
//        {
//            Album album = _db.Albums.Include(a => a.Artist).Include(a => a.Genre);//.ToList(); //.Single(a => a.AlbumID == 1);

//            //if (true) { return BadRequest("Something Went Wrong"); }
//            //else
//            //{
//            //    return Ok(new Artist { Name = "SomeArtist", Bio = "Awesome Musician" });
//            //}

//            return Ok(album);
//        }
        
//    }
//}
