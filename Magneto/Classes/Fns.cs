using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magneto.Classes
{   /// <summary>Esta clase contiene las funciones que podrian ser reutilizadas para otras funcionalidades;           
    /// </summary>
    public class Fns

    {
        /// <summary>Enumera los tipos de busqueda que podemos realizar(vertical, horizontal, oblicua, oblicua inversa);           
        /// </summary>
        public enum SearchTypes
        {
            Vertical,
            Horizontal,
            oblicua,
            inverseOblicua,
        }


        /// <summary>Conviert un listado de strings en una matriz multidimensional,
        /// realizando un split del string y repartiendo los caracteres segun su posicion.           
        /// </summary>
        /// <param name="dnaRows">Contiene el listado de string que se convertira en una matriz multidimensional</param>
        /// /// <returns>
        /// Retorna un matriz multidimensional de caracteres(char[,]).
        /// </returns>
        public static char[,] convertToMatrix(IEnumerable<string> dnaRows)
        {
            char[,] dnaSecuences = new char[dnaRows.Count(), 6];

            for (int i = 0; i < dnaRows.Count(); i++)
            {
                char[] dnaLetters = dnaRows.ElementAt(i).ToCharArray();

                for (int a = 0; a < dnaLetters.Length; a++)
                {
                    dnaSecuences[i, a] = dnaLetters[a];
                }
            }

            return dnaSecuences;
        }
    }
}
