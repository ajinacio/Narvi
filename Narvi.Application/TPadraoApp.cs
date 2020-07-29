using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class TPadraoApp
    {
        private ConexaoBD cnx;

        private TPadrao One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new TPadrao()
                {
                    TPadraoId = int.Parse(dt.Rows[pos]["tpadraoid"].ToString()),
                    Tipo = dt.Rows[pos]["tipo"].ToString(),
                    Texto = dt.Rows[pos]["texto"].ToString()
                };
                return registro;
            }
            else
                return new TPadrao();
        }

        private TPadrao One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<TPadrao> ListStandart(string strQuery)
        {
            var lista = new List<TPadrao>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(TPadrao tpadrao)
        {
            var strQuery = "";
            int id;
            var lid = new List<TPadrao>();
            lid = ListAll();
            id = lid[lid.Count - 1].TPadraoId + 1;
            strQuery += "INSERT INTO tbltpadrao(tpadraoid, tipo, texto) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}')", id,
                tpadrao.Tipo, tpadrao.Texto);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(TPadrao tpadrao)
        {
            var strQuery = "";
            strQuery += "UPDATE tbltpadrao SET ";
            strQuery += string.Format("tipo='{0}', texto='{1}'",
            tpadrao.Tipo, tpadrao.Texto);
            strQuery += "WHERE tpadraoid=" + tpadrao.TPadraoId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(TPadrao tpadrao)
        {
            if (tpadrao.TPadraoId > 0)
                Alterar(tpadrao);
            else
                Inserir(tpadrao);
        }

        public void Excluir(TPadrao tpadrao)
        {
            var strQuery = string.Format("DELETE FROM tbltpadrao WHERE tpadraoid={0}", tpadrao.TPadraoId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public TPadrao OneId(int id)
        {
            return One("SELECT * FROM tbltpadrao WHERE tpadraoid = " + id.ToString());
        }

        public List<TPadrao> ListAll()
        {
            return ListStandart("SELECT * FROM tbltpadrao ORDER BY tpadraoid");
        }

        public List<TPadrao> ListTipo(string tipo)
        {
            return ListStandart("SELECT * FROM tbltpadrao WHERE tipo=" + tipo);
        }
    }
}
