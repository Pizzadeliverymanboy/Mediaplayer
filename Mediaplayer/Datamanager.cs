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
    // Properties
    // Lists of audio, video, and image playlists
    public List<Audiolist> audios { get; private set; } = new List<Audiolist>();
    public List<Videolist> videos { get; private set; } = new List<Videolist>();
    public List<Imagelist> images { get;private set; } = new List<Imagelist>();

    // Property to check if a playlist was deleted
    public bool playlistDelete { get; private set; } = false;

    // Method to add a playlist to the database
    public void AddPlaylistToDatabase(string playlistname, string playlisttype)
    {
        DatabaseHandler.Instance.AddPlaylist(playlistname, playlisttype);
    }

    // Method to add an audio playlist to the list
    public void AddAudioList(Audiolist audio)
    {
        this.audios.Add(audio);
    }

    // Method to add a video playlist to the list
    public void AddVideoList(Videolist video)
    {
        this.videos.Add(video);
    }

    // Method to add an image playlist to the list
    public void AddImageList(Imagelist image)
    {
        this.images.Add(image);
    }

    // Method to load playlists from the database
    public void LoadLists()
    {
        this.audios.Clear();
        this.videos.Clear();
        this.images.Clear();
        DatabaseHandler.Instance.GetPlaylists();
    }

    // Method to add a file to the database
    public void AddFile(string filename, string filepath, string filetype, int playlistid)
    {
        DatabaseHandler.Instance.AddFile(filename, filepath, filetype, playlistid);
    }

    // Method to delete a file from the database
    public void DeleteFile(int mediafileid)
    {
        DatabaseHandler.Instance.DeleteFile(mediafileid);
    }

    // Method to delete a playlist from the database
    public void DeletePlaylist(int playlistid)
    {
        DatabaseHandler.Instance.DeletePlaylist(playlistid);
        playlistDelete = true;
    }

    public void SetPlaylistDeleteToNo()
    {
        playlistDelete = false;
    }

}