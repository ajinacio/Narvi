using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class GrupoRestrApp
    {
        private ConexaoBD cnx;

        private GrupoRestr One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new GrupoRestr()
                {
                    GrupoRestrId = int.Parse(dt.Rows[pos]["idgrestr"].ToString()),
                    Grupo = dt.Rows[pos]["grupo"].ToString(),
                    Obs = dt.Rows[pos]["obs"].ToString(),
                    Ordem = int.Parse(dt.Rows[pos]["ordem"].ToString()),
                    Codigo = dt.Rows[pos]["codigo"].ToString()
                };
                return registro;
            }
            else
                return new GrupoRestr();
        }

        private GrupoRestr One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<GrupoRestr> ListStandart(string strQuery)
        {
            var lista = new List<GrupoRestr>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(GrupoRestr gr)
        {
            var dados = new List<string>();
            var strQuery = "";
            int id;
            var lid = new List<GrupoRestr>();
            lid = ListAll();
            id = lid[lid.Count - 1].GrupoRestrId + 1;
            strQuery += "INSERT INTO tblgruporestr(idgrestr, grupo, obs, codigo, ordem) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}', {4})", id, gr.Grupo, 
                gr.Obs, gr.Codigo, gr.Ordem);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(GrupoRestr gr)
        {
            var strQuery = "";
            strQuery += "UPDATE tblgruporestr SET ";
            strQuery += string.Format("grupo='{0}', obs='{1}', codigo='{2}', ordem={3} ", 
                gr.Grupo, gr.Obs, gr.Codigo, gr.Ordem);
            strQuery += string.Format("WHERE idgrestr={0} ", gr.GrupoRestrId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(GrupoRestr gr)
        {
            if (gr.GrupoRestrId > 0)
                Alterar(gr);
            else
                Inserir(gr);
        }

        public void Excluir(GrupoRestr gr)
        {
            var strQuery = string.Format("DELETE FROM tblgruporestr WHERE idgrestr={0}", gr.GrupoRestrId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public GrupoRestr OneId(int id)
        {
            return One("SELECT * FROM tblgruporestr WHERE idrestr=" + id);
        }

        public GrupoRestr OneGrupo(string gr)
        {
            return One("SELECT * FROM tblgruporestr WHERE grupo='" + gr + "'");
        }

        public GrupoRestr OneCodigo(int cod)
        {
            return One("SELECT * FROM tblgruporestr WHERE codigo='" + cod + "'");
        }

        public List<GrupoRestr> ListAll()
        {
            return ListStandart("SELECT * FROM tblgruporestr ORDER BY idgrestr");
        }
    }
}
