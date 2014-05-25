using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTorrent.BEncode {

    /// <summary>
    /// 
    /// </summary>
    public class BEncodedString : BEncodedValue {

        /// <summary>
        /// Décode une chaine de caractères
        /// </summary>
        /// <param name="stack"></param>
        public override void Decode(Queue<byte> stack) {
            
            long integerResult = 0;
            string number = "";
            char ch;

            do {
                
                //on récupère le 1er caratère de la pile
                ch = (char)stack.Dequeue();     
                
                //si c'est un chiffre, on concatène à la string résultante
                if(long.TryParse(ch.ToString(), out integerResult))
                    number += ch;
                //la chaine récupéré n'est pas un chiffre
                else
                    integerResult = 0;

            } while (integerResult > 0);

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

            Trace.WriteLine(new string(tmp));

            //return new string(tmp);
        }
    }
}
