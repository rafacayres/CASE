using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class JornadaSetorRepository
    {

        public List<JornadaSetor> ListarPorSetor(int idSetor)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_jornada,id_setor,`data`,id_turno FROM jornadasetor WHERE id_setor=@id ORDER BY `data`,id_jornada",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idSetor)
            );
        }

        public JornadaSetor BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_jornada,id_setor,`data`,id_turno FROM jornadasetor WHERE id_jornada=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public JornadaSetor BuscarPorSetorDataTurno(int idSetor, System.DateTime data, int idTurno)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_jornada,id_setor,`data`,id_turno FROM jornadasetor WHERE id_setor=@setor AND `data`=@data AND id_turno=@turno",
                ConverterLinha,
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@setor", idSetor);
                    AcessoDados.AdicionarParametro(comando, "@data", data.Date);
                    AcessoDados.AdicionarParametro(comando, "@turno", idTurno);
                });
        }

        public int Cadastrar(JornadaSetor item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO jornadasetor (id_setor,`data`,id_turno) VALUES (@setor,@data,@turno)",
                comando => Vincular(comando, item)
            );
        }

        public bool Atualizar(JornadaSetor item)
        {
            return AcessoDados.Executar(
                "UPDATE jornadasetor SET id_setor=@setor,`data`=@data,id_turno=@turno WHERE id_jornada=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdJornada);
                    Vincular(comando, item);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, JornadaSetor item)
        {
            AcessoDados.AdicionarParametro(comando, "@setor", item.IdSetor);
            AcessoDados.AdicionarParametro(comando, "@data", item.Data);
            AcessoDados.AdicionarParametro(comando, "@turno", item.IdTurno);
        }

        private static JornadaSetor ConverterLinha(MySqlDataReader leitor)
        {
            return new JornadaSetor
            {
                IdJornada = leitor.GetInt32("id_jornada"),
                IdSetor = leitor.GetInt32("id_setor"),
                Data = leitor.GetDateTime("data"),
                IdTurno = leitor.GetInt32("id_turno")
            };
        }
    }
}
