using System;
using System.Collections.Generic;
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

        public ulong CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string Encoding { get; set; }
        public string Comment { get; set; }
        public string Announce { get; set; }
        public ulong PieceLength { get; set; }
        public string Name { get; set; }

        public Torrent(string path) {

             BEncoding decoding = new BEncoding(path);
             _data = (BEncodedDictionary)decoding.Decode();
        }

        public Torrent(BEncodedDictionary dictionary) {
            _data = dictionary;
        }

        public void Download() {
            throw new NotImplementedException();
        }
    }
}
