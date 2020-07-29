using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class SetorApp
    {
        private ConexaoBD cnx;

        private Setor One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Setor()
                {
                    SetorId = int.Parse(dt.Rows[pos]["idsetor"].ToString()),
                    Sigla = dt.Rows[pos]["sigla"].ToString(),
                    Descricao = dt.Rows[pos]["descricao"].ToString(),
                    Responsavel = dt.Rows[pos]["responsavel"].ToString()
                };
                return registro;
            }
            else
                return new Setor();
        }

        private Setor One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Setor> ListStandart(string strQuery)
        {
            var lista = new List<Setor>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Setor setor)
        {
            var strQuery = "";
            int id;
            var lid = new List<Setor>();
            lid = ListAll();
            id = lid[lid.Count - 1].SetorId + 1;
            strQuery += "INSERT INTO tblsetor (idsetor, sigla, descricao, responsavel) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}')", id,
                setor.Sigla, setor.Descricao, setor.Responsavel);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Setor setor)
        {
            var strQuery = "";
            strQuery += "UPDATE tblsetor SET ";
            strQuery += string.Format("sigla='{0}', descricao='{1}', responsavel='{2}' ",
                setor.Sigla, setor.Descricao, setor.Responsavel);
            strQuery += "WHERE idsetor=" + setor.SetorId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Setor setor)
        {
            if (setor.SetorId > 0)
                Alterar(setor);
            else
                Inserir(setor);
        }

        public void Excluir(Setor setor)
        {
            var strQuery = string.Format("DELETE FROM tblsetor WHERE idsetor={0}", setor.SetorId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Setor OneId(int id)
        {
            return One("SELECT * FROM tblsetor WHERE idsetor = " + id.ToString());
        }

        public Setor OneSigla(string sg)
        {
            return One("SELECT * FROM tblsetor WHERE sigla = '" + sg + "'");
        }

        public List<Setor> ListAll()
        {
            return ListStandart("SELECT * FROM tblsetor ORDER BY idsetor");
        }

    }
}
