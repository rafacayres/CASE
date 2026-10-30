# CASE - Commercial Automation Software

Frontend Windows Forms em .NET Framework 4.7.2. Os dados atuais são de demonstração, mantidos em memória.

```text
CASE/
  Telas/
    Acesso/          Login, cadastro e aprovação de usuários
    Principal/       Janela principal e navegação
    Dashboard/       Painéis de indicadores
    Eventos/         Cadastro e listagem de eventos
    Equipe/          Pessoas e setores
    Escalas/         Organização das escalas
    Produtos/        Cadastro e listagem de produtos
    Estoque/         Saldos, entradas, transferências e movimentações
    Fornecedores/    Cadastro de fornecedores
    Registros/       Vendas e perdas
    Solicitacoes/    Pedidos e ocorrências
    Financeiro/      Despesas e apuração
    Relatorios/      Controle de relatórios existente
    Configuracoes/   Preferências da interface
  DadosExemplo/      OperacoesPreview e QuermessePreview
  Utils/             Tema, arredondamento e componentes visuais
  Resources/         Imagens e ícones Lucide
  Properties/        Recursos e configurações gerados pelo Visual Studio
  Program.cs         Inicialização da aplicação
  App.config         Configuração da aplicação
docs/                Documentação
tests/               Verificações do frontend
scripts/             Ferramentas auxiliares
```

Cada tela mantém seus arquivos `.cs`, `.Designer.cs` e `.resx`, quando existentes, na mesma pasta. No Gerenciador de Soluções, abra o arquivo principal da tela no Designer. Os namespaces existentes foram preservados para manter as referências das telas e dos recursos.

Para compilar, use o Visual Studio com as ferramentas de desenvolvimento desktop .NET e o targeting pack do .NET Framework 4.7.2. No Developer PowerShell:

```powershell
msbuild CASE/CASE.csproj /t:Build /p:Configuration=Debug
powershell -NoProfile -ExecutionPolicy Bypass -File tests/FrontendSmoke.ps1
powershell -NoProfile -ExecutionPolicy Bypass -File tests/RoundedRendering.ps1
```

O planejamento de integração está em [Organização e futuro backend](docs/organizacao-e-backend.md).
