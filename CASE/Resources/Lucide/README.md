# Ícones Lucide

Origem: https://github.com/lucide-icons/lucide
Revisão: 20aef31db93fe4d9f398dbb15b120d99c48eef95

Os SVG originais estão preservados nesta pasta. Os PNG transparentes são usados
nos PictureBox: azul claro no menu, azul marinho no cabeçalho e variantes
com sufixo -azul para os cabeçalhos, formulários e indicadores das telas.
Os recursos são incluídos no aplicativo; não há download durante a execução.

Licenças e atribuições: LICENSE.txt (Lucide e Feather).
Ícones das telas de fornecedores, acessos e transferência:

- https://lucide.dev/icons/truck
- https://lucide.dev/icons/shield-check
- https://lucide.dev/icons/arrow-right-left

Esses SVGs usam a mesma revisão indicada acima. As variantes `-azul` têm 64 px;
`-acao`, `-menu` e `-branco` têm 20 px. As cores seguem a paleta do CASE.

Para gerar novamente as variantes das novas telas:

```powershell
npm.cmd install --prefix "$env:TEMP/case-icon-converter" --no-audit --no-fund --ignore-scripts @resvg/resvg-js@2.6.2
node scripts/Convert-LucideIcons.cjs
```

O conversor fica na pasta temporária, sem dependência Node no aplicativo.
