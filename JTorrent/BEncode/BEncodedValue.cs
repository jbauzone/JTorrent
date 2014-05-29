using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Représentation des données BEncode de base permettant leur décodage
    /// </summary>
    public abstract class BEncodedValue {

        protected byte[] Data { get; set; }

        protected BEncodedValue() { }

        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        protected BEncodedValue(string data) {
            Data = Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        protected BEncodedValue(byte[] data) {
            Data = data;
        }

        /// <summary>
        /// Décode les données du champ "Data" précédemment founies
        /// </summary>
        public void Decode() {

            if (Data == null)
                throw new ArgumentNullException("Data", "Data field must be set before call Decode method.");

            Decode(new Queue<byte>(Data));
        }

        /// <summary>
        /// Permet de décoder les données fournies en argument
        /// </summary>
        /// <param name="stack"></param>
        public abstract void Decode(Queue<byte> stack);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator long(BEncodedValue value) {
            return long.Parse(value.ToString());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(BEncodedValue value) {
            return value.ToString();
        }
    }
}
