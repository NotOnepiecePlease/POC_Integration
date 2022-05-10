using System.Data;
using Microsoft.Data.SqlClient;
using POC_SQL_Integration.Conexao_DB;

namespace POC_SQL_Integration.Dados
{
    internal class InserirDados
    {
        private static readonly ConexaoLab conexaoLab = new ConexaoLab();

        public void InserirInstrumentoEmLab(string _instrumentoPocID)
        {
            try
            {
                using (SqlConnection conexao = conexaoLab.AbrirConexao())
                {
                    string query = "INSERT INTO instrument(instrumentid,instrumentdesc,usersequence,model,serialnum,purchasedt,cost,inserviceflag," +
                                   "certificationreqflag,location,servicecontractflag,notes,auditsequence,tracelogid,templateflag,createdt,createby," +
                                   "createtool,moddt,modby,instrumenttype,modtool,overrideallowedflag,instrumentmodelid,hostname,hostport,simulationmodeflag," +
                                   "activeflag,parentinstrumentid,instrumentstatus,unavailabilityreason,partflag,totalusagecount,totalusagetime,defaultusagetime," +
                                   "usagebytimeflag,usagetype,trackusageflag,calendarid,testingdepartmentid,workareadepartmentid,collectorpropertytreeid," +
                                   "collectorextendnodeid,collectorvaluetree,operationcommitflag,sdmspausedflag,postponeprocessingflag,sdmscollectorid,rebootrequiredflag) " +

                                   "VALUES(@ID_Instrumento, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', 'N', NULL, NULL, NULL, 1, NULL, 'N', 2022 - 05 - 05, 'admin', 'AddSDI', " +
                                   "2022 - 05 - 05, 'admin', 'GC', 'AddSDI', NULL, 'Balance', NULL, NULL, NULL, 'Y', NULL, @STATUS, NULL, 'N', NULL, NULL, NULL, 'N', " +
                                   "NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL); ";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.Add("@ID_Instrumento", SqlDbType.VarChar).Value = _instrumentoPocID;
                    cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Available";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao inserir instrumento em LAB:\n{e.Message}");
            }
        }
    }
}