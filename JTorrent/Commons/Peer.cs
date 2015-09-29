using System.Net;
namespace JTorrent.Commons {

    /// <summary>
    /// Représente un Peer
    /// </summary>
    public class Peer {

        public IPEndPoint IPEndPoint { get; set; }

        /// <summary>
        /// Construit un peer à partir de son adresse IP:port
        /// </summary>
        /// <param name="ipEndPoint">Adresse IP:port du peer</param>
        public Peer(IPEndPoint ipEndPoint) {
            IPEndPoint = ipEndPoint;
        }
    }
}
