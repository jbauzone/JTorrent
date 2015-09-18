using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.Tests.Commons {

    /// <summary>
    /// Méthodes "outils" pour les tests
    /// </summary>
    static class Tools {

        /// <summary>
        /// Récupère le chemin absolu du fichier de test fourni 
        /// </summary>
        /// <param name="fileName">Fichier de test de Test_Date</param>
        /// <returns>Chemin absolu vers le fichier de test</returns>
        public static string GetTestDataFilePath(string fileName) {

            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            string projectPath = string.Join (Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - 2));
            return Path.Combine(projectPath, "Test_Data", fileName);
        }
    }
}
