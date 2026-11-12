using System;
using System.Data;
using CASE.Data;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class ConsultaOperacionalRepository
    {
        // O escopo acompanha o evento; para membros, exige a escala da própria pessoa.
        private const string Escopo = "(@perfil='Administrador' OR " +
            "(@perfil='Gerente' AND ev.id_responsavel=@pessoa) OR " +
            "(@perfil='Membro' AND eq.id_pessoa=@pessoa AND eq.ativo=1))";

        public DataTable Escalas(string perfil, int pessoa, int evento)
        {
            string sql = "SELECT e.id_escala, j.id_jornada, j.data AS data_jornada, s.id_setor, " +
                "CONCAT(ev.nome,' / ',s.nome,' / ',DATE_FORMAT(j.data,'%d/%m/%Y')," +
                "' / ',p.nome,' / ',e.entrada_prevista,' - ',e.saida_prevista) AS descricao " +
                "FROM escala e JOIN equipeevento eq ON eq.id_equipe=e.id_equipe " +
                "JOIN pessoa p ON p.id_pessoa=eq.id_pessoa " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor JOIN evento ev ON ev.id_evento=s.id_evento " +
                "WHERE eq.id_evento=ev.id_evento AND " + Escopo +
                " AND (@evento=0 OR ev.id_evento=@evento) ORDER BY j.data,s.nome,p.nome";
            return Consultar(sql, perfil, pessoa, evento, DateTime.Today, DateTime.Today);
        }

        public DataTable ProdutosDaEscala(int escala)
        {
            return Consultar("SELECT ps.id_produto_setor,p.nome,ps.preco_venda " +
                "FROM escala e JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN produtosetor ps ON ps.id_setor=j.id_setor " +
                "JOIN produto p ON p.id_produto=ps.id_produto " +
                "WHERE e.id_escala=@evento AND p.ativo=1 ORDER BY p.nome",
                string.Empty, 0, escala, DateTime.Today, DateTime.Today);
        }

        public DataTable Registros(string perfil, int pessoa, int evento, DateTime inicio, DateTime fim)
        {
            string sql = "SELECT r.id_registro,j.id_jornada,r.data_hora,p.nome AS responsavel,s.nome AS setor," +
                "t.nome AS turno,r.tipo,pr.nome AS produto,ri.quantidade," +
                "ri.preco_unitario_praticado,r.forma_pagamento,r.observacao," +
                "ri.quantidade*ri.preco_unitario_praticado AS total " +
                "FROM registro r JOIN escala e ON e.id_escala=r.id_escala " +
                "JOIN equipeevento eq ON eq.id_equipe=e.id_equipe " +
                "JOIN usuario u ON u.id_usuario=r.id_usuario_responsavel " +
                "JOIN pessoa p ON p.id_pessoa=u.id_pessoa " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada JOIN turno t ON t.id_turno=j.id_turno " +
                "JOIN setor s ON s.id_setor=j.id_setor JOIN evento ev ON ev.id_evento=s.id_evento " +
                "JOIN registroitem ri ON ri.id_registro=r.id_registro " +
                "JOIN produtosetor ps ON ps.id_produto_setor=ri.id_produto_setor " +
                "JOIN produto pr ON pr.id_produto=ps.id_produto WHERE eq.id_evento=ev.id_evento " +
                "AND ps.id_setor=s.id_setor AND " + Escopo +
                " AND (@evento=0 OR ev.id_evento=@evento) AND r.data_hora>=@inicio " +
                "AND r.data_hora<@fim ORDER BY r.data_hora DESC,r.id_registro DESC";
            return Consultar(sql, perfil, pessoa, evento, inicio, fim);
        }

        public DataTable Estoques(string perfil, int pessoa)
        {
            string sql = "SELECT l.id_local_estoque,l.nome AS local,p.id_produto,p.nome AS produto,p.unidade_medida," +
                "es.quantidade_atual,es.quantidade_minima,es.quantidade_ideal " +
                "FROM estoque es JOIN localestoque l ON l.id_local_estoque=es.id_local_estoque " +
                "JOIN produto p ON p.id_produto=es.id_produto " +
                "LEFT JOIN evento ev ON ev.id_evento=l.id_evento WHERE " +
                "(@perfil='Administrador' OR (@perfil='Gerente' AND (ev.id_responsavel=@pessoa OR " +
                "(l.eh_central=1 AND EXISTS (SELECT 1 FROM evento ce WHERE ce.id_responsavel=@pessoa " +
                "AND ce.id_instituicao=l.id_instituicao)))) " +
                "OR (@perfil='Membro' AND EXISTS (SELECT 1 FROM estoquesetor xs " +
                "JOIN jornadasetor j ON j.id_setor=xs.id_setor " +
                "JOIN escala e ON e.id_jornada=j.id_jornada " +
                "JOIN equipeevento eq ON eq.id_equipe=e.id_equipe " +
                "WHERE xs.id_local_estoque=l.id_local_estoque AND eq.id_evento=xs.id_evento " +
                "AND eq.ativo=1 AND eq.id_pessoa=@pessoa))) " +
                "ORDER BY l.nome,p.nome";
            return Consultar(sql, perfil, pessoa, 0, DateTime.Today, DateTime.Today);
        }

        public DataTable Movimentacoes(string perfil, int pessoa, DateTime inicio, DateTime fim)
        {
            // Transferências aparecem como uma operação com origem e destino, sem duplicar o total institucional.
            string sql = "SELECT m.data_hora,m.tipo,m.produto,m.quantidade,m.origem,m.destino,m.responsavel,m.id_origem,m.id_destino " +
                "FROM (SELECT en.data_hora,'entrada' AS tipo,p.nome AS produto,i.quantidade," +
                "'' AS origem,l.nome AS destino,u.login AS responsavel,NULL AS evento,0 AS id_origem,l.id_local_estoque AS id_destino " +
                "FROM entradamercadoria en JOIN entradaitem i ON i.id_entrada=en.id_entrada " +
                "JOIN produto p ON p.id_produto=i.id_produto " +
                "JOIN localestoque l ON l.id_local_estoque=en.id_local_destino " +
                "JOIN usuario u ON u.id_usuario=en.id_usuario_responsavel WHERE @perfil='Administrador' " +
                "UNION ALL SELECT tr.data_hora,'transferencia',p.nome,i.quantidade,o.nome,d.nome,u.login," +
                "COALESCE(o.id_evento,d.id_evento),o.id_local_estoque,d.id_local_estoque FROM transferenciaestoque tr " +
                "JOIN transferenciaitem i ON i.id_transferencia=tr.id_transferencia " +
                "JOIN produto p ON p.id_produto=i.id_produto " +
                "JOIN localestoque o ON o.id_local_estoque=tr.id_local_origem " +
                "JOIN localestoque d ON d.id_local_estoque=tr.id_local_destino " +
                "JOIN usuario u ON u.id_usuario=tr.id_usuario_responsavel " +
                "WHERE @perfil<>'Membro' UNION ALL SELECT r.data_hora,r.tipo,p.nome,i.quantidade," +
                "s.nome,'',u.login,ev.id_evento,es.id_local_estoque,0 FROM registro r " +
                "JOIN registroitem i ON i.id_registro=r.id_registro " +
                "JOIN produtosetor ps ON ps.id_produto_setor=i.id_produto_setor " +
                "JOIN produto p ON p.id_produto=ps.id_produto " +
                "JOIN escala e ON e.id_escala=r.id_escala JOIN equipeevento eq ON eq.id_equipe=e.id_equipe " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor JOIN evento ev ON ev.id_evento=s.id_evento " +
                "JOIN estoquesetor es ON es.id_setor=s.id_setor " +
                "JOIN usuario u ON u.id_usuario=r.id_usuario_responsavel " +
                "WHERE eq.id_evento=ev.id_evento AND ps.id_setor=s.id_setor AND " + Escopo +
                ") m LEFT JOIN evento ev ON ev.id_evento=m.evento " +
                "WHERE m.data_hora>=@inicio AND m.data_hora<@fim AND " +
                "(@perfil='Administrador' OR (@perfil='Gerente' AND ev.id_responsavel=@pessoa) " +
                "OR (@perfil='Membro' AND m.tipo IN ('venda','perda'))) ORDER BY m.data_hora DESC";
            return Consultar(sql, perfil, pessoa, 0, inicio, fim);
        }

        private static DataTable Consultar(string sql, string perfil, int pessoa, int evento,
            DateTime inicio, DateTime fim)
        {
            using (var conexao = Conexao.CriarConexao())
            using (var comando = new MySqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@perfil", perfil);
                comando.Parameters.AddWithValue("@pessoa", pessoa);
                comando.Parameters.AddWithValue("@evento", evento);
                comando.Parameters.AddWithValue("@inicio", inicio.Date);
                comando.Parameters.AddWithValue("@fim", fim.Date.AddDays(1));
                conexao.Open();
                using (var leitor = comando.ExecuteReader())
                {
                    DataTable dados = new DataTable();
                    dados.Load(leitor);
                    return dados;
                }
            }
        }
    }
}
