namespace Mediaplayer.Playlists;

// Mediafile class represents a media file.
// It contains information about the file, such as its ID, name, path, and type.
public class Mediafile
{
    // Properties
    public int fileid { get; private set; }
    public string filename { get; private set; }
    public string filepath { get; private set; }
    public string filetype { get; private set; }

    // Constructor
    public Mediafile(int fileid, string filename, string filepath, string filetype)
    {
        this.fileid = fileid;
        this.filename = filename;
        this.filepath = filepath;
        this.filetype = filetype;
    }
}