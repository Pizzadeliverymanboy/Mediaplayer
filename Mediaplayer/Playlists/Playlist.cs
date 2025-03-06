namespace Mediaplayer.Playlists;
// Playlist class represents a playlist containing media files.
public class Playlist
{
    // Properties
    public int playlistid { get; private set; }
    public string playlistname { get; private set; }
    
    public List<Mediafile> mediafiles { get; private set; }

    // Constructor
    public Playlist(int playlistid, string playlistname, List<Mediafile> mediafiles)
    {
        this.playlistid = playlistid;
        this.playlistname = playlistname;
        this.mediafiles = mediafiles;
    }
}