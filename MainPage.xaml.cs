using System;
using System.Collections.Generic;
using System.IO;

namespace CzytaniePliku
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        // Struktura przechowująca dane albumu
        struct Album
        {
            public string artist;
            public string album;
            public int songsNumber;
            public int year;
            public long downloadNumber;
        }

        // Lista albumów
        List<Album> albums = new List<Album>();

        public MainPage()
        {
            InitializeComponent();

            // Wczytanie pliku
            LoadMauiAsset();
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

                a.artist = lines[i].Trim().TrimStart('\uFEFF');
                a.album = lines[++i].Trim().Trim('"');
                a.songsNumber = int.Parse(lines[++i].Trim());
                a.year = int.Parse(lines[++i].Trim());
                a.downloadNumber = long.Parse(lines[++i].Trim());

                albums.Add(a);
            }
            SomeLabel.Text = $"Album 1: {albums[1].album}";
        }

    
    }
}