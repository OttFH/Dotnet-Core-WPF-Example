using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetCoreWpfExample.Models
{
    public class YouTubeVideo : INotifyPropertyChanged
    {
        private string id, comment;
        private LikeType likeState;
        private OEmbedData oEmbed;

        public string ID
        {
            get => id;
            set
            {
                if (value == id) return;

                id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public LikeType LikeState
        {
            get => likeState;
            set
            {
                if (value == likeState) return;

                likeState = value;
                OnPropertyChanged(nameof(LikeState));
            }
        }

        public string Comment
        {
            get => comment;
            set
            {
                if (value == comment) return;

                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public OEmbedData OEmbed
        {
            get => oEmbed;
            set
            {
                if (value.Equals(oEmbed)) return;

                oEmbed = value;
                OnPropertyChanged(nameof(OEmbed));
            }
        }

        public async Task LoadOEmbedData()
        {
            using HttpClient client = new HttpClient();

            string url = "http://www.youtube.com/oembed?url=https://www.youtube.com/watch?v=" + ID;
            string response = await client.GetStringAsync(url);

            OEmbed = (OEmbedData)JsonSerializer.Deserialize(response, typeof(OEmbedData));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return OEmbed.title ?? ID;
        }
    }
}
