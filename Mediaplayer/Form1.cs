using System;
using System.IO;
using LibVLCSharp.Shared;
using Microsoft.Win32;
using Vlc.DotNet.Forms;
using Vlc.DotNet.Core;
using System.Drawing.Drawing2D;

namespace Mediaplayer;

public partial class Form1 : Form
{
    private LibVLC _libVlc;

    private MediaPlayer _mediaPlayer;

    private System.Windows.Forms.Timer progressTimer;
    public Form1()
    {

        Console.WriteLine(Environment.CurrentDirectory);

        InitializeComponent();

        _libVlc = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVlc);
        videoView1.MediaPlayer = _mediaPlayer;
        // Set the initial volume based on the TrackBar's value
        _mediaPlayer.Volume = tb_.Value;

        // Set up the progress timer
        progressTimer = new System.Windows.Forms.Timer();
        progressTimer.Interval = 500; // Update every 500ms
        progressTimer.Tick += ProgressTimer_Tick;
        progressTimer.Start();

        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        btn_play_stop.AutoSize = true;
        btn_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\stop-24.png");
        btn_stop.AutoSize = true;
        //roundButton(btn_play_stop);

    }



    private void btn_open_file_Click(object sender, EventArgs e)
    {

        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ".mp3",
            Filter = "Audio files (*.mp3;*.wav;*.ogg;*.flac)|*.mp3;*.wav;*.ogg;*.flac|All Files|*.*",
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
            }

            Console.WriteLine($"Media Path: {ofd.FileName}");
            Console.WriteLine(ofd.CheckFileExists);
            Console.WriteLine(ofd.InitialDirectory);

        }
    }

    private void btn_open_image_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ".mp3",
            Filter = "Image Files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|All Files|*.*",
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

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void roundButton(Button btn)
    {
        GraphicsPath p = new GraphicsPath();
        p.AddEllipse(10, 10, btn.Width, btn.Height);
        btn.Region = new Region(p);
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
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
        }
        else
        {
            _mediaPlayer.Play();
            btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\media-pause-24.png");
        }
    }

    private void btn_stop_Click(object sender, EventArgs e)
    {
        _mediaPlayer.Stop();
        btn_play_stop.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\play-24.png");
    }

    private void tb__Scroll(object sender, EventArgs e)
    {
        // Update the media player's volume with the TrackBar's current value.
        _mediaPlayer.Volume = tb_.Value;
    }

    private void ProgressTimer_Tick(object sender, EventArgs e)
    {
        if (_mediaPlayer != null && _mediaPlayer.IsPlaying)
        {
            long totalTime = _mediaPlayer.Length;  // Total length in milliseconds
            long currentTime = _mediaPlayer.Time;    // Current playback time in milliseconds

            // Ensure totalTime is greater than zero to avoid division by zero.
            if (totalTime > 0)
            {
                // Set the progress bar's maximum to the total length of the media.
                pb_track.Maximum = (int)totalTime;

                // Update the progress bar with the current time.
                // Math.Min ensures that Value does not exceed Maximum.
                pb_track.Value = (int)Math.Min(currentTime, totalTime);
            }
        }
    }
}
