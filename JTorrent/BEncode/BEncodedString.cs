using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Décodage des string BEncode
    /// </summary>
    public class BEncodedString : BEncodedValue, IComparable<BEncodedString>, IEquatable<BEncodedString> {

        public string Value { get; set; }

        public BEncodedString() : base() { }

        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        public BEncodedString(string data) : base(data) { }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        public BEncodedString(byte[] data) : base(data) { }

        /// <summary>
        /// Décode une chaine de caractères
        /// </summary>
        /// <param name="stack"></param>
        public override void Decode(Queue<byte> stack) {

            bool isValid = true;
            long integerResult = 0;
            string number = "";
            char ch;

            do {
                
                //on récupère le 1er caratère de la pile
                ch = (char)stack.Dequeue();     
                
                //si c'est un chiffre, on concatène à la string résultante
                if ((isValid = long.TryParse(ch.ToString(), out integerResult)))
                    number += ch;

            } while (isValid);

            List<byte> chaine = new List<byte>();
            integerResult = long.Parse(number);

            //si la taille de la pile est plus grande que la value récupéré
            if (stack.Count < integerResult)
                throw new BEncodingException("String length doesnt fit the size argument.");

            //on récupère dans la pile la chaine de la taille de integerResult
            for (int i = 0; i < integerResult; i++)
                chaine.Add(stack.Dequeue());

            //on encode toutes les string en ANSI
            char[] tmp = new char[chaine.Count];
            Encoding.Default.GetChars(chaine.ToArray(), 0, chaine.Count, tmp, 0);

            Value = new string(tmp);
        }

        /// <summary>
        /// Retourne la représentation BEncoded de cette valeur
        /// </summary>
        /// <returns></returns>
        public override string GetEncodedValue() {
            return string.Format("{0}:{1}", Value.Length, Value);
        }

        /// <summary>
        /// Compare cette instance et indique si cette instance précède, suit ou apparait
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(BEncodedString other) {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Détermine si cette instance et celle en argument ont la même valeur
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BEncodedString other) {
            return Value.Equals(other.Value);
        }

        /// <summary>
        /// Détermine si cette instance et l'instance passée en argument ont la même valeur
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            
            if (obj is string)
                return Value.Equals((string)obj);
            else if (obj is BEncodedString) {
                BEncodedString other = (BEncodedString)obj;
                return Value.Equals(other.Value);
            }

            return false;            
        }

        /// <summary>
        /// Retourne cette instance sous sa forme string
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Value;
        }

        /// <summary>
        /// Retourne le code hash
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
 	        return Value.GetHashCode();
        }

        /// <summary>
        /// Cast implicite vers BEncodedString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator BEncodedString(string value) {            
            return new BEncodedString { Value = value };
        }

        /// <summary>
        /// Cast implite vers string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(BEncodedString value) {            
            return value.Value;
        }

        //opérateurs
        public static bool operator ==(BEncodedString a, BEncodedString b) { return a.Equals(b); }
        public static bool operator !=(BEncodedString a, BEncodedString b) { return !(a.Equals(b)); }
        public static bool operator ==(BEncodedString a, string b) { return a.Equals(b); }
        public static bool operator !=(BEncodedString a, string b) { return !(a.Equals(b)); }
        public static bool operator ==(string a, BEncodedString b) { return b.Equals(a); }
        public static bool operator !=(string a, BEncodedString b) { return !b.Equals(a); }
    }
}
