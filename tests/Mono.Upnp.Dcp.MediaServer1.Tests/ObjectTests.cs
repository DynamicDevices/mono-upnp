// 
// ObjectTests.cs
//  
// Author:
//       Scott Thomas <lunchtimemama@gmail.com>
// 
// Copyright (c) 2010 Scott Thomas
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

using NUnit.Framework;

using Mono.Upnp.Dcp.MediaServer1.ConnectionManager1;
using Mono.Upnp.Dcp.MediaServer1.ContentDirectory1;
using Mono.Upnp.Dcp.MediaServer1.ContentDirectory1.AV;

namespace Mono.Upnp.Dcp.MediaServer1.Tests
{
    [TestFixture]
    public class ObjectTests
    {
        [Test]
        public void ResourceInstantiation ()
        {
            var options = new ResourceOptions ();
            SetResourceOptions (options);
            var resource = new Resource (new Uri ("http://0"), options);
            AssertResource (resource, options);
            AssertResource (resource, resource.GetOptions ());
        }

        static void SetResourceOptions (ResourceOptions options)
        {
            options.Size = 0;
            options.Duration = new TimeSpan (0, 0, 1);
            options.BitRate = 2;
            options.SampleFrequency = 3;
            options.BitsPerSample = 4;
            options.NrAudioChannels = 5;
            options.Resolution = new Resolution (6, 7);
            options.ColorDepth = 8;
            options.ProtocolInfo = new ProtocolInfo ("foo");
            options.Protection = "9";
            options.ImportUri = new Uri ("http://10");
        }

        static void AssertResource (Resource resource, ResourceOptions options)
        {
            Assert.AreEqual (resource.Size, options.Size);
            Assert.AreEqual (resource.Duration, options.Duration);
            Assert.AreEqual (resource.BitRate, options.BitRate);
            Assert.AreEqual (resource.SampleFrequency, options.SampleFrequency);
            Assert.AreEqual (resource.BitsPerSample, options.BitsPerSample);
            Assert.AreEqual (resource.NrAudioChannels, options.NrAudioChannels);
            Assert.AreEqual (resource.Resolution, options.Resolution);
            Assert.AreEqual (resource.ColorDepth, options.ColorDepth);
            Assert.AreEqual (resource.ProtocolInfo, options.ProtocolInfo);
            Assert.AreEqual (resource.Protection, options.Protection);
            Assert.AreEqual (resource.ImportUri, options.ImportUri);
        }

        [Test]
        public void ObjectInstantiation ()
        {
            var options = new ObjectOptions ();
            SetObjectOptions (options);
            var @object = new Mono.Upnp.Dcp.MediaServer1.ContentDirectory1.Object ("-1", "-1", options);
            Assert.AreEqual ("-1", @object.Id);
            AssertObject (@object, options);
            AssertObject (@object, @object.GetOptions ());
        }

        static void SetObjectOptions (ObjectOptions options)
        {
            options.Title = "1";
            options.Creator = "2";
            options.WriteStatus = WriteStatus.Protected;
            options.IsRestricted = true;
            var resource_options = new ResourceOptions ();
            SetResourceOptions (resource_options);
            options.Resources = new[] { new Resource (new Uri ("http://0"), resource_options) };
        }

        static void AssertObject (Mono.Upnp.Dcp.MediaServer1.ContentDirectory1.Object @object, ObjectOptions options)
        {
            Assert.AreEqual (@object.Title, options.Title);
            Assert.AreEqual (@object.Creator, options.Creator);
            Assert.AreEqual (@object.WriteStatus, options.WriteStatus);
            Assert.AreEqual (@object.IsRestricted, options.IsRestricted);
            Assert.IsTrue (@object.Resources.IsReadOnly);
            var object_enumerator = @object.Resources.GetEnumerator ();
            var options_enumerator = options.Resources.GetEnumerator ();
            Assert.IsTrue (object_enumerator.MoveNext ());
            Assert.IsTrue (options_enumerator.MoveNext ());
            AssertResource (object_enumerator.Current, options_enumerator.Current.GetOptions ());
        }

        [Test]
        public void ItemInstantiation ()
        {
            var options = new ItemOptions ();
            SetItemOptions (options);
            var item = new Item ("-1", "-1", options);
            AssertItem (item, options);
            AssertItem (item, item.GetOptions ());
        }

        static void SetItemOptions (ItemOptions options)
        {
            SetObjectOptions (options);
            options.Title = "3";
            options.RefId = "4";
        }

        static void AssertItem (Item item, ItemOptions options)
        {
            AssertObject (item, options);
            Assert.AreEqual (item.RefId, options.RefId);
        }

        [Test]
        public void ContainerInstantiation ()
        {
            var options = new ContainerOptions ();
            SetContainerOptions (options);
            var container = new Container ("-1", "-1", options);
            AssertContainer (container, options);
            AssertContainer (container, container.GetOptions ());
        }

        static void SetContainerOptions (ContainerOptions options)
        {
            SetObjectOptions (options);
            options.Title = "5";
            options.ChildCount = 6;
            options.IsSearchable = true;
            options.SearchClasses = new[] { new ClassReference (false, "foo") };
            options.CreateClasses = new[] { new ClassReference (false, "bar") };
        }

        static void AssertContainer (Container container, ContainerOptions options)
        {
            AssertObject (container, options);
            Assert.AreEqual (container.ChildCount, options.ChildCount);
            Assert.AreEqual (container.IsSearchable, options.IsSearchable);
            Assert.IsTrue (container.SearchClasses.IsReadOnly);
            Assert.IsTrue (container.CreateClasses.IsReadOnly);
            CollectionAssert.AreEqual (container.SearchClasses, options.SearchClasses);
            CollectionAssert.AreEqual (container.CreateClasses, options.CreateClasses);
        }

        [Test]
        public void AlbumInstantiation ()
        {
            var options = new AlbumOptions ();
            SetAlbumOptions (options);
            var album = new Album ("-1", "-1", options);
            AssertAlbum (album, options);
            AssertAlbum (album, album.GetOptions ());
        }

        static void SetAlbumOptions (AlbumOptions options)
        {
            SetContainerOptions (options);
            options.StorageMedium = "7";
            options.LongDescription = "8";
            options.Description = "9";
            options.Date = "10";
            options.Publishers = new[] { "publisher" };
            options.Contributors = new[] { "contributor" };
            options.Relations = new[] { new Uri ("http://relation") };
            options.Rights = new[] { "right" };
        }

        static void AssertAlbum (Album album, AlbumOptions options)
        {
            AssertContainer (album, options);
            Assert.AreEqual (album.StorageMedium, options.StorageMedium);
            Assert.AreEqual (album.LongDescription, options.LongDescription);
            Assert.AreEqual (album.Description, options.Description);
            Assert.AreEqual (album.Date, options.Date);
            Assert.IsTrue (album.Publishers.IsReadOnly);
            Assert.IsTrue (album.Contributors.IsReadOnly);
            Assert.IsTrue (album.Relations.IsReadOnly);
            Assert.IsTrue (album.Rights.IsReadOnly);
            CollectionAssert.AreEqual (album.Publishers, options.Publishers);
            CollectionAssert.AreEqual (album.Contributors, options.Contributors);
            CollectionAssert.AreEqual (album.Relations, options.Relations);
            CollectionAssert.AreEqual (album.Rights, options.Rights);
        }

        [Test]
        public void AudioBookInstantiation ()
        {
            var options = new AudioBookOptions ();
            SetAudioBookOptions (options);
            var audio_book = new AudioBook ("-1", "-1", options);
            AssertAudioBook (audio_book, options);
            AssertAudioBook (audio_book, audio_book.GetOptions ());
        }

        static void SetAudioBookOptions (AudioBookOptions options)
        {
            SetAudioItemOptions (options);
            options.StorageMedium = "13";
            options.Date = "14";
            options.Producers = new[] { "producer" };
            options.Contributors = new[] { "contributor" };
        }

        static void AssertAudioBook (AudioBook audioBook, AudioBookOptions options)
        {
            AssertAudioItem (audioBook, options);
            Assert.AreEqual (audioBook.StorageMedium, options.StorageMedium);
            Assert.AreEqual (audioBook.Date, options.Date);
            Assert.IsTrue (audioBook.Producers.IsReadOnly);
            Assert.IsTrue (audioBook.Contributors.IsReadOnly);
            CollectionAssert.AreEqual (audioBook.Producers, options.Producers);
            CollectionAssert.AreEqual (audioBook.Contributors, options.Contributors);
        }

        [Test]
        public void AudioBroadcastInstantiation ()
        {
            var options = new AudioBroadcastOptions ();
            SetAudioBroadcastOptions (options);
            var audio_broadcast = new AudioBroadcast ("-1", "-1", options);
            AssertAudioBroadcast (audio_broadcast, options);
            AssertAudioBroadcast (audio_broadcast, audio_broadcast.GetOptions ());
        }

        static void SetAudioBroadcastOptions (AudioBroadcastOptions options)
        {
            SetAudioItemOptions (options);
            options.Region = "15";
            options.RadioCallSign = "16";
            options.RadioStationId = "17";
            options.RadioBand = "18";
            options.ChannelNr = 19;
        }

        static void AssertAudioBroadcast (AudioBroadcast audioBroadcast, AudioBroadcastOptions options)
        {
            AssertAudioItem (audioBroadcast, options);
            Assert.AreEqual (audioBroadcast.Region, options.Region);
            Assert.AreEqual (audioBroadcast.RadioCallSign, options.RadioCallSign);
            Assert.AreEqual (audioBroadcast.RadioStationId, options.RadioStationId);
            Assert.AreEqual (audioBroadcast.RadioBand, options.RadioBand);
            Assert.AreEqual (audioBroadcast.ChannelNr, options.ChannelNr);
        }

        [Test]
        public void AudioItemInstantiation ()
        {
            var options = new AudioItemOptions ();
            SetAudioItemOptions (options);
            var audio_item = new AudioItem ("-1", "-1", options);
            AssertAudioItem (audio_item, options);
            AssertAudioItem (audio_item, audio_item.GetOptions ());
        }

        static void SetAudioItemOptions (AudioItemOptions options)
        {
            SetItemOptions (options);
            options.Description = "20";
            options.LongDescription = "21";
            options.Language = "22";
            options.Genres = new[] { "genre" };
            options.Publishers = new[] { "publisher" };
            options.Relations = new[] { new Uri ("http://relation") };
            options.Rights = new[] { "right" };
        }

        static void AssertAudioItem (AudioItem audioItem, AudioItemOptions options)
        {
            AssertItem (audioItem, options);
            Assert.AreEqual (audioItem.Description, options.Description);
            Assert.AreEqual (audioItem.LongDescription, options.LongDescription);
            Assert.AreEqual (audioItem.Language, options.Language);
            Assert.IsTrue (audioItem.Genres.IsReadOnly);
            Assert.IsTrue (audioItem.Publishers.IsReadOnly);
            Assert.IsTrue (audioItem.Relations.IsReadOnly);
            Assert.IsTrue (audioItem.Rights.IsReadOnly);
            CollectionAssert.AreEqual (audioItem.Genres, options.Genres);
            CollectionAssert.AreEqual (audioItem.Publishers, options.Publishers);
            CollectionAssert.AreEqual (audioItem.Relations, options.Relations);
            CollectionAssert.AreEqual (audioItem.Rights, options.Rights);
        }

        [Test]
        public void GenreInstantiation ()
        {
            var options = new GenreOptions ();
            SetGenreOptions (options);
            var genre = new Genre ("-1", "-1", options);
            AssertGenre (genre, options);
            AssertGenre (genre, genre.GetOptions ());
        }

        static void SetGenreOptions (GenreOptions options)
        {
            SetContainerOptions (options);
            options.LongDescription = "23";
            options.Description = "24";
        }

        static void AssertGenre (Genre genre, GenreOptions options)
        {
            AssertContainer (genre, options);
            Assert.AreEqual (genre.LongDescription, options.LongDescription);
            Assert.AreEqual (genre.Description, options.Description);
        }

        [Test]
        public void ImageItemInstantiation ()
        {
            var options = new ImageItemOptions ();
            SetImageItemOptions (options);
            var image_item = new ImageItem ("-1", "-1", options);
            AssertImageItem (image_item, options);
            AssertImageItem (image_item, image_item.GetOptions ());
        }

        static void SetImageItemOptions (ImageItemOptions options)
        {
            SetItemOptions (options);
            options.LongDescription = "25";
            options.Description = "26";
            options.StorageMedium = "27";
            options.Rating = "28";
            options.Date = "29";
            options.Publishers = new[] { "publisher" };
            options.Rights = new[] { "right" };
        }

        static void AssertImageItem (ImageItem imageItem, ImageItemOptions options)
        {
            AssertItem (imageItem, options);
            Assert.AreEqual (imageItem.LongDescription, options.LongDescription);
            Assert.AreEqual (imageItem.Description, options.Description);
            Assert.AreEqual (imageItem.StorageMedium, options.StorageMedium);
            Assert.AreEqual (imageItem.Rating, options.Rating);
            Assert.AreEqual (imageItem.Date, options.Date);
            Assert.IsTrue (imageItem.Publishers.IsReadOnly);
            Assert.IsTrue (imageItem.Rights.IsReadOnly);
            CollectionAssert.AreEqual (imageItem.Publishers, options.Publishers);
            CollectionAssert.AreEqual (imageItem.Rights, options.Rights);
        }

        [Test]
        public void MovieInstantiation ()
        {
            var options = new MovieOptions ();
            SetMovieOptions (options);
            var movie = new Movie ("-1", "-1", options);
            AssertMovie (movie, options);
            AssertMovie (movie, movie.GetOptions ());
        }

        static void SetMovieOptions (MovieOptions options)
        {
            SetVideoItemOptions (options);
            options.StorageMedium = "30";
            options.DvdRegionCode = 31;
            options.ChannelName = "32";
            options.ScheduledStartTimes = new[] { DateTime.Now };
            options.ScheduledEndTimes = new[] { DateTime.Now + new TimeSpan (0, 0, 10) };
        }

        static void AssertMovie (Movie movie, MovieOptions options)
        {
            AssertVideoItem (movie, options);
            Assert.AreEqual (movie.StorageMedium, options.StorageMedium);
            Assert.AreEqual (movie.DvdRegionCode, options.DvdRegionCode);
            Assert.AreEqual (movie.ChannelName, options.ChannelName);
            Assert.IsTrue (movie.ScheduledStartTimes.IsReadOnly);
            Assert.IsTrue (movie.ScheduledEndTimes.IsReadOnly);
            CollectionAssert.AreEqual (movie.ScheduledStartTimes, options.ScheduledStartTimes);
            CollectionAssert.AreEqual (movie.ScheduledEndTimes, options.ScheduledEndTimes);
        }

        [Test]
        public void MusicAlbumInstantiation ()
        {
            var options = new MusicAlbumOptions ();
            SetMusicAlbumOptions (options);
            var music_album = new MusicAlbum ("-1", "-1", options);
            AssertMusicAlbum (music_album, options);
            AssertMusicAlbum (music_album, music_album.GetOptions ());
        }

        static void SetMusicAlbumOptions (MusicAlbumOptions options)
        {
            SetAlbumOptions (options);
            options.Toc = "33";
            options.Artists = new[] { new PersonWithRole ("artist", "role") };
            options.Genres = new[] { "genre" };
            options.Producers = new[] { "genres" };
            options.AlbumArtUris = new[] { new Uri ("http://albumart") };
        }

        static void AssertMusicAlbum (MusicAlbum musicAlbum, MusicAlbumOptions options)
        {
            AssertAlbum (musicAlbum, options);
            Assert.AreEqual (musicAlbum.Toc, options.Toc);
            Assert.IsTrue (musicAlbum.Artists.IsReadOnly);
            Assert.IsTrue (musicAlbum.Genres.IsReadOnly);
            Assert.IsTrue (musicAlbum.Producers.IsReadOnly);
            Assert.IsTrue (musicAlbum.AlbumArtUris.IsReadOnly);
            CollectionAssert.AreEqual (musicAlbum.Artists, options.Artists);
            CollectionAssert.AreEqual (musicAlbum.Genres, options.Genres);
            CollectionAssert.AreEqual (musicAlbum.Producers, options.Producers);
            CollectionAssert.AreEqual (musicAlbum.AlbumArtUris, options.AlbumArtUris);
        }

        [Test]
        public void MusicArtistInstantiation ()
        {
            var options = new MusicArtistOptions ();
            SetMusicArtistOptions (options);
            var music_artist = new MusicArtist ("-1", "-1", options);
            AssertMusicArtist (music_artist, options);
            AssertMusicArtist (music_artist, music_artist.GetOptions ());
        }

        static void SetMusicArtistOptions (MusicArtistOptions options)
        {
            SetPersonOptions (options);
            options.ArtistDiscographyUri = new Uri ("http://34");
        }

        static void AssertMusicArtist (MusicArtist musicArtist, MusicArtistOptions options)
        {
            AssertPerson (musicArtist, options);
        }

        [Test]
        public void MusicTrackInstantiation ()
        {
            var options = new MusicTrackOptions ();
            SetMusicTrackOptions (options);
            var music_track = new MusicTrack ("-1", "-1", options);
            AssertMusicTrack (music_track, options);
            AssertMusicTrack (music_track, music_track.GetOptions ());
        }

        static void SetMusicTrackOptions (MusicTrackOptions options)
        {
            SetAudioItemOptions (options);
            options.AlbumArtUri = new Uri ("http://35");
            options.OriginalTrackNumber = 36;
            options.StorageMedium = "37";
            options.Date = "38";
            options.Artists = new[] { new PersonWithRole ("artist", "role") };
            options.Albums = new[] { "album" };
            options.Playlists = new[] { "playlist" };
            options.Contributors = new[] { "contributor" };
        }

        static void AssertMusicTrack (MusicTrack musicTrack, MusicTrackOptions options)
        {
            AssertAudioItem (musicTrack, options);
            Assert.AreEqual (musicTrack.AlbumArtUri, options.AlbumArtUri);
            Assert.AreEqual (musicTrack.OriginalTrackNumber, options.OriginalTrackNumber);
            Assert.AreEqual (musicTrack.StorageMedium, options.StorageMedium);
            Assert.AreEqual (musicTrack.Date, options.Date);
            Assert.IsTrue (musicTrack.Artists.IsReadOnly);
            Assert.IsTrue (musicTrack.Albums.IsReadOnly);
            Assert.IsTrue (musicTrack.Playlists.IsReadOnly);
            Assert.IsTrue (musicTrack.Contributors.IsReadOnly);
            CollectionAssert.AreEqual (musicTrack.Artists, options.Artists);
            CollectionAssert.AreEqual (musicTrack.Albums, options.Albums);
            CollectionAssert.AreEqual (musicTrack.Playlists, options.Playlists);
            CollectionAssert.AreEqual (musicTrack.Contributors, options.Contributors);
        }

        [Test]
        public void MusicVideoClipInstantiation ()
        {
            var options = new MusicVideoClipOptions ();
            SetMusicVideoClipOptions (options);
            var music_video_clip = new MusicVideoClip ("-1", "-1", options);
            AssertMusicVideoClip (music_video_clip, options);
            AssertMusicVideoClip (music_video_clip, music_video_clip.GetOptions ());
        }

        static void SetMusicVideoClipOptions (MusicVideoClipOptions options)
        {
            SetVideoItemOptions (options);
            options.StorageMedium = "39";
            options.Date = "40";
            options.Artists = new[] { new PersonWithRole ("artist", "role") };
            options.Albums = new[] { "album" };
            options.ScheduledStartTimes = new[] { DateTime.Now };
            options.ScheduledEndTimes = new[] { DateTime.Now + new TimeSpan (0, 0, 10) };
            options.Contributors = new[] { "contributor" };
        }

        static void AssertMusicVideoClip (MusicVideoClip musicVideoClip, MusicVideoClipOptions options)
        {
            AssertVideoItem (musicVideoClip, options);
            Assert.AreEqual (musicVideoClip.StorageMedium, options.StorageMedium);
            Assert.AreEqual (musicVideoClip.Date, options.Date);
            Assert.IsTrue (musicVideoClip.Artists.IsReadOnly);
            Assert.IsTrue (musicVideoClip.Albums.IsReadOnly);
            Assert.IsTrue (musicVideoClip.ScheduledStartTimes.IsReadOnly);
            Assert.IsTrue (musicVideoClip.ScheduledEndTimes.IsReadOnly);
            Assert.IsTrue (musicVideoClip.Contributors.IsReadOnly);
            CollectionAssert.AreEqual (musicVideoClip.Artists, options.Artists);
            CollectionAssert.AreEqual (musicVideoClip.Albums, options.Albums);
            CollectionAssert.AreEqual (musicVideoClip.ScheduledStartTimes, options.ScheduledStartTimes);
            CollectionAssert.AreEqual (musicVideoClip.ScheduledEndTimes, options.ScheduledEndTimes);
            CollectionAssert.AreEqual (musicVideoClip.Contributors, options.Contributors);
        }

        [Test]
        public void PersonInstantiation ()
        {
            var options = new PersonOptions ();
            SetPersonOptions (options);
            var person = new Person ("-1", "-1", options);
            AssertPerson (person, options);
            AssertPerson (person, person.GetOptions ());
        }

        static void SetPersonOptions (PersonOptions options)
        {
            SetContainerOptions (options);
            options.Language = "41";
        }

        static void AssertPerson (Person person, PersonOptions options)
        {
            AssertContainer (person, options);
            Assert.AreEqual (person.Language, options.Language);
        }

        [Test]
        public void PhotoInstantiation ()
        {
            var options = new PhotoOptions ();
            SetPhotoOptions (options);
            var photo = new Photo ("-1", "-1", options);
            AssertPhoto (photo, options);
            AssertPhoto (photo, photo.GetOptions ());
        }

        static void SetPhotoOptions (PhotoOptions options)
        {
            SetImageItemOptions (options);
            options.Albums = new[] { "album" };
        }

        static void AssertPhoto (Photo photo, PhotoOptions options)
        {
            AssertImageItem (photo, options);
            Assert.IsTrue (photo.Albums.IsReadOnly);
            CollectionAssert.AreEqual (photo.Albums, options.Albums);
        }

        [Test]
        public void PlaylistContainerInstantiation ()
        {
            var options = new PlaylistContainerOptions ();
            SetPlaylistContainerOptions (options);
            var playlist_container = new PlaylistContainer ("-1", "-1", options);
            AssertPlaylistContainer (playlist_container, options);
            AssertPlaylistContainer (playlist_container, playlist_container.GetOptions ());
        }

        static void SetPlaylistContainerOptions (PlaylistContainerOptions options)
        {
            SetContainerOptions (options);
            options.StorageMedium = "42";
            options.LongDescription = "43";
            options.Description = "44";
            options.Date = "45";
            options.Language = "46";
            options.Artists = new[] { new PersonWithRole ("artist", "role") };
            options.Genres = new[] { "genre" };
            options.Contributors = new[] { "contributor" };
            options.Rights = new[] { "right" };
        }

        static void AssertPlaylistContainer (PlaylistContainer playlistContainer, PlaylistContainerOptions options)
        {
            AssertContainer (playlistContainer, options);
            Assert.AreEqual (playlistContainer.StorageMedium, options.StorageMedium);
            Assert.AreEqual (playlistContainer.LongDescription, options.LongDescription);
            Assert.AreEqual (playlistContainer.Description, options.Description);
            Assert.AreEqual (playlistContainer.Date, options.Date);
            Assert.AreEqual (playlistContainer.Language, options.Language);
            Assert.IsTrue (playlistContainer.Artists.IsReadOnly);
            Assert.IsTrue (playlistContainer.Genres.IsReadOnly);
            Assert.IsTrue (playlistContainer.Contributors.IsReadOnly);
            Assert.IsTrue (playlistContainer.Rights.IsReadOnly);
            CollectionAssert.AreEqual (playlistContainer.Artists, options.Artists);
            CollectionAssert.AreEqual (playlistContainer.Genres, options.Genres);
            CollectionAssert.AreEqual (playlistContainer.Contributors, options.Contributors);
            CollectionAssert.AreEqual (playlistContainer.Rights, options.Rights);
        }

        [Test]
        public void PlaylistItemInstantiation ()
        {
            var options = new PlaylistItemOptions ();
            SetPlaylistItemOptions (options);
            var playlist_item = new PlaylistItem ("-1", "-1", options);
            AssertPlaylistItem (playlist_item, options);
            AssertPlaylistItem (playlist_item, playlist_item.GetOptions ());
        }

        static void SetPlaylistItemOptions (PlaylistItemOptions options)
        {
            SetItemOptions (options);
            options.StorageMedium = "47";
            options.LongDescription = "48";
            options.Description = "49";
            options.Date = "50";
            options.Language = "51";
            options.Artists = new[] { new PersonWithRole ("artist", "role") };
            options.Genres = new[] { "genre" };
        }

        static void AssertPlaylistItem (PlaylistItem playlistItem, PlaylistItemOptions options)
        {
            AssertItem (playlistItem, options);
            Assert.AreEqual (playlistItem.StorageMedium, options.StorageMedium);
            Assert.AreEqual (playlistItem.LongDescription, options.LongDescription);
            Assert.AreEqual (playlistItem.Description, options.Description);
            Assert.AreEqual (playlistItem.Date, options.Date);
            Assert.AreEqual (playlistItem.Language, options.Language);
            Assert.IsTrue (playlistItem.Artists.IsReadOnly);
            Assert.IsTrue (playlistItem.Genres.IsReadOnly);
            CollectionAssert.AreEqual (playlistItem.Artists, options.Artists);
            CollectionAssert.AreEqual (playlistItem.Genres, options.Genres);
        }

        [Test]
        public void StorageFolderInstantiation ()
        {
            var options = new StorageFolderOptions ();
            SetStorageFolderOptions (options);
            var storage_folder = new StorageFolder ("-1", "-1", options);
            AssertStorageFolder (storage_folder, options);
            AssertStorageFolder (storage_folder, storage_folder.GetOptions ());
        }

        static void SetStorageFolderOptions (StorageFolderOptions options)
        {
            SetContainerOptions (options);
            options.StorageUsed = 52;
        }

        static void AssertStorageFolder (StorageFolder storageFolder, StorageFolderOptions options)
        {
            AssertContainer (storageFolder, options);
            Assert.AreEqual (storageFolder.StorageUsed, options.StorageUsed);
        }

        [Test]
        public void StorageSystemInstantiation ()
        {
            var options = new StorageSystemOptions ();
            SetStorageSystemOptions (options);
            var storage_system = new StorageSystem ("-1", "-1", options);
            AssertStorageSystem (storage_system, options);
            AssertStorageSystem (storage_system, storage_system.GetOptions ());
        }

        static void SetStorageSystemOptions (StorageSystemOptions options)
        {
            SetStorageVolumeOptions (options);
            options.StorageMaxPartition = 53;
        }

        static void AssertStorageSystem (StorageSystem storageSystem, StorageSystemOptions options)
        {
            AssertContainer (storageSystem, options);
            Assert.AreEqual (storageSystem.StorageUsed, options.StorageUsed);
            Assert.AreEqual (storageSystem.StorageTotal, options.StorageTotal);
            Assert.AreEqual (storageSystem.StorageFree, options.StorageFree);
            Assert.AreEqual (storageSystem.StorageMedium, options.StorageMedium);
            Assert.AreEqual (storageSystem.StorageMaxPartition, options.StorageMaxPartition);
        }

        [Test]
        public void StorageVolumeInstantiation ()
        {
            var options = new StorageVolumeOptions ();
            SetStorageVolumeOptions (options);
            var storage_volume = new StorageVolume ("-1", "-1", options);
            AssertStorageVolume (storage_volume, options);
            AssertStorageVolume (storage_volume, storage_volume.GetOptions ());
        }

        static void SetStorageVolumeOptions (StorageVolumeOptions options)
        {
            SetStorageFolderOptions (options);
            options.StorageTotal = 54;
            options.StorageFree = 55;
            options.StorageMedium = "56";
        }

        static void AssertStorageVolume (StorageVolume storageVolume, StorageVolumeOptions options)
        {
            AssertContainer (storageVolume, options);
            Assert.AreEqual (storageVolume.StorageUsed, options.StorageUsed);
            Assert.AreEqual (storageVolume.StorageTotal, options.StorageTotal);
            Assert.AreEqual (storageVolume.StorageFree, options.StorageFree);
            Assert.AreEqual (storageVolume.StorageMedium, options.StorageMedium);
        }

        [Test]
        public void TextItemInstantiation ()
        {
            var options = new TextItemOptions ();
            SetTextItemOptions (options);
            var text_item = new TextItem ("-1", "-1", options);
            AssertTextItem (text_item, options);
            AssertTextItem (text_item, text_item.GetOptions ());
        }

        static void SetTextItemOptions (TextItemOptions options)
        {
            SetItemOptions (options);
            options.Protection = "57";
            options.LongDescription = "58";
            options.StorageMedium = "59";
            options.Description = "60";
            options.Rating = "61";
            options.Date = "62";
            options.Language = "63";
            options.Authors = new[] { new PersonWithRole ("artist", "role") };
            options.Publishers = new[] { "publisher" };
            options.Contributors = new[] { "contributor" };
            options.Relations = new[] { new Uri ("http://relation") };
            options.Rights = new[] { "right" };
        }

        static void AssertTextItem (TextItem textItem, TextItemOptions options)
        {
            AssertItem (textItem, options);
            Assert.AreEqual (textItem.Protection, options.Protection);
            Assert.AreEqual (textItem.LongDescription, options.LongDescription);
            Assert.AreEqual (textItem.Description, options.Description);
            Assert.AreEqual (textItem.Rating, options.Rating);
            Assert.AreEqual (textItem.Date, options.Date);
            Assert.AreEqual (textItem.Language, options.Language);
            Assert.IsTrue (textItem.Authors.IsReadOnly);
            Assert.IsTrue (textItem.Publishers.IsReadOnly);
            Assert.IsTrue (textItem.Contributors.IsReadOnly);
            Assert.IsTrue (textItem.Relations.IsReadOnly);
            Assert.IsTrue (textItem.Rights.IsReadOnly);
            CollectionAssert.AreEqual (textItem.Authors, options.Authors);
            CollectionAssert.AreEqual (textItem.Publishers, options.Publishers);
            CollectionAssert.AreEqual (textItem.Contributors, options.Contributors);
            CollectionAssert.AreEqual (textItem.Relations, options.Relations);
            CollectionAssert.AreEqual (textItem.Rights, options.Rights);
        }

        [Test]
        public void VideoBroadcastInstantiation ()
        {
            var options = new VideoBroadcastOptions ();
            SetVideoBroadcastOptions (options);
            var video_broadcast = new VideoBroadcast ("-1", "-1", options);
            AssertVideoBroadcast (video_broadcast, options);
            AssertVideoBroadcast (video_broadcast, video_broadcast.GetOptions ());
        }

        static void SetVideoBroadcastOptions (VideoBroadcastOptions options)
        {
            SetVideoItemOptions (options);
            options.Icon = new Uri ("http://64");
            options.Region = "65";
            options.ChannelNr = 66;
        }

        static void AssertVideoBroadcast (VideoBroadcast videoBroadcast, VideoBroadcastOptions options)
        {
            AssertVideoItem (videoBroadcast, options);
            Assert.AreEqual (videoBroadcast.Icon, options.Icon);
            Assert.AreEqual (videoBroadcast.Region, options.Region);
            Assert.AreEqual (videoBroadcast.ChannelNr, options.ChannelNr);
        }

        [Test]
        public void VideoItemInstantiation ()
        {
            var options = new VideoItemOptions ();
            SetVideoItemOptions (options);
            var video_item = new VideoItem ("-1", "-1", options);
            AssertVideoItem (video_item, options);
            AssertVideoItem (video_item, video_item.GetOptions ());
        }

        static void SetVideoItemOptions (VideoItemOptions options)
        {
            SetItemOptions (options);
            options.LongDescription = "67";
            options.Description = "68";
            options.Rating = "69";
            options.Language = "70";
            options.Genres = new[] { "genre" };
            options.Producers = new[] { "producer" };
            options.Actors = new[] { new PersonWithRole ("artist", "role") };
            options.Directors = new[] { "director" };
            options.Publishers = new[] { "publisher" };
            options.Relations = new[] { new Uri ("http://relation") };
        }

        static void AssertVideoItem (VideoItem videoItem, VideoItemOptions options)
        {
            AssertItem (videoItem, options);
            Assert.AreEqual (videoItem.LongDescription, options.LongDescription);
            Assert.AreEqual (videoItem.Description, options.Description);
            Assert.AreEqual (videoItem.Rating, options.Rating);
            Assert.AreEqual (videoItem.Language, options.Language);
            Assert.IsTrue (videoItem.Genres.IsReadOnly);
            Assert.IsTrue (videoItem.Producers.IsReadOnly);
            Assert.IsTrue (videoItem.Actors.IsReadOnly);
            Assert.IsTrue (videoItem.Directors.IsReadOnly);
            Assert.IsTrue (videoItem.Publishers.IsReadOnly);
            Assert.IsTrue (videoItem.Relations.IsReadOnly);
            CollectionAssert.AreEqual (videoItem.Genres, options.Genres);
            CollectionAssert.AreEqual (videoItem.Producers, options.Producers);
            CollectionAssert.AreEqual (videoItem.Actors, options.Actors);
            CollectionAssert.AreEqual (videoItem.Directors, options.Directors);
            CollectionAssert.AreEqual (videoItem.Publishers, options.Publishers);
            CollectionAssert.AreEqual (videoItem.Relations, options.Relations);
        }
    }
}
