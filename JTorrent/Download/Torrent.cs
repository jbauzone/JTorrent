using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JTorrent.BEncode;
using JTorrent.Commons;

namespace JTorrent.Download {

    /// <summary>
    /// Défini un fichier torrent
    /// </summary>
    public class Torrent : IDownloadable {

        private BEncodedDictionary _data;

        public long CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public int NbPieces { get; set; }
        public long PieceLength { get; set; }
        public string Pieces { get; set; }
        public List<File> Files { get; set; }
        public List<TorrentTracker> Trackers { get; set; }

        public Torrent() {
            Files = new List<File>();
            Trackers = new List<TorrentTracker>();
        }

        /// <summary>
        /// Charge un torrent depuis un fichier
        /// </summary>
        /// <param name="path"></param>
        public Torrent(string path) : this() {

             BEncoding decoding = new BEncoding(path);
             _data = (BEncodedDictionary)decoding.Decode();

            //charge le torrent depuis les données récupérés
            LoadTorrent();
        }

        /// <summary>
        /// Charge un torrent depuis sa représentation BEncodedDictionary
        /// </summary>
        /// <param name="dictionary"></param>
        public Torrent(BEncodedDictionary dictionary) : this() {
            _data = dictionary;

            //charge le torrent depuis les données récupérés
            LoadTorrent();
        }

        /// <summary>
        /// Construis l'instance
        /// </summary>
        private void LoadTorrent() {

            if (_data == null)
                throw new ArgumentNullException("_data", "_data cannot be null.");

            //on récupère chacune des données si présentes
            if (_data.ContainsKey("creation date"))
                CreationDate = _data["creation date"];
            
            if (_data.ContainsKey("created by"))
                CreatedBy = _data["created by"];
            
            if (_data.ContainsKey("comment"))
                Comment = _data["comment"];

            if (_data.ContainsKey("announce")) {

                Tracker tracker = new Tracker(_data["announce"]);
                Trackers.Add(new TorrentTracker(this, tracker));
            }

            if (_data.ContainsKey("info")) {

                BEncodedDictionary info = (BEncodedDictionary)_data["info"];

                if (info.ContainsKey("piece length"))
                    PieceLength = info["piece length"];

                if (info.ContainsKey("pieces"))
                    Pieces = info["pieces"];

                if (info.ContainsKey("name"))
                    Name = info["name"];

                //on véfifie la présence des fichiers
                if(info.ContainsKey("files")) {

                    BEncodedList files = (BEncodedList)info["files"];

                    foreach (var item in files) {

                        BEncodedDictionary fileEncoded = (BEncodedDictionary)item;

                        File file = new File(fileEncoded["path"], fileEncoded["length"]);
                        Files.Add(file);
                    }
                }
            }
        }

        /// <summary>
        /// Récupère le hash du dictionnaire "info"
        /// </summary>
        /// <returns></returns>
        public string GetInfoHash() {

            if (_data == null)
                throw new ArgumentNullException("_data", "_data cannot be null.");

            return BitConverter.ToString(SHA1.Create().
                ComputeHash(Encoding.Default.GetBytes(_data["info"].GetEncodedValue()))).Replace("-", "");
        }
        
        /// <summary>
        /// Récupère le info_hash encodé pour être transmis via url
        /// </summary>
        /// <returns></returns>
        public string GetEncodedInfoHash() {
            return UriHelper.UrlEncode(GetInfoHash());
        }

        public void Download() {

            foreach(TorrentTracker tracker in Trackers) {
                tracker.Request();    
            }
        }
    }
}
