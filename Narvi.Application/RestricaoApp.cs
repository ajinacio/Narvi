using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class RestricaoApp
    {
        private ConexaoBD cnx;

        private Restricao One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Restricao()
                {
                    RestricaoId = int.Parse(dt.Rows[pos]["idrestr"].ToString()),
                    GRestrId = int.Parse(dt.Rows[pos]["idgrestr"].ToString()),
                    Legexec = int.Parse(dt.Rows[pos]["legexec"].ToString()),
                    Codigo = dt.Rows[pos]["codigo"].ToString(),
                    Descricao = dt.Rows[pos]["descricao"].ToString()
                };
                return registro;
            }
            else
                return new Restricao();
        }

        private Restricao One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Restricao> ListStandart(string strQuery)
        {
            var lista = new List<Restricao>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Restricao restr)
        {
            var strQuery = "";
            int id;
            var lid = new List<Restricao>();
            lid = ListAll();
            id = lid[lid.Count - 1].RestricaoId + 1;
            strQuery += "INSERT INTO tblrestricao(idrestr, idgrestr, codigo, descricao, legexec) ";
            strQuery += string.Format("VALUES ({0}, {1}, '{2}', '{3}', {4})", id, restr.GRestrId, 
                restr.Codigo, restr.Descricao, restr.Legexec);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Restricao restr)
        {
            var strQuery = "";
            strQuery += "UPDATE tblrestricao SET ";
            strQuery += string.Format("idgrestr={0}, codigo='{1}', descricao='{2}', legexec={3} ", 
                restr.GRestrId, restr.Codigo, restr.Descricao, restr.Legexec);
            strQuery += string.Format("WHERE idrestr={0} ", restr.RestricaoId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Restricao restr)
        {
            if (restr.RestricaoId > 0)
                Alterar(restr);
            else
                Inserir(restr);
        }

        public void Excluir(Restricao restr)
        {
            var strQuery = string.Format("DELETE FROM tblrestricao WHERE idrestr={0}", restr.RestricaoId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Restricao OneId(int id)
        {
            return One("SELECT * FROM tblrestricao WHERE idrestr=" + id);
        }

        public Restricao OneCodigo(string cod)
        {
            return One("SELECT * FROM tblrestricao WHERE codigo='" + cod + "'");
        }

        public List<Restricao> ListAll()
        {
            return ListStandart("SELECT * FROM tblrestricao ORDER BY idrestricao");
        }

        public List<Restricao> ListaGrupo(int id)
        {
            return ListStandart("SELECT * FROM tblrestricao WHERE idgrestr=" + id);
        }
    }
}
