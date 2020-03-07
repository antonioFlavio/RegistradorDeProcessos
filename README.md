# Raspagem e Ingestão de Processos

Este projeto é composto por 2 aplicações: Um crawler e uma WebAPI.
O Crawler realiza uma requisição que realiza a consulta de um processo específico e faz a raspagem dos dados de um processo (Número, Classe, Área, Assunto, Origem, Distribuição e Relator) e também suas movimentações (Data, Descrição, Legenda e Indicador de anexo). As informações são enviadas a uma WebAPI que irá persistir os dados.

Crawler: Python com BeautifulSoup
WebAPI - .Net Core 3.1
Persistência: Entity Framework Core
Banco de dados: SQlite

## Pré-requisitos
.Net Core 3.1 sdk instalado.


## Como compilar

### Criando o aquivo de banco de dados
Abra o promp de comando e navegue até o diretório da solução (.sln). E execute os comandos abaixo:

>Crie o arquivo de banco de dados, executando o comando:
``` 
dotnet ef migrations add CatalogoInicial
```
>Aplique as atualizações ao banco de dados, executando o comando:
```
dotnet ef database update
```

### Compilando a webapi
> Ainda no diretório da solução, execute:
```
dotnet build .\RegistradorDeProcessos.sln
```
## Executando a webapi
A partir do diretório da solução, via prompt de comando, acesse o diretório com os binários gerados na compilação:
> \RegistradorDeProcessos.WebAPI\bin\Debug\netcoreapp3.1\

Execute o comando abaixo e mantenha o prompt de comando aberto, para iniciar a aplicação:
```
dotnet RegistradorDeProcessos.WebAPI.dll
```
Acesse a interface do swagger para interagir com a WebApi, através do endereço:
[http://localhost:5000/index.html](http://localhost:5000/index.html)

## Executando o script python
Após iniciar a WebApi, em uma nova janela do prompt de comando, acesse o diretório 'Crawler' dentro do diretório da solução e execute o comando:
```
python .\crawler.py
```

## Resolução de problemas
Caso ocorro erro ao criar o catálogo inicial do banco, possivelmente as ferramentas do Entity Framework core não estão instaladas. Instale utilizando o comando:
```
dotnet tool install --global dotnet-ef --version 3.1.0
```
