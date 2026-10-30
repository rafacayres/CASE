const fs = require('fs');
const path = require('path');
const os = require('os');
const { Resvg } = require(path.join(os.tmpdir(), 'case-icon-converter/node_modules/@resvg/resvg-js'));

const folder = path.resolve(__dirname, '../CASE/Resources/Lucide');
const icons = {
    truck: ['azul', 'acao', 'menu'],
    'shield-check': ['azul', 'acao', 'menu'],
    'arrow-right-left': ['azul', 'acao'],
    'package-plus': ['branco']
};
const variants = {
    azul: { color: '#1769d2', size: 64 },
    acao: { color: '#1769d2', size: 20 },
    menu: { color: '#d5e1ef', size: 20 },
    branco: { color: '#ffffff', size: 20 }
};

for (const [name, styles] of Object.entries(icons)) {
    const source = fs.readFileSync(path.join(folder, `${name}.svg`), 'utf8');
    for (const style of styles) {
        const { color, size } = variants[style];
        const svg = source.replace(/currentColor/g, color);
        const renderer = new Resvg(svg, { fitTo: { mode: 'width', value: size } });
        fs.writeFileSync(path.join(folder, `${name}-${style}.png`), renderer.render().asPng());
    }
}
