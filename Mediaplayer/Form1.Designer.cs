namespace Mediaplayer;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        videoView1 = new LibVLCSharp.WinForms.VideoView();
        gb_playbuttons = new GroupBox();
        btn_next = new Button();
        btn_previous = new Button();
        btn_stop = new Button();
        btn_play_stop = new Button();
        tb_ = new TrackBar();
        tb_progress = new TrackBar();
        lv_playlist = new ListView();
        menuStrip1 = new MenuStrip();
        tsmi_media = new ToolStripMenuItem();
        tsmi_file = new ToolStripMenuItem();
        tsmi_audio = new ToolStripMenuItem();
        tsmi_video = new ToolStripMenuItem();
        tsmi_image = new ToolStripMenuItem();
        toolStripMenuItem3 = new ToolStripMenuItem();
        tsmi_audiolist = new ToolStripMenuItem();
        tsmitb_audio = new ToolStripTextBox();
        tsmi_videolist = new ToolStripMenuItem();
        tsmitb_video = new ToolStripTextBox();
        ismi_imagelist = new ToolStripMenuItem();
        tsmitb_image = new ToolStripTextBox();
        tsmi_delete_playlist = new ToolStripMenuItem();
        tsmi_ = new ToolStripMenuItem();
        tsmi_audiolists = new ToolStripMenuItem();
        tscb_audio = new ToolStripComboBox();
        tsmi_videolists = new ToolStripMenuItem();
        tscb_video = new ToolStripComboBox();
        tsmi_album = new ToolStripMenuItem();
        tscb_album = new ToolStripComboBox();
        lb_playlistname = new Label();
        lb_current_playlist = new Label();
        lb_playlistid = new Label();
        lb_currenttracktime = new Label();
        lb_trackduration = new Label();
        btn_addfile = new Button();
        pb_audioart = new PictureBox();
        btn_deletefile = new Button();
        lb_fileid = new Label();
        lb_listtype = new Label();
        lb_listpos = new Label();
        ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
        gb_playbuttons.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tb_).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tb_progress).BeginInit();
        menuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pb_audioart).BeginInit();
        SuspendLayout();
        // 
        // videoView1
        // 
        videoView1.BackColor = Color.Black;
        videoView1.Location = new Point(146, 76);
        videoView1.MediaPlayer = null;
        videoView1.Name = "videoView1";
        videoView1.Size = new Size(692, 508);
        videoView1.TabIndex = 3;
        // 
        // gb_playbuttons
        // 
        gb_playbuttons.BackColor = Color.DarkOrange;
        gb_playbuttons.Controls.Add(btn_next);
        gb_playbuttons.Controls.Add(btn_previous);
        gb_playbuttons.Controls.Add(btn_stop);
        gb_playbuttons.Controls.Add(btn_play_stop);
        gb_playbuttons.Location = new Point(283, 695);
        gb_playbuttons.Name = "gb_playbuttons";
        gb_playbuttons.Size = new Size(416, 74);
        gb_playbuttons.TabIndex = 5;
        gb_playbuttons.TabStop = false;
        // 
        // btn_next
        // 
        btn_next.BackColor = Color.Black;
        btn_next.FlatStyle = FlatStyle.Popup;
        btn_next.Location = new Point(266, 26);
        btn_next.Name = "btn_next";
        btn_next.Size = new Size(35, 29);
        btn_next.TabIndex = 4;
        btn_next.UseVisualStyleBackColor = false;
        btn_next.Click += btn_next_Click;
        // 
        // btn_previous
        // 
        btn_previous.BackColor = Color.Black;
        btn_previous.FlatStyle = FlatStyle.Popup;
        btn_previous.Location = new Point(143, 26);
        btn_previous.Name = "btn_previous";
        btn_previous.Size = new Size(35, 29);
        btn_previous.TabIndex = 3;
        btn_previous.UseVisualStyleBackColor = false;
        btn_previous.Click += btn_previous_Click;
        // 
        // btn_stop
        // 
        btn_stop.BackColor = Color.Black;
        btn_stop.FlatStyle = FlatStyle.Popup;
        btn_stop.Location = new Point(225, 26);
        btn_stop.Name = "btn_stop";
        btn_stop.Size = new Size(35, 29);
        btn_stop.TabIndex = 2;
        btn_stop.UseVisualStyleBackColor = false;
        btn_stop.Click += btn_stop_Click;
        // 
        // btn_play_stop
        // 
        btn_play_stop.BackColor = Color.Black;
        btn_play_stop.FlatStyle = FlatStyle.Popup;
        btn_play_stop.Location = new Point(184, 26);
        btn_play_stop.Name = "btn_play_stop";
        btn_play_stop.Size = new Size(35, 29);
        btn_play_stop.TabIndex = 1;
        btn_play_stop.UseVisualStyleBackColor = false;
        btn_play_stop.Click += btn_play_stop_Click;
        // 
        // tb_
        // 
        tb_.BackColor = Color.DarkOrange;
        tb_.LargeChange = 1;
        tb_.Location = new Point(782, 635);
        tb_.Maximum = 100;
        tb_.Name = "tb_";
        tb_.Orientation = Orientation.Vertical;
        tb_.Size = new Size(56, 199);
        tb_.TabIndex = 7;
        tb_.TickStyle = TickStyle.Both;
        tb_.Value = 50;
        tb_.Scroll += tb__Scroll;
        // 
        // tb_progress
        // 
        tb_progress.Location = new Point(146, 600);
        tb_progress.Maximum = 100;
        tb_progress.Name = "tb_progress";
        tb_progress.Size = new Size(692, 56);
        tb_progress.TabIndex = 8;
        tb_progress.TickStyle = TickStyle.None;
        tb_progress.MouseDown += tb_progress_MouseDown;
        tb_progress.MouseUp += tb_progress_MouseUp;
        // 
        // lv_playlist
        // 
        lv_playlist.FullRowSelect = true;
        lv_playlist.GridLines = true;
        lv_playlist.Location = new Point(922, 73);
        lv_playlist.Name = "lv_playlist";
        lv_playlist.Size = new Size(275, 514);
        lv_playlist.TabIndex = 9;
        lv_playlist.UseCompatibleStateImageBehavior = false;
        lv_playlist.View = View.Details;
        lv_playlist.SelectedIndexChanged += lv_playlist_SelectedIndexChanged;
        lv_playlist.DoubleClick += lv_playlist_DoubleClick;
        // 
        // menuStrip1
        // 
        menuStrip1.ImageScalingSize = new Size(20, 20);
        menuStrip1.Items.AddRange(new ToolStripItem[] { tsmi_media, tsmi_ });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1262, 28);
        menuStrip1.TabIndex = 10;
        menuStrip1.Text = "menuStrip1";
        // 
        // tsmi_media
        // 
        tsmi_media.DropDownItems.AddRange(new ToolStripItem[] { tsmi_file, toolStripMenuItem3, tsmi_delete_playlist });
        tsmi_media.Name = "tsmi_media";
        tsmi_media.Size = new Size(65, 24);
        tsmi_media.Text = "Media";
        // 
        // tsmi_file
        // 
        tsmi_file.DropDownItems.AddRange(new ToolStripItem[] { tsmi_audio, tsmi_video, tsmi_image });
        tsmi_file.Name = "tsmi_file";
        tsmi_file.Size = new Size(198, 26);
        tsmi_file.Text = "Datei öffnen";
        // 
        // tsmi_audio
        // 
        tsmi_audio.Name = "tsmi_audio";
        tsmi_audio.Size = new Size(134, 26);
        tsmi_audio.Text = "Audio";
        tsmi_audio.Click += tsmi_audio_Click;
        // 
        // tsmi_video
        // 
        tsmi_video.Name = "tsmi_video";
        tsmi_video.Size = new Size(134, 26);
        tsmi_video.Text = "Video";
        tsmi_video.Click += tsmi_video_Click;
        // 
        // tsmi_image
        // 
        tsmi_image.Name = "tsmi_image";
        tsmi_image.Size = new Size(134, 26);
        tsmi_image.Text = "Image";
        tsmi_image.Click += tsmi_image_Click;
        // 
        // toolStripMenuItem3
        // 
        toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { tsmi_audiolist, tsmi_videolist, ismi_imagelist });
        toolStripMenuItem3.Name = "toolStripMenuItem3";
        toolStripMenuItem3.Size = new Size(198, 26);
        toolStripMenuItem3.Text = "Playlist erstellen";
        // 
        // tsmi_audiolist
        // 
        tsmi_audiolist.DropDownItems.AddRange(new ToolStripItem[] { tsmitb_audio });
        tsmi_audiolist.Name = "tsmi_audiolist";
        tsmi_audiolist.Size = new Size(134, 26);
        tsmi_audiolist.Text = "Audio";
        // 
        // tsmitb_audio
        // 
        tsmitb_audio.Name = "tsmitb_audio";
        tsmitb_audio.Size = new Size(100, 27);
        tsmitb_audio.KeyUp += tsmitb_audio_KeyUp;
        // 
        // tsmi_videolist
        // 
        tsmi_videolist.DropDownItems.AddRange(new ToolStripItem[] { tsmitb_video });
        tsmi_videolist.Name = "tsmi_videolist";
        tsmi_videolist.Size = new Size(134, 26);
        tsmi_videolist.Text = "Video";
        // 
        // tsmitb_video
        // 
        tsmitb_video.Name = "tsmitb_video";
        tsmitb_video.Size = new Size(100, 27);
        tsmitb_video.KeyUp += tsmitb_video_KeyUp;
        // 
        // ismi_imagelist
        // 
        ismi_imagelist.DropDownItems.AddRange(new ToolStripItem[] { tsmitb_image });
        ismi_imagelist.Name = "ismi_imagelist";
        ismi_imagelist.Size = new Size(134, 26);
        ismi_imagelist.Text = "Image";
        // 
        // tsmitb_image
        // 
        tsmitb_image.Name = "tsmitb_image";
        tsmitb_image.Size = new Size(100, 27);
        tsmitb_image.KeyUp += tsmitb_image_KeyUp;
        // 
        // tsmi_delete_playlist
        // 
        tsmi_delete_playlist.Name = "tsmi_delete_playlist";
        tsmi_delete_playlist.Size = new Size(198, 26);
        tsmi_delete_playlist.Text = "Playlist löschen";
        tsmi_delete_playlist.Click += tsmi_delete_playlist_Click;
        // 
        // tsmi_
        // 
        tsmi_.DropDownItems.AddRange(new ToolStripItem[] { tsmi_audiolists, tsmi_videolists, tsmi_album });
        tsmi_.Name = "tsmi_";
        tsmi_.Size = new Size(75, 24);
        tsmi_.Text = "Playlists";
        // 
        // tsmi_audiolists
        // 
        tsmi_audiolists.DropDownItems.AddRange(new ToolStripItem[] { tscb_audio });
        tsmi_audiolists.Name = "tsmi_audiolists";
        tsmi_audiolists.Size = new Size(151, 26);
        tsmi_audiolists.Text = "Audiolist";
        // 
        // tscb_audio
        // 
        tscb_audio.DropDownStyle = ComboBoxStyle.Simple;
        tscb_audio.Name = "tscb_audio";
        tscb_audio.Size = new Size(121, 150);
        tscb_audio.SelectedIndexChanged += tscb_audio_SelectedIndexChanged;
        // 
        // tsmi_videolists
        // 
        tsmi_videolists.DropDownItems.AddRange(new ToolStripItem[] { tscb_video });
        tsmi_videolists.Name = "tsmi_videolists";
        tsmi_videolists.Size = new Size(151, 26);
        tsmi_videolists.Text = "Videolist";
        // 
        // tscb_video
        // 
        tscb_video.DropDownStyle = ComboBoxStyle.Simple;
        tscb_video.Name = "tscb_video";
        tscb_video.Size = new Size(121, 150);
        tscb_video.SelectedIndexChanged += tscb_video_SelectedIndexChanged;
        // 
        // tsmi_album
        // 
        tsmi_album.DropDownItems.AddRange(new ToolStripItem[] { tscb_album });
        tsmi_album.Name = "tsmi_album";
        tsmi_album.Size = new Size(151, 26);
        tsmi_album.Text = "Album";
        // 
        // tscb_album
        // 
        tscb_album.DropDownStyle = ComboBoxStyle.Simple;
        tscb_album.Name = "tscb_album";
        tscb_album.Size = new Size(121, 150);
        tscb_album.SelectedIndexChanged += tscb_album_SelectedIndexChanged;
        // 
        // lb_playlistname
        // 
        lb_playlistname.AutoSize = true;
        lb_playlistname.Location = new Point(1026, 50);
        lb_playlistname.Name = "lb_playlistname";
        lb_playlistname.Size = new Size(0, 20);
        lb_playlistname.TabIndex = 11;
        lb_playlistname.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lb_current_playlist
        // 
        lb_current_playlist.AutoSize = true;
        lb_current_playlist.Location = new Point(1026, 50);
        lb_current_playlist.Name = "lb_current_playlist";
        lb_current_playlist.Size = new Size(55, 20);
        lb_current_playlist.TabIndex = 12;
        lb_current_playlist.Text = "Playlist";
        // 
        // lb_playlistid
        // 
        lb_playlistid.AutoSize = true;
        lb_playlistid.Location = new Point(1212, 28);
        lb_playlistid.Name = "lb_playlistid";
        lb_playlistid.Size = new Size(69, 20);
        lb_playlistid.TabIndex = 13;
        lb_playlistid.Text = "playlistid";
        lb_playlistid.Visible = false;
        // 
        // lb_currenttracktime
        // 
        lb_currenttracktime.AutoSize = true;
        lb_currenttracktime.Location = new Point(104, 600);
        lb_currenttracktime.Name = "lb_currenttracktime";
        lb_currenttracktime.Size = new Size(36, 20);
        lb_currenttracktime.TabIndex = 14;
        lb_currenttracktime.Text = "--:--";
        // 
        // lb_trackduration
        // 
        lb_trackduration.AutoSize = true;
        lb_trackduration.Location = new Point(844, 600);
        lb_trackduration.Name = "lb_trackduration";
        lb_trackduration.Size = new Size(36, 20);
        lb_trackduration.TabIndex = 15;
        lb_trackduration.Text = "--:--";
        // 
        // btn_addfile
        // 
        btn_addfile.BackColor = Color.Black;
        btn_addfile.Enabled = false;
        btn_addfile.FlatStyle = FlatStyle.Popup;
        btn_addfile.Location = new Point(967, 596);
        btn_addfile.Name = "btn_addfile";
        btn_addfile.Size = new Size(31, 29);
        btn_addfile.TabIndex = 16;
        btn_addfile.UseVisualStyleBackColor = false;
        btn_addfile.Click += btn_addfile_Click;
        // 
        // pb_audioart
        // 
        pb_audioart.BackColor = Color.OrangeRed;
        pb_audioart.Location = new Point(146, 76);
        pb_audioart.Name = "pb_audioart";
        pb_audioart.Size = new Size(692, 508);
        pb_audioart.SizeMode = PictureBoxSizeMode.Zoom;
        pb_audioart.TabIndex = 17;
        pb_audioart.TabStop = false;
        pb_audioart.Visible = false;
        // 
        // btn_deletefile
        // 
        btn_deletefile.BackColor = Color.Black;
        btn_deletefile.Enabled = false;
        btn_deletefile.FlatStyle = FlatStyle.Popup;
        btn_deletefile.Location = new Point(1109, 596);
        btn_deletefile.Name = "btn_deletefile";
        btn_deletefile.Size = new Size(31, 29);
        btn_deletefile.TabIndex = 18;
        btn_deletefile.TabStop = false;
        btn_deletefile.UseVisualStyleBackColor = false;
        btn_deletefile.Click += btn_deletefile_Click;
        // 
        // lb_fileid
        // 
        lb_fileid.AutoSize = true;
        lb_fileid.Location = new Point(1147, 28);
        lb_fileid.Name = "lb_fileid";
        lb_fileid.Size = new Size(43, 20);
        lb_fileid.TabIndex = 19;
        lb_fileid.Text = "fileid";
        lb_fileid.Visible = false;
        // 
        // lb_listtype
        // 
        lb_listtype.AutoSize = true;
        lb_listtype.Location = new Point(1212, 48);
        lb_listtype.Name = "lb_listtype";
        lb_listtype.Size = new Size(57, 20);
        lb_listtype.TabIndex = 20;
        lb_listtype.Text = "listtype";
        lb_listtype.Visible = false;
        // 
        // lb_listpos
        // 
        lb_listpos.AutoSize = true;
        lb_listpos.Location = new Point(1147, 48);
        lb_listpos.Name = "lb_listpos";
        lb_listpos.Size = new Size(41, 20);
        lb_listpos.TabIndex = 21;
        lb_listpos.Text = "listid";
        lb_listpos.Visible = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.DarkOrange;
        ClientSize = new Size(1262, 873);
        Controls.Add(lb_listpos);
        Controls.Add(lb_listtype);
        Controls.Add(lb_fileid);
        Controls.Add(btn_deletefile);
        Controls.Add(pb_audioart);
        Controls.Add(btn_addfile);
        Controls.Add(lb_trackduration);
        Controls.Add(lb_currenttracktime);
        Controls.Add(lb_playlistid);
        Controls.Add(lb_current_playlist);
        Controls.Add(lb_playlistname);
        Controls.Add(lv_playlist);
        Controls.Add(tb_);
        Controls.Add(gb_playbuttons);
        Controls.Add(videoView1);
        Controls.Add(tb_progress);
        Controls.Add(menuStrip1);
        Cursor = Cursors.Hand;
        Name = "Form1";
        Text = "Mediaplayer";
        ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
        gb_playbuttons.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)tb_).EndInit();
        ((System.ComponentModel.ISupportInitialize)tb_progress).EndInit();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pb_audioart).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.ListView lv_playlist;

    private System.Windows.Forms.TrackBar tb_progress;

    

    #endregion

    private LibVLCSharp.WinForms.VideoView videoView1;
    private System.Windows.Forms.GroupBox gb_playbuttons;
    private Button btn_play_stop;
    private Button btn_next;
    private Button btn_previous;
    private Button btn_stop;
    private System.Windows.Forms.TrackBar tb_;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem tsmi_media;
    private ToolStripMenuItem tsmi_file;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem tsmi_delete_playlist;
    private ToolStripMenuItem tsmi_audio;
    private ToolStripMenuItem tsmi_video;
    private ToolStripMenuItem tsmi_image;
    private ToolStripMenuItem tsmi_audiolist;
    private ToolStripMenuItem tsmi_videolist;
    private ToolStripMenuItem ismi_imagelist;
    private ToolStripTextBox tsmitb_audio;
    private ToolStripTextBox tsmitb_video;
    private ToolStripTextBox tsmitb_image;
    private ToolStripMenuItem tsmi_;
    private ToolStripMenuItem tsmi_audiolists;
    private ToolStripMenuItem tsmi_videolists;
    private ToolStripMenuItem tsmi_album;
    private ToolStripComboBox tscb_audio;
    private ToolStripComboBox tscb_video;
    private ToolStripComboBox tscb_album;
    private Label lb_playlistname;
    private System.Windows.Forms.Label lb_current_playlist;
    private Label lb_playlistid;
    private Label lb_currenttracktime;
    private Label lb_trackduration;
    private Button btn_addfile;
    private PictureBox pb_audioart;
    private Button btn_deletefile;
    private Label lb_fileid;
    private Label lb_listtype;
    private Label lb_listpos;
}