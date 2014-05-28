using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTorrent.BEncode;

namespace JTorrent.Download {

    /// <summary>
    /// Défini un fichier torrent
    /// </summary>
    public class Torrent : IDownloadable {

        private BEncodedDictionary _data;

        public long CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string Encoding { get; set; }
        public string Comment { get; set; }
        public string Announce { get; set; }
        public ulong PieceLength { get; set; }
        public string Name { get; set; }

        public Torrent(string path) {

             BEncoding decoding = new BEncoding(path);
             _data = (BEncodedDictionary)decoding.Decode();

            //charge le torrent depuis les données récupérés
            LoadTorrent();
        }

        public Torrent(BEncodedDictionary dictionary) {
            _data = dictionary;

            //charge le torrent depuis les données récupérés
            LoadTorrent();
        }

        private void LoadTorrent() {

            if (_data == null)
                throw new ArgumentNullException("_data", "_data cannot be null.");

            Trace.WriteLine("a" + _data.Count);
            Trace.WriteLine("a" + _data["announce"]);
            Trace.WriteLine(_data.ContainsKey("announce"));

           // if (_data.ContainsKey("creation date"))
             //   CreationDate = (((BEncodedDictionary)(_data["announce"]))["creation date"]);
        }

        public void Download() {
            throw new NotImplementedException();
        }
    }
}
