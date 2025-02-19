using System;
using System.IO;
using LibVLCSharp.Shared;
using Microsoft.Win32;
using Vlc.DotNet.Forms;
using Vlc.DotNet.Core;
using System.Drawing.Drawing2D;
using Mediaplayer.Playlists;
using TagLib;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Reflection;

namespace Mediaplayer;

public partial class Form1 : Form
{
    private LibVLC _libVlc;

    private MediaPlayer _mediaPlayer;

    private System.Windows.Forms.Timer progressTimer;
    public Form1()
    {

        InitializeComponent();

        loadLists();

        _libVlc = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVlc);
        videoView1.MediaPlayer = _mediaPlayer;

        // Set the initial volume based on the TrackBar's value
        _mediaPlayer.Volume = tb_.Value;

        lv_playlist.Columns.Add("Filename", 200);
        lv_playlist.Columns.Add("Duration", 75);

        // Set up the progress timer
        progressTimer = new System.Windows.Forms.Timer();
        

        // Configure the timer
        progressTimer.Interval = 500; // Update every 500 ms
        progressTimer.Tick += TimerProgress_Tick;
        progressTimer.Start();

        // Load button icons from the file system and configure button properties.
        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        btn_play_stop.AutoSize = true;
        btn_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\stop-24.png");
        btn_stop.AutoSize = true;
        btn_previous.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\previous-24.png");
        btn_previous.AutoSize = true;
        btn_next.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\next-24.png");
        btn_next.AutoSize = true;
        btn_addfile.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\add-file-24.png");
        btn_addfile.AutoSize = true;
        btn_deletefile.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\x-mark-3-24.png");
        btn_deletefile.AutoSize = true;

    }

    // Play/Pause Button Click Event Handler
    // This method toggles between playing and pausing the media.
    private void btn_play_stop_Click(object sender, EventArgs e)
    {
        // If no media is loaded, show a message to the user.
        if (_mediaPlayer.Length == -1)
        {
            MessageBox.Show("No media loaded");
            return;
        }
        // If media is playing, pause it; otherwise, play the media.
        if (_mediaPlayer.IsPlaying)
        {
            // Update the button icon to indicate "play".
            _mediaPlayer.Pause();
            progressTimer.Stop();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        }
        else
        {
            // Update the button icon to indicate "pause".
            _mediaPlayer.Play();
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
        }
    }

    // Stop Button Click Event Handler
    // Stops media playback, resets progress bar, and updates the UI.
    private void btn_stop_Click(object sender, EventArgs e)
    {
        _mediaPlayer.Stop();
        progressTimer.Stop();
        tb_progress.Value = 0;
        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
    }

    private void tb__Scroll(object sender, EventArgs e)
    {
        // Update the media player's volume with the TrackBar's current value.
        _mediaPlayer.Volume = tb_.Value;
    }
    // Timer Tick Event Handler
    // Updates the current playback time, total duration, and progress bar.
    private void TimerProgress_Tick(object sender, EventArgs e)
    {
        if (_mediaPlayer != null && _mediaPlayer.Media != null && _mediaPlayer.IsPlaying)
        {
            long totalTime = _mediaPlayer.Length;  // Total length in milliseconds
            long currentTime = _mediaPlayer.Time;    // Current playback time in milliseconds

            // Format the times as "mm:ss" and update the respective labels.
            string formattedCurrentTime = TimeSpan.FromMilliseconds(currentTime).ToString(@"mm\:ss");
            lb_currenttracktime.Text = $"{formattedCurrentTime}";


            string formattedDuration = TimeSpan.FromMilliseconds(totalTime).ToString(@"mm\:ss");
            lb_trackduration.Text = $"{formattedDuration}";


            // The Position property returns a float between 0 and 1.
            // Multiply by Maximum to get a percentage value.
            int progressValue = (int)(_mediaPlayer.Position * tb_progress.Maximum);

            // Ensure we don't exceed the trackbar maximum
            tb_progress.Value = Math.Min(tb_progress.Maximum, progressValue);
        }
    }


    // Stop media playback
    private void btnStop_Click(object sender, EventArgs e)
    {
        pb_audioart.Visible = false;
        _mediaPlayer.Stop();
        progressTimer.Stop();
        tb_progress.Value = 0;

    }

    // Event handler for when the user clicks down on the progress TrackBar.
    // Stops the timer to allow manual seeking without interference.
    private void tb_progress_MouseDown(object sender, MouseEventArgs e)
    {
        // Stop timer while dragging
        progressTimer.Stop();
    }

    // Event handler for when the user releases the mouse button on the progress TrackBar.
    // Sets the new media playback position based on the TrackBar's value and resumes the timer.
    private void tb_progress_MouseUp(object sender, MouseEventArgs e)
    {
        // Calculate new position based on trackbar value
        float newPosition = tb_progress.Value / (float)tb_progress.Maximum;
        _mediaPlayer.Position = newPosition;
        progressTimer.Start();
    }

    // Opens an OpenFileDialog to allow the user to select an audio file.
    // Plays the selected audio file and extracts album art if available.
    private void tsmi_audio_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ".mp3",
            Filter = "Audio files (*.mp3;*.wav;*.ogg;*.flac)|*.mp3;*.wav;*.ogg;*.flac",
            FilterIndex = 2,
            RestoreDirectory = true,
            ReadOnlyChecked = true,
            ShowReadOnly = true
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            // Create a new media from the file path
            using (var media = new Media(_libVlc, ofd.FileName, FromType.FromPath))
            {

                // Play the media file
                this.Text = ofd.SafeFileName; // Display the file name in the form's title bar
                videoView1.MediaPlayer.Play(media);
                btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");

                // Extract and display album art using TagLib#
                try
                {
                    var tagFile = TagLib.File.Create(ofd.FileName);
                    if (tagFile.Tag.Pictures.Length > 0)
                    {
                        var picData = tagFile.Tag.Pictures[0].Data.Data;
                        using (var ms = new MemoryStream(picData))
                        {
                            pb_audioart.Image = Image.FromStream(ms);
                            pb_audioart.Visible = true;
                            pb_audioart.BackColor = Color.Black;
                        }
                    }
                    else
                    {
                        pb_audioart.Image = null; // Or set a default image
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error extracting album art: " + ex.Message);
                }

            }

        }
    }

    // Opens an OpenFileDialog for video files, plays the selected video,
    // and updates UI elements accordingly.
    private void tsmi_video_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
            CheckFileExists = true,
            CheckPathExists = true,
            // Set the default file extension to .mp4
            DefaultExt = ".mp4",
            // Filter to show only video files (and an option for all files)
            Filter = "Video Files (*.mp4;*.avi;*.mov;*.mkv)|*.mp4;*.avi;*.mov;*.mkv",
            FilterIndex = 2,
            RestoreDirectory = true,
            ReadOnlyChecked = true,
            ShowReadOnly = true
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            // Create a new media from the file path
            using (var media = new Media(_libVlc, ofd.FileName, FromType.FromPath))
            {

                // Play the media file
                videoView1.MediaPlayer.Play(media);
                this.Text = ofd.SafeFileName;
                btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                pb_audioart.Visible = false;
            }
        }
    }

    // Opens an OpenFileDialog for image files and displays the selected image in a new form.
    private void tsmi_image_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ".jpg",
            Filter = "Image Files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif",
            FilterIndex = 2,
            RestoreDirectory = true,
            ReadOnlyChecked = true,
            ShowReadOnly = true
        };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            Form2 form2 = new Form2(ofd.FileName);
            form2.Show();

        }
    }

    // Handles adding a new audio playlist when the Enter key is pressed.
    private void tsmitb_audio_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && tsmitb_audio.Text.Length != 0)
        {
            // Add the new playlist using the Datamanager.
            Datamanager.Instance.AddPlaylistToDatabase(tsmitb_audio.Text, "Audio");
            MessageBox.Show("Playlist added");
            tsmitb_audio.Text = "";
            Datamanager.Instance.loadLists();
            tscb_audio.Items.Clear();
            if (Datamanager.Instance.audios != null)
            {
                foreach (Audiolist audio in Datamanager.Instance.audios)
                {
                    tscb_audio.Items.Add(audio.playlistname);
                }
            }
        }
    }

    // Handles adding a new video playlist when the Enter key is pressed.
    private void tsmitb_video_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && tsmitb_video.Text.Length != 0)
        {
            Datamanager.Instance.AddPlaylistToDatabase(tsmitb_video.Text, "Video");
            MessageBox.Show("Playlist added");
            tsmitb_video.Text = "";
            Datamanager.Instance.loadLists();
            tscb_video.Items.Clear();
            if (Datamanager.Instance.videos != null)
            {
                foreach (Videolist video in Datamanager.Instance.videos)
                {
                    tscb_video.Items.Add(video.playlistname);
                }
            }
        }
    }

    // Handles adding a new image playlist when the Enter key is pressed.
    private void tsmitb_image_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && tsmitb_image.Text.Length != 0)
        {
            Datamanager.Instance.AddPlaylistToDatabase(tsmitb_image.Text, "Image");
            MessageBox.Show("Playlist added");
            tsmitb_image.Text = "";
            Datamanager.Instance.loadLists();
            tscb_album.Items.Clear();
            if (Datamanager.Instance.images != null)
            {
                foreach (Imagelist image in Datamanager.Instance.images)
                {
                    tscb_album.Items.Add(image.playlistname);
                }
            }
        }

    }

    // When an audio playlist is selected, populate the ListView with its media files.
    private void tscb_audio_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Deselect video and image playlists
        tscb_video.SelectedIndex = -1;
        tscb_album.SelectedIndex = -1;
        lv_playlist.Items.Clear();
        if (tscb_audio.SelectedIndex != -1)
        {
            // Update Hidden UI labels with the selected playlist details.
            lb_playlistid.Text = Datamanager.Instance.audios[tscb_audio.SelectedIndex].playlistid.ToString();
            lb_current_playlist.Text = Datamanager.Instance.audios[tscb_audio.SelectedIndex].playlistname;
            // Populate the ListView with file names and durations.
            foreach (Mediafile file in Datamanager.Instance.audios[tscb_audio.SelectedIndex].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString(@"mm\:ss"));
                lv_playlist.Items.Add(item);
            }
            lb_listtype.Text = "Audio";
            lb_listpos.Text = tscb_audio.SelectedIndex.ToString();
            btn_addfile.Enabled = true;

        }
    }

    // When a video playlist is selected, populate the ListView with its media files.
    private void tscb_video_SelectedIndexChanged(object sender, EventArgs e)
    {
        tscb_audio.SelectedIndex = -1;
        tscb_album.SelectedIndex = -1;
        lv_playlist.Items.Clear();
        if (tscb_video.SelectedIndex != -1)
        {
            lb_playlistid.Text = Datamanager.Instance.videos[tscb_video.SelectedIndex].playlistid.ToString();
            lb_current_playlist.Text = Datamanager.Instance.videos[tscb_video.SelectedIndex].playlistname;
            foreach (Mediafile file in Datamanager.Instance.videos[tscb_video.SelectedIndex].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString(@"mm\:ss"));
                lv_playlist.Items.Add(item);
            }
            lb_listtype.Text = "Video";
            lb_listpos.Text = tscb_video.SelectedIndex.ToString();
            btn_addfile.Enabled = true;
        }
    }

    // When an image playlist is selected, open a new form to display the images.
    private void tscb_album_SelectedIndexChanged(object sender, EventArgs e)
    {
        tscb_audio.SelectedIndex = -1;
        tscb_video.SelectedIndex = -1;
        if (tscb_album.SelectedIndex != -1)
        {
            Form2 form2 = new Form2(Datamanager.Instance.images[tscb_album.SelectedIndex], tscb_album.SelectedIndex);
            form2.Show();
        }
    }

    // Enable the delete button if a file is selected in the playlist ListView.
    private void lv_playlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lv_playlist.SelectedItems.Count > 0)
        {
            btn_deletefile.Enabled = true;
            
        }
        else
        {
            btn_deletefile.Enabled = false;
        }
    }
    // When a media file in the playlist ListView is double-clicked,
    // start playing that file and update the UI accordingly.
    private void lv_playlist_DoubleClick(object sender, EventArgs e)
    {

        if (lb_listtype.Text.Equals("Audio"))
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filepath, FromType.FromPath));
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
            this.Text = Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filename;
            // Update album art for the new audio file.
            try
            {
                var tagFile = TagLib.File.Create(Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filepath);
                if (tagFile.Tag.Pictures.Length > 0)
                {
                    var picData = tagFile.Tag.Pictures[0].Data.Data;
                    using (var ms = new MemoryStream(picData))
                    {
                        pb_audioart.Image = Image.FromStream(ms);
                        pb_audioart.Visible = true;
                        pb_audioart.BackColor = Color.Black;
                    }
                }
                else
                {
                    pb_audioart.Image = null; // Or set a default image
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting album art: " + ex.Message);
            }
        }
        else if (lb_listtype.Text.Equals("Video"))
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filepath, FromType.FromPath));
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
            pb_audioart.Visible = false;
            this.Text = Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filename;
        }
    }

    // Allows the user to add a new media file to the currently selected playlist.
    private void btn_addfile_Click(object sender, EventArgs e)
    {
        if (lb_listtype.Text.Equals("Audio"))
        {
            // Open a file dialog to select an audio file.
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".mp3",
                Filter = "Audio files (*.mp3;*.wav;*.ogg;*.flac)|*.mp3;*.wav;*.ogg;*.flac",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Add the selected audio file to the database.
                Datamanager.Instance.addFile(ofd.SafeFileName, ofd.FileName, "Audio", Convert.ToInt32(lb_playlistid.Text));
                MessageBox.Show("File added");
                lv_playlist.Items.Clear();
                loadLists();
                // Repopulate the ListView with the updated list of audio files.
                foreach (Mediafile file in Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles)
                {
                    
                    var tagFile = TagLib.File.Create(file.filepath);
                    ListViewItem item = new ListViewItem(file.filename);
                    item.SubItems.Add(tagFile.Properties.Duration.ToString(@"mm\:ss"));
                    lv_playlist.Items.Add(item);
                    
                }

                
            }
        }

        else if (lb_listtype.Text.Equals("Video"))
        {
            // Open a file dialog to select a video file.
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".mp4",
                Filter = "Video Files (*.mp4;*.avi;*.mov;*.mkv)|*.mp4;*.avi;*.mov;*.mkv",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Add the selected video file to the database.
                Datamanager.Instance.addFile(ofd.SafeFileName, ofd.FileName, "Video", Convert.ToInt32(lb_playlistid.Text));
                MessageBox.Show("File added");
                lv_playlist.Items.Clear();
                loadLists();
                // Repopulate the ListView with the updated list of video files.
                foreach (Mediafile file in Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles)
                {
                    var tagFile = TagLib.File.Create(file.filepath);
                    ListViewItem item = new ListViewItem(file.filename);
                    item.SubItems.Add(tagFile.Properties.Duration.ToString());
                    lv_playlist.Items.Add(item);

                }
            }
        }
    }
    // Deletes the selected media file from the current playlist.
    private void btn_deletefile_Click(object sender, EventArgs e)
    {
        if (lb_listtype.Text.Equals("Audio"))
        {
            // Delete the audio file from the database.
            Datamanager.Instance.deleteFile(Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].fileid);
            lv_playlist.Items.Clear();
            loadLists();
            foreach (Mediafile file in Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles)
            {
                // Repopulate the ListView with the updated list of audio files.
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString(@"mm\:ss"));
                lv_playlist.Items.Add(item);
            }
        }
        else if (lb_listtype.Text.Equals("Video"))
        {
            // Delete the video file from the database.
            Datamanager.Instance.deleteFile(Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].fileid);
            lv_playlist.Items.Clear();
            loadLists();
            // Repopulate the ListView with the updated list of video files.
            foreach (Mediafile file in Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString());
                lv_playlist.Items.Add(item);
            }
        }

    }
    // Opens a dialog to delete an entire playlist.
    private void tsmi_delete_playlist_Click(object sender, EventArgs e)
    {
        Form3 form3 = new Form3();
        form3.ShowDialog();
        if (Datamanager.Instance.playlistDelete)
        {
            loadLists();

        }
    }
    // Reloads playlists and updates the ComboBoxes and ListView.
    private void loadLists()
    {
        Datamanager.Instance.loadLists();
        tscb_audio.Items.Clear();
        tscb_video.Items.Clear();
        tscb_album.Items.Clear();
        lv_playlist.Items.Clear();
        // Populate audio playlists.
        if (Datamanager.Instance.audios != null)
        {
            foreach (Audiolist audio in Datamanager.Instance.audios)
            {
                tscb_audio.Items.Add(audio.playlistname);
            }
        }
        // Populate video playlists.
        if (Datamanager.Instance.videos != null)
        {
            foreach (Videolist video in Datamanager.Instance.videos)
            {
                tscb_video.Items.Add(video.playlistname);
            }
        }
        // Populate image playlists.
        if (Datamanager.Instance.images != null)
        {
            foreach (Imagelist image in Datamanager.Instance.images)
            {
                tscb_album.Items.Add(image.playlistname);
            }
        }
    }
    // Navigate to the previous media file in the playlist.
    private void btn_previous_Click(object sender, EventArgs e)
    {
        if (lv_playlist.Items.Count > 0)
        {
            if (lv_playlist.SelectedItems.Count > 0)
            {
                int index = lv_playlist.SelectedItems[0].Index - 1;
                Console.WriteLine(lv_playlist.Items.Count);
                if (index > -1)
                {
                    if (lb_listtype.Text.Equals("Audio"))
                    {

                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[index + 1].Selected = false;
                        lv_playlist.Items[index].Selected = true;
                        this.Text = Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filename;
                        // Update album art for the new audio file.
                        try
                        {
                            var tagFile = TagLib.File.Create(Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath);
                            if (tagFile.Tag.Pictures.Length > 0)
                            {
                                var picData = tagFile.Tag.Pictures[0].Data.Data;
                                using (var ms = new MemoryStream(picData))
                                {
                                    pb_audioart.Image = Image.FromStream(ms);
                                    pb_audioart.Visible = true;
                                    pb_audioart.BackColor = Color.Black;
                                }
                            }
                            else
                            {
                                pb_audioart.Image = null; // Or set a default image
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error extracting album art: " + ex.Message);
                        }
                    }
                    else if (lb_listtype.Text.Equals("Video"))
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[index + 1].Selected = false;
                        lv_playlist.Items[index].Selected = true;
                        pb_audioart.Visible = false;
                        this.Text = Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filename;
                    }
                }
            }
        }
    }
    // Navigate to the next media file in the playlist.
    private void btn_next_Click(object sender, EventArgs e)
    {
        if (lv_playlist.Items.Count > 0)
        {
            if (lv_playlist.SelectedItems.Count > 0)
            {
                int index = lv_playlist.SelectedItems[0].Index + 1;
                Console.WriteLine(lv_playlist.Items.Count);
                if (index < lv_playlist.Items.Count)
                {
                    if (lb_listtype.Text.Equals("Audio"))
                    {

                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[index - 1].Selected = false;
                        lv_playlist.Items[index].Selected = true;
                        this.Text = Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filename;
                        // Update album art for the new audio file.
                        try
                        {
                            var tagFile = TagLib.File.Create(Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath);
                            if (tagFile.Tag.Pictures.Length > 0)
                            {
                                var picData = tagFile.Tag.Pictures[0].Data.Data;
                                using (var ms = new MemoryStream(picData))
                                {
                                    pb_audioart.Image = Image.FromStream(ms);
                                    pb_audioart.Visible = true;
                                    pb_audioart.BackColor = Color.Black;
                                }
                            }
                            else
                            {
                                pb_audioart.Image = null; // Or set a default image
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error extracting album art: " + ex.Message);
                        }
                    }
                    else if (lb_listtype.Text.Equals("Video"))
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[index - 1].Selected = false;
                        lv_playlist.Items[index].Selected = true;
                        pb_audioart.Visible = false;
                        this.Text = Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[index].filename;
                    }
                }


            }
        }
    }
}
