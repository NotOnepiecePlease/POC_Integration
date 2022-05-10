using System.Data;
using Microsoft.Data.SqlClient;
using POC_SQL_Integration.Conexao_DB;

namespace POC_SQL_Integration.Dados
{
    internal class AtualizarDados
    {
        private static readonly ConexaoLab conexaoLab = new ConexaoLab();

        public void AtualizarStatusInstrumentoLab(string _idInstrumento)
        {
            try
            {
                using (SqlConnection conexao = conexaoLab.AbrirConexao())
                {
                    string query = $"update instrument set instrumentstatus = 'Available' where instrumentId = @ID_Instrumento";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.Add("ID_Instrumento", SqlDbType.VarChar).Value = _idInstrumento;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao atualizar status do instrumento LAB:\n{e.Message}");
            }
        }
    }
}