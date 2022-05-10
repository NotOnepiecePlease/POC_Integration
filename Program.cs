// compile with:
// /r:Microsoft.SqlServer.Smo.dll
// /r:Microsoft.SqlServer.ConnectionInfo.dll
// /r:Microsoft.SqlServer.Management.Sdk.Sfc.dll

using POC_SQL_Integration.Dados;

public class DataBaseConnect
{
    private static readonly BuscarDados buscarDados = new BuscarDados();
    private static readonly AtualizarDados atualizarDados = new AtualizarDados();
    private static readonly VerificarDados verificarDados = new VerificarDados();
    private static readonly InserirDados inserirDados = new InserirDados();

    private static Dictionary<string, string> instrumentosLAB;
    private static Dictionary<string, string> instrumentosPOC;

    public static void Main()
    {
        while (true)
        {
            IniciarProjeto();
            Console.WriteLine("\n\n");
            Thread.Sleep(3000);
        }
    }

    private static void IniciarProjeto()
    {
        ListarInstrumentos();
        VerificarStatusInstrumentosLab();
        VerificarExistenciaInstrumentos();
        Console.WriteLine("\n\nExecutando novamente em 3 segundos....");
    }

    private static void ListarInstrumentos()
    {
        instrumentosLAB = buscarDados.BuscarInstrumentoLAB();

        Console.WriteLine("-------= Instrumentos da LAB_Vantage =-------");
        foreach (KeyValuePair<string, string> instrumentoLAB in instrumentosLAB)
        {
            Console.WriteLine(instrumentoLAB.Key + " | " + instrumentoLAB.Value);
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("-------= Instrumentos da POC_Integration =---------");
        instrumentosPOC = buscarDados.BuscarInstrumentoPOC();
        foreach (KeyValuePair<string, string> instrumentoPOC in instrumentosPOC)
        {
            Console.WriteLine(instrumentoPOC.Key + " | " + instrumentoPOC.Value.TrimEnd(' '));
        }
    }

    private static void VerificarStatusInstrumentosLab()
    {
        Console.WriteLine("\n\nVerificando instrumentos e atualizando os status...");
        //Dictionary<string, string> instrumentosLabVantage = buscarDados.BuscarInstrumentoLAB();
        //Dictionary<string, string> instrumentosPocIntegration = buscarDados.BuscarInstrumentoPOC();

        foreach (KeyValuePair<string, string> instrumento_lab in instrumentosLAB)
        {
            foreach (KeyValuePair<string, string> instrumento_poc in instrumentosPOC)
            {
                if (instrumento_lab.Key == instrumento_poc.Key)
                {
                    if (instrumento_lab.Value.Trim() == "Available" && instrumento_poc.Value.Trim() == "MONT")
                    {
                        //Faz nada pois o instrumento existe e ele esta disponivel, igual a poc
                    }
                    else if (instrumento_lab.Value.Trim() == "Unavailable" && instrumento_poc.Value.Trim() != "MONT")
                    {
                        //Faz nada pois o instrumento existe e ele esta indisponivel, igual a poc
                    }
                    else
                    {
                        atualizarDados.AtualizarStatusInstrumentoLab(instrumento_lab.Key);
                        Console.WriteLine($"Instrumento: {instrumento_lab.Key} -- Status Atualizado!");
                    }
                }
            }
        }

        Console.WriteLine("\nPronto!");
    }

    private static void VerificarExistenciaInstrumentos()
    {
        Console.WriteLine("\n\nVerificando se todos os instrumentos da POC estao na LAB...");
        foreach (KeyValuePair<string, string> instrumentoPOC in instrumentosPOC)
        {
            bool isInstrumentoExiste = verificarDados.VerificarInstrumentoExisteEmLAB(instrumentoPOC.Key);

            if (isInstrumentoExiste == false)
            {
                inserirDados.InserirInstrumentoEmLab(instrumentoPOC.Key);
                Console.WriteLine($"Instrumento: {instrumentoPOC.Key} - Inserido com Sucesso!");
            }
        }

        Console.WriteLine("\nPronto!");
    }

    #region Codigo antigo do gabriel

    //public void Gabriel()
    //{
    //    String sqlServerLogin = "labvantage";
    //    String password = "Inter@1234";
    //    String labVantageInstanceName = "labvantage";
    //    String viewInstanceName = "POC_Integration";
    //    String SvrName = "ITFNOTE24\\SQLEXPRESS";
    //    String connectionStringLabvantage = "Data Source=" + SvrName + ";Initial Catalog=" + labVantageInstanceName + ";Integrated Security=true";
    //    String connectionStringView = "Data Source=" + SvrName + ";Initial Catalog=" + viewInstanceName + ";Integrated Security=true";

    //    String[] labvatangeInstrumentsIDs;
    //    String[] viewInstrumentsIDs;
    //    try
    //    {
    //        #region LabvantageConnection
    //        using (SqlConnection connection = new SqlConnection(connectionStringLabvantage))
    //        {
    //            Console.WriteLine("\nQuery data example:");
    //            Console.WriteLine("=========================================\n");

    //            String sql = "SELECT instrumentid, instrumentstatus FROM instrument";
    //            //String sql = "SELECT * FROM instrument";

    //            using (SqlCommand command = new SqlCommand(sql, connection))
    //            {
    //                connection.Open();
    //                using (SqlDataReader reader = command.ExecuteReader())
    //                {
    //                    while (reader.Read())
    //                    {
    //                        //Console.WriteLine(reader.GetString(columnIndex));
    //                        labvatangeInstrumentsIDs = new string[] { reader.GetString(0) };
    //                        Console.WriteLine(String.Format("{0}", reader[0]) + ";" + String.Format("{0}", reader[1]));
    //                        //viewInstrumentsIDs = new string[] { "Bal101", "BalanceAD", "Galaxie_GC_2", "GC101", "PCR101", "pH101", "pianio", "Seq201", "Titrator101", "UX4200H", "Galaxie_GC_1" };
    //                    }
    //                }
    //            }
    //        }
    //        #endregion
    //    }
    //    catch (SqlException e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //    Console.ReadLine();
    //}

    #endregion Codigo antigo do gabriel
}