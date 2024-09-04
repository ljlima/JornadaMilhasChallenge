using Bogus;
using JornadaMilhas.Data.Dtos.DepoimentosDtos;
using JornadaMilhas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.DataBuilders;

internal class DepoimentosDataBuilder : Faker<Depoimentos>
{
    public string? Nome { get; set; }
    public string? Depoimento { get; set; }
    public string? Foto { get; set; }

    public DepoimentosDataBuilder()
    {
        CustomInstantiator(f =>
        {
            string nomeFake = Nome ?? f.Name.FirstName();
            string depoimentoFake = Depoimento ?? f.Lorem.Sentence(10);
            string fotoFake = Foto ?? f.Lorem.Sentence(3);

            return new Depoimentos(nomeFake, depoimentoFake, fotoFake);
            
        });
    }

}
