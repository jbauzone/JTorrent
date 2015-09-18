using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTorrent.Download;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTorrent.Tests.Commons;

namespace JTorrent.Tests.Download {

    [TestClass]
    public class TorrentTest {

        /// <summary>
        /// Charge un fichier torrent en sa représentation de la classe Torrent
        /// </summary>
        [TestMethod]
        public void LoadTorrentFromFile() {

            Torrent torrent = new Torrent(Tools.GetTestDataFilePath("ubuntu-15.04-desktop-amd64.iso.torrent"));
            Assert.AreEqual(1429786237, torrent.CreationDate);
        }

        /// <summary>
        /// Vérifie le hash du dictionnaire "info" présent dans le fichier
        /// </summary>
        [TestMethod]
        public void GetInfoHash() {

            Torrent torrent = new Torrent(Tools.GetTestDataFilePath("ubuntu-15.04-desktop-amd64.iso.torrent"));
            Assert.AreEqual("FC8A15A2FAF2734DBB1DC5F7AFDC5C9BEAEB1F59", torrent.GetInfoHash());
        }

        /// <summary>
        /// Vérifie le hash encodé d'un Torrent
        /// </summary>
        [TestMethod]
        public void GetEncodedInfoHash() {

            Torrent torrent = new Torrent(Tools.GetTestDataFilePath("ubuntu-15.04-desktop-amd64.iso.torrent"));
            Assert.AreEqual("%fc%8a%15%a2%fa%f2sM%bb%1d%c5%f7%af%dc%5c%9b%ea%eb%1fY", torrent.GetEncodedInfoHash());
        }
    }
}
