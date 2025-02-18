namespace Mediaplayer.Playlists;

public class Mediafile
{
    public int fileid { get; private set; }
    public string filename { get; private set; }
    public string filepath { get; private set; }
    public string filetype { get; private set; }

    public Mediafile(int fileid, string filename, string filepath, string filetype)
    {
        this.fileid = fileid;
        this.filename = filename;
        this.filepath = filepath;
        this.filetype = filetype;
    }
}