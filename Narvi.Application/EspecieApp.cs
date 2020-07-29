using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class EspecieApp
    {
        private ConexaoBD cnx;

        private Especie One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Especie()
                {
                    EspecieId = int.Parse(dt.Rows[pos]["idespecie"].ToString()),
                    especie = dt.Rows[pos]["especie"].ToString()
                };
                return registro;
            }
            else
                return new Especie();
        }

        private Especie One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Especie> ListStandart(string strQuery)
        {
            var lista = new List<Especie>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Especie especie)
        {
            var strQuery = "";
            int id;
            var lid = new List<Especie>();
            lid = ListAll();
            id = lid[lid.Count - 1].EspecieId + 1;
            strQuery += "INSERT INTO tblespecie(idespecie, especie) VALUES (" + id + ", '" + especie.especie + "')";

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Especie especie)
        {
            var strQuery = "UPDATE tblespecie SET especie='" + especie.especie +
            "' WHERE idespecie=" + especie.EspecieId.ToString(); ;

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Especie especie)
        {
            if (especie.EspecieId > 0)
                Alterar(especie);
            else
                Inserir(especie);
        }

        public void Excluir(Especie especie)
        {
            var strQuery = "DELETE FROM tblespecie WHERE idespecie = " + especie.EspecieId;

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Especie OneId(int id)
        {
            return One("SELECT * FROM tblespecie WHERE idespecie = " + id.ToString());
        }

        public Especie OneEspecie(string esp)
        {
            return One("SELECT * FROM tblespecie WHERE especie='" + esp + "'");
        }

        public List<Especie> ListAll()
        {
            return ListStandart("SELECT * FROM tblespecie ORDER BY idespecie");
        }
    }
}
