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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            btn_delete.Enabled = false;

            foreach (var playlist in Datamanager.Instance.audios)
            {
                lv_audio.Items.Add(playlist.playlistname);
            }

            foreach (var playlist in Datamanager.Instance.videos)
            {
                lv_video.Items.Add(playlist.playlistname);
            }

            foreach (var playlist in Datamanager.Instance.images)
            {
                lv_album.Items.Add(playlist.playlistname);
            }


        }

        private void lv_audio_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv_video.SelectedItems.Clear();
            lv_album.SelectedItems.Clear();
            if (lv_audio.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;

            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        private void lv_video_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv_audio.SelectedItems.Clear();
            lv_album.SelectedItems.Clear();
            if (lv_video.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;

            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        private void lv_album_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv_audio.SelectedItems.Clear();
            lv_video.SelectedItems.Clear();
            if (lv_album.SelectedItems.Count > 0)
            {
                btn_delete.Enabled = true;

            }
            else
            {
                btn_delete.Enabled = false;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            Console.WriteLine(lv_audio.SelectedItems.Count);
            Console.WriteLine(lv_video.SelectedItems.Count);
            Console.WriteLine(lv_album.SelectedItems.Count);
            if (lv_audio.SelectedItems.Count > 0)
            {
                Datamanager.Instance.deletePlaylist(Datamanager.Instance.audios[lv_audio.SelectedItems[0].Index].playlistid);
                
            }
            else if (lv_video.SelectedItems.Count > 0)
            {
                Datamanager.Instance.deletePlaylist(Datamanager.Instance.videos[lv_video.SelectedItems[0].Index].playlistid);
            }
            else if (lv_album.SelectedItems.Count > 0)
            {
                Datamanager.Instance.deletePlaylist(Datamanager.Instance.images[lv_album.SelectedItems[0].Index].playlistid);
            }
            Datamanager.Instance.loadLists();
            lv_audio.Items.Clear();
            lv_video.Items.Clear();
            lv_album.Items.Clear();
            foreach (var playlist in Datamanager.Instance.audios)
            {
                lv_audio.Items.Add(playlist.playlistname);
            }
            foreach (var playlist in Datamanager.Instance.videos)
            {
                lv_video.Items.Add(playlist.playlistname);
            }
            foreach (var playlist in Datamanager.Instance.images)
            {
                lv_album.Items.Add(playlist.playlistname);
            }
            btn_delete.Enabled = false;
        }
    }
}
