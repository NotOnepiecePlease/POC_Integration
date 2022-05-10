using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace POC_SQL_Integration.Conexao_DB
{
    internal class ConexaoLab
    {
        //Tipo 1: Server=localhost;Database=POC_Integration;User Id=sa;Password=123adr;
        //Tipo 2: Data Source= localhost;Initial Catalog=POC_Integration;Integrated Security=true;

        private static string Datasource = "localhost";
        private static string BancoDeDados = "labvantage";
        private static string LoginSQL = "sa";
        private static string SenhaSQL = "123adr";

        public SqlConnection AbrirConexao()
        {
            SqlConnection? con = null;
            string connectionString =
                $"Server={Datasource};Database={BancoDeDados};User Id={LoginSQL};Password={SenhaSQL}";

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao conectar com o banco: {e.Message}");
            }

            return con;
        }
    }
}