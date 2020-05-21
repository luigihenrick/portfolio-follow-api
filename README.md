# Portfolio Follow API

API desevolvida em dotnet com o objetivo de fornecer informações para o aplicativo em flutter que consolida uma carteira de investimentos.

Link do swagger da API: [Swagger](https://portfolio-follow-api.herokuapp.com/swagger)

Aplicativo pode ser encontrado aqui: [Portfolio Follow App](https://github.com/luigihenrick/portfolio-follow-app)

## Como executar

Para rodar você precisa adicionar as seguintes configurações no arquivo appsettings.json: 

- ConnectionString do MongoDB (Recomendo utilizar o [mlab](https://mlab.com/))

- API Key da [Alphavantage](https://www.alphavantage.co/) para consulta de preços de ativos de renda variável

Após adicionar as configurações, execute os comandos:

    dotnet dev-certs https

    dotnet run

## Foi ainda feito o release dessa API no heroku através de um container docker.

Para mais informações acesse: [Medium](https://medium.com/dockerbr/publishing-asp-net-core-application-on-heroku-with-docker-cf30072842e2)

Arquivo de configuração do docker: [DockerFile](https://github.com/luigihenrick/portfolio-follow-api/blob/master/app/DockerFile)