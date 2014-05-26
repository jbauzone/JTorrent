using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// 
    /// </summary>
    public class BEncodedList : BEncodedValue {

        public List<BEncodedValue> Value { get; set; }

        public BEncodedList() : base() {
            Value = new List<BEncodedValue>();
        }
        
        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        public BEncodedList(string data) : base(data) {
            Value = new List<BEncodedValue>();
        }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        public BEncodedList(byte[] data) : base(data) {
            Value = new List<BEncodedValue>();
        }

        public override void Decode(Queue<byte> stack) {
            throw new NotImplementedException();
        }
    }
}
