namespace Mediaplayer.Playlists;

public class Playlist
{
    public int playlistid { get; private set; }
    public string playlistname { get; private set; }
    
    public List<Mediafile> mediafiles { get; private set; }

    public Playlist(int playlistid, string playlistname, List<Mediafile> mediafiles)
    {
        this.playlistid = playlistid;
        this.playlistname = playlistname;
        this.mediafiles = mediafiles;
    }
}