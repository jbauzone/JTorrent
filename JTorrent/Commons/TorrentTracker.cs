using JTorrent.BEncode;
using JTorrent.Download;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace JTorrent.Commons {

    /// <summary>
    /// Représente un tracker associé à un torrent
    /// </summary>
    public class TorrentTracker {

        public Torrent Torrent { get; set; }
        public Tracker Tracker { get; set; }

        public bool Compact { get; set; }
        public BEncodedInteger Complete { get; set; }
        public BEncodedInteger Incomplete { get; set; }
        public BEncodedInteger Interval { get; set; }
        public List<Peer> Peers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="torrent"></param>
        /// <param name="tracker"></param>
        public TorrentTracker(Torrent torrent, Tracker tracker) {
            Torrent = torrent;
            Tracker = tracker;
            Peers = new List<Peer>();
            
            // par défaut, les peers ne sont pas compactés
            Compact = false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="torrent"></param>
        /// <param name="tracker"></param>
        /// <param name="compact"></param>
        public TorrentTracker(Torrent torrent, Tracker tracker, bool compact) : this(torrent, tracker) {
            Compact = compact;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Request() {

            string encoded_hash = Torrent.GetEncodedInfoHash();

            //construction de la requête vers le tracker
            StringBuilder builder = new StringBuilder(Tracker.Url);
            builder.AppendFormat("?info_hash={0}", encoded_hash);
            builder.Append("&peer_id=adkiepeycosozpsngtoi");
            builder.Append("&uploaded=0");
            builder.Append("&downloaded=0");
            builder.AppendFormat("&compact={0}", Compact ? 1 : 0);
            builder.Append("&left=120000");
            builder.Append("&event=started");
            builder.Append("&port=6881");

            //création de la requête GET
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
            request.Method = "GET";

            //envoi de la requête
            using(WebResponse response = request.GetResponse()) {

                //récupération de la réponse
                using (Stream stream = response.GetResponseStream()) {

                    using (var reader = new StreamReader(stream, Encoding.Default)) {
                        string responseText = reader.ReadToEnd();

                        byte[] data = Encoding.Default.GetBytes(responseText);

                        BEncoding encoding = new BEncoding(data);
                        BEncodedDictionary dictionary = (BEncodedDictionary)encoding.Decode();

                        Complete = (BEncodedInteger)dictionary["complete"];
                        Incomplete = (BEncodedInteger)dictionary["incomplete"];
                        Interval = (BEncodedInteger)dictionary["interval"];

                        // la liste des peers peut être soit une liste, soit une chaine simplifiée en big endian
                        if (dictionary["peers"] is BEncodedList) {

                            BEncodedList peers = (BEncodedList)dictionary["peers"];

                        } else if (dictionary["peers"] is BEncodedString) {

                            byte[] peers = Encoding.Default.GetBytes((BEncodedString)dictionary["peers"]);

                            for (int i = 0; i < peers.Length; i = i + 6) {

                                byte[] ip = new byte[4];
                                byte[] port = new byte[2];

                                Array.Copy(peers, i, ip, 0, 4);
                                Array.Copy(peers, i + 4, port, 0, 2);
                                Array.Reverse(port);

                                IPEndPoint ipEndPoint = new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(port, 0));

                                Peer peer = new Peer(ipEndPoint);
                                Peers.Add(peer);
                            }
                        }
                    }
                }
            }
        }
    }
}
