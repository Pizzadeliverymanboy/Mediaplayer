using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediaplayer
{
    public partial class Form2 : Form
    {

        private Image myImage;
        private float rotationAngle = 0f;  // in degrees
        private float zoomFactor = 1f;       // 1 = 100%
        public Form2(string path)
        {
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();
            // Enable double buffering for smoother rendering
            this.DoubleBuffered = true;

            // Load the image (ensure the file path is correct)
            myImage = Image.FromFile(path);

            btn_rotate.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\anticlockwise-2-24.png");


            //this.ClientSize = new Size(800, 450);
            //this.Width = 1000;
            //this.Height = 1000;

        }

        // Override the OnPaint method to perform custom drawing.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (myImage != null)
            {
                Graphics g = e.Graphics;
                // Save the current graphics state.
                var state = g.Save();

                // Calculate the center of the form's client area.
                float centerX = this.ClientSize.Width / 2f;
                float centerY = this.ClientSize.Height / 2f;

                // Move the origin to the center of the form.
                g.TranslateTransform(centerX, centerY);

                // Apply rotation and zoom.
                g.RotateTransform(rotationAngle);
                g.ScaleTransform(zoomFactor, zoomFactor);

                // Translate so that the image is drawn centered.
                g.TranslateTransform(-myImage.Width / 2f, -myImage.Height / 2f);

                // Draw the image.
                g.DrawImage(myImage, new Point(0, 0));

                // Restore the graphics state.
                g.Restore(state);
            }
        }



        private void btn_rotate_clockwise_Click(object sender, EventArgs e)
        {
            
        }





        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoomFactor = tb_zoom.Value / 100f;
            Invalidate();  // Redraw the form with the new zoom factor.
        }

        private void btn_rotate_Click(object sender, EventArgs e)
        {
            rotationAngle += 90f;  // Rotate counterclockwise.
            Invalidate();          // Redraw the form.
        }
    }
}

