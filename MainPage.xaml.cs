using System;
using System.Collections.Generic;
using System.IO;

namespace CzytaniePliku
{
    public partial class MainPage : ContentPage
    {
        private int current = 1;
        private int max = 1;
        struct Album
        {
            public string artist;
            public string album;
            public int songsNumber;
            public int year;
            public long downloadNumber;
        }
        List<Album> albums = new List<Album>();

        public MainPage()
        {
            InitializeComponent();
            LoadMauiAsset();    // Wczytanie pliku
            
        }
        private void UpdateAlbum()
        {
            AlbumLabel.Text = albums[current].album;
            ArtistLabel.Text = albums[current].artist;
            IleKiedyLabel.Text = $"{albums[current].songsNumber.ToString()} utworów   {albums[current].year.ToString()}";
            DownloadLabel.Text = albums[current].downloadNumber.ToString();
        }
        private void OnDalejButtonClicked(object sender, EventArgs e)
        {
            current++;
            if (current >= max)
                current = 0;
            UpdateAlbum();
        }
        private void OnWstaczButtonClicked(object sender, EventArgs e)
        {
            current--;
            if (current < 0)
                current = max - 1;
            UpdateAlbum();
        }
        private void OnPobierzBtnClicked(object sender, EventArgs e)
        {
            Album tmpA = albums[current];
            tmpA.downloadNumber++;
            albums[current] = tmpA;
            UpdateAlbum();
        }
            async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("Data.txt");
            using var reader = new StreamReader(stream);

            var contents = await reader.ReadToEndAsync();

            string[] lines = contents.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                Album a;

                a.artist = lines[i].Trim();
                a.album = lines[++i].Trim().Trim('"');
                a.songsNumber = int.Parse(lines[++i].Trim());
                a.year = int.Parse(lines[++i].Trim());
                a.downloadNumber = long.Parse(lines[++i].Trim());

                albums.Add(a);
            }
            SomeLabel.Text = $"Album 2: {albums[1].artist}";
            max = albums.Count;
            
        }

    
    }
}