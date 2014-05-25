using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTorrent.BEncode;
using System.IO;

namespace JTorrent.Tests.BEncode {

    /// <summary>
    /// Tests concernant le décodage du bencode
    /// </summary>
    [TestClass]
    public class BEncodingTest {

        /// <summary>
        /// Vérification de l'existence du fichier
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "The given file was not found.")]
        public void CheckFileExists() {            
            BEncoding encoding = new BEncoding("fichierquiexistepas.torrent");
        }

        /// <summary>
        /// Vérifie un fichier torrent en extension
        /// </summary>
        [TestMethod]
        public void CheckFileTypeTorrent() {

            try {

                BEncoding encoding = new BEncoding("fichierquiexistepas.torrent");
                Assert.Fail("An exception should have been thrown");
            
            } catch(FileNotFoundException ex) {
                Assert.AreEqual("The given file was not found.", ex.Message);
            } catch (Exception ex) {
                Assert.Fail(string.Format( "Unexpected exception of type {0} caught: {1}", ex.GetType(), ex.Message));
            }
        }

        /// <summary>
        /// Vérification de l'extension du fichier en cas d'un fichier différent de .torrent
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The given file is not a .torrent file.")]
        public void CheckFileTypeNotTorrent() {
            BEncoding encoding = new BEncoding("fichierquiexistepas.extension");
        }
        
        /// <summary>
        /// Vérification de l'absence de fichier passé
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath argument cannot be null or empty.")]
        public void CheckFilePathEmpty() {
            BEncoding encoding = new BEncoding(string.Empty);
        }

        /// <summary>
        /// Vérification de la longueur des datas passées (ne peut être égale à 0)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "data argument cannot have a length of 0.")]
        public void CheckDataEmpty() {
            BEncoding encoding = new BEncoding(new byte[0]);
        }
    }
}
