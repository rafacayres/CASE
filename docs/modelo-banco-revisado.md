# CASE — proposta revisada de banco de dados

Esta é uma proposta lógica para o trabalho da faculdade, não uma migração executada. As novas telas são somente frontend: usam exemplos em memória, descartados ao encerrar o aplicativo, sem banco, arquivos, autenticação ou autorização real.

Os nomes Usuario, Produto, Estoque, Custo, Fornecedor, Categoria, Registro e Perda foram preservados do esboço original. Registro é o histórico de movimentações; Custo representa gastos do evento, enquanto o custo unitário de aquisição fica em EntradaItem. A tabela Tempo é dispensável porque cada operação possui sua data e hora. Não são tabelas adicionais: Registro substitui o nome MovimentacaoEstoque e Custo substitui o nome Despesa usados na proposta anterior.

## Premissas para validar com a instituição

- Uma instituição possui pessoas, eventos e locais de estoque. O depósito central pode existir antes de qualquer evento.
- Um produto pode existir em vários locais; um local pode guardar vários produtos.
- O perfil geral da conta é independente da função da pessoa em cada evento.
- Uma pessoa pode participar sem ter login. Uma conta pertence a exatamente uma pessoa.
- O saldo disponível significa saldo físico nesta versão. Não há reservas de mercadoria; uma solicitação pendente não reduz o saldo.
- A apuração é por turno do setor. Caso seja por caixa ou vendedor, seu identificador deve fazer parte da apuração.
- Os tipos de dados e detalhes de índices serão definidos após a escolha do SGBD. Valores monetários e quantidades usam decimal, nunca ponto flutuante para dinheiro.

Legenda: **PK** chave primária; **FK** chave estrangeira; **UQ** unicidade; **?** opcional. Uma lista entre parênteses em PK/UQ representa uma combinação de colunas.

## Pessoas, contas e acesso

| Tabela | Atributos e chaves |
|---|---|
| Instituicao | **id_instituicao PK**, nome, documento?, email?, telefone? |
| Pessoa | **id_pessoa PK**, **id_instituicao FK → Instituicao**, nome, email?, telefone? |
| PerfilSistema | **id_perfil_sistema PK**, nome UQ (`Administrador`, `Padrão`) |
| Usuario | **id_usuario PK**, **id_pessoa FK → Pessoa UQ**, **id_perfil_sistema FK → PerfilSistema**, login UQ, senha_hash, situacao_conta, solicitado_em |
| AnaliseUsuario | **id_analise PK**, **id_usuario FK → Usuario**, **id_analisador FK → Usuario**, decisao, analisado_em, justificativa? |
| PerfilEvento | **id_perfil_evento PK**, nome UQ (`Coordenador`, `Gerente`, `Vendedor`) |

`situacao_conta` admite Pendente, Aprovado, Rejeitado e Suspenso. Substitui o booleano `ativo` na conta, evitando estados contraditórios. A pessoa e a conta têm responsabilidades distintas: nome e contato ficam em Pessoa; credenciais e situação de acesso ficam em Usuario.

O cadastro público informa somente nome, login, email, senha e confirmação. O futuro backend deve atribuir Pendente e Padrão independentemente de qualquer valor recebido do cliente. Senha de confirmação não é armazenada. As telas atuais não armazenam senha alguma.

AnaliseUsuario mantém o histórico de decisões; Usuario guarda o estado atual. Uma decisão e a alteração de estado devem ocorrer na mesma transação no futuro backend. Rejeição e suspensão exigem justificativa. Reativação de conta suspensa retorna a Aprovado. Uma solicitação rejeitada não é aprovada silenciosamente: uma eventual reabertura deve ser registrada como nova decisão explícita.

O administrador geral gerencia contas e cria eventos sem precisar pertencer a uma equipe. Uma função de gerente no evento não concede administração geral. Antes de permitir acesso real, o backend verificará situação aprovada, instituição e o escopo da permissão. O primeiro administrador será criado por provisionamento controlado na instalação; nunca por escolha livre no cadastro. Não permitir remover/suspender o último administrador aprovado da instituição.

## Eventos, setores e escala

| Tabela | Atributos e chaves |
|---|---|
| TipoEvento | **id_tipo_evento PK**, nome UQ |
| Evento | **id_evento PK**, **id_instituicao FK → Instituicao**, **id_tipo_evento FK → TipoEvento**, nome, local, data_inicio, data_fim, **id_responsavel FK → Pessoa**, situacao, observacao? |
| Setor | **id_setor PK**, nome UQ, descricao? |
| EventoSetor | **id_evento_setor PK**, **id_evento FK → Evento**, **id_setor FK → Setor**, UQ(id_evento, id_setor) |
| EquipeEvento | **id_equipe_evento PK**, **id_evento FK → Evento**, **id_pessoa FK → Pessoa**, **id_perfil_evento FK → PerfilEvento**, ativo, UQ(id_evento, id_pessoa) |
| Turno | **id_turno PK**, **id_evento_setor FK → EventoSetor**, nome, inicio, fim |
| Escala | **id_escala PK**, **id_turno FK → Turno**, **id_equipe_evento FK → EquipeEvento**, UQ(id_turno, id_equipe_evento) |

Turno é uma ocorrência com data e hora, por exemplo Bebidas, 30/10, 18h–23h. A equipe e o turno da escala precisam pertencer ao mesmo evento. O responsável do evento deve pertencer à instituição organizadora. Esta versão admite uma função por pessoa em cada evento; múltiplas funções simultâneas exigiriam uma associação adicional.

## Produtos e fornecedores

| Tabela | Atributos e chaves |
|---|---|
| Categoria | **id_categoria PK**, nome UQ |
| UnidadeMedida | **id_unidade PK**, sigla UQ, nome |
| Produto | **id_produto PK**, **id_categoria FK → Categoria**, **id_unidade_estoque FK → UnidadeMedida**, **id_unidade_conteudo FK → UnidadeMedida?**, nome, descricao?, conteudo_embalagem?, preco_venda_atual?, caminho_imagem?, ativo |
| Fornecedor | **id_fornecedor PK**, nome, documento?, email?, telefone? |
| ProdutoFornecedor | **PK(id_produto, id_fornecedor)**, **id_produto FK → Produto**, **id_fornecedor FK → Fornecedor**, codigo_no_fornecedor? |

Documento do fornecedor pode ser CPF ou CNPJ, com unicidade quando informado. Permite identificar pessoas físicas e doadores. ProdutoFornecedor informa quem oferece o produto; o fornecedor de uma entrega concreta está em EntradaMercadoria.

Uma lata de 350 mL pode ter unidade de estoque `un`, conteúdo 350 e unidade de conteúdo `mL`. Toda movimentação usa a unidade de estoque; conversões de caixa para unidade devem ser definidas se forem necessárias. Preço de venda atual não substitui o preço histórico praticado na venda.

## Estoque e recebimento

| Tabela | Atributos e chaves |
|---|---|
| LocalEstoque | **id_local_estoque PK**, **id_instituicao FK → Instituicao**, nome, ativo, UQ(id_instituicao, nome) |
| EstoqueEventoSetor | **id_local_estoque PK/FK → LocalEstoque**, **id_evento_setor FK → EventoSetor** |
| Estoque | **id_estoque PK**, **id_local_estoque FK → LocalEstoque**, **id_produto FK → Produto**, quantidade_minima, quantidade_ideal, UQ(id_local_estoque, id_produto) |
| TipoMovimentacao | **id_tipo_movimentacao PK**, nome UQ, sentido (`Entrada` ou `Saída`) |
| Registro | **id_registro PK**, **id_estoque FK → Estoque**, **id_tipo_movimentacao FK → TipoMovimentacao**, **id_usuario FK → Usuario**, data_hora, quantidade, observacao? |
| EntradaMercadoria | **id_entrada PK**, **id_fornecedor FK → Fornecedor?**, natureza (`Compra`, `Doação`, `Saldo inicial`), data_documento?, numero_documento?, observacao? |
| EntradaItem | **id_registro PK/FK → Registro**, **id_entrada FK → EntradaMercadoria**, custo_unitario |

O depósito central existe em LocalEstoque sem linha em EstoqueEventoSetor. Para uma barraca, a associação identifica o setor e o evento atendidos. Um local associado pertence a um único evento/setor nesta proposta; locais físicos compartilhados entre eventos exigem rever essa regra. A instituição do local e a instituição do evento associado devem coincidir.

EntradaMercadoria agrupa a entrega e seu fornecedor. Cada EntradaItem identifica uma movimentação de entrada que contém produto (via Estoque), destino, quantidade, autor e data/hora do recebimento. Não duplicamos esses atributos no item. `data_documento` pode ser diferente da data/hora de recebimento.

Compra exige fornecedor; doação e saldo inicial podem dispensá-lo. A aplicação pode oferecer cadastro de fornecedor a partir do recebimento. O custo de uma doação não é automaticamente seu valor de mercado: a política de valorização será definida com a instituição.

**Fonte única do saldo:**

```text
saldo(local, produto) = soma das movimentações de entrada − soma das movimentações de saída
```

Não somar EntradaItem novamente. Ele documenta a origem da movimentação, não representa uma segunda entrada. A quantidade inicial é registrada como movimentação de saldo inicial. Não há tabela Tempo: cada acontecimento registra sua data.

Garantias para a implementação futura: quantidade > 0, custo >= 0, mínimo >= 0, ideal >= mínimo; um movimento de entrada não pode pertencer a duas entregas. Confirmar recebimento grava cabeçalho, itens e movimentos em uma transação com proteção contra confirmação duplicada. Tipos já usados não devem mudar de sentido.

## Reposição efetivada, venda e perda

| Tabela | Atributos e chaves |
|---|---|
| TransferenciaEstoque | **id_transferencia PK**, **id_solicitacao FK → Solicitacao?**, observacao? |
| TransferenciaItem | **id_transferencia_item PK**, **id_transferencia FK → TransferenciaEstoque**, **id_registro_saida FK → Registro UQ**, **id_registro_entrada FK → Registro UQ** |
| Venda | **id_venda PK**, **id_turno FK → Turno**, **id_vendedor FK → Pessoa** |
| VendaItem | **id_registro PK/FK → Registro**, **id_venda FK → Venda**, preco_unitario_praticado |
| Perda | **id_perda PK**, **id_registro FK → Registro UQ**, motivo |

A transferência possui duas movimentações por item: saída da origem e entrada no destino, com mesmo produto e quantidade, locais diferentes e mesma instituição. Quantidade, produto, autor e data são acessíveis pelas movimentações. Isso permite reabastecer uma barraca sem aumentar o estoque total da instituição. Cada saída deve ter exatamente sua entrada correspondente e vice-versa.

Uma solicitação não altera estoque; somente uma entrega confirmada o faz. Transferências e a verificação do saldo precisam ser atômicas no futuro backend, inclusive com usuários simultâneos. Um movimento não deve ser reutilizado em operações incompatíveis (venda e perda, por exemplo).

O vendedor pode ser diferente do usuário que registrou a operação. O vínculo com o turno permite apuração. VendaItem preserva o preço histórico; quantidade e produto vêm da movimentação. Perda adiciona somente o motivo à saída correspondente. Correções de operações confirmadas devem manter histórico, preferencialmente por estorno identificado, em vez de apagar registros.

## Solicitações, financeiro e apuração

| Tabela | Atributos e chaves |
|---|---|
| Solicitacao | **id_solicitacao PK**, **id_evento_setor FK → EventoSetor**, **id_solicitante FK → Pessoa**, **id_responsavel FK → Pessoa?**, tipo, situacao, data_abertura, data_encerramento?, observacao? |
| SolicitacaoItem | **PK(id_solicitacao, id_produto)**, **id_solicitacao FK → Solicitacao**, **id_produto FK → Produto**, quantidade_solicitada |
| Custo | **id_custo PK**, **id_evento_setor FK → EventoSetor**, **id_usuario FK → Usuario**, **id_fornecedor FK → Fornecedor?**, descricao, valor, data_hora |
| Apuracao | **id_turno PK/FK → Turno**, **id_conferente FK → Usuario**, valor_informado, data_conferencia, situacao, observacao? |

Para despesas gerais do evento pode existir o setor Organização. Valor esperado da apuração é calculado pelas vendas do turno; diferença = informado − esperado. O modelo pressupõe uma apuração por turno; revisões auditáveis exigiriam histórico adicional.

A quantidade atendida da solicitação pode ser obtida pelas transferências vinculadas e seus produtos. Validar que os produtos entregues pertencem à solicitação, que o destino corresponde ao setor solicitado e que não houve entrega excedente sem autorização. Entregas parciais mantêm a solicitação em atendimento; apenas a conclusão efetiva deve encerrá-la.

A instituição deve escolher se o financeiro acompanha caixa, competência ou custo dos produtos vendidos. Uma compra de mercadoria não deve ser somada duas vezes como custo do estoque e despesa no mesmo indicador. Essa regra e o método de valorização do estoque (por exemplo custo médio) ainda precisam ser definidos.

## Relatórios e 3FN

Relatórios de saldo, entradas, perdas, vendas e despesas são consultas/views. Uma tabela RelatorioGerado é opcional para arquivar documentos: id_relatorio PK, id_usuario FK → Usuario, tipo, gerado_em, periodo_inicio?, periodo_fim?, caminho_arquivo. Para reproduzir um relatório antigo, preservar o documento ou seus resultados, não apenas filtros.

- **1FN:** não há lista de produtos, fornecedores ou pessoas em uma célula. As relações são representadas por linhas em tabelas de associação ou itens.
- **2FN:** nas chaves compostas, os atributos não-chave dependem do par completo. Exemplo: quantidade_solicitada depende de solicitação + produto; o nome do produto não fica em SolicitacaoItem.
- **3FN:** descrições de categoria, perfil e fornecedor ficam em suas tabelas. Usuario não repete nome da pessoa ou nome do perfil; Estoque não repete nome do produto. EquipeEvento guarda função por evento, não a permissão geral da conta.
- PKs artificiais não bastam: as combinações UQ são chaves candidatas relevantes. A validação final da 3FN depende das dependências funcionais e regras acima confirmadas pela instituição.
- Valores derivados não são automaticamente proibidos pela 3FN; a escolha de calcular saldos e totais evita redundância operacional. Histórico de análise e situação atual possuem finalidades diferentes e precisam permanecer consistentes.

## Telas desta entrega

| Acesso | Tela e comportamento demonstrativo |
|---|---|
| Estoque | Saldo por local e histórico das movimentações da sessão. |
| Estoque → Registrar entrada | Compra/doação/saldo inicial, fornecedor, destino, vários itens, custo unitário e total. Confirmar acrescenta entradas à demonstração. Cancelar não altera saldo. |
| Estoque → Repor / transferir | Origem, destino, produto, quantidade, disponível e observação. Confirmar produz uma saída e uma entrada; não permite quantidade acima da disponível na amostra. |
| Fornecedores | Pesquisa, inclusão e edição temporária de contatos; fornecedores ficam disponíveis no recebimento. |
| Usuários e acessos / Configurações → Usuários | Filtro por situação, justificativa, aprovação, rejeição, suspensão, reativação e perfil geral. Exibido ao administrador na prévia de usuário. |
| Cadastro público | Solicita análise sem oferecer situação, aprovador ou perfil administrativo. A senha não é retida na demonstração. |
| Funcionário e setor | Identifica a função como perfil neste evento. |

Limites: as tabelas são proposta documental; não há SQL, API, autenticação ou regras de concorrência implementadas. O histórico operacional novo usa sua própria amostra; vendas/financeiro antigos continuam com seus exemplos visuais. O campo de referência na transferência é texto ilustrativo, não um vínculo persistente com solicitações. Quantidades da amostra estão em unidades. A validação do documento do fornecedor no frontend verifica formato/tamanho, não sua existência ou validade cadastral. A validação de segurança deve ser refeita no servidor quando houver backend.
