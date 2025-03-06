using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediaplayer.Playlists;
using TagLib;

namespace Mediaplayer
{
    // Form for displaying images with rotation and zoom functionality.
    public partial class ImageDisplayWindow : Form
    {
        // Index for the current image list.
        private int imagelistpos;
        // The currently displayed image.
        private Image myImage;
        // Rotation angle in degrees (initially 0).
        private float rotationAngle = 0f;
        // Zoom factor (1 = 100%).
        private float zoomFactor = 1f;
        public ImageDisplayWindow(string path)
        {
            // Maximize the form and disable resizing.
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();

            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            // Set the zoom trackbar's value and label text.
            lb_zoom.Text = $"Zoom: {tb_zoom.Value}%";

            // Enable double buffering for smoother rendering.
            this.DoubleBuffered = true;

            // Load the image from the provided path.
            try 
            { 
                myImage = Image.FromFile(path); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
            }
            

            // Set the rotate button's icon.
            btn_rotate.Image = Image.FromFile(Environment.CurrentDirectory.Split("\\bin")[0] + "\\ButtonIcons\\anticlockwise-2-24.png");
            // Set the form's title to the filename.
            this.Text = Path.GetFileName(path) + " " + myImage.Size;

            // Resize the image to fit the form.

            if (myImage.Width >= myImage.Height)
            {
                myImage = ResizeImage(myImage, 500, 390);
            }
            else
            {
                myImage = ResizeImage(myImage, 250, 350);
            }

            



        }

        // Constructor to display an image from a playlist, along with album navigation.
        public ImageDisplayWindow(Imagelist list, int imagelistpos)
        {
            // Set the form to full screen and disable resizing.
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.imagelistpos = imagelistpos;

            InitializeComponent();

            // Set the zoom trackbar's value and label text.
            lb_zoom.Text = $"Zoom: {tb_zoom.Value}%";

            // Enable double buffering for smoother rendering
            this.DoubleBuffered = true;

            // Set up button icons and visibility for album navigation.
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

            // Populate the album ListView with the image filenames.
            foreach (Mediafile file in list.mediafiles)
            {
                lv_album.Items.Add(file.filename);
            }

        }

        // Resize an image to the specified dimensions.
        // This method preserves the image's aspect ratio and resolution.
        // The new image is centered within the specified dimensions.
        public Image ResizeImage(Image originalImage, int newWidth, int newHeight)
        {
            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);

            // Keep the same resolution as the original image.
            destImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

            // Create a graphics object to draw the resized image.
            // Set the graphics properties for high-quality rendering.
            
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Set the wrap mode to TileFlipXY to prevent edge artifacts.
                // Draw the resized image to the destination rectangle.
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(originalImage, destRect, 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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

                // Calculate the base scale to fit the image within the form's client area.
                float baseScale = 1f;
                if (myImage.Width > this.ClientSize.Width || myImage.Height > this.ClientSize.Height)
                {
                    baseScale = Math.Min(
                        (float)this.ClientSize.Width / myImage.Width,
                        (float)this.ClientSize.Height / myImage.Height);
                }

                // The effective scale is the base scale (to fit) multiplied by any user-controlled zoom.
                float effectiveScale = baseScale * zoomFactor;

                // Calculate the center of the form's client area.
                float centerX = this.ClientSize.Width / 2f;
                float centerY = this.ClientSize.Height / 2f;

                // Adjust the center for portrait images.
                if (myImage.Width > myImage.Height)
                {
                    centerY = this.ClientSize.Height / 2f - 100;
                }
                else
                {
                    centerX = this.ClientSize.Width / 2f - 50;
                    centerY = this.ClientSize.Height / 2f - 50;
                }

                // Move the origin to the center of the form.
                g.TranslateTransform(centerX, centerY);

                // Apply rotation and zoom.
                g.RotateTransform(rotationAngle);
                g.ScaleTransform(effectiveScale, effectiveScale);

                // Translate so that the image is drawn centered.
                g.TranslateTransform(-myImage.Width / 2f, -myImage.Height / 2f);

                // Draw the image.
                g.DrawImage(myImage, new Point(0, 0));

                // Restore the graphics state.
                g.Restore(state);
            }
        }


        // Event handler for the zoom trackbar scroll event.
        // Updates the zoom factor based on the trackbar's value and requests a redraw.
        private void tb_zoom_Scroll(object sender, EventArgs e)
        {
            zoomFactor = tb_zoom.Value / 100f;
            lb_zoom.Text = $"Zoom: {tb_zoom.Value}%";
            Invalidate();  // Redraw the form with the updated zoom factor.
        }

        // Event handler for the rotate button click event.
        // Rotates the image by 90 degrees counterclockwise and triggers a redraw.
        private void btn_rotate_Click(object sender, EventArgs e)
        {
            rotationAngle += 90f;
            Invalidate(); // Request the form to repaint with the new rotation angle.
        }

        // Event handler for the previous button click event.
        // Navigates to the previous image in the album.
        private void btn_previous_Click(object sender, EventArgs e)
        {
            if (lv_album.Items.Count > 0)
            {
                if (lv_album.SelectedItems.Count > 0)
                {
                    // Calculate the index of the previous image.
                    int index = lv_album.SelectedItems[0].Index - 1;

                    if (index > -1)
                    {
                        try
                        {
                            // Dispose the current image to free resources.
                            if (myImage != null)
                            {
                                myImage.Dispose();
                            }
                            // Load the previous image from the image playlist.
                            myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[index].filepath);
                            // Update the window title with the new image's filename.
                            this.Text = Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filename + " " + myImage.Size;

                            // Resize the image to fit the form.

                            if (myImage.Width >= myImage.Height)
                            {
                                myImage = ResizeImage(myImage, 500, 390);
                            }
                            else
                            {
                                myImage = ResizeImage(myImage, 250, 350);
                            }

                            // Force the form to redraw with the new image.
                            this.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                        // Update the ListView selection.
                        lv_album.Items[index + 1].Selected = false;
                        lv_album.Items[index].Selected = true;
                    }
                }
            }
        }

        // Event handler for the next button click event.
        // Navigates to the next image in the album.
        private void btn_next_Click(object sender, EventArgs e)
        {
            if (lv_album.Items.Count > 0)
            {
                if (lv_album.SelectedItems.Count > 0)
                {
                    // Calculate the index of the next image.
                    int index = lv_album.SelectedItems[0].Index + 1;

                    if (index < lv_album.Items.Count)
                    {
                        try
                        {
                            // Dispose the current image to free resources.
                            if (myImage != null)
                            {
                                myImage.Dispose();
                            }
                            // Load the next image from the playlist.
                            myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[index].filepath);
                            // Update the window title with the new image's filename.
                            this.Text = Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filename + " " + myImage.Size;

                            // Resize the image to fit the form.

                            if (myImage.Width >= myImage.Height)
                            {
                                myImage = ResizeImage(myImage, 500, 390);
                            }
                            else
                            {
                                myImage = ResizeImage(myImage, 250, 350);
                            }
                            // Force the form to redraw with the new image.
                            this.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                        // Update the ListView selection.
                        lv_album.Items[index - 1].Selected = false;
                        lv_album.Items[index].Selected = true;
                    }
                }
            }
        }

        // Event handler for the add button click event.
        // Opens a dialog for the user to add a new image to the album.
        private void btn_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".jpg",
                Filter = "Image Files (*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                List<Mediafile> removeFilesNotFound = new List<Mediafile>();
                // Add the selected image file to the album using the Datamanager.
                Datamanager.Instance.AddFile(ofd.SafeFileName, ofd.FileName, "Image", Datamanager.Instance.images[Convert.ToInt32(imagelistpos)].playlistid);
                MessageBox.Show("File added");
                // Refresh the album ListView with the updated file list.
                lv_album.Items.Clear();
                Datamanager.Instance.LoadLists();
                foreach (Mediafile file in Datamanager.Instance.images[Convert.ToInt32(imagelistpos)].mediafiles)
                {
                    if (System.IO.File.Exists(file.filepath))
                    {
                        lv_album.Items.Add(file.filename);
                    }
                    else
                    {
                        removeFilesNotFound.Add(file);
                    }
                    
                }
                foreach (Mediafile file in removeFilesNotFound)
                {
                    Datamanager.Instance.images[Convert.ToInt32(imagelistpos)].mediafiles.Remove(file);
                }

            }
        }

        // Event handler for the remove button click event.
        // Removes the selected image from the album.
        private void btn_remove_Click(object sender, EventArgs e)
        {
            List<Mediafile> removeFilesNotFound = new List<Mediafile>();
            // Delete the selected file using the Datamanager.
            Datamanager.Instance.DeleteFile(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].fileid);
            // Refresh the album ListView after deletion.
            lv_album.Items.Clear();
            Datamanager.Instance.LoadLists();
            // Add to Album and remove File from Imagelist if not found
            foreach (Mediafile file in Datamanager.Instance.images[imagelistpos].mediafiles)
            {
                if (System.IO.File.Exists(file.filepath))
                {
                    lv_album.Items.Add(file.filename);
                }
                else
                {
                    removeFilesNotFound.Add(file);
                }
                
            }
            // Remove files that are not found
            foreach (Mediafile file in removeFilesNotFound)
            {
                Datamanager.Instance.images[imagelistpos].mediafiles.Remove(file);   
            }
        }

        // Event handler for double-clicking an item in the album ListView.
        // Loads and displays the selected image.
        private void lv_album_DoubleClick(object sender, EventArgs e)
        {
            if (lv_album.SelectedItems.Count > 0)
            {
                try
                {
                    // Dispose the current image to free resources.
                    if (myImage != null)
                    {
                        myImage.Dispose();
                    }
                    // Load the image corresponding to the selected item.
                    myImage = Image.FromFile(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filepath);
                    // Update the window title with the image's filename.
                    this.Text = Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filename + " " + myImage.Size;
                    // Optionally, metadata can be extracted using TagLib.
                    var tagFile = TagLib.File.Create(Datamanager.Instance.images[imagelistpos].mediafiles[lv_album.SelectedItems[0].Index].filepath);

                    // Resize the image to fit the form.

                    if (myImage.Width >= myImage.Height)
                    {
                        myImage = ResizeImage(myImage, 550, 400);
                    }
                    else
                    {
                        myImage = ResizeImage(myImage, 400, 550);
                    }

                    // Force the form to redraw with the new image.
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
        }

        // Event handler for ListView selection changes.
        // Enables or disables the remove button based on whether an item is selected.
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
    }
}

