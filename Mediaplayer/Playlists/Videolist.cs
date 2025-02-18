namespace Mediaplayer.Playlists;

public class Videolist : Playlist
{
    public Videolist(int playlistid, string playlistname, List<Mediafile> mediafiles) : base(playlistid, playlistname, mediafiles)
    {
        
    }
}