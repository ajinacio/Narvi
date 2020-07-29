using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class RespProcApp
    {

        private ConexaoBD cnx;

        public List<RespProc> Lista(int idproc)
        {
            var lista = new List<RespProc>();

            using (cnx = new ConexaoBD())
            {
                var strQuery = "SELECT * FROM tblresponsavel WHERE idprocesso = " + idproc.ToString();
                var dt = new DataTable();
                var dtr = new DataTable();
                dtr = cnx.Datatable(strQuery);

                for (int x=0; x < dtr.Rows.Count; x++)
                {
                    var registro = new RespProc();

                    registro.ProcessoId = int.Parse(dtr.Rows[x]["idprocesso"].ToString());
                    registro.PessoaId = int.Parse(dtr.Rows[x]["idpessoa"].ToString());
                    registro.Convenente = dtr.Rows[x]["convenente"].ToString();

                    strQuery = "SELECT * FROM tblpessoa WHERE idpessoa=" +
                        dtr.Rows[x]["idpessoa"].ToString();

                    dt = cnx.Datatable(strQuery);

                    registro.Nome = dt.Rows[0]["nome"].ToString();
                    registro.CPF = dt.Rows[0]["cpf"].ToString();
                    registro.Endereco = dt.Rows[0]["endereco"].ToString();
                    registro.Complemento = dt.Rows[0]["complemento"].ToString();
                    registro.Bairro = dt.Rows[0]["bairro"].ToString();
                    registro.CEP = dt.Rows[0]["cep"].ToString();
                    registro.Cidade = dt.Rows[0]["cidade"].ToString();
                    registro.UF = dt.Rows[0]["uf"].ToString();

                    lista.Add(registro);
                }

            }

            return lista;
        }
    }
}
