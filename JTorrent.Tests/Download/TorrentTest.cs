using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTorrent.Download;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JTorrent.Tests.Download {

    [TestClass]
    public class TorrentTest {

        /// <summary>
        /// Charge un fichier torrent en sa représentation de la classe Torrent
        /// </summary>
        [TestMethod]
        public void LoadTorrentFromFile() {

            Torrent torrent = new Torrent("pirates.torrent");
            Assert.AreEqual(1231877749, torrent.CreationDate);
        }

        /// <summary>
        /// Vérifie le hash du dictionnaire "info" présent dans le fichier
        /// </summary>
        [TestMethod]
        public void GetInfoHash() {

            Torrent torrent = new Torrent("pirates.torrent");
            Assert.AreEqual("3373240EC08A5F9F0AB1369B3E276D04970405AA", torrent.GetInfoHash());
        }
    }
}
