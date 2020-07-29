using Narvi.Models;
using Narvi.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class HistRespApp
    {
        private ConexaoBD cnx;

        private HistResp One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new HistResp()
                {
                    RespId = int.Parse(dt.Rows[pos]["idresp"].ToString()),
                    Datan = int.Parse(dt.Rows[pos]["datan"].ToString()),
                    Documento = dt.Rows[pos]["documento"].ToString(),
                    Situacao = int.Parse(dt.Rows[pos]["situacao"].ToString()),
                    Data = DateTime.Parse(dt.Rows[pos]["data"].ToString())
                };
                return registro;
            }
            else
                return new HistResp();
        }

        private HistResp One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<HistResp> Lista(string strQuery)
        {
            var lista = new List<HistResp>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Especie();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        public void Inserir(HistResp histresp)
        {
            var strQuery = "";
            strQuery += "INSERT INTO tblhistresp(idresp, datan, documento, situacao, data) ";
            strQuery += string.Format("VALUES ({0}, {1}, '{2}', {3}, '{4}')",
                histresp.RespId.ToString(), histresp.Datan.ToString(), histresp.Documento, 
                histresp.Situacao.ToString(), histresp.Data.ToString());

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Alterar(HistResp hr)
        {
            var strQuery = "";
            strQuery += "UPDATE tblhistresp SET ";
            strQuery += string.Format("idresp={0}, datan={1}, documento='{2}', situacao={3}, data='{4}' ",
                hr.RespId.ToString(), hr.Datan.ToString(), hr.Documento, hr.Situacao.ToString(), 
                hr.Data.ToString());
            strQuery += string.Format("WHERE idresp={0} AND data='{1}' AND documento='{2}'",
                hr.RespId.ToString(), hr.Data.ToString(), hr.Documento);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Excluir(HistResp hr)
        {
            var strQuery = string.Format("DELETE FROM tblhistresp WHERE idresp={0} AND " +
                "data='{1}' AND documento='{2}'", hr.RespId, hr.Data, hr.Documento);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public HistResp OneId(int id, string dt, string doc)
        {
            return One(string.Format("SELECT * FROM tblhistresp WHERE idresp={0} AND data='{1}' " +
                "AND documento='{2}'", id.ToString(), dt, doc));
        }

        public List<HistResp> ListResp(int id)
        {
            return Lista("SELECT * FROM tblhistresp WHERE idresp=" + id.ToString());
        }
    }
}
