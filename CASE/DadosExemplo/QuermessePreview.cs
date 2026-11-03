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
        public bool Ativo { get; set; } = true;
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
        public string Evento { get; set; } = "";
        public List<string> Setores { get; } = new List<string>();
        public List<PessoaPreview> Pessoas { get; } = new List<PessoaPreview>();
        public List<ProdutoPreview> Produtos { get; } = new List<ProdutoPreview>();
        public List<RegistroPreview> Registros { get; } = new List<RegistroPreview>();
        public List<CustoPreview> Custos { get; } = new List<CustoPreview>();
        // Identidade local temporária para navegar pela interface enquanto o backend não existe.
        public PessoaPreview Atual { get; set; } = new PessoaPreview
        {
            Nome = "Administrador",
            Login = "admin",
            Perfil = PerfilUsuario.Administrador,
            AdministradorSistema = true,
            Setor = "",
            Turno = ""
        };

        public QuermessePreview() { }

    }
}
