using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magneto.Services;
using Magneto.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Magneto.Classes.Fns;

namespace Magneto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly MagnetoContext _context;
        public MutantController(MagnetoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> isMutant(DNAViewModel dna)
        {
            bool mutant = isMutant(dna.dna);
            IHuman query = new HumanService(_context);
            await query.PostHuman(string.Join(",", dna.dna.ToArray()), mutant);

            if (mutant)
                return Ok();
            else
                return StatusCode(403);
        }

        /// <summary>Evalua si existen más de una secuencia de 4 caracteres iguales, 
        /// si existen indica que el adn es de un mutante
        /// si no existen indica que el adn no es de un mutante
        /// </summary>
        /// <param name="dnaRows">Contiene el listado de string con la secuencia de adn que sera evaluado</param>
        /// <returns>
        /// Retorna un booleano(true, false) que indica si es adn de un mutante o no.
        /// </returns>
        private static bool isMutant(IEnumerable<string> dnaRows)
        {
            //counter => concurrencia o cantidad de veces que se encuentra el mismo caracter, cuando llega a 4 se identifica como secuencia y se reinicia;
            //secuenceCount => cantidad de secuencias que se encuentran, cuando llega a 2 se identifica como mutante y se rompen los ciclos;
            //rowsLength => cantidad de filas que tiene nuestra muestra de adn;
            //rowsLength => cantidad de filas que tiene nuestra muestra de adn;
            //dnaSecuences => array multidimensional contiene todo el adn y permite buscar en las diferentes direcciones(vertical, horizontal, oblicua, oblicua inversa);
            int concurrence, secuenceCount = 0, rowsLength = dnaRows.Count();
            char[,] dnaSecuences = convertToMatrix(dnaRows);

            //i => indica la fila que estamos evaluando;
            //a => indica la columna que estamos evaluando;
            //letter => indica la letra o valor que estamos buscado;
            for (int i = 0; i < rowsLength && !isMutant(); i++)
            {
                for (int a = 0; a < 6 && !isMutant(); a++)
                {
                    char letter = dnaSecuences[i, a];
                    //Busqueda Horizontal
                    //5 es el indice maximo por que los strings son de 6 caracteres, si el indice es menos la columna es menor a 3 ya no hay posibilidad de tener una secuencia.
                    //debido a que no hay las suficientes columnas de manera horizontal para completar una secuencia de 4 letras.
                    if (5 - a >= 3)
                    {
                        concurrence = 1;
                        findValue(i, a + 1, letter, SearchTypes.Horizontal);
                    }
                    //Busqueda Vertical
                    //5 es el indice maximo por que los strings son de 6 caracteres, si el indice es menos la columna es menor a 3 ya no hay posibilidad de tener una secuencia.
                    //debido a que no hay las suficientes columnas de manera horizontal para completar una secuencia de 4 letras.
                    if (!isMutant() && ((rowsLength - 1) - i) >= 3)
                    {
                        concurrence = 1;
                        findValue(i + 1, a, letter, SearchTypes.Vertical);
                    }
                    //Busqueda Oblicuo
                    if (!isMutant() && rowsLength > 3 && (((rowsLength - 1) - i) >= 3) && (5 - a >= 3))
                    {
                        concurrence = 1;
                        findValue(i + 1, a + 1, letter, SearchTypes.oblicua);
                    }
                    //Busqueda Oblicuo inverse
                    if (!isMutant() && rowsLength > 3 && (((rowsLength - 1) - i) < 3) && (5 - a > 2))
                    {
                        concurrence = 1;
                        findValue(i - 1, a + 1, letter, SearchTypes.inverseOblicua);
                    }
                }
            }

            /// <summary>Evalua si el siguiente valor segun el tipo de busqueda es igual a la letra que estamos buscando;
            /// Si es igual se invoca de manera recursiva para buscar el siguiente valor y validar de nuevo hasta completar 4 concurrencias
            /// Si no rompe el metodo y vuelve al ciclo anterior            
            /// </summary>
            /// <param name="row">indica la fila siguiente a buscar</param>
            /// <param name="column">indica la columna siguiente a buscar</param>
            /// <param name="value">indica el valor de la letra a buscar</param>
            /// <param name="type">indica el tipo de busqueda a realizar(vertical, horizontal, oblicua, oblicua inversa)</param>
            void findValue(int row, int column, char value, SearchTypes type)
            {
                if (concurrence == 4)
                    secuenceCount++;

                if (concurrence < 4 && dnaSecuences[row, column] == value)
                {
                    concurrence++;
                    switch (type)
                    {
                        case SearchTypes.Horizontal:
                            findValue(row, column + 1, value, type);
                            break;
                        case SearchTypes.Vertical:
                            findValue(row + 1, column, value, type);
                            break;
                        case SearchTypes.oblicua:
                            findValue(row + 1, column + 1, value, type);
                            break;
                        case SearchTypes.inverseOblicua:
                            findValue(row - 1, column + 1, value, type);
                            break;
                    }
                }
            }


            /// <summary>Evalua si hay mas de una secuencia igual de 4 letras;
            /// Si hay mas de 1 identifica el muntante
            /// Si no hay mas de 1 identifica como no muntante
            /// Se realiza esta valdicion aparte pensando en qeu podria cambiar el numero de secuencias requeridas para identificar un mutante, en ese caso solo se modificaria este metodo.
            /// </summary>
            /// <returns>
            /// Retorna un booleano(true, false) que indica si es adn de un mutante o no.
            /// </returns>
            bool isMutant()
            {
                return secuenceCount >= 2;
            }

            return isMutant();
        }
    }
}
