using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Narvi.Repository
{
    public class ConexaoBD : IDisposable
    {
        private readonly SqlConnection conexao;
        private readonly OleDbConnection conex;
      //  string TipoBD = "SQLServer";
       // string TipoBD = "Access";
       // string strcnx = "Provider = Microsoft.Jet.OLEDB.4.0; User ID = Admin; Data Source = Narvi.mdb";
        string strcnx = "server = GRUMIO\\MANDII;Initial Catalog = Narvi; User ID = sa; Password=12tres;";

        public ConexaoBD()
        {
            // conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString);
            //if (TipoBD == "SQLServer")
            //{
                conexao = new SqlConnection(strcnx);
                conexao.Open();
            //}
            //else
            //{
            //    conex = new OleDbConnection(strcnx);
            //    conex.Open();
            //}
        }

        public void CommNom(string strQuery)
        {
            //if (TipoBD == "SQLServer")
            //{
                var cmdComando = new SqlCommand
                {
                    CommandText = strQuery,
                    CommandType = CommandType.Text,
                    Connection = conexao
                };
                cmdComando.ExecuteNonQuery();
            //}
            //else
            //{
            //    var cmdComando = new OleDbCommand
            //    {
            //        CommandText = strQuery,
            //        CommandType = CommandType.Text,
            //        Connection = conex
            //    };
            //    cmdComando.ExecuteNonQuery();
            //}
        }

        public DataTable Datatable(string strQuery)
        {
            var dt = new DataTable();
            var da = new SqlDataAdapter();
          //  var daOleDb = new OleDbDataAdapter();

            //if (TipoBD == "SQLServer")
            //{
                da.SelectCommand = new SqlCommand(strQuery, conexao);
                da.Fill(dt);
            //}
            //else
            //{
            //    daOleDb.SelectCommand = new OleDbCommand(strQuery, conex);
            //    daOleDb.Fill(dt);
            //}
            return dt;
        }

        public void Dispose()
        {
            //if (TipoBD == "SQLServer")
            //{
                if (conexao.State == ConnectionState.Open) conexao.Close();
            //}
            //else
            //{
            //    if (conex.State == ConnectionState.Open) conex.Close();
            //}
        }
    }
}
