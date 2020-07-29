using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class RespApp
    {
        private ConexaoBD cnx;

        private Responsavel One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Responsavel()
                {
                    RespId = int.Parse(dt.Rows[pos]["idresp"].ToString()),
                    ProcessoId = int.Parse(dt.Rows[pos]["idprocesso"].ToString()),
                    PessoaId = int.Parse(dt.Rows[pos]["idpessoa"].ToString()),
                    Convenente = dt.Rows[pos]["convenente"].ToString(),
                    PatronoId = int.Parse(dt.Rows[pos]["idpatrono"].ToString()),
                    Situacao = int.Parse(dt.Rows[pos]["situacao"].ToString()),
                    Obs = dt.Rows[pos]["obs"].ToString(),
                    TipoPatrono = dt.Rows[pos]["tipopatrono"].ToString()
                };
                return registro;
            }
            else
                return new Responsavel();
        }

        private Responsavel One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Responsavel> ListStandart(string strQuery)
        {
            var lista = new List<Responsavel>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Responsavel resp)
        {
            var strQuery = "";
            int id;
            var lid = new List<Responsavel>();
            lid = ListAll();
            id = lid[lid.Count - 1].RespId + 1;
            strQuery += string.Format("INSERT INTO tblresponsavel(idresp, idprocesso, idpessoa, convenente, " +
                "patronoid, situacao, obs, tipopatrono) VALUES ({0}, {1}, {2}, '{3}', {4}, {5}, " +
                "'{6}', '{7}')", id, resp.ProcessoId.ToString(), resp.PessoaId.ToString(), resp.Convenente, 
                resp.PatronoId.ToString(), resp.Situacao.ToString(), resp.Obs, resp.TipoPatrono);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Responsavel resp)
        {
            var strQuery = "";
            strQuery += "UPDATE tblresponsavel SET ";
            strQuery += string.Format("idprocesso={0}, idpessoa={1}, convenente='{2}', idpatrono={3}, " +
                "situacao={4}, obs='{5}', tipopatrono='{6}'", resp.ProcessoId.ToString(), resp.PessoaId.ToString(),
                resp.Convenente, resp.PatronoId.ToString(), resp.Situacao.ToString(), resp.Obs, 
                resp.TipoPatrono);
            strQuery += "WHERE idresp=" + resp.RespId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Responsavel resp)
        {
            if (resp.RespId > 0) Alterar(resp);
            else Inserir(resp);
        }

        public void Excluir(Responsavel resp)
        {
            var strQuery = "DELETE FROM tblresponsavel WHERE idresp=" + resp.RespId.ToString();
            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Responsavel OneId(int id)
        {
            return One("SELECT * FROM tblresponsavel WHERE idresp=" + id.ToString());
        }

        public Responsavel OneData(int proc, int pess, string cv)
        {
            return One("SELECT * FROM tblresponsavel WHERE idprocesso=" + 
                proc.ToString() + " AND idpessoa=" + pess.ToString() +
                " AND convenente='" + cv + "'");
        }

        public List<Responsavel> ListAll()
        {
            return ListStandart("SELECT * FROM tblresponsavel ORDER BY idresp");
        }

        public List<Responsavel> ListProcesso(int proc, string conv)
        {
            if (conv == "")
                return ListStandart("SELECT * FROM tblresponsavel WHERE idprocesso=" +
                    proc.ToString());
            else return ListStandart("SELECT * FROM tblresponsavel WHERE idprocesso=" +
                    proc.ToString() + " AND convenente='" + conv + "'");
        }
    }
}
