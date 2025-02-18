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
        progressTimer.Interval = 500; // Update every 500ms
        //progressTimer.Tick += ProgressTimer_Tick;
        progressTimer.Start();

        // Configure the timer
        progressTimer.Interval = 1000; // Update every second
        progressTimer.Tick += TimerProgress_Tick;

        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        btn_play_stop.AutoSize = true;
        btn_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\stop-24.png");
        btn_stop.AutoSize = true;
        btn_previous.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\previous-24.png");
        btn_previous.AutoSize = true;
        btn_next.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\next-24.png");
        btn_next.AutoSize = true;
        //roundButton(btn_play_stop);

        foreach (Audiolist audio in Datamanager.Instance.audios)
        {
            Console.WriteLine(audio.playlistid);
            Console.WriteLine(audio.playlistname);
            foreach (Mediafile file in audio.mediafiles)
            {
                Console.WriteLine(file.filename);
            }
        }

    }

    private void btn_play_stop_Click(object sender, EventArgs e)
    {
        if (_mediaPlayer.Length == -1)
        {
            MessageBox.Show("No media loaded");
            return;
        }
        if (_mediaPlayer.IsPlaying)
        {
            _mediaPlayer.Pause();
            progressTimer.Stop();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        }
        else
        {
            _mediaPlayer.Play();
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
        }
    }

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
    private void TimerProgress_Tick(object sender, EventArgs e)
    {
        if (_mediaPlayer != null && _mediaPlayer.Media != null && _mediaPlayer.IsPlaying)
        {
            long totalTime = _mediaPlayer.Length;  // Total length in milliseconds
            long currentTime = _mediaPlayer.Time;    // Current playback time in milliseconds

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

    // Optional: Allow user to seek by dragging the TrackBar
    private void tb_progress_MouseDown(object sender, MouseEventArgs e)
    {
        // Stop timer while dragging
        progressTimer.Stop();
    }

    private void tb_progress_MouseUp(object sender, MouseEventArgs e)
    {
        // Calculate new position based on trackbar value
        float newPosition = tb_progress.Value / (float)tb_progress.Maximum;
        _mediaPlayer.Position = newPosition;
        progressTimer.Start();
    }

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

    private void tsmi_image_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ".mp3",
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

    private void tsmitb_audio_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && tsmitb_audio.Text.Length != 0)
        {
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

    private void tscb_audio_SelectedIndexChanged(object sender, EventArgs e)
    {

        lv_playlist.Items.Clear();
        Console.WriteLine(tscb_audio.SelectedIndex);
        if (tscb_audio.SelectedIndex != -1)
        {
            Console.WriteLine(tscb_audio.SelectedIndex);
            lb_playlistid.Text = Datamanager.Instance.audios[tscb_audio.SelectedIndex].playlistid.ToString();
            lb_current_playlist.Text = Datamanager.Instance.audios[tscb_audio.SelectedIndex].playlistname;
            foreach (Mediafile file in Datamanager.Instance.audios[tscb_audio.SelectedIndex].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString());
                lv_playlist.Items.Add(item);
            }
            lb_listtype.Text = "Audio";
            lb_listpos.Text = tscb_audio.SelectedIndex.ToString();
            btn_addfile.Enabled = true;

        }
    }

    private void tscb_video_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                item.SubItems.Add(tagFile.Properties.Duration.ToString());
                lv_playlist.Items.Add(item);
            }
            lb_listtype.Text = "Video";
            lb_listpos.Text = tscb_video.SelectedIndex.ToString();
            btn_addfile.Enabled = true;
        }
    }

    private void lv_playlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lv_playlist.SelectedItems.Count > 0)
        {
            btn_deletefile.Enabled = true;
            if (lb_listtype.Text.Equals("Audio"))
            {

            }
            else if (lb_listtype.Text.Equals("Video"))
            {

            }

        }
        else
        {
            btn_deletefile.Enabled = false;
        }
    }

    private void lv_playlist_DoubleClick(object sender, EventArgs e)
    {
        if (lb_listtype.Text.Equals("Audio"))
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filepath, FromType.FromPath));
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
        }
        else if (lb_listtype.Text.Equals("Video"))
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].filepath, FromType.FromPath));
            progressTimer.Start();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
        }
    }

    private void btn_addfile_Click(object sender, EventArgs e)
    {
        if (lb_listtype.Text.Equals("Audio"))
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
                Datamanager.Instance.addFile(ofd.SafeFileName, ofd.FileName, "Audio", Convert.ToInt32(lb_playlistid.Text));
                MessageBox.Show("File added");
                lv_playlist.Items.Clear();
                loadLists();
                foreach (Mediafile file in Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles)
                {
                    lv_playlist.Items.Add(file.filename);
                }

                var tagFile = TagLib.File.Create(ofd.FileName);
            }
        }

        else if (lb_listtype.Text.Equals("Video"))
        {
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
                Datamanager.Instance.addFile(ofd.SafeFileName, ofd.FileName, "Video", Convert.ToInt32(lb_playlistid.Text));
                MessageBox.Show("File added");
                lv_playlist.Items.Clear();
                loadLists();
                foreach (Mediafile file in Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles)
                {
                    lv_playlist.Items.Add(file.filename);
                }
            }
        }
    }

    private void btn_deletefile_Click(object sender, EventArgs e)
    {
        if (lb_listtype.Text.Equals("Audio"))
        {
            Datamanager.Instance.deleteFile(Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].fileid);
            lv_playlist.Items.Clear();
            loadLists();
            foreach (Mediafile file in Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString());
                lv_playlist.Items.Add(item);
            }
        }
        else if (lb_listtype.Text.Equals("Video"))
        {
            Datamanager.Instance.deleteFile(Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index].fileid);
            lv_playlist.Items.Clear();
            loadLists();
            foreach (Mediafile file in Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles)
            {
                var tagFile = TagLib.File.Create(file.filepath);
                ListViewItem item = new ListViewItem(file.filename);
                item.SubItems.Add(tagFile.Properties.Duration.ToString());
                lv_playlist.Items.Add(item);
            }
        }

    }

    private void tsmi_delete_playlist_Click(object sender, EventArgs e)
    {
        Form3 form3 = new Form3();
        form3.ShowDialog();
        if (Datamanager.Instance.playlistDelete)
        {
            loadLists();

        }
    }

    private void loadLists()
    {
        Datamanager.Instance.loadLists();
        tscb_audio.Items.Clear();
        tscb_video.Items.Clear();
        tscb_album.Items.Clear();
        lv_playlist.Items.Clear();
        if (Datamanager.Instance.audios != null)
        {
            foreach (Audiolist audio in Datamanager.Instance.audios)
            {
                tscb_audio.Items.Add(audio.playlistname);
            }
        }
        if (Datamanager.Instance.videos != null)
        {
            foreach (Videolist video in Datamanager.Instance.videos)
            {
                tscb_video.Items.Add(video.playlistname);
            }
        }
        if (Datamanager.Instance.images != null)
        {
            foreach (Imagelist image in Datamanager.Instance.images)
            {
                tscb_album.Items.Add(image.playlistname);
            }
        }
    }

    private void btn_previous_Click(object sender, EventArgs e)
    {
        if (lv_playlist.Items.Count > 0)
        {
            if (lb_listtype.Text.Equals("Audio"))
            {
                if (lv_playlist.SelectedItems.Count > 0)
                {
                    if (lv_playlist.SelectedItems[0].Index > 0)
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index - 1].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[lv_playlist.SelectedItems[0].Index - 1].Selected = true;
                    }
                }
            }
            else if (lb_listtype.Text.Equals("Video"))
            {
                if (lv_playlist.SelectedItems.Count > 0)
                {
                    if (lv_playlist.SelectedItems[0].Index > 0)
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index - 1].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[lv_playlist.SelectedItems[0].Index - 1].Selected = true;
                    }
                }
            }
        }
    }

    private void btn_next_Click(object sender, EventArgs e)
    {
        if(lv_playlist.Items.Count > 0)
        {
            Console.WriteLine(lv_playlist.Items.Count);
            if (lb_listtype.Text.Equals("Audio"))
            {
                Console.WriteLine(lb_listtype.Text);
                if (lv_playlist.SelectedItems.Count > 0)
                {Console.WriteLine(lv_playlist.SelectedItems.Count);
                    if (lv_playlist.SelectedItems[0].Index < lv_playlist.Items.Count - 1)
                    {
                        Console.WriteLine(lv_playlist.SelectedItems[0].Index);
                        Console.WriteLine(lv_playlist.Items.Count - 1);
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.audios[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index + 1].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[lv_playlist.SelectedItems[0].Index + 1].Selected = true;
                    }
                }
            }
            else if (lb_listtype.Text.Equals("Video"))
            {
                if (lv_playlist.SelectedItems.Count > 0)
                {
                    if (lv_playlist.SelectedItems[0].Index < lv_playlist.Items.Count - 1)
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Play(new Media(_libVlc, Datamanager.Instance.videos[Convert.ToInt32(lb_listpos.Text)].mediafiles[lv_playlist.SelectedItems[0].Index + 1].filepath, FromType.FromPath));
                        progressTimer.Start();
                        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
                        lv_playlist.Items[lv_playlist.SelectedItems[0].Index + 1].Selected = true;
                    }
                }
            }
        }
    }
}
