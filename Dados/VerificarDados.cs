using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using POC_SQL_Integration.Conexao_DB;

namespace POC_SQL_Integration.Dados
{
    internal class VerificarDados
    {
        private static readonly ConexaoLab conexaoPoc = new ConexaoLab();

        public bool VerificarInstrumentoExisteEmLAB(string _idInstrumento)
        {
            using (SqlConnection conexao = conexaoPoc.AbrirConexao())
            {
                // string query = "SELECT CASE WHEN EXISTS(select EQUI_ID, EQUI_TX_STATUS_SISTEMA from instruments where EQUI_ID = @ID_Instrumento) THEN 'TRUE' ELSE 'FALSE' END";
                string query = "SELECT CASE WHEN EXISTS(select instrumentId, instrumentstatus from instrument where instrumentid = @ID_Instrumento) THEN 'TRUE' ELSE 'FALSE' END";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                adapter.SelectCommand.Parameters.AddWithValue("@ID_Instrumento", _idInstrumento);
                SqlDataReader reader = adapter.SelectCommand.ExecuteReader();

                reader.Read();

                string isInstrumentoExiste = reader.GetString(0);

                if (isInstrumentoExiste == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}