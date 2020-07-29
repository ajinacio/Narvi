using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class RelatorApp
    {
        private ConexaoBD cnx;

        private Relator One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Relator()
                {
                    RelatorId = int.Parse(dt.Rows[pos]["idrelator"].ToString()),
                    Nome = dt.Rows[pos]["nome"].ToString(),
                    Matricula = dt.Rows[pos]["matricula"].ToString(),
                    NomeGuerra = dt.Rows[pos]["nomeguerra"].ToString(),
                    ConsAud = dt.Rows[pos]["consaud"].ToString(),
                    Lotacao = dt.Rows[pos]["lotacao"].ToString(),
                    Status = int.Parse(dt.Rows[pos]["status"].ToString())
                };
                return registro;
            }
            else
                return new Relator();
        }

        private Relator One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Relator> Lista(string strQuery)
        {
            var lista = new List<Relator>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Relator relator)
        {
            var strQuery = "";
            int id;
            var lid = new List<Relator>();
            lid = ListAll();
            id = lid[lid.Count - 1].RelatorId + 1;
            strQuery += "INSERT INTO tblrelator(idrelator, nome, matricula, nomeguerra, consaud, lotacao, status) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6})", id,
                relator.Nome, relator.Matricula, relator.NomeGuerra, relator.ConsAud, relator.Lotacao,
                relator.Status.ToString());

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Relator relator)
        {
            var strQuery = "";
            strQuery += "UPDATE tblrelator SET ";
            strQuery += string.Format("nome='{0}', matricula='{1}', nomeguerra='{2}', consaud='{3}', lotacao='{4}', status={3} ",
                relator.Nome, relator.Matricula, relator.NomeGuerra, relator.ConsAud, relator.Lotacao,relator.Status.ToString());
            strQuery += "WHERE idrelator=" + relator.RelatorId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Relator relator)
        {
            if (relator.RelatorId > 0)
                Alterar(relator);
            else
                Inserir(relator);
        }

        public void Excluir(Relator relator)
        {
            var strQuery = string.Format("DELETE FROM tblrelator WHERE idrelator={0}", relator.RelatorId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Relator OneId(int id)
        {
            return One("SELECT * FROM tblrelator WHERE idrelator=" + id.ToString());
        }

        public Relator OneNome(string nome)
        {
            return One("SELECT * FROM tblrelator WHERE nome='" + nome + "'");
        }

        public List<Relator> ListAll()
        {
            return Lista("SELECT * FROM tblrelator ORDER BY idrelator");
        }
    }
}
