using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class PessoaApp
    {
        private ConexaoBD cnx;

        private Pessoa One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Pessoa()
                {
                    PessoaId = int.Parse(dt.Rows[pos]["idpessoa"].ToString()),
                    Nome = dt.Rows[pos]["nome"].ToString(),
                    Titulo = dt.Rows[pos]["titulo"].ToString(),
                    FisJur = dt.Rows[pos]["fisjur"].ToString(),
                    CPF = dt.Rows[pos]["cpf"].ToString(),
                    CNPJ = dt.Rows[pos]["cnpj"].ToString(),
                    Endereco = dt.Rows[pos]["endereco"].ToString(),
                    Complemento = dt.Rows[pos]["complemento"].ToString(),
                    Bairro = dt.Rows[pos]["bairro"].ToString(),
                    CEP = dt.Rows[pos]["cep"].ToString(),
                    Cidade = dt.Rows[pos]["cidade"].ToString(),
                    UF = dt.Rows[pos]["uf"].ToString(),
                    Telefone1 = dt.Rows[pos]["telefone1"].ToString(),
                    Telefone2 = dt.Rows[pos]["telefone2"].ToString(),
                    Qualidade = dt.Rows[pos]["qualidade"].ToString()
                };
                return registro;
            }
            else
                return new Pessoa();
        }

        private Pessoa One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Pessoa> ListStandart(string strQuery)
        {
            var lista = new List<Pessoa>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Pessoa pessoa)
        {
            var strQuery = "";
            int id;
            var lid = new List<Pessoa>();
            lid = ListAll();
            id = lid[lid.Count - 1].PessoaId + 1;
            strQuery += "INSERT INTO tblpessoa(idpessoa, nome, titulo, fisjur, cpf, cnpj, " +
                "endereco, complemento, bairro, cep, cidade, uf, telefone1, " +
                "telefone2, qualidade) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}', " +
                "'{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', " +
                "'{12}', '{13}', '{14}')", id, pessoa.Nome, pessoa.Titulo, pessoa.FisJur,
                pessoa.CPF, pessoa.CNPJ, pessoa.Endereco, pessoa.Complemento,
                pessoa.Bairro, pessoa.CEP, pessoa.Cidade, pessoa.UF,
                pessoa.Telefone1, pessoa.Telefone2, pessoa.Qualidade);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Pessoa pessoa)
        {
            var strQuery = "";
            strQuery += "UPDATE tblpessoa SET ";
            strQuery += string.Format("nome='{0}', titulo='{1}', fisjur='{2}', cpf='{3}', cnpj='{4}', " +
                "endereco='{5}', complemento='{6}', bairro='{7}', cep='{8}', cidade='{9}', uf='{10}', " +
                "telefone1='{11}', telefone2='{12}', qualidade='{13}' ", pessoa.Nome, pessoa.Titulo, 
                pessoa.FisJur, pessoa.CPF, pessoa.CNPJ, pessoa.Endereco, pessoa.Complemento, pessoa.Bairro, 
                pessoa.CEP, pessoa.Cidade, pessoa.UF, pessoa.Telefone1, pessoa.Telefone2, pessoa.Qualidade);

            strQuery += "WHERE idpessoa=" + pessoa.PessoaId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Pessoa pessoa)
        {
            if (pessoa.PessoaId > 0)
                Alterar(pessoa);
            else
                Inserir(pessoa);
        }

        public void Excluir(Pessoa pessoa)
        {
            var strQuery = string.Format("DELETE FROM tblpessoa WHERE idpessoa={0}", pessoa.PessoaId.ToString());

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Pessoa OneId(int id)
        {
            return One(string.Format("SELECT * FROM tblpessoa WHERE idpessoa={0}", id.ToString()));
        }

        public Pessoa OneNome(string nome)
        {
            return One("SELECT * FROM tblpessoa WHERE nome='" + nome + "'");
        }

        public Pessoa OneCPF(string cpf)
        {
            return One("SELECT * FROM tblpessoa WHERE cpf='" + cpf + "'");
        }

        public Pessoa OneCNPJ(string cnpj)
        {
            return One("SELECT * FROM tblpessoa WHERE cnpj='" + cnpj + "'");
        }
        public List<Pessoa> ListAll()
        {
            return ListStandart("SELECT * FROM tblpessoa ORDER BY idpessoa");
        }

        public List<Pessoa> ListAllF()
        {
            return ListStandart("SELECT * FROM tblpessoa WHERE fisjur='F'");
        }

        public List<Pessoa> ListAllJ()
        {
            return ListStandart("SELECT * FROM tblpessoa WHERE fisjur='J'");
        }

        public List<Pessoa> ListQualiF(string quali)
        {
            return ListStandart("SELECT * FROM tblpessoa WHERE fisjur='F' " + 
                "AND qualidade='" + quali + "'");
        }

        public List<Pessoa> ListQualiJ(string quali)
        {
            return ListStandart("SELECT * FROM tblpessoa WHERE fisjur='J' " +
                "AND qualidade='" + quali + "'");
        }
    }
}
