namespace Mediaplayer
{
    partial class Form2
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
            btn_rotate = new Button();
            tb_zoom = new TrackBar();
            btn_previous = new Button();
            btn_next = new Button();
            lv_album = new ListView();
            btn_add = new Button();
            btn_remove = new Button();
            ((System.ComponentModel.ISupportInitialize)tb_zoom).BeginInit();
            SuspendLayout();
            // 
            // btn_rotate
            // 
            btn_rotate.Location = new Point(247, 12);
            btn_rotate.Name = "btn_rotate";
            btn_rotate.Size = new Size(36, 36);
            btn_rotate.TabIndex = 0;
            btn_rotate.UseVisualStyleBackColor = true;
            btn_rotate.Click += btn_rotate_Click;
            // 
            // tb_zoom
            // 
            tb_zoom.Location = new Point(12, 12);
            tb_zoom.Maximum = 200;
            tb_zoom.Minimum = 1;
            tb_zoom.Name = "tb_zoom";
            tb_zoom.Size = new Size(229, 56);
            tb_zoom.TabIndex = 1;
            tb_zoom.TickStyle = TickStyle.None;
            tb_zoom.Value = 100;
            tb_zoom.Scroll += trackBar1_Scroll;
            // 
            // btn_previous
            // 
            btn_previous.Location = new Point(17, 90);
            btn_previous.Name = "btn_previous";
            btn_previous.Size = new Size(31, 29);
            btn_previous.TabIndex = 2;
            btn_previous.UseVisualStyleBackColor = true;
            btn_previous.Visible = false;
            btn_previous.Click += btn_previous_Click;
            // 
            // btn_next
            // 
            btn_next.Location = new Point(67, 90);
            btn_next.Name = "btn_next";
            btn_next.Size = new Size(31, 29);
            btn_next.TabIndex = 3;
            btn_next.UseVisualStyleBackColor = true;
            btn_next.Visible = false;
            btn_next.Click += btn_next_Click;
            // 
            // lv_album
            // 
            lv_album.Location = new Point(12, 134);
            lv_album.Name = "lv_album";
            lv_album.Size = new Size(96, 615);
            lv_album.TabIndex = 4;
            lv_album.UseCompatibleStateImageBehavior = false;
            lv_album.Visible = false;
            lv_album.SelectedIndexChanged += lv_album_SelectedIndexChanged;
            lv_album.DoubleClick += lv_album_DoubleClick;
            // 
            // btn_add
            // 
            btn_add.Location = new Point(17, 764);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(31, 29);
            btn_add.TabIndex = 5;
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Visible = false;
            btn_add.Click += btn_add_Click;
            // 
            // btn_remove
            // 
            btn_remove.Enabled = false;
            btn_remove.Location = new Point(67, 764);
            btn_remove.Name = "btn_remove";
            btn_remove.Size = new Size(31, 29);
            btn_remove.TabIndex = 7;
            btn_remove.UseVisualStyleBackColor = true;
            btn_remove.Visible = false;
            btn_remove.Click += btn_remove_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 853);
            Controls.Add(btn_remove);
            Controls.Add(btn_add);
            Controls.Add(lv_album);
            Controls.Add(btn_next);
            Controls.Add(btn_previous);
            Controls.Add(tb_zoom);
            Controls.Add(btn_rotate);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)tb_zoom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_rotate;
        private TrackBar tb_zoom;
        private Button btn_previous;
        private Button btn_next;
        private ListView lv_album;
        private Button btn_add;
        private Button btn_remove;
    }
}