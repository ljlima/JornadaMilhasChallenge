using Api.Tests.ControllersTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Teste.Integracao.Api.ControllersTest;
[CollectionDefinition(nameof(ContextoCollection))]
public class ContextoCollection : ICollectionFixture<JornadaMilhasWebApplicationFactory>
{
}
