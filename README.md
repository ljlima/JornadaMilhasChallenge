# Jornada Milhas Challenge

## `Objetivo Desafio`: 
Desenvolver Api Crud simples + Testes de integração, contendo os recursos:

### API

#### Rota : `/Depoimentos`: CRUD de depoimentos de usuários da plataforma

|Método|Descrição|
|-----|-----|
| `POST` | Realiza cadastro Depoimentos com dados: `Nome`, `Depoimento`, `Foto`.| 
|`GET`| Requisita um depoimento por `id` ou consulta paginada.| 
|`PUT`| Atualiza depoimentos.| 
|`DELETE`| Deleta um depoimento.| 

#### Rota : `/Depoimentos-home`: Requisição de *três depoimentos* aleatórios no conjunto de dados
|Método|Descrição|
|-----|-----|
|`GET`| Requisita três depoimentos aleatórios| 

#### Rota : `/Destinos`: CRUD de Destinos de viagem da plataforma
|Método|Descrição|
|-----|-----|
| `POST` | Realiza cadastro destino com dados: `nome`,`foto1Destino`,`foto2Destino`,`textoMeta`,`textoDescritivo`,`precoViagem`. Caso `textoDescritivo` seja nulo ou vazio faz requisição a api_chatgpt_openai (standard model), preenchendo texto descritivo sobre o destino.| 
|`GET`| Requisita um destino (por `id` ou por `nome`) ou consulta paginada..| 
|`PUT`| Atualiza destino.| 
|`DELETE`| Deleta um destino.| 

### Testes Integração

São gerados dados fake usando biblioteca `bogus` nas classes dataBuilder e realizados os testes:
#### Rota : `/Depoimentos`: 

##### Classe: `JornadaMilhasDepoimentosCrud`
* Status code: `POST`,`GET`,`PUT`,`DELETE`;
* Quantidade de elementos de lista retornada: Consulta paginada;
* Dados não existentes;
  
#### Rota : `/Depoimentos-home`

##### Classe: `JornadaMilhasDepoimentosGetHome`
* Status code: `POST`,`GET`,`PUT`,`DELETE`;
* Quantidade de elementos de lista retornada: Garante *três* depoimentos;
  
#### Rota : `/Destinos`
##### Classe: `JornadaMilhasDestinosCrud`
* Status code: `POST`,`GET`,`PUT`,`DELETE`;
* Teste Destino por `id` ou por `nome`;

##### Classe: `JornadaMilhasDescricaoIA`
* Descricao_openApi_chatgpt: comparação nome destino;

## Dependências da solução
  
### Projeto : `dotnet core web api.net 6` 
* `EntityFramework`: core , core.tools
* `Pomelo.MySql`
* `AutoMapper`
* `OpenAi_API`
* `bogus`
### Teste : `Xunit .net 6`
* `MvcTesting`
* `AutoMapper`
  
