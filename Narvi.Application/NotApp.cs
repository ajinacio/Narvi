using Narvi.Models;
using Narvi.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class NotApp
    {
        private ConexaoBD cnx;

        private Notificacao One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Notificacao()
                {
                    NotificacaoId = int.Parse(dt.Rows[pos]["notificacaoid"].ToString()),
                    Numero = dt.Rows[pos]["numero"].ToString(),
                    Assunto = dt.Rows[pos]["assunto"].ToString(),
                    PessoaId = int.Parse(dt.Rows[pos]["pessoaid"].ToString()),
                    ProcessoId = int.Parse(dt.Rows[pos]["processoid"].ToString()),
                    AgenteId = int.Parse(dt.Rows[pos]["agenteid"].ToString()),
                    Emissao = DateTime.Parse(dt.Rows[pos]["emissao"].ToString()),
                    Recebimento = DateTime.Parse(dt.Rows[pos]["recebimento"].ToString())
                };
                return registro;
            }
            else
                return new Notificacao();
        }

        private Notificacao One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Notificacao> ListStandart(string strQuery)
        {
            var lista = new List<Notificacao>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Notificacao not)
        {
            var strQuery = "";
            int id;
            var lid = new List<Notificacao>();
            lid = ListAll();
            id = lid[lid.Count - 1].NotificacaoId + 1;
            strQuery += "INSERT INTO tblnotificacao(notificacaoid, numero, assunto, pessoaid, " +
                "processoid, agenteid, emissao, recebimento) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, '{6}', '{7}')", 
                id, not.Numero, not.Assunto, not.PessoaId, not.ProcessoId, not.AgenteId,
                not.Emissao, not.Recebimento);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Notificacao not)
        {
            var strQuery = "";
            strQuery += "UPDATE tblnotificacao SET ";
            strQuery += string.Format("numero='{0}', assunto='{1}', pessoaid={2}, " +
                "processoid={3}, agenteid={4}, emissao='{5}', recebimento='{6}'",
            not.Numero, not.Assunto, not.PessoaId, not.ProcessoId, not.AgenteId, not.Emissao, not.Recebimento);
            strQuery += "WHERE notificacaoid=" + not.NotificacaoId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Notificacao not)
        {
            if (not.NotificacaoId > 0)
                Alterar(not);
            else
                Inserir(not);
        }

        public void Excluir(Notificacao not)
        {
            var strQuery = string.Format("DELETE FROM tblnotificacao WHERE notificacaoid={0}", not.NotificacaoId);
            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Notificacao OneId(int id)
        {
            return One("SELECT * FROM tblnotificacao WHERE notificacaoid=" + id.ToString());
        }

        public Notificacao OneNumero(string numero)
        {
            return One("SELECT * FROM tblnotificacao WHERE numero='" + numero + "'");
        }

        public List<Notificacao> ListAll()
        {
            return ListStandart("SELECT * FROM tblnotificacao ORDER BY notificacaoid");
        }

        public List<Notificacao> ListProcesso(int idproc)
        {
            return ListStandart("SELECT * FROM tblnotificacao WHERE processoid=" + idproc);
        }

        public List<Notificacao> ListPessoa(int idpess)
        {
            return ListStandart("SELECT * FROM tblnotificacao WHERE pessoaid=" + idpess);
        }
    }
}
