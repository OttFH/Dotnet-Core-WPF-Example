using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DotnetCoreWpfExample.Models
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<YouTubeVideo> videos;

        public ObservableCollection<YouTubeVideo> Videos
        {
            get => videos;
            private set
            {
                if (value == videos) return;

                videos = value;
                OnPropertyChanged(nameof(Videos));
            }
        }

        public ViewModel()
        {
            Videos = new ObservableCollection<YouTubeVideo>();
        }

        public void AddVideoOrderd(YouTubeVideo video)
        {
            int index = Videos.Count(v => v.OEmbed.title.CompareTo(video.OEmbed.title) < 0);
            Videos.Insert(index, video);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
