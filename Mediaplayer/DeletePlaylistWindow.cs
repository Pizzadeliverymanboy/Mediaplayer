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
    // DeletePlaylistWindow provides a UI for deleting playlists.
    public partial class DeletePlaylistWindow : Form
    {
        // Constructor: Initializes the form, loads playlists into the respective ListViews,
        // and disables the delete button until a playlist is selected.
        public DeletePlaylistWindow()
        {
            InitializeComponent();

            // Initially disable the delete button since no playlist is selected.
            btn_delete.Enabled = false;

            // Populate the audio playlist ListView with playlist names from the Datamanager.
            foreach (var playlist in Datamanager.Instance.audios)
            {
                lv_audio.Items.Add(playlist.playlistname);
            }

            // Populate the video playlist ListView.
            foreach (var playlist in Datamanager.Instance.videos)
            {
                lv_video.Items.Add(playlist.playlistname);
            }

            // Populate the image (album) playlist ListView.
            foreach (var playlist in Datamanager.Instance.images)
            {
                lv_album.Items.Add(playlist.playlistname);
            }
        }

        // Event handler for when an audio playlist is selected.
        private void lv_audio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear selections in the video and image ListViews to ensure only one playlist is selected.
            lv_video.SelectedItems.Clear();
            lv_album.SelectedItems.Clear();

            // Enable the delete button if an audio playlist is selected; otherwise disable it.
            if (lv_audio.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;
            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        // Event handler for when a video playlist is selected.
        private void lv_video_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear selections in the audio and image ListViews.
            lv_audio.SelectedItems.Clear();
            lv_album.SelectedItems.Clear();

            // Enable the delete button if a video playlist is selected.
            if (lv_video.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;
            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        // Event handler for when an image (album) playlist is selected.
        private void lv_album_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear selections in the audio and video ListViews.
            lv_audio.SelectedItems.Clear();
            lv_video.SelectedItems.Clear();

            // Enable the delete button if an image playlist is selected.
            if (lv_album.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;
            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        // Event handler for when the delete button is clicked.
        private void btn_delete_Click(object sender, EventArgs e)
        {

            // Check which ListView has an active selection and delete the corresponding playlist.
            if (lv_audio.SelectedItems.Count > 0)
            {
                // Delete the selected audio playlist using its playlist ID.
                Datamanager.Instance.DeletePlaylist(Datamanager.Instance.audios[lv_audio.SelectedItems[0].Index].playlistid);
            }
            else if (lv_video.SelectedItems.Count > 0)
            {
                // Delete the selected video playlist.
                Datamanager.Instance.DeletePlaylist(Datamanager.Instance.videos[lv_video.SelectedItems[0].Index].playlistid);
            }
            else if (lv_album.SelectedItems.Count > 0)
            {
                // Delete the selected image (album) playlist.
                Datamanager.Instance.DeletePlaylist(Datamanager.Instance.images[lv_album.SelectedItems[0].Index].playlistid);
            }

            // Reload the playlists from the data manager.
            Datamanager.Instance.LoadLists();

            // Clear all ListViews to prepare for repopulation.
            lv_audio.Items.Clear();
            lv_video.Items.Clear();
            lv_album.Items.Clear();

            // Repopulate the audio ListView with the updated playlist data.
            foreach (var playlist in Datamanager.Instance.audios)
            {
                lv_audio.Items.Add(playlist.playlistname);
            }
            // Repopulate the video ListView.
            foreach (var playlist in Datamanager.Instance.videos)
            {
                lv_video.Items.Add(playlist.playlistname);
            }
            // Repopulate the image (album) ListView.
            foreach (var playlist in Datamanager.Instance.images)
            {
                lv_album.Items.Add(playlist.playlistname);
            }

            // Disable the delete button again now that no playlist is selected.
            btn_delete.Enabled = false;
        }
    }
}

