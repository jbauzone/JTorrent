using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTorrent.BEncode;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

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

        /// <summary>
        /// Décode une string valide
        /// </summary>
        [TestMethod]
        public void DecodeString() {
            
            string data = "11:test string";
            BEncodedString encoding = new BEncodedString();
            encoding.Decode(new Queue<byte>(Encoding.UTF8.GetBytes(data)));
        }

        /// <summary>
        /// Décode une chaine dont la taille ne correspond pas à sa taille réelle
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BEncodingException), "String length doesnt fit the size argument.")]
        public void DecodeTooShortString() {

            string data = "40:tooshort";
            BEncodedString encoding = new BEncodedString();
            encoding.Decode(new Queue<byte>(Encoding.UTF8.GetBytes(data)));
        }

        [TestMethod]
        public void DecodeInteger() {

            string data = "i123456789e";
            BEncodedInteger encoding = new BEncodedInteger();
            encoding.Decode(new Queue<byte>(Encoding.UTF8.GetBytes(data)));

            Trace.Flush();
        }
    }
}
