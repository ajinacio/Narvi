using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class OrgaoApp
    {
        private ConexaoBD cnx;

        private Orgao One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Orgao()
                {
                    OrgaoId = int.Parse(dt.Rows[pos]["idorgao"].ToString()),
                    Tipo = dt.Rows[pos]["tipo"].ToString(),
                    Sigla = dt.Rows[pos]["sigla"].ToString(),
                    Descricao = dt.Rows[pos]["descricao"].ToString(),
                    CNPJ = dt.Rows[pos]["cnpj"].ToString(),
                    Endereco = dt.Rows[pos]["endereco"].ToString(),
                    Complemento = dt.Rows[pos]["complemento"].ToString(),
                    CEP = dt.Rows[pos]["cep"].ToString(),
                    Cidade = dt.Rows[pos]["cidade"].ToString(),
                    UF = dt.Rows[pos]["uf"].ToString(),
                    Telefone1 = dt.Rows[pos]["telefone1"].ToString(),
                    Telefone2 = dt.Rows[pos]["telefone2"].ToString(),
                    Estado = dt.Rows[pos]["estado"].ToString(),
                    Municipio = dt.Rows[pos]["municipio"].ToString(),
                };
                return registro;
            }
            else
                return new Orgao();
        }

        private Orgao One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                using (cnx = new ConexaoBD())
                {
                    var dt = cnx.Datatable(strQuery);
                    return One(dt, 0);
                }
            }
        }

        private List<Orgao> ListStandart(string strQuery)
        {
            var lista = new List<Orgao>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Orgao orgao)
        {
            var strQuery = "";
            int id;
            var lid = new List<Orgao>();
            lid = ListAll();
            id = lid[lid.Count - 1].OrgaoId + 1;
            strQuery += string.Format("INSERT INTO tblorgao(idorgao, tipo, sigla, descricao, cnpj, " +
                "endereco, complemento, cep, cidade, uf, telefone1, telefone2, estado, municipio) " +
                "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', " +
                "'{11}', '{12}', '{13}')", id, orgao.Tipo, orgao.Sigla, orgao.Descricao, orgao.CNPJ,
                orgao.Endereco, orgao.Complemento, orgao.CEP, orgao.Cidade, orgao.UF, orgao.Telefone1, 
                orgao.Telefone2, orgao.Estado, orgao.Municipio);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Orgao orgao)
        {
            var strQuery = "";
            strQuery += "UPDATE tblorgao SET ";
            strQuery += string.Format("tipo='{0}', sigla='{1}', descricao='{2}', cnpj='{3}', " +
                "endereco='{4}', complemento='{5}', cep='{6}', cidade='{7}', uf='{8}', " + 
                "telefone1='{9}', telefone2='{10}', estado='{11}', " +
                "municipio='{12}' ", orgao.Tipo, orgao.Sigla, orgao.Descricao, orgao.CNPJ, 
                orgao.Endereco, orgao.Complemento, orgao.CEP, orgao.Cidade, orgao.UF, 
                orgao.Telefone1, orgao.Telefone2, orgao.Estado, orgao.Municipio);
            strQuery += "WHERE idorgao=" + orgao.OrgaoId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Orgao orgao)
        {
            if (orgao.OrgaoId > 0)
                Alterar(orgao);
            else
                Inserir(orgao);
        }

        public void Excluir(Orgao orgao)
        {
            var strQuery = string.Format("DELETE FROM tblorgao WHERE idorgao={0}", orgao.OrgaoId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Orgao OneId(int id)
        {
            return One("SELECT * FROM tblorgao WHERE idorgao=" + id.ToString());
        }

        public Orgao OneDescricao(string descr)
        {
            return One("SELECT * FROM tblorgao WHERE descricao='" + descr + "'");
        }

        public Orgao OneSigla(string sigla)
        {
            return One("SELECT * FROM tblorgao WHERE sigla='" + sigla + "'");
        }

        public Orgao OneCNPJ(string cnpj)
        {
            return One("SELECT * FROM tblorgao WHERE cnpj='" + cnpj + "'");
        }

        public List<Orgao> ListAll()
        {
            return ListStandart("SELECT * FROM tblorgao ORDER BY idorgao");
        }
    }
}
