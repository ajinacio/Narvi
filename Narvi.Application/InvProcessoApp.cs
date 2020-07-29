using Narvi.Models;
using Narvi.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class InvProcessoApp
    {
        private ConexaoBD cnx;

        private InvProcesso One(DataTable dt, int pos)
        {
            string aux;
            string aux1 = "";
            int vol;
            double dou;
            if (dt.Rows.Count > 0)
            {
                aux = dt.Rows[pos]["DiaInventario"].ToString();

                if (aux.Length > 8)
                {
                    aux = aux.Substring(0, 10);
                    aux1 = aux.Substring(6, 4) + "/" + aux.Substring(3, 2) + "/" + aux.Substring(0, 2);
                }
                else aux1 = "2000/01/01";

                if (String.IsNullOrEmpty(dt.Rows[pos]["volume"].ToString())) vol = 0;
                else vol = int.Parse(dt.Rows[pos]["volume"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["valorglobal"].ToString())) dou = 0;
                else dou = double.Parse(dt.Rows[pos]["valorglobal"].ToString()) / 100;

                var registro = new InvProcesso()
                {
                    Obra = dt.Rows[pos]["obra"].ToString(),
                    DEATV = dt.Rows[pos]["deatv"].ToString(),
                    DiaInventario = DateTime.Parse(aux1),
                    Inventariante = dt.Rows[pos]["Inventariante"].ToString(),
                    Inventariado = dt.Rows[pos]["Inventariado"].ToString(),
                    Processo = dt.Rows[pos]["Processo"].ToString(),
                    Apenso = dt.Rows[pos]["Apenso"].ToString(),
                    RecOrdinario = dt.Rows[pos]["RecOrdinario"].ToString(),
                    RecReconsideracao = dt.Rows[pos]["RecReconsideracao"].ToString(),
                    RecRevisao = dt.Rows[pos]["RecRevisao"].ToString(),
                    Natureza = dt.Rows[pos]["Natureza"].ToString(),
                    Volume = vol,
                    Ajuste = dt.Rows[pos]["Ajuste"].ToString(),
                    Parcela = dt.Rows[pos]["Parcela"].ToString(),
                    ADManaus = dt.Rows[pos]["ADManaus"].ToString(),
                    AIManaus = dt.Rows[pos]["AIManaus"].ToString(),
                    Prefeitura = dt.Rows[pos]["Prefeitura"].ToString(),
                    Camara = dt.Rows[pos]["Camara"].ToString(),
                    ADAmazonas = dt.Rows[pos]["ADAmazonas"].ToString(),
                    AIAmazonas = dt.Rows[pos]["AIAmazonas"].ToString(),
                    EPPrefeitura = dt.Rows[pos]["EPPrefeitura"].ToString(),
                    EPSFL = dt.Rows[pos]["EPSFL"].ToString(),
                    Objeto = dt.Rows[pos]["Objeto"].ToString(),
                    ValorGlobal = dou,
                    SitConcedente = dt.Rows[pos]["SitConcedente"].ToString(),
                    SitConvenente = dt.Rows[pos]["SitConvenente"].ToString(),
                    Relator = dt.Rows[pos]["Relator"].ToString(),
                    Localizacao = dt.Rows[pos]["localizacao"].ToString(),
                    Armario = dt.Rows[pos]["Armario"].ToString(),
                    Observacao = dt.Rows[pos]["Observacao"].ToString(),
                    Concedente = dt.Rows[pos]["Concedente"].ToString(),
                    Convenente = dt.Rows[pos]["Convenente"].ToString(),
                    OBS = dt.Rows[pos]["OBS"].ToString()
                };
                return registro;
            }
            else
                return new InvProcesso();
        }

        private InvProcesso One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<InvProcesso> ListStandart(string strQuery)
        {
            var lista = new List<InvProcesso>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new InvProcesso();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        public InvProcesso OneProcesso(string proc)
        {
            return One("SELECT * FROM invprocesso WHERE processo=" + proc);
        }

        public List<InvProcesso> ListAll()
        {
            return ListStandart("SELECT * FROM invprocesso");
        }
    }
}
