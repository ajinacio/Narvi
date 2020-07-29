using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class NaturezaApp
    {
        private ConexaoBD cnx;

        private Natureza One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Natureza()
                {
                    NaturezaId = int.Parse(dt.Rows[pos]["idnatureza"].ToString()),
                    natureza = dt.Rows[pos]["natureza"].ToString()
                };
                return registro;
            }
            else
                return new Natureza();
        }

        private Natureza One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Natureza> ListStandart(string strQuery)
        {
            var lista = new List<Natureza>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Natureza natureza)
        {
            var strQuery = "";
            int id;
            var lid = new List<Natureza>();
            lid = ListAll();
            id = lid[lid.Count - 1].NaturezaId + 1;
            strQuery += "INSERT INTO tblnatureza(idnatureza, natureza) VALUES (" + id + ", '" + natureza.natureza + "')";

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Natureza natureza)
        {
            var strQuery = "UPDATE tblnatureza SET natureza = '" + natureza.natureza +
            "' WHERE idnatureza=" + natureza.NaturezaId.ToString(); 

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Natureza natureza)
        {
            if (natureza.NaturezaId > 0)
                Alterar(natureza);
            else
                Inserir(natureza);
        }

        public void Excluir(Natureza natureza)
        {
            var strQuery = "DELETE FROM tblnatureza WHERE idnatureza = " + natureza.NaturezaId;

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Natureza OneId(int id)
        {
            return One("SELECT * FROM tblnatureza WHERE idnatureza = " + id.ToString());
        }

        public Natureza OneNatureza(string nat)
        {
            return One("SELECT * FROM tblnatureza WHERE natureza='" + nat + "'");
        }

        public List<Natureza> ListAll()
        {
            return ListStandart("SELECT * FROM tblnatureza ORDER BY idnatureza");
        }
    }
}
