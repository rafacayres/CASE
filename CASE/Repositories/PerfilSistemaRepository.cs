using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class PerfilSistemaRepository
    {

        public List<PerfilSistema> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_perfil_sistema,nome FROM perfilsistema ORDER BY id_perfil_sistema",
                ConverterLinha
            );
        }

        public PerfilSistema BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_perfil_sistema,nome FROM perfilsistema WHERE id_perfil_sistema=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(PerfilSistema item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO perfilsistema (nome) VALUES (@nome)",
                comando => AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome)
            );
        }

        public bool Atualizar(PerfilSistema item)
        {
            return AcessoDados.Executar(
                "UPDATE perfilsistema SET nome=@nome WHERE id_perfil_sistema=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdPerfilSistema);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                }) > 0;

        }

        private static PerfilSistema ConverterLinha(MySqlDataReader leitor)
        {
            return new PerfilSistema
            {
                IdPerfilSistema = leitor.GetInt32("id_perfil_sistema"),
                Nome = leitor.GetString("nome")
            };
        }
    }
}
