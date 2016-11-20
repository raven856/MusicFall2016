using System.Collections.Generic;

namespace MusicFall2016.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        //public Album[] List { get; set; }
        public List<PlaylistTag> PlaylistTags { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}
