using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// 
    /// </summary>
    public class BEncodedDictionary : BEncodedValue {

        public Dictionary<BEncodedString, BEncodedValue> Value { get; set; }

        public override void Decode(Queue<byte> stack) {
            throw new NotImplementedException();
        }
    }
}
