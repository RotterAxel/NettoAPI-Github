using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Respawn;

namespace Application.IntegrationTests
{
    public class CheckpointMySql : Checkpoint
    {
        public override async Task Reset(string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await base.Reset((DbConnection) connection);
            }
        }
    }
}