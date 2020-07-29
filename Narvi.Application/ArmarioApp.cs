using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class ArmarioApp
    {
        private ConexaoBD cnx;

        private Armario One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Armario()
                {
                    ArmarioId = int.Parse(dt.Rows[pos]["idarmario"].ToString()),
                    Codigo = dt.Rows[pos]["codigo"].ToString(),
                    Descricao = dt.Rows[pos]["descricao"].ToString()
                };
                return registro;
            }
            else
                return new Armario();
        }

        private Armario One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Armario> ListStandart(string strQuery)
        {
            var lista = new List<Armario>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Armario();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Armario armario)
        {
            var strQuery = "";
            int id;
            List<Armario> lid;
            lid = ListAll();
            id = lid[lid.Count - 1].ArmarioId + 1;
            strQuery += "INSERT INTO tblarmario(idarmario, codigo, descricao) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}')", 
                id, armario.Codigo, armario.Descricao);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Armario armario)
        {
            var strQuery = "";
            strQuery += "UPDATE tblarmario SET ";
            strQuery += string.Format("codigo='{0}', descricao='{1}' ",
                armario.Codigo, armario.Descricao);
            strQuery += string.Format("WHERE idarmario={0} ", armario.ArmarioId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Armario armario)
        {
            if (armario.ArmarioId > 0)
                Alterar(armario);
            else
                Inserir(armario);
        }

        public void Excluir(Armario armario)
        {
            var strQuery = string.Format("DELETE FROM tblarmario WHERE idarmario={0}", armario.ArmarioId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Armario OneId(int id)
        {
            return One("SELECT * FROM tblarmario WHERE idarmario=" + id.ToString());
        }

        public Armario OneCodigo(string cod)
        {
            return One("SELECT * FROM tblarmario WHERE codigo='" + cod + "'");
        }

        public List<Armario> ListAll()
        {
            return ListStandart("SELECT * FROM tblarmario ORDER BY idarmario");
        }
    }
}
