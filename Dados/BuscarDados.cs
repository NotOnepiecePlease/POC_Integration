using Microsoft.Data.SqlClient;
using POC_SQL_Integration.Conexao_DB;

namespace POC_SQL_Integration.Dados
{
    internal class BuscarDados
    {
        private static readonly ConexaoPoc conexaoPoc = new ConexaoPoc();
        private static readonly ConexaoLab conexaoLab = new ConexaoLab();

        public Dictionary<string, string> BuscarInstrumentoPOC()
        {
            Dictionary<string, string> listaInstrumentosPOC = new Dictionary<string, string>();
            //List<KeyValuePair<string, string>> listaInstrumentosPOC = new List<KeyValuePair<string, string>>();
            try
            {
                using (SqlConnection conexao = conexaoPoc.AbrirConexao())
                {
                    string query = "select EQUI_ID, EQUI_TX_STATUS_SISTEMA from instruments order by EQUI_ID asc";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    SqlDataReader reader = adapter.SelectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        //Usando Key Value Pair
                        //var instrumento = new KeyValuePair<string, string>(reader.GetString(0), reader.GetString(1));
                        //listaInstrumentosPOC.Add(instrumento);

                        listaInstrumentosPOC.Add(reader.GetString(0), reader.GetString(1));
                    }

                    return listaInstrumentosPOC;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar instrumentos da Lab Vantage:\n{e.Message}");
                return listaInstrumentosPOC;
            }
        }

        public Dictionary<string, string> BuscarInstrumentoLAB()
        {
            Dictionary<string, string> listaInstrumentosLAB = new Dictionary<string, string>();
            try
            {
                using (SqlConnection conexao = conexaoLab.AbrirConexao())
                {
                    string query = "select instrumentId, instrumentstatus from instrument order by instrumentid asc";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    SqlDataReader reader = adapter.SelectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        listaInstrumentosLAB.Add(reader.GetString(0), reader.GetString(1));
                    }

                    return listaInstrumentosLAB;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar instrumentos da Lab Vantage:\n{e.Message}");
                return listaInstrumentosLAB;
            }
        }
    }
}