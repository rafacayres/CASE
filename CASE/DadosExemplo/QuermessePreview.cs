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
        public string Evento { get; private set; } = "Quermesse de São João 2026";
        public List<string> Setores { get; } = new List<string> { "Bebidas", "Alimentação", "Doces", "Brincadeiras" };
        public List<PessoaPreview> Pessoas { get; } = new List<PessoaPreview>();
        public List<ProdutoPreview> Produtos { get; } = new List<ProdutoPreview>();
        public List<RegistroPreview> Registros { get; } = new List<RegistroPreview>();
        public List<CustoPreview> Custos { get; } = new List<CustoPreview>();
        public PessoaPreview Atual { get; set; }

        public QuermessePreview()
        {
            Pessoas.AddRange(new[] {
                new PessoaPreview { Id = 1, Nome = "Rafael Cayres", Login = "rafael", Perfil = PerfilUsuario.Administrador, AdministradorSistema = true, Setor = "Organização", Turno = "Noite" },
                new PessoaPreview { Id = 2, Nome = "Ana Silva", Login = "ana", Perfil = PerfilUsuario.Gerente, Setor = "Gestão", Turno = "Noite" },
                new PessoaPreview { Id = 3, Nome = "Marina Souza", Login = "marina", Perfil = PerfilUsuario.Vendedor, Setor = "Bebidas", Turno = "Noite" },
                new PessoaPreview { Id = 4, Nome = "Pedro Lima", Login = "pedro", Perfil = PerfilUsuario.Vendedor, Setor = "Alimentação", Turno = "Noite" },
                new PessoaPreview { Id = 5, Nome = "Julia Santos", Login = "julia", Perfil = PerfilUsuario.Vendedor, Setor = "Doces", Turno = "Tarde" }
            });
            Produtos.AddRange(new[] {
                new ProdutoPreview { Nome = "Refrigerante", Setor = "Bebidas" },
                new ProdutoPreview { Nome = "Água mineral", Setor = "Bebidas" },
                new ProdutoPreview { Nome = "Cachorro-quente", Setor = "Alimentação" },
                new ProdutoPreview { Nome = "Pastel", Setor = "Alimentação" },
                new ProdutoPreview { Nome = "Bolo de milho", Setor = "Doces" },
                new ProdutoPreview { Nome = "Pescaria", Setor = "Brincadeiras" }
            });
            Atual = Pessoas[0];
            Registros.AddRange(new[] {
                new RegistroPreview { Data = DateTime.Today.AddHours(18), PessoaId = 3, Pessoa = "Marina Souza", Setor = "Bebidas", Turno = "Noite", Tipo = "Venda", Produto = "Refrigerante", Quantidade = 9, Observacao = "" },
                new RegistroPreview { Data = DateTime.Today.AddHours(18), PessoaId = 4, Pessoa = "Pedro Lima", Setor = "Alimentação", Turno = "Noite", Tipo = "Venda", Produto = "Cachorro-quente", Quantidade = 12, Observacao = "" },
                new RegistroPreview { Data = DateTime.Today.AddHours(18), PessoaId = 5, Pessoa = "Julia Santos", Setor = "Doces", Turno = "Tarde", Tipo = "Venda", Produto = "Bolo de milho", Quantidade = 15, Observacao = "" }
            });
            Custos.Add(new CustoPreview { Descricao = "Compra de bebidas", Setor = "Bebidas", Valor = 120,
                Data = DateTime.Today, Responsavel = "Ana Silva" });
            Custos.Add(new CustoPreview { Descricao = "Ingredientes e embalagens", Setor = "Alimentação", Valor = 85,
                Data = DateTime.Today, Responsavel = "Ana Silva" });
        }

    }
}
