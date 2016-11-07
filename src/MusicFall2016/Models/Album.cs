using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MusicFall2016.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        [Required(ErrorMessage = "Album title is required")]
        public string Title { get; set; }
        [Range(0.01,100.00)]
        public decimal Price { get; set; }
        public int Likes { get; set; }

        // Foreign key
        [Display(Name = "Artist")]
        public int ArtistID { get; set; }
        // Navigation property
        public Artist Artist { get; set; }
        [Display(Name = "Genre")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        //public Album()
        //{
        //    this.Likes = 0;
        //}

    }
}
