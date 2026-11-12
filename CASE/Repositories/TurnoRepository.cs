using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class TurnoRepository
    {

        public List<Turno> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_turno,nome FROM turno ORDER BY id_turno",
                ConverterLinha
            );
        }

        public Turno BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_turno,nome FROM turno WHERE id_turno=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(Turno item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO turno (nome) VALUES (@nome)",
                comando => AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome)
            );
        }

        public bool Atualizar(Turno item)
        {
            return AcessoDados.Executar(
                "UPDATE turno SET nome=@nome WHERE id_turno=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdTurno);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                }) > 0;

        }

        private static Turno ConverterLinha(MySqlDataReader leitor)
        {
            return new Turno
            {
                IdTurno = leitor.GetInt32("id_turno"),
                Nome = leitor.GetString("nome")
            };
        }
    }
}
