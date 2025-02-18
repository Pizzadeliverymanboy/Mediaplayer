using Microsoft.VisualBasic.Devices;
using Mediaplayer.Playlists;

namespace Mediaplayer;

public class Datamanager
{
    //private so the instance cannot be accessed from outside this class, static so only one exists
    private static Datamanager _instance;
    
    //private constructor, can only be called from inside the class
    private Datamanager(){}
    
    //public static, so it can be accessed from outside without concurrency problems
    public static Datamanager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Datamanager();
            }
            return _instance;
        }
    }

    public List<Audiolist> audios { get; private set; } = new List<Audiolist>();
    public List<Videolist> videos { get; private set; } = new List<Videolist>();
    public List<Imagelist> images { get;private set; } = new List<Imagelist>();

    public bool playlistDelete { get; private set; } = false;

    public void AddPlaylistToDatabase(string playlistname, string playlisttype)
    {
        DatabaseHandler.Instance.AddPlaylist(playlistname, playlisttype);
    }

    public void addAudioList(Audiolist audio)
    {
        this.audios.Add(audio);
    }

    public void addVideoList(Videolist video)
    {
        this.videos.Add(video);
    }

    public void addImageList(Imagelist image)
    {
        this.images.Add(image);
    }
    
    public void loadLists()
    {
        this.audios.Clear();
        this.videos.Clear();
        this.images.Clear();
        DatabaseHandler.Instance.GetPlaylists();
    }

    public void addFile(string filename, string filepath, string filetype, int playlistid)
    {
        DatabaseHandler.Instance.AddFile(filename, filepath, filetype, playlistid);
    }

    public void deleteFile(int mediafileid)
    {
        DatabaseHandler.Instance.DeleteFile(mediafileid);
    }

    public void deletePlaylist(int playlistid)
    {
        Console.WriteLine("Deleting playlist with id: " + playlistid);
        DatabaseHandler.Instance.DeletePlaylist(playlistid);
        playlistDelete = true;
    }

}