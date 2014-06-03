using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JTorrent.Download;

namespace JTorrent.Commons {

    /// <summary>
    /// Représente un tracker
    /// </summary>
    public class Tracker {

        public string Url { get; set; }
        public Torrent Torrent { get; set; }

        public Tracker() { }

        /// <summary>
        /// Construis un tracker à partir de son url et du torrent associé
        /// </summary>
        /// <param name="url"></param>
        /// <param name="torrent"></param>
        public Tracker(string url, Torrent torrent) {
            Url = url;
            Torrent = torrent;
        }

        public void Request() {

            //on récupère l'info hash de ce torrent
            string hash = Torrent.GetInfoHash();

            StringBuilder infoHash = new StringBuilder();

            //on échappe chacun des caractères
            for(int i = 0; i < hash.Length; i = i+2) {
                infoHash.AppendFormat("%{0}{1}", hash[i], hash[i+1]);
            }

            //construction de la requête vers le tracker
            StringBuilder builder = new StringBuilder(string.Format("{0}?info_hash={1}", Url, infoHash.ToString()));
            builder.AppendFormat("&peer_id=adkiepeycosozpsngtoi&uploaded=0&downloaded=0&compact=1&numwant=50&left=120000&event=started");

            //création de la requête GET
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
            request.Method = "GET";

            //envoi de la requête
            using(WebResponse response = request.GetResponse()) {

                //récupération de la réponse
                using (Stream stream = response.GetResponseStream()) {

                    using (var reader = new StreamReader(stream, Encoding.Default)) {
                        string responseText = reader.ReadToEnd();
                    }    
                }
            }
        }
    }
}
