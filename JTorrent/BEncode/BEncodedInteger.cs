using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// 
    /// </summary>
    public class BEncodedInteger : BEncodedValue {

        public override void Decode(Queue<byte> queue) {

            char ch = (char)queue.Dequeue();
            string number = "";

            while((ch = (char)queue.Dequeue()) != 'e') {
                number += ch;
            }

            Trace.WriteLine(number);
        }
    }
}
