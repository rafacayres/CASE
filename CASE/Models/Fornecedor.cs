using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE.Models
{
        public class Fornecedor
        {
            public int IdFornecedor { get; set; }

            public string Nome { get; set; }

            public string Documento { get; set; }

            public string Telefone { get; set; }

            public string Email { get; set; }
        }
}
