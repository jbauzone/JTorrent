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

            string encoded_hash = Torrent.GetEncodedInfoHash();

            //construction de la requête vers le tracker
            StringBuilder builder = new StringBuilder(string.Format("{0}?info_hash={1}", Url, encoded_hash));
            builder.AppendFormat("&peer_id=adkiepeycosozpsngtoi&uploaded=0&downloaded=0&compact=1&numwant=50&left=120000&event=started&port=6881");

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
