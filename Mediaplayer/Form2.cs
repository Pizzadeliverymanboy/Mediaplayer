using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediaplayer.Playlists;
using TagLib;

namespace Mediaplayer
{
    public partial class Form2 : Form
    {
        private int imagelistpos;
        private Image myImage;
        private float rotationAngle = 0f;  // in degrees
        private float zoomFactor = 1f;       // 1 = 100%
        public Form2(string path)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

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

        public Form2(Imagelist list, int imagelistpos)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.imagelistpos = imagelistpos;

            InitializeComponent();
            // Enable double buffering for smoother rendering
            this.DoubleBuffered = true;
            btn_rotate.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\anticlockwise-2-24.png");
            btn_add.Visible = true;
            btn_add.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\add-file-24.png");
            btn_add.AutoSize = true;
            btn_remove.Visible = true;
            btn_remove.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\x-mark-3-24.png");
            btn_remove.AutoSize = true;
            btn_next.Visible = true;
            btn_next.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\next-24.png");
            btn_next.AutoSize = true;
            btn_previous.Visible = true;
            btn_previous.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\previous-24.png");
            btn_previous.AutoSize = true;
            lv_album.Visible = true;

            foreach (Mediafile file in list.mediafiles)
            {
                lv_album.Items.Add(file.filename);
            }
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

        private void btn_previous_Click(object sender, EventArgs e)
        {
            if (lv_album.Items.Count > 0)
            {
                if (lv_album.SelectedItems.Count > 0)
                {
                    int index = lv_album.SelectedItems[0].Index - 1;

                    if (index > -1)
                    {
                        try
                        {
                            // Dispose the old image if necessary.
                            if (myImage != null)
                            {
                                myImage.Dispose();
                            }
                            // Load the image.
                            myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[index].filepath);
                            // Force the form to redraw, which will call OnPaint.
                            this.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                        lv_album.Items[index + 1].Selected = false;
                        lv_album.Items[index].Selected = true;
                    }
                }
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (lv_album.Items.Count > 0)
            {
                if (lv_album.SelectedItems.Count > 0)
                {
                    int index = lv_album.SelectedItems[0].Index + 1;

                    if (index < lv_album.Items.Count)
                    {
                        try
                        {
                            // Dispose the old image if necessary.
                            if (myImage != null)
                            {
                                myImage.Dispose();
                            }
                            // Load the image.
                            myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[index].filepath);
                            // Force the form to redraw, which will call OnPaint.
                            this.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                        lv_album.Items[index - 1].Selected = false;
                        lv_album.Items[index].Selected = true;
                    }
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
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
                Datamanager.Instance.addFile(ofd.SafeFileName, ofd.FileName, "Image", Datamanager.Instance.images[Convert.ToInt32(imagelistpos)].playlistid);
                MessageBox.Show("File added");
                lv_album.Items.Clear();
                Datamanager.Instance.loadLists();
                foreach (Mediafile file in Datamanager.Instance.images[Convert.ToInt32(imagelistpos)].mediafiles)
                {
                    lv_album.Items.Add(file.filename);
                }

            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].fileid);
            Datamanager.Instance.deleteFile(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].fileid);
            lv_album.Items.Clear();
            Datamanager.Instance.loadLists();
            foreach (Mediafile file in Datamanager.Instance.images[imagelistpos].mediafiles)
            {

                lv_album.Items.Add(file.filename);
            }
        }

        private void lv_album_DoubleClick(object sender, EventArgs e)
        {
            if (lv_album.SelectedItems.Count > 0)
            {

                try
                {
                    // Dispose the old image if necessary.
                    if (myImage != null)
                    {
                        myImage.Dispose();
                    }
                    // Load the image.
                    myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filepath);

                    var tagFile = TagLib.File.Create(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filepath);
                    
                    // Force the form to redraw, which will call OnPaint.
                    this.Invalidate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }

                
            }
        }

        private void lv_album_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_album.SelectedItems.Count > 0)
            {
                btn_remove.Enabled = true;

            }
            else
            {
                btn_remove.Enabled = false;
            }
        }

        private void displayMeta(string filepath)
        {

        }
    }
}

