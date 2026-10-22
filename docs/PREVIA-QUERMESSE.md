# Telas da quermesse

O CASE atende uma única instituição nesta etapa. A tela Quermesses representa os eventos ou edições dessa instituição; não existe cadastro ou seleção de instituições.

Este trabalho é somente frontend: controles, formulários, cores, navegação e exemplos fixos para compor as telas. Não há backend, autenticação, autorização, salvamento, sincronização, cálculo financeiro, baixa de estoque ou fechamento. Esses comportamentos ficam para a implementação do responsável pelo backend.

O seletor **Prévia · apresentação por usuário** muda apenas a apresentação demonstrativa do cabeçalho e das telas de visão geral e registros. Todos os itens do menu ficam disponíveis para revisar o frontend; não há controle de acesso implementado.

## Telas

- Visão geral: DashboardControl original, com cards de estoque, gráfico e produtos em destaque ilustrativos. A proposta alternativa QuermesseDashboardControl permanece no projeto, sem substituir a tela inicial.
- Quermesses: lista de eventos e formulário de cadastro/edição.
- Equipes e setores: funcionários, perfis, setores e formulário de vínculo.
- Escala de trabalho: funcionários por setor, dia e turno, com formulário de alocação e horários.
- Registros: exemplo de acompanhamento da equipe e formulário visual de vendas, perdas e reposições na apresentação do vendedor.
- Solicitações: lista de reposições e ocorrências, filtros visuais e formulário com exemplo de detalhes.
- Produtos e estoque: esboços existentes, com apresentação de produtos de quermesse.
- Financeiro: cards ilustrativos, lista de custos e campos de lançamento. Os totais são fixos e não são recalculados pelos filtros.
- Apuração: exemplo visual da conferência, sem somar valores nem executar fechamento.
- Configurações: apresentação existente de dados pessoais e preferências.

## Interações

Os menus abrem as telas. Botões de novo/editar abrem os formulários. Campos e seletores podem ser preenchidos, e os filtros alteram apenas a exibição das linhas fictícias. Cancelar e fechar retornam à tela anterior. Salvar, registrar e conferir exibem um aviso neutro de prévia, sem alterar as amostras nem simular sucesso de uma operação.

As amostras em `CASE/Quermesse/QuermessePreview.cs` servem exclusivamente à montagem visual. Não há métodos de registro, custos, conferência ou regras de perfil nessa classe.

## Conferência

Compile com `msbuild CASE\CASE.csproj /t:Build /p:Configuration=Debug`. Execute `powershell -NoProfile -STA -File scripts\Test-QuermessePreview.ps1` para conferir a abertura das telas, os filtros de apresentação e a ausência de gravação nos botões da prévia. A renderização fora da tela não substitui a conferência interativa no Windows.

Para conferir as janelas abertas, execute `powershell -NoProfile -STA -File scripts\Test-QuermesseRuntime.ps1`. A verificação abre a tela principal com o ciclo normal do WinForms, percorre as onze telas, verifica a apresentação do vendedor e abre/cancela cinco modais. As capturas ficam na pasta temporária `CASE-runtime-review`. Essa verificação não salva dados.


O seletor de usuários e as amostras são temporários para revisão da interface. A repaginação usa a paleta do LoginForm, com estilos de campos, botões, bordas e espaços de ícones centralizados no VisualUtils. Os PictureBox de ícones já usam os recursos Lucide; as áreas de fotografia continuam disponíveis para imagens de produtos.


## Organização no Visual Studio

As doze telas e formulários da pasta `CASE/Quermesse` e o `ProdutoForm` usam o mesmo padrão do LoginForm: classe parcial, chamada a InitializeComponent(), layout e controles no arquivo .Designer.cs e um .resx associado. Os eventos e exemplos visuais ficam no .cs. As antigas bases que criavam o layout por métodos auxiliares foram removidas. A aparência é aplicada pelo FrontendTheme usando o VisualUtils.

Abra `CASE.slnx`. No Gerenciador de Soluções, abra `CASE/Quermesse/QuermessesControl.cs` com Exibir Designer (Shift+F7). O .Designer.cs e o .resx ficam agrupados sob o .cs. Para conferir a aplicação, use F5.


O PrincipalForm usa 15 ícones oficiais Lucide: 12 no menu e 3 no cabeçalho, todos em PictureBox definidos no Designer. As telas, formulários e indicadores também usam variantes azuis dos ícones Lucide, atribuídas nos respectivos arquivos Designer. A área de foto do produto contém um pequeno ícone de seleção de imagem. O DashboardControl original abre inicialmente dentro do panelConteudo do PrincipalForm; os demais menus substituem o conteúdo dessa mesma área. Os SVG originais, PNG transparentes, revisão de origem e licença ficam em `CASE/Resources/Lucide`. Os PNG são incorporados aos recursos do aplicativo e a licença acompanha a saída da compilação.
