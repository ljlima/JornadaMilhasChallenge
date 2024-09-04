using Bogus;
using JornadaMilhas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.DataBuilders;

internal class DestinoDataBuilder : Faker<Destinos>
{
    public string Nome { get; set; }
    public string Foto1 { get; set; }
    public string Foto2 { get; set; }
    public string Meta { get; set; }
    public string TextoDescritivo { get; set; }
    public double PrecoMinimo { get; set; }
    public double PrecoMaximo { get; set; }

    public DestinoDataBuilder()
    {
        CustomInstantiator(f =>
        {
            string nomeFake = Nome ?? f.Lorem.Sentence(2);
            string foto1Fake = Foto1 ?? f.Lorem.Sentence(5);
            string foto2Fake = Foto2 ?? f.Lorem.Sentence(5);
            string metaFake = Meta ?? f.Lorem.Sentence(2);
            string textoDescritivoFake = TextoDescritivo ?? f.Lorem.Sentence(10);
            double precoFake = f.Random.Double(PrecoMinimo, PrecoMaximo);
            return new Destinos(nomeFake, foto1Fake, foto2Fake, precoFake)
            {
                Meta = metaFake,
                TextoDescritivo = textoDescritivoFake
            };
        });
    }
    
}
