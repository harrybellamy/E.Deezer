﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestSharp.Deserializers;

namespace E.Deezer.Api
{
    /// <summary>
    /// Deezer Playlist object
    /// </summary>
    public interface IPlaylist
    {
        /// <summary>
        /// Deezer library ID number
        /// </summary>
        uint Id { get; set; }

        /// <summary>
        /// Playlist title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Public availablity of playlist
        /// </summary>
        bool Public { get; set; }

        /// <summary>
        /// Number of tracks in playlist
        /// </summary>
        uint NumTracks { get; set; }

        /// <summary>
        /// Deezer.com link to playlist
        /// </summary>
        string Link { get; set; }
        
        /// <summary>
        /// Link to playlist picture
        /// </summary>
        string Picture { get; set; }

        /// <summary>
        /// Link to playlist tracklist
        /// </summary>
        string Tracklist { get; set; }

        /// <summary>
        /// Username of playlist creator
        /// </summary>
        string CreatorName { get; }

        /// <summary>
        /// Gets the tracks in the playlist
        /// </summary>
        /// <returns>First page of tracks in playlist.</returns>
        IPagedResponse<ITrack> GetTracks();

    }

    internal class Playlist : IPlaylist
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public bool Public { get; set; }

        [DeserializeAs(Name = "nb_tracks")]
        public uint NumTracks { get; set; }

        public string Link { get; set; }
        public string Picture { get; set; }
        public string Tracklist { get; set; }
        public string CreatorName
        {
            //Required as sometime playlist creator is references as Creator and sometimes references as User
            get
            {
                if (UserInternal == null && CreatorInternal == null) { return string.Empty; }
                return (UserInternal == null) ? CreatorInternal.Name : UserInternal.Name;
            }
        }

        [DeserializeAs(Name = "user")]
        public User UserInternal { get; set; }

        [DeserializeAs(Name = "creator")]
        public User CreatorInternal { get; set; }

        private DeezerClient Client { get; set; }
        internal void Deserialize(DeezerClient aClient)
        {
            Client = aClient;
        }

        internal IPlaylist ConvertToInterface()
        {
            return this as IPlaylist;
        }


        public IPagedResponse<ITrack> GetTracks()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Title, CreatorName);
        }
    }
}
