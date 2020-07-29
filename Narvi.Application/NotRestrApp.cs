using Narvi.Models;
using Narvi.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class NotRestrApp
    {
        private ConexaoBD cnx;

        private NotRestr One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new NotRestr()
                {
                    NotificacaoId = int.Parse(dt.Rows[pos]["notificacaoid"].ToString()),
                    RestricaoId = int.Parse(dt.Rows[pos]["restricaoid"].ToString()),
                    Complemento = dt.Rows[pos]["complemento"].ToString(),
                    Formatacao = dt.Rows[pos]["formatacao"].ToString(),
                    Valor = Double.Parse(dt.Rows[pos]["valor"].ToString())
                };
                return registro;
            }
            else
                return new NotRestr();
        }

        private NotRestr One(string strQuery)
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

        private List<NotRestr> ListStandart(string strQuery)
        {
            var lista = new List<NotRestr>();
                using (cnx = new ConexaoBD())
                {
                    var dt = cnx.Datatable(strQuery);
                    var registro = new Especie();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        lista.Add(One(dt, i));
                }
                return lista;
            }

        public void Inserir(NotRestr notrestr)
        {
            var strQuery = "";
            strQuery += "INSERT INTO tblnotrestr(notificacaoid, restricaoid, ordem, complemento, " + 
                "formatacao, valor) ";
            strQuery += string.Format("VALUES ({0}, {1}, '{2}', '{3}', {4})", notrestr.NotificacaoId,
                notrestr.RestricaoId, notrestr.Complemento, notrestr.Formatacao,
                notrestr.Valor);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Excluir(int idnot)
        {
            var strQuery = "DELETE FROM tblnotrestr WHERE notificacaoid=" + idnot;

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public NotRestr OneId(int notid, int restrid)
        {
            return One(string.Format("SELECT * FROM tblnotrestr WHERE notificacaoid = {0} " +
                "AND restricaoid = {1}", notid.ToString(), restrid.ToString()));
        }

        public List<NotRestr> ListNot(int notid)
        {
            return ListStandart("SELECT * FROM tblnotrestr WHERE notificacaoid=" + notid.ToString());
        }

        public List<NotRestr> ListRestr(int restrid)
        {
            return ListStandart("SELECT * FROM tblnotrestr WHERE restricaoid=" + restrid.ToString());
        }
    }
}
