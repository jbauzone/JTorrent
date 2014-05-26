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

        private Queue<byte> _queue;

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
            _queue = new Queue<byte>(File.ReadAllBytes(filePath));
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

            _queue = new Queue<byte>(data);
        }

        public BEncoding(Queue<byte> queue) {

            //data ne peut être null
            if (queue == null)
                throw new ArgumentNullException("queue argument cannot be null or empty.", "queue");

            _queue = queue;
        }

        public BEncodedValue Decode() {
            return Decode(_queue);
        }

        public BEncodedValue Decode(Queue<byte> blist) {
            
            char ch = (char)blist.Peek();
            long integerResult = 0;

            BEncodedValue value = null;

            //cas des dictionnaires
            if(ch == 'd') {
                value = new BEncodedDictionary();
            } 
            //les listes
            else if (ch == 'l') {
                value = new BEncodedList();
            } 
            //integer
            else if (ch == 'i') {
                value = new BEncodedInteger();
            } 
            //une string
            else if (long.TryParse(ch.ToString(), out integerResult)) {
                value = new BEncodedString();
            } 
            //aucun des types défini, on génère une erreur
            else {
                throw new Exception("Unable to find the value to decode.");
            }

            //on décode la donnée en fonction du type
            value.Decode(blist);

            return value;
        }
    }
}
