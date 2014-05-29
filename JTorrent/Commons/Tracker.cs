using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.Commons {

    /// <summary>
    /// Représente un tracker
    /// </summary>
    public class Tracker {

        public string Url { get; set; }

        public Tracker() { }

        /// <summary>
        /// Construis un tracker à partir de son url
        /// </summary>
        /// <param name="url"></param>
        public Tracker(string url) {
            Url = url;
        }
    }
}
