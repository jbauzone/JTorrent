using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Décodage d'une liste représentée en BEncode
    /// </summary>
    public class BEncodedList : BEncodedValue, IList<BEncodedValue> {

        public List<BEncodedValue> Value { get; set; }

        public BEncodedList() : base() {
            Value = new List<BEncodedValue>();
        }
        
        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        public BEncodedList(string data) : base(data) {
            Value = new List<BEncodedValue>();
        }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        public BEncodedList(byte[] data) : base(data) {
            Value = new List<BEncodedValue>();
        }

        /// <summary>
        /// Décode une liste BEncode
        /// </summary>
        /// <param name="stack"></param>
        public override void Decode(Queue<byte> queue) {
           
            //on supprime la 1ère valeur qui est égale à 'l', qui permettait d'identifier le type de BEncode
            char ch = (char)queue.Dequeue();
            
            //on récupère chacun des caractères tant que nous ne sommes pas à la fin de la queue
            while((ch = (char)queue.Peek()) != 'e') {

                //on récupère la valeur
                BEncoding decoding = new BEncoding(queue);
                BEncodedValue value = decoding.Decode();

                //on ajoute la valeur à la liste
                Value.Add(value);
            }

            //suppression de la lettre de fin 'e'
            queue.Dequeue();
        }

        /// <summary>
        /// Recherche l'occurrence et retourne l'index de la première occurence
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(BEncodedValue item) {
            return Value.IndexOf(item);
        }

        /// <summary>
        /// Ajoute l'élément à l'index donné
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, BEncodedValue item) {
            Value.Insert(index, item);
        }

        /// <summary>
        /// Supprime l'élément à l'index donné
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index) {
            Value.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BEncodedValue this[int index] {
            get {
                return Value[index];
            } set {
                Value[index] = value;
            }
        }

        /// <summary>
        /// Ajoute l'élément à la liste
        /// </summary>
        /// <param name="item"></param>
        public void Add(BEncodedValue item) {
            Value.Add(item);
        }

        /// <summary>
        /// Supprime tous les éléments de la liste
        /// </summary>
        public void Clear() {
            Value.Clear();
        }

        /// <summary>
        /// Indique si l'objet est contenu dans la liste
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(BEncodedValue item) {
            return Value.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(BEncodedValue[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne le nombre d'éléments de la liste
        /// </summary>
        public int Count {
            get { return Value.Count; }
        }

        /// <summary>
        /// Retourne toujours false
        /// </summary>
        public bool IsReadOnly {
            get { return false; }
        }

        /// <summary>
        /// Supprime l'élement de la liste
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(BEncodedValue item) {
            return Value.Remove(item);
        }

        /// <summary>
        /// Retourne un énumérateur qui itérate la collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<BEncodedValue> GetEnumerator() {
            return Value.GetEnumerator();
        }

        /// <summary>
        /// Retourne un énumérateur qui itérate la collection
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return Value.GetEnumerator();
        }
    }
}
