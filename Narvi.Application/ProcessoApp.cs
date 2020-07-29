using Narvi.Models;
using Narvi.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class ProcessoApp
    {
        private ConexaoBD cnx;

        private Processo One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                int[] aux = new int[14];
                double dou = 0;

                if (String.IsNullOrEmpty(dt.Rows[pos]["valorglobal"].ToString())) dou = 0;
                else dou = double.Parse(dt.Rows[pos]["valorglobal"].ToString())/100;

                if (String.IsNullOrEmpty(dt.Rows[pos]["obra"].ToString())) aux[0] = 0;
                else aux[0] = int.Parse(dt.Rows[pos]["obra"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idapenso"].ToString())) aux[1] = 0;
                else aux[1] = int.Parse(dt.Rows[pos]["idapenso"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idnatureza"].ToString())) aux[2] = 0;
                else aux[2] = int.Parse(dt.Rows[pos]["idnatureza"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idespecie"].ToString())) aux[3] = 0;
                else aux[3] = int.Parse(dt.Rows[pos]["idespecie"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idprocurador"].ToString())) aux[4] = 0;
                else aux[4] = int.Parse(dt.Rows[pos]["idprocurador"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["volume"].ToString())) aux[5] = 0;
                else aux[5] = int.Parse(dt.Rows[pos]["volume"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idsetor"].ToString())) aux[6] = 0;
                else aux[6] = int.Parse(dt.Rows[pos]["idsetor"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idarmario"].ToString())) aux[7] = 0;
                else aux[7] = int.Parse(dt.Rows[pos]["idarmario"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idconcedente"].ToString())) aux[8] = 0;
                else aux[8] = int.Parse(dt.Rows[pos]["idconcedente"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idconvpubl"].ToString())) aux[9] = 0;
                else aux[9] = int.Parse(dt.Rows[pos]["idconvpubl"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idconvpriv"].ToString())) aux[10] = 0;
                else aux[10] = int.Parse(dt.Rows[pos]["idconvpriv"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idrelator"].ToString())) aux[11] = 0;
                else aux[11] = int.Parse(dt.Rows[pos]["idrelator"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["situacao"].ToString())) aux[12] = 0;
                else aux[12] = int.Parse(dt.Rows[pos]["situacao"].ToString());

                if (String.IsNullOrEmpty(dt.Rows[pos]["idagente"].ToString())) aux[13] = 0;
                else aux[13] = int.Parse(dt.Rows[pos]["idagente"].ToString());

                var registro = new Processo()
                {
                    ProcessoId = int.Parse(dt.Rows[pos]["idprocesso"].ToString()),
                    Numero = int.Parse(dt.Rows[pos]["numero"].ToString()),
                    Ano = int.Parse(dt.Rows[pos]["ano"].ToString()),
                    Obra = aux[0],
                    ApensoId = aux[1],
                    NaturezaId = aux[2],
                    EspecieId = aux[3],
                    ProcuradorId = aux[4],
                    Objeto = dt.Rows[pos]["objeto"].ToString(),
                    Volume = aux[5],
                    Ajuste = dt.Rows[pos]["ajuste"].ToString(),
                    ValorGlobal = dou,
                    SetorId = aux[6],
                    ArmarioId = aux[7],
                    ConcedenteId = aux[8],
                    ConvPublId = aux[9],
                    ConvPrivId = aux[10],
                    Parcela = dt.Rows[pos]["parcela"].ToString(),
                    RelatorId = aux[11],
                    Obs = dt.Rows[pos]["obs"].ToString(),
                    Situacao = aux[12],
                    AgenteId = aux[13],
                    Inventariado = dt.Rows[pos]["inventariado"].ToString(),
           //         Inventariante = dt.Rows[pos]["inventariante"].ToString(),
                };
                return registro;
            }
            else
                return new Processo();
        }

        private Processo One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Processo> ListStandart(string strQuery)
        {
            var lista = new List<Processo>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }
        
        private void Inserir(Processo processo)
        {
            var strQuery = "";
            int id;
            var lid = new List<Processo>();
            lid = ListAll();
            if (lid.Count > 0) id = lid[lid.Count - 1].ProcessoId + 1;
            else id = 1;
            strQuery += "INSERT INTO tblprocesso(idprocesso, numero, ano, obra, idapenso, " +
                "idnatureza, idespecie, idprocurador, objeto, volume, ajuste, " +
                "valorglobal, idsetor, idarmario, idconcedente, idconvpubl, " +
                "idconvpriv, parcela, idrelator, obs, situacao, idagente, inventariado, inventariante) ";
            strQuery += string.Format("VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, " +
                "'{8}', {9}, '{10}', {11}, {12}, {13}, {14}, {15}, {16}, '{17}', " +
                "{18}, '{19}', {20}, {21}, '{22}', '{23}')", id, processo.Numero, 
                processo.Ano, processo.Obra, processo.ApensoId,
                processo.NaturezaId, processo.EspecieId,
                processo.ProcuradorId, processo.Objeto, processo.Volume, 
                processo.Ajuste, processo.ValorGlobal, processo.SetorId, 
                processo.ArmarioId, processo.ConcedenteId, 
                processo.ConvPublId, processo.ConvPrivId,
                processo.Parcela, processo.RelatorId, processo.Obs, 
                processo.Situacao, processo.AgenteId, processo.Inventariado,
                processo.Inventariante);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Processo processo)
        {
            var strQuery = "";
            strQuery += "UPDATE tblprocesso SET ";
            strQuery += string.Format("numero={0}, ano={1}, obra={2}, idapenso={3}, " +
                "idnatureza={4}, idespecie={5}, idprocurador={6}, objeto='{7}', volume={8}, " +
                "ajuste='{9}', valorglobal={10}, idsetor={11}, idarmario={12}, " +
                "idconcedente={13}, idconvPubl={14}, idconvPriv={15}, parcela='{16}', " +
                "idrelator={17}, obs='{18}', situacao={19}, idagente={20}, " +
                "inventariado='{21}', inventariante='{22}' ", processo.Numero.ToString(), processo.Ano.ToString(),
                processo.Obra.ToString(), processo.ApensoId.ToString(), processo.NaturezaId.ToString(), 
                processo.EspecieId.ToString(), processo.ProcuradorId.ToString(), processo.Objeto, 
                processo.Volume.ToString(), processo.Ajuste, processo.ValorGlobal.ToString(), 
                processo.SetorId.ToString(), processo.ArmarioId.ToString(), processo.ConcedenteId.ToString(), 
                processo.ConvPublId.ToString(), processo.ConvPrivId.ToString(), processo.Parcela, 
                processo.RelatorId.ToString(), processo.Obs, processo.Situacao.ToString(),
                processo.AgenteId.ToString(), processo.Inventariado, processo.Inventariante);
            strQuery += "WHERE idprocesso=" + processo.ProcessoId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
         }

        public void Salvar(Processo processo)
        {
            if (processo.ProcessoId > 0)
                Alterar(processo);
            else
                Inserir(processo);
        }

        public void Excluir(Processo processo)
        {
            var strQuery = string.Format("DELETE FROM tblprocesso WHERE idprocesso={0}", processo.ProcessoId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Processo OneId(int id)
        {
            return One("SELECT * FROM tblprocesso WHERE idprocesso = " + id.ToString());
        }

        public Processo OneNumAno(string num, string ano)
        {
            return One("SELECT * FROM tblprocesso WHERE numero=" + num +
                " AND ano=" + ano);
        }

        public List<Processo> ListAll()
        {
            return ListStandart("SELECT * FROM tblprocesso ORDER BY idprocesso");
        }

    }
}
