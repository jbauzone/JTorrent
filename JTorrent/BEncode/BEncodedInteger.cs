using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// Classe permettant de décoder des entiers dans le BEncode
    /// </summary>
    public class BEncodedInteger : BEncodedValue, IComparable<BEncodedInteger>, IEquatable<BEncodedInteger> {

        public long Value { get; set; }

        public BEncodedInteger() : base() { }

        /// <summary>
        /// Initialise les données BEncode à décoder avec la chaine de caractères fournie
        /// </summary>
        /// <param name="data"></param>
        public BEncodedInteger(string data) : base(data) { }

        /// <summary>
        /// Initialise les données BEncode à décoder avec le tableau de byte fourni
        /// </summary>
        /// <param name="data"></param>
        public BEncodedInteger(byte[] data) : base(data) { }

        /// <summary>
        /// Décode une file de byte afin de le convertir en entier
        /// </summary>
        /// <param name="queue"></param>
        public override void Decode(Queue<byte> queue) {

            char ch = (char)queue.Dequeue();
            string number = "";

            while((ch = (char)queue.Dequeue()) != 'e') {
                number += ch;
            }

            long integerValue = 0;

            if (!long.TryParse(number, out integerValue))
                throw new BEncodingException("The value is not a valid integer.");

            Value = long.Parse(number);
        }

        /// <summary>
        /// Retourne la représentation BEncoded de cette valeur
        /// </summary>
        /// <returns></returns>
        public override string GetEncodedValue() {
            return string.Format("i{0}e", Value);
        }

        /// <summary>
        /// Compare cette instance et indique si cette instance précède, suit ou apparait
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(BEncodedInteger other) {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Détermine si cette instance et celle en argument ont la même valeur
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BEncodedInteger other) {
            return Value.Equals(other.Value);
        }

        /// <summary>
        /// Détermine si cette instance et l'instance passée en argument ont la même valeur
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {

            var other = obj as BEncodedInteger;
            if(other == null) return false;

            return Equals(other);             
        }

        /// <summary>
        /// Retourne cette instance sous sa représentation en chaine
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Value.ToString();
        }

        /// <summary>
        /// Cast implicite vers BEncodedInteger
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator BEncodedInteger(long value) {
            return new BEncodedInteger { Value = value };
        }

        /// <summary>
        /// Cast implicite vers long
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator long(BEncodedInteger value) {
            return value.Value;
        }

        //opérateurs
        public static bool operator ==(BEncodedInteger a, BEncodedInteger b) { return a.Equals(b); }
        public static bool operator !=(BEncodedInteger a, BEncodedInteger b) { return !(a.Equals(b)); }
        public static bool operator ==(BEncodedInteger a, long b) { return a.Equals(b); }
        public static bool operator !=(BEncodedInteger a, long b) { return !(a.Equals(b)); }
        public static bool operator ==(long a, BEncodedInteger b) { return b.Equals(a); }
        public static bool operator !=(long a, BEncodedInteger b) { return !b.Equals(a); }
    }
}
