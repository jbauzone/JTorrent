using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Classe permettant le décodage d'une donnée BEncode (voir http://en.wikipedia.org/wiki/Bencode)
    /// </summary>
    public class BEncoding {

        private byte[] _data;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.FileNotFoundException"></exception>
        /// <param name="filePath"></param>
        public BEncoding(string filePath) {

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath", "filePath argument cannot be null or empty.");

            if (!Path.GetExtension(filePath).Equals(".torrent"))
                throw new ArgumentOutOfRangeException("filePath", "The given file is not a .torrent file.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The given file was not found.", filePath);

            //on récupère tout le fichier sous forme de bytes
            _data = File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <param name="data"></param>
        public BEncoding(byte[] data) {
            
            //data ne peut être null
            if (data == null)
                throw new ArgumentNullException("data argument cannot be null or empty.", "data");

            //data ne peut être vide
            if (data.Length == 0)
                throw new ArgumentException("data argument cannot have a length of 0.", "data");

            _data = data;
        }
    }
}
