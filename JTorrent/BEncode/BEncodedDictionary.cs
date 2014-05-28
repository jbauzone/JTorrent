using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Décodage des dictionnaires représentés en BEncode
    /// </summary>
    public class BEncodedDictionary : BEncodedValue, IDictionary<BEncodedString, BEncodedValue> {

        public Dictionary<BEncodedString, BEncodedValue> Value { get; set; }

        public BEncodedDictionary() : base() {
            Value = new Dictionary<BEncodedString, BEncodedValue>();
        }
        
        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        public BEncodedDictionary(string data) : base(data) {
            Value = new Dictionary<BEncodedString, BEncodedValue>();
        }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        public BEncodedDictionary(byte[] data) : base(data) {
            Value = new Dictionary<BEncodedString, BEncodedValue>();
        }

        /// <summary>
        /// Décode un dictionnaire BEncoded
        /// </summary>
        /// <param name="stack"></param>
        public override void Decode(Queue<byte> queue) {

            //on supprime la 1ère valeur qui est égale à 'd', qui permettait d'identifier le type de BEncode
            char ch = (char)queue.Dequeue();
            
            //on récupère chacun des caractères tant que nous ne sommes pas à la fin de la queue
            while((ch = (char)queue.Peek()) != 'e') {
                
                //on récupère la clé
                BEncoding decodingKey = new BEncoding(queue);
                BEncodedString key = (BEncodedString)decodingKey.Decode();

                //on récupère la valeur
                BEncoding decodingValue = new BEncoding(queue);
                BEncodedValue value = decodingValue.Decode();

                //on ajoute la clé / valeur au dico final
                Value.Add(key, value);
            }

            //suppression de la lettre de fin 'e'
            queue.Dequeue();
        }

        /// <summary>
        /// Ajoute la clé et la valeur au dictionnaire
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(BEncodedString key, BEncodedValue value) {
            Value.Add(key, value);
        }

        /// <summary>
        /// Détermine si la clé donnée existe dans le dictionnaire
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(BEncodedString key) {
            return Value.ContainsKey(key);
        }

        /// <summary>
        /// Retourne une collection contenant l'ensemble des clés
        /// </summary>
        public ICollection<BEncodedString> Keys {
            get { return Value.Keys; }
        }

        /// <summary>
        /// Supprime la valeur avec la clé spécifiée
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(BEncodedString key) {
            return Value.Remove(key);
        }

        /// <summary>
        /// Récupère la valeur associée à cette clé
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(BEncodedString key, out BEncodedValue value) {
            return Value.TryGetValue(key, out value);
        }

        /// <summary>
        /// Retourne une collection contenant l'ensemble des valeurs
        /// </summary>
        public ICollection<BEncodedValue> Values {
            get { return Value.Values; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public BEncodedValue this[BEncodedString key] {
            get {
                return Value[key];
            } set {
                Value[key] = value;
            }
        }

        /// <summary>
        /// Ajoute la clé et la valeur au dictionnaire
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<BEncodedString, BEncodedValue> item) {
            Value.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Supprime toutes les clés/valeurs du dictionnaire
        /// </summary>
        public void Clear() {
            Value.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<BEncodedString, BEncodedValue> item) {
            return Value.Contains(item);
        }

        public void CopyTo(KeyValuePair<BEncodedString, BEncodedValue>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Récupère le nombre d'éléments contenu dans le dictionnaire
        /// </summary>
        public int Count {
            get { return Value.Count; }
        }

        /// <summary>
        /// Indique si le dictionnaire est en lecture seule ou non
        /// </summary>
        public bool IsReadOnly {
            get { return false; }
        }

        /// <summary>
        /// Supprime la valeur avec la clé spécifiée
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<BEncodedString, BEncodedValue> item) {
            return Value.Remove(item.Key);
        }

        /// <summary>
        /// Retourne un énumateur qui itére le dictionnaire
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<BEncodedString, BEncodedValue>> GetEnumerator() {
            return Value.GetEnumerator();
        }

        /// <summary>
        /// Retourne un énumateur qui itére le dictionnaire
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return Value.GetEnumerator();
        }
    }
}
