using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    public class BEncodingException : Exception {

        public BEncodingException() : base() { }
        public BEncodingException(string message) : base(message) { }
        public BEncodingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
