using System.Data;
using Mediaplayer.Playlists;
using Microsoft.Data.Sqlite;

namespace Mediaplayer;

public class DatabaseHandlerLite
{
    //private so the instance cannot be accessed from outside this class, static so only one exists
    private static DatabaseHandlerLite _instance;
    
    //private constructor, can only be called from inside the class
    private DatabaseHandlerLite()
    {
        
    }
    
    private static readonly string connectionString = "Data Source=C:\\Users\\Depon\\RiderProjects\\Mediaplayer\\Mediaplayer\\Database\\Mediaplayer.db";
    
    //public static, so it can be accessed from outside without concurrency problems
    public static DatabaseHandlerLite Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseHandlerLite();
            }
            return _instance;
        }
    }
    
    // Method to Add Playlist to Database
    public void AddPlaylist(string playlistname, string playlisttype)
    {
        string query = "INSERT INTO Playlist(playlistname, playlisttype) VALUES (@playlistname, @playlisttype)";
        
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playlistname", playlistname);
                    command.Parameters.AddWithValue("@playlisttype", playlisttype);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung nicht hergestellt" + ex);
            throw;
        }
    }
    
    // Method to Delete Playlist to Database
    public void DeletePlaylist(int playlistid)
    {
        string query = "DELETE FROM Playlist WHERE playlistid = @playlistid";
        
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playlistid", playlistid);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung nicht hergestellt" + ex);
            throw;
        }
    }
    // Method to Get Playlists from Database
    // Adds the playlists to the datamanager
    public void GetPlaylists()
    {
        string query = "SELECT * FROM Playlist;";
        
        DataTable resultTable = new DataTable();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Check the playlisttype and add the playlist to the datamanager
                                switch (reader.GetString(2))
                                {
                                    case "Audio":
                                        Datamanager.Instance.AddAudioList(new Audiolist(reader.GetInt32(0), reader.GetString(1), getMediaFiles(reader.GetInt32(0), reader.GetString(2))));
                                        break;
                                    case "Video":
                                        Datamanager.Instance.AddVideoList(new Videolist(reader.GetInt32(0), reader.GetString(1), getMediaFiles(reader.GetInt32(0), reader.GetString(2))));
                                        break;
                                    case "Image":
                                        Datamanager.Instance.AddImageList(new Imagelist(reader.GetInt32(0), reader.GetString(1), getMediaFiles(reader.GetInt32(0), reader.GetString(2))));
                                        break;
                                }
                            }
                            
                        }
                        
                    }
                }
                
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung fehlgeschlagen" + ex);
            throw;
        }
    }

    // Method to Add File to Database
    public void AddFile(string filename, string filepath, string filetype, int playlistid)
    {
        string query = "INSERT INTO Mediafiles(filename, filepath, filetype, playlistid) VALUES (@filename, @filepath, @filetype, @playlistid)";
        
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@filename", filename);
                    command.Parameters.AddWithValue("@filepath", filepath);
                    command.Parameters.AddWithValue("@filetype", filetype);
                    command.Parameters.AddWithValue("@playlistid", playlistid);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung nicht hergestellt" + ex);
            throw;
        }
    }

    // Method to Delete File to Database
    public void DeleteFile(int fileid)
    {
        string query = "DELETE FROM Mediafiles WHERE fileid = @fileid";
        
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fileid", fileid);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung nicht hergestellt" + ex);
            throw;
        }
    }

    // Method to Get Mediafiles from Database
    // Returns a list of mediafiles
    public List<Mediafile> getMediaFiles(int playlistid, string playlisttype)
    {
        string query = "SELECT * FROM Mediafiles WHERE playlistid = @playlistid;";

        // List to store the mediafiles
        List<Mediafile> mediafiles = new List<Mediafile>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@playlistid", playlistid);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            
                            while (reader.Read())
                            {
                                mediafiles.Add(new Mediafile(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                    playlisttype));
                            }
                        }
                        
                    }
                }

            }

            return mediafiles;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datenbankverbindung fehlgeschlagen" + ex);
            throw;
        }
    }
}