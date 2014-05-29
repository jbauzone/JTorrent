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

        [TestMethod]
        public void LoadTorrentFromFile() {

            Torrent torrent = new Torrent("pirates.torrent");
            Assert.AreEqual(1231877749, torrent.CreationDate);
        }
    }
}
