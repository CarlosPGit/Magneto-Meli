using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Magneto.Services
{
    public class MagnetoContext : IDisposable
    {
        public MySqlConnection Connection { get; set; }

        public MagnetoContext(string connectionString)
        {
            this.Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
