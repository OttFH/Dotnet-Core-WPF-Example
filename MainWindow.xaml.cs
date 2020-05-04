using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using DotnetCoreWpfExample.Models;

namespace DotnetCoreWpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string videosFileName = "videos.json";

        protected ViewModel Model => (ViewModel)Resources["ViewModel"];

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using Stream fileStream = File.OpenRead(videosFileName);
                YouTubeVideo[] videos = await JsonSerializer.DeserializeAsync<YouTubeVideo[]>(fileStream);

                foreach (YouTubeVideo video in videos) Model.Videos.Add(video);
            }
            catch (FileNotFoundException) { }
        }

        private async void BtnAddId_Click(object sender, RoutedEventArgs e)
        {
            string videoId = tbxNewId.Text;
            if (Model.Videos.Any(video => video.ID == videoId))
            {
                MessageBox.Show("Video already in list", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            FrameworkElement element = (FrameworkElement)sender;
            element.IsEnabled = false;

            YouTubeVideo video = new YouTubeVideo()
            {
                ID = tbxNewId.Text
            };

            try
            {
                await video.LoadOEmbedData();
                int index = Model.Videos.TakeWhile(v => v.OEmbed.title.CompareTo(video.OEmbed.title) < 0).Count();
                Model.Videos.Insert(index, video);

                tbxNewId.Text = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Loading data error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            element.IsEnabled = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string json = JsonSerializer.Serialize<IEnumerable<YouTubeVideo>>(Model.Videos);
            File.WriteAllText(videosFileName, json);
        }

        private async void BtnReloadOEmbed_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.IsEnabled = false;

            YouTubeVideo video = (YouTubeVideo)element.DataContext;

            try
            {
                await video.LoadOEmbedData();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Loading data error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            element.IsEnabled = true;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            YouTubeVideo video = (YouTubeVideo)((FrameworkElement)sender).DataContext;
            string url = "https://www.youtube.com/watch?v=" + video.ID;

            try
            {
                Process.Start(url);
            }
            catch (Exception exc)
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    MessageBox.Show(exc.Message, "Open video error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
