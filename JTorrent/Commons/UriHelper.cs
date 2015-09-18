using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.Commons {

    /// <summary>
    /// Classe de méthodes d'aide pour la gestion des Uri
    /// </summary>
    class UriHelper {

        /// <summary>
        /// Encode la chaine data selon la norme RFC1738
        /// </summary>
        /// <param name="data">Chaîne à encoder</param>
        /// <returns>Chaine encodée</returns>
        public static string UrlEncode(string data) { 

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i += 2) {

                string tmpCode = string.Format("{0}{1}", data[i], data[i + 1]);

                char caracter = (char)Convert.ToInt32(tmpCode, 16);
                bool isSpecChar = false;

                isSpecChar = (caracter == '.' || caracter == '-' || caracter == '~');

                if ((char.IsLetter(caracter) || char.IsNumber(caracter) || isSpecChar) && !char.IsControl(caracter) && caracter <= 127)
                    builder.Append(caracter);
                else
                    builder.AppendFormat("%{0}", tmpCode.ToLower());
            }

            return builder.ToString();
        }
    }
}
