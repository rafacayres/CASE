using System;
using System.Collections.Generic;

namespace CASE.Quermesse
{
    public enum PerfilUsuario { Vendedor, Gerente, Administrador }

    public sealed class PessoaPreview
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public PerfilUsuario Perfil { get; set; }
        // Perfil do evento e administração geral são independentes nesta prévia.
        public bool AdministradorSistema { get; set; }
        public string Setor { get; set; }
        public string Turno { get; set; }
        public bool Ativo { get; set; }

        public PessoaPreview()
        {
            Ativo = true;
        }

        public override string ToString() { return Nome + " · " + Perfil; }
    }

    public sealed class ProdutoPreview
    {
        public string Nome { get; set; }
        public string Setor { get; set; }

        public override string ToString() { return Nome; }
    }

    public sealed class RegistroPreview
    {
        public DateTime Data { get; set; }
        public int PessoaId { get; set; }
        public string Pessoa { get; set; }
        public string Setor { get; set; }
        public string Turno { get; set; }
        public string Tipo { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string FormaPagamento { get; set; }

    }

    public sealed class CustoPreview
    {
        public string Descricao { get; set; }
        public string Setor { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
    }

    // Amostras fixas para montar as telas; sem operações ou regras de negócio.
    public sealed class QuermessePreview
    {
        public string Evento { get; set; }
        public List<string> Setores { get; private set; }
        public List<PessoaPreview> Pessoas { get; private set; }
        public List<ProdutoPreview> Produtos { get; private set; }
        public List<RegistroPreview> Registros { get; private set; }
        public List<CustoPreview> Custos { get; private set; }
        // Identidade local temporária para navegar pela interface enquanto o backend não existe.
        public PessoaPreview Atual { get; set; }

        public QuermessePreview()
        {
            Evento = string.Empty;
            Setores = new List<string>();
            Pessoas = new List<PessoaPreview>();
            Produtos = new List<ProdutoPreview>();
            Registros = new List<RegistroPreview>();
            Custos = new List<CustoPreview>();
            Atual = new PessoaPreview();
            Atual.Nome = "Administrador";
            Atual.Login = "admin";
            Atual.Perfil = PerfilUsuario.Administrador;
            Atual.AdministradorSistema = true;
            Atual.Setor = string.Empty;
            Atual.Turno = string.Empty;
        }

    }
}
