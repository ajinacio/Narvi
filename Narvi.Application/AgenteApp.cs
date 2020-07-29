using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class AgenteApp
    {
        private ConexaoBD cnx;

        private Agente One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Agente()
                {
                    AgenteId = int.Parse(dt.Rows[pos]["idagente"].ToString()),
                    Nome = dt.Rows[pos]["nome"].ToString(),
                    NomeCompleto = dt.Rows[pos]["nomecompleto"].ToString(),
                    Cargo = dt.Rows[pos]["cargo"].ToString()
                };
                return registro;
            }
            else
                return new Agente();
        }

        private Agente One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Agente> ListStandart(string strQuery)
        {
            var lista = new List<Agente>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Agente();
                for (int i = 0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Agente agente)
        {
            var strQuery = "";
            int id;
            var lid = new List<Agente>();
            lid = ListAll();
            id = lid[lid.Count - 1].AgenteId + 1;
            strQuery += "INSERT INTO tblagente(idagente, nome, nomecompleto, matricula, cargo) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", id, agente.Nome, 
                agente.NomeCompleto, agente.Matricula, agente.Cargo);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Agente agente)
        {
            var strQuery = "";
            strQuery += "UPDATE tblagente SET ";
            strQuery += string.Format("nome='{0}', nomecompleto='{1}', matricula='{2}', cargo='{3}' ", 
                agente.Nome, agente.NomeCompleto, agente.Matricula, agente.Cargo);
            strQuery += string.Format("WHERE idagente={0} ", agente.AgenteId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Agente agente)
        {
            if (agente.AgenteId > 0)
                Alterar(agente);
            else
                Inserir(agente);
        }

        public void Excluir(Agente agente)
        {
            var strQuery = string.Format("DELETE FROM tblagente WHERE idagente={0}", agente.AgenteId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Agente OneId(int id)
        {
            return One("SELECT * FROM tblagente WHERE idagente=" + id.ToString());
        }

        public Agente OneNome(string nome)
        {
            return One("SELECT * FROM tblagente WHERE nome='" + nome + "'");
        }

        public Agente OneNomeCompleto(string nome)
        {
            return One("SELECT * FROM tblagente WHERE nomecompleto='" + nome + "'");
        }

        public List<Agente> ListAll()
        {
            return ListStandart("SELECT * FROM tblagente ORDER BY idagente");
        }

        public List<Agente> ListCargo(string cargo)
        {
             return ListStandart("SELECT * FROM tblagente WHERE cargo='" + cargo + "'");
        }
    }
}
