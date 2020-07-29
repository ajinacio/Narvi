using Narvi.Models;
using Narvi.Repository;
using System.Collections.Generic;
using System.Data;

namespace Narvi.Application
{
    public class UsuarioApp
    {
        private ConexaoBD cnx;

        private Usuario One(DataTable dt, int pos)
        {
            if (dt.Rows.Count > 0)
            {
                var registro = new Usuario()
                {
                    UsuarioId = int.Parse(dt.Rows[pos]["idusuario"].ToString()),
                    Nome = dt.Rows[pos]["nome"].ToString(),
                    Login = dt.Rows[pos]["login"].ToString(),
                    Senha = dt.Rows[pos]["senha"].ToString()
                };
                return registro;
            }
            else
                return new Usuario();
        }

        private Usuario One(string strQuery)
        {
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                return One(dt, 0);
            }
        }

        private List<Usuario> ListStandart(string strQuery)
        {
            var lista = new List<Usuario>();
            using (cnx = new ConexaoBD())
            {
                var dt = cnx.Datatable(strQuery);
                var registro = new Usuario();
                for (int i=0; i < dt.Rows.Count; i++)
                    lista.Add(One(dt, i));
            }
            return lista;
        }

        private void Inserir(Usuario usuario)
        {
            var strQuery = "";
            int id;
            var lid = new List<Usuario>();
            lid = ListAll();
            id = lid[lid.Count - 1].UsuarioId + 1;
            strQuery += "INSERT INTO tblusuario(idusuario, nome, login, senha) ";
            strQuery += string.Format("VALUES ({0}, '{1}', '{2}', '{3}')", id, 
                usuario.Nome, usuario.Login, usuario.Senha);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        private void Alterar(Usuario usuario)
        {
            var strQuery = "";
            strQuery += "UPDATE tblusuario SET ";
            strQuery += string.Format("nome='{0}', login='{1}', senha='{2}' ", usuario.Nome,
            usuario.Login, usuario.Senha);
            strQuery += "WHERE idusuario=" + usuario.UsuarioId.ToString();

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public void Salvar(Usuario usuario)
        {
            if (usuario.UsuarioId > 0)
                Alterar(usuario);
            else
                Inserir(usuario);
        }

        public void Excluir(Usuario usuario)
        {
            var strQuery = string.Format("DELETE FROM tblusuario WHERE idusuario={0}", usuario.UsuarioId);

            using (cnx = new ConexaoBD())
                cnx.CommNom(strQuery);
        }

        public Usuario OneId(int id)
        {
            return One("SELECT * FROM tblusuario WHERE idusuario=" + id.ToString());
        }

        public Usuario OneNome(string nome)
        {
            return One("SELECT * FROM tblusuario WHERE nome='" + nome + "'");
        }

        public Usuario OneLogin(string login)
        {
            return One("SELECT * FROM tblusuario WHERE login='" + login + "'");
        }

        public List<Usuario> ListAll()
        {
            return ListStandart("SELECT * FROM tblusuario ORDER BY idusuario");
        }
    }
}
