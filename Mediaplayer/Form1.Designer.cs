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
        btn_open_file = new Button();
        btn_open_image = new Button();
        videoView1 = new LibVLCSharp.WinForms.VideoView();
        groupBox1 = new GroupBox();
        button3 = new Button();
        button2 = new Button();
        btn_stop = new Button();
        btn_play_stop = new Button();
        pb_track = new ProgressBar();
        tb_ = new TrackBar();
        ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tb_).BeginInit();
        SuspendLayout();
        // 
        // btn_open_file
        // 
        btn_open_file.Location = new Point(40, 76);
        btn_open_file.Name = "btn_open_file";
        btn_open_file.Size = new Size(82, 56);
        btn_open_file.TabIndex = 1;
        btn_open_file.Text = "Media Öffnen";
        btn_open_file.UseVisualStyleBackColor = true;
        btn_open_file.Click += btn_open_file_Click;
        // 
        // btn_open_image
        // 
        btn_open_image.Location = new Point(40, 260);
        btn_open_image.Name = "btn_open_image";
        btn_open_image.Size = new Size(82, 52);
        btn_open_image.TabIndex = 2;
        btn_open_image.Text = "Bild Öffnen";
        btn_open_image.UseVisualStyleBackColor = true;
        btn_open_image.Click += btn_open_image_Click;
        // 
        // videoView1
        // 
        videoView1.BackColor = Color.Black;
        videoView1.Location = new Point(198, 76);
        videoView1.MediaPlayer = null;
        videoView1.Name = "videoView1";
        videoView1.Size = new Size(416, 236);
        videoView1.TabIndex = 3;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(button3);
        groupBox1.Controls.Add(button2);
        groupBox1.Controls.Add(btn_stop);
        groupBox1.Controls.Add(btn_play_stop);
        groupBox1.Location = new Point(198, 366);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(416, 72);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "groupBox1";
        // 
        // button3
        // 
        button3.BackColor = Color.Black;
        button3.FlatStyle = FlatStyle.Popup;
        button3.Location = new Point(266, 26);
        button3.Name = "button3";
        button3.Size = new Size(35, 29);
        button3.TabIndex = 4;
        button3.UseVisualStyleBackColor = false;
        // 
        // button2
        // 
        button2.BackColor = Color.Black;
        button2.FlatStyle = FlatStyle.Popup;
        button2.Location = new Point(143, 26);
        button2.Name = "button2";
        button2.Size = new Size(35, 29);
        button2.TabIndex = 3;
        button2.UseVisualStyleBackColor = false;
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
        // pb_track
        // 
        pb_track.Location = new Point(198, 331);
        pb_track.Name = "pb_track";
        pb_track.Size = new Size(416, 29);
        pb_track.TabIndex = 6;
        // 
        // tb_
        // 
        tb_.BackColor = Color.Black;
        tb_.LargeChange = 1;
        tb_.Location = new Point(745, 322);
        tb_.Maximum = 100;
        tb_.Name = "tb_";
        tb_.Orientation = Orientation.Vertical;
        tb_.Size = new Size(56, 130);
        tb_.TabIndex = 7;
        tb_.TickStyle = TickStyle.Both;
        tb_.Value = 50;
        tb_.Scroll += tb__Scroll;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.DarkOrange;
        ClientSize = new Size(800, 450);
        Controls.Add(tb_);
        Controls.Add(pb_track);
        Controls.Add(groupBox1);
        Controls.Add(videoView1);
        Controls.Add(btn_open_image);
        Controls.Add(btn_open_file);
        Cursor = Cursors.Hand;
        Name = "Form1";
        Text = "Mediaplayer";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
        groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)tb_).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btn_open_image;

    private System.Windows.Forms.Button btn_open_file;

    

    #endregion

    private LibVLCSharp.WinForms.VideoView videoView1;
    private GroupBox groupBox1;
    private Button btn_play_stop;
    private Button button3;
    private Button button2;
    private Button btn_stop;
    private ProgressBar pb_track;
    private TrackBar tb_;
}