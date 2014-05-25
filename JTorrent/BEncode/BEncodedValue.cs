using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    public abstract class BEncodedValue {

        public abstract void Decode(Queue<byte> stack);
    }
}
