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
            ((System.ComponentModel.ISupportInitialize)tb_zoom).BeginInit();
            SuspendLayout();
            // 
            // btn_rotate
            // 
            btn_rotate.Location = new Point(12, 12);
            btn_rotate.Name = "btn_rotate";
            btn_rotate.Size = new Size(36, 36);
            btn_rotate.TabIndex = 0;
            btn_rotate.UseVisualStyleBackColor = true;
            btn_rotate.Click += btn_rotate_Click;
            // 
            // tb_zoom
            // 
            tb_zoom.Dock = DockStyle.Right;
            tb_zoom.Location = new Point(699, 0);
            tb_zoom.Maximum = 300;
            tb_zoom.Minimum = 1;
            tb_zoom.Name = "tb_zoom";
            tb_zoom.Size = new Size(101, 450);
            tb_zoom.TabIndex = 1;
            tb_zoom.TickStyle = TickStyle.None;
            tb_zoom.Value = 1;
            tb_zoom.Scroll += trackBar1_Scroll;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}