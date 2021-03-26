using Magneto.Models;
using Magneto.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Magneto.Services
{
    public class HumanService : IHuman, IHumanStats
    {
        internal MagnetoContext context { get; set; }
        public HumanService()
        {
        }

        internal HumanService(MagnetoContext _context)
        {
            this.context = _context;
        }

        /// <summary>Servicio encargado de crear el registro de adn en la base de datos.
        /// </summary>
        /// <param name="dna">Contiene el adn la persona para guardarlo en la base de datos</param>
        /// <param name="mutant">Contiene el resultado del analisi del adn</param>
        public async Task PostHuman(string dna, bool mutant)
        {
            using var cmd = context.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `human` (`dna`, `mutant`) VALUES (@adn, @mutant);";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@adn",
                DbType = DbType.String,
                Value = dna,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@mutant",
                DbType = DbType.Boolean,
                Value = mutant,
            });
            await context.Connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await context.Connection.CloseAsync();
        }

        /// <summary>Servicio encargado de retornar los datos de todos los registros de humanos o mutantes de la base de datos.
        /// </summary>
        /// <returns>
        /// Retorna el listado con la informacion de los mutantes o no mutantes de la base de datos.
        /// </returns>
        public async Task<List<DNA>> GetHumans()
        {
            using var cmd = context.Connection.CreateCommand();
            //aqui voy especificamente por el dato de bd que necesito para realizar las estadisticas solicitadas,
            //si quisiera saber que adns fueron evaludos podria recuperar el adn solicitado aqui la columna [adn]
            cmd.CommandText = @"SELECT mutant FROM human";
            await context.Connection.OpenAsync();
            var list = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            await context.Connection.CloseAsync();
            return list;
        }

        /// <summary>Servicio encargado de mapear los datos de la base de datos.
        /// </summary>
        /// <param name="reader">Contiene la informacion retornada de la base de datos</param>
        /// <returns>
        /// Retorna el listado con la informacion de los mutantes o no mutantes de la base de datos.
        /// </returns>
        private async Task<List<DNA>> ReadAllAsync(DbDataReader reader)
        {
            List<DNA> list = new List<DNA>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    //aqui mapeo el dato de bd que necesito para realizar las estadisticas solicitadas,
                    //si quisiera mapear otro dato por ejemplo 'adn = reader.GetString(1)' indico la posicion segun el query y lo obtengo,
                    //esto ultimo implica modificar el query por ejemplo 'SELECT mutant, adn FROM human'
                    DNA dna = new DNA()
                    {
                        mutant = reader.GetBoolean(0),
                    };

                    list.Add(dna);
                }
            }
            return list;
        }

    }
}
