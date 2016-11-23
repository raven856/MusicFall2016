namespace MusicFall2016.Models
{
    public class PlaylistTag
    {
        public int AlbumId { get; set; }
        public Album aAlbum { get; set; }

        public int PlaylistId { get; set; }
        public Playlist aPlaylist { get; set; }
    }
}