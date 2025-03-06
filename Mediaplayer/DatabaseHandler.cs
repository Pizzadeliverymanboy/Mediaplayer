using System.Data;
using Mediaplayer.Playlists;
using Microsoft.Data.SqlClient;

namespace Mediaplayer;

public class DatabaseHandler
{
    //private so the instance cannot be accessed from outside this class, static so only one exists
    private static DatabaseHandler _instance;
    
    //private constructor, can only be called from inside the class
    private DatabaseHandler(){}
    
    private static readonly string connectionString = @"Data Source=176.96.137.229,1433;Initial Catalog=Mediaplayer;User ID=Mediaplayer_user;Password=88Xpt6Ned33F; TrustServerCertificate=True;";
    
    //public static, so it can be accessed from outside without concurrency problems
    public static DatabaseHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseHandler();
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@playlistname", SqlDbType.VarChar);
                    command.Parameters["@playlistname"].Value = playlistname;
                    command.Parameters.Add("@playlisttype", SqlDbType.VarChar);
                    command.Parameters["@playlisttype"].Value = playlisttype;

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@playlistid", SqlDbType.Int);
                    command.Parameters["@playlistid"].Value = playlistid;

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        adapter.Fill(resultTable);
                    }
                }
                
            }

            // Loop through the result table and add the playlists to the datamanager
            foreach (DataRow row in resultTable.Rows)
            {

                // Check the playlisttype and add the playlist to the datamanager
                switch ((string)row["playlisttype"])
                {
                    case "Audio":
                        Datamanager.Instance.AddAudioList(new Audiolist((int)row["playlistid"], (string)row["playlistname"], getMediaFiles((int)row["playlistid"], (string)row["playlisttype"])));
                        break;
                    case "Video":
                        Datamanager.Instance.AddVideoList(new Videolist((int)row["playlistid"], (string)row["playlistname"], getMediaFiles((int)row["playlistid"], (string)row["playlisttype"])));
                        break;
                    case "Image":
                        Datamanager.Instance.AddImageList(new Imagelist((int)row["playlistid"], (string)row["playlistname"], getMediaFiles((int)row["playlistid"], (string)row["playlisttype"])));
                        break;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@filename", SqlDbType.NVarChar);
                    command.Parameters["@filename"].Value = filename;
                    command.Parameters.Add("@filepath", SqlDbType.NVarChar);
                    command.Parameters["@filepath"].Value = filepath;
                    command.Parameters.Add("@filetype", SqlDbType.VarChar);
                    command.Parameters["@filetype"].Value = filetype;
                    command.Parameters.Add("@playlistid", SqlDbType.Int);
                    command.Parameters["@playlistid"].Value = playlistid;

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@fileid", SqlDbType.Int);
                    command.Parameters["@fileid"].Value = fileid;

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

        DataTable resultTable = new DataTable();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Verbindung aufgebaut.");

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        command.Parameters.Add("@playlistid", SqlDbType.Int);
                        command.Parameters["@playlistid"].Value = playlistid;

                        adapter.Fill(resultTable);
                    }
                }

            }
            // List to store the mediafiles
            List<Mediafile> mediafiles = new List<Mediafile>();
            // Loop through the result table and add the mediafiles to the list
            foreach (DataRow row in resultTable.Rows)
            {
                mediafiles.Add(new Mediafile((int)row["fileid"], (string)row["filename"], (string)row["filepath"],
                    playlisttype));
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