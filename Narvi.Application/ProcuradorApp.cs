using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class ProcuradorApp
    {
        private ConexaoBD cnx;

        private Procurador One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Procurador()
                {
                    ProcuradorId = int.Parse(dt.Rows[pos]["idprocurador"].ToString()),
                    Nome = dt.Rows[pos]["nome"].ToString(),
                    Matricula = dt.Rows[pos]["matricula"].ToString(),
                    Lotacao = dt.Rows[pos]["lotacao"].ToString(),
                    Status = int.Parse(dt.Rows[pos]["status"].ToString())
                };
                return registro;
            }
            else
                return new Procurador();
        }

        private Procurador One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Procurador> Lista(string strQuery)
        {
            var lista = new List<Procurador>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Procurador procurador)
        {
            var strQuery = "";
            int id;
            var lid = new List<Procurador>();
            lid = ListAll();
            id = lid[lid.Count - 1].ProcuradorId + 1;
            strQuery += "INSERT INTO tblprocurador(idprocurador, nome, matricula, lotacao, status) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}', {4})", id,
                procurador.Nome, procurador.Matricula, procurador.Lotacao,
                procurador.Status.ToString());

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Procurador procurador)
        {
            var strQuery = "";
            strQuery += "UPDATE tblprocurador SET ";
            strQuery += string.Format("nome='{0}', matricula='{1}', lotacao='{2}', status={3} ",
                procurador.Nome, procurador.Matricula, procurador.Lotacao,
                procurador.Status.ToString());
            strQuery += "WHERE idprocurador=" + procurador.ProcuradorId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Procurador procurador)
        {
            if (procurador.ProcuradorId > 0)
                Alterar(procurador);
            else
                Inserir(procurador);
        }

        public void Excluir(Procurador procurador)
        {
            var strQuery = string.Format("DELETE FROM tblprocurador WHERE idprocurador={0}", procurador.ProcuradorId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Procurador OneId(int id)
        {
            return One(string.Format("SELECT * FROM tblprocurador WHERE idprocurador = {0}", id.ToString()));
        }

        public Procurador OneNome(string nome)
        {
            return One("SELECT * FROM tblprocurador WHERE nome='" + nome + "'");
        }

        public List<Procurador> ListAll()
        {
            return Lista("SELECT * FROM tblprocurador ORDER BY idprocurador");
        }
    }
}
