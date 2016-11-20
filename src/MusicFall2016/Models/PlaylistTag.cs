namespace MusicFall2016.Models
{
    public class PlaylistTag
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
    }
}