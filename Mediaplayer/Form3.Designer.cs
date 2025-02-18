namespace Mediaplayer
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            lv_audio = new ListView();
            lv_video = new ListView();
            lv_album = new ListView();
            btn_delete = new Button();
            lb_audio = new Label();
            lb_video = new Label();
            lb_album = new Label();
            SuspendLayout();
            // 
            // lv_audio
            // 
            lv_audio.Location = new Point(88, 58);
            lv_audio.Name = "lv_audio";
            lv_audio.Size = new Size(151, 273);
            lv_audio.TabIndex = 0;
            lv_audio.UseCompatibleStateImageBehavior = false;
            lv_audio.SelectedIndexChanged += lv_audio_SelectedIndexChanged;
            // 
            // lv_video
            // 
            lv_video.Location = new Point(327, 58);
            lv_video.Name = "lv_video";
            lv_video.Size = new Size(151, 273);
            lv_video.TabIndex = 1;
            lv_video.UseCompatibleStateImageBehavior = false;
            lv_video.SelectedIndexChanged += lv_video_SelectedIndexChanged;
            // 
            // lv_album
            // 
            lv_album.Location = new Point(576, 58);
            lv_album.Name = "lv_album";
            lv_album.Size = new Size(151, 273);
            lv_album.TabIndex = 2;
            lv_album.UseCompatibleStateImageBehavior = false;
            lv_album.SelectedIndexChanged += lv_album_SelectedIndexChanged;
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(357, 377);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(94, 29);
            btn_delete.TabIndex = 3;
            btn_delete.Text = "Delete";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // lb_audio
            // 
            lb_audio.AutoSize = true;
            lb_audio.Location = new Point(140, 9);
            lb_audio.Name = "lb_audio";
            lb_audio.Size = new Size(49, 20);
            lb_audio.TabIndex = 4;
            lb_audio.Text = "Audio";
            // 
            // lb_video
            // 
            lb_video.AutoSize = true;
            lb_video.Location = new Point(376, 9);
            lb_video.Name = "lb_video";
            lb_video.Size = new Size(48, 20);
            lb_video.TabIndex = 5;
            lb_video.Text = "Video";
            // 
            // lb_album
            // 
            lb_album.AutoSize = true;
            lb_album.Location = new Point(627, 9);
            lb_album.Name = "lb_album";
            lb_album.Size = new Size(53, 20);
            lb_album.TabIndex = 6;
            lb_album.Text = "Album";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOrange;
            ClientSize = new Size(800, 450);
            Controls.Add(lb_album);
            Controls.Add(lb_video);
            Controls.Add(lb_audio);
            Controls.Add(btn_delete);
            Controls.Add(lv_album);
            Controls.Add(lv_video);
            Controls.Add(lv_audio);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lv_audio;
        private ListView lv_video;
        private ListView lv_album;
        private Button btn_delete;
        private Label lb_audio;
        private Label lb_video;
        private Label lb_album;
    }
}