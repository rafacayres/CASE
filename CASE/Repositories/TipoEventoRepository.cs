using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class TipoEventoRepository
    {

        public List<TipoEvento> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_tipo_evento,nome FROM tipoevento ORDER BY id_tipo_evento",
                ConverterLinha
            );
        }

        public TipoEvento BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_tipo_evento,nome FROM tipoevento WHERE id_tipo_evento=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(TipoEvento item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO tipoevento (nome) VALUES (@nome)",
                comando => AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome)
            );
        }

        public bool Atualizar(TipoEvento item)
        {
            return AcessoDados.Executar(
                "UPDATE tipoevento SET nome=@nome WHERE id_tipo_evento=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdTipoEvento);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                }) > 0;

        }

        private static TipoEvento ConverterLinha(MySqlDataReader leitor)
        {
            return new TipoEvento
            {
                IdTipoEvento = leitor.GetInt32("id_tipo_evento"),
                Nome = leitor.GetString("nome")
            };
        }
    }
}
