namespace JTorrent.Commons {

    /// <summary>
    /// Représente un tracker
    /// </summary>
    public class Tracker {

        public string Url { get; set; }
        
        /// <summary>
        /// Construis un tracker à partir de son url et du torrent associé
        /// </summary>
        /// <param name="url"></param>
        /// <param name="torrent"></param>
        public Tracker(string url) {
            Url = url;
        }
    }
}
