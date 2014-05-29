using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.Commons {

    /// <summary>
    /// Représente un fichier à télécharger contenu dans un torrent
    /// </summary>
    public class File {

        public long Length { get; set; }
        public string Path { get; set; }

        public File() { }

        /// <summary>
        /// Construis un fichier à partir de son chemin et de sa taille
        /// </summary>
        /// <param name="path"></param>
        /// <param name="length"></param>
        public File(string path, long length) {
            Length = length;
            Path = path;
        }
    }
}
