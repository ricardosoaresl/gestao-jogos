### Instalação

Requer [Docker](https://www.docker.com).

```sh
cd backend-api
docker build -t gestao-jogos-backend -f Dockerfile .
cd ..
docker-compose up -d

cd frontend-web
yarn
yarn start
```

### Acesso
 - http://localhost:3000/admin/dashboard (Aplicacão)
 - http://localhost:44364/swagger/index.html (Api)

### Informacões sobre o projeto
Gestão de jogos é uma solução que tem por objetivo demonstrar minhas capacidades técnicas em algumas tecnologias

Esta solução consiste em dois projetos principais, backend e frontend.

O backend (API) foi desenvolvido em c#,
Foi utilizado .net Core como framework
Foi utilizado o ORM entity framework core,
Foi utilizado banco MSSQL.
O projeto está baseado em DDD.
Foi utilizando o conceito de CQRS
Foi utilizado o conceito de domínio sempre válido.
Foi utilizado fluent validation.
Foi utilizado AutoMapping, para facilitar a transição dos dados entre as camadas da aplicação.
Foi utilizado OData, para facilitar a implementação do conceito de api REST, e facilitar também as consultas.
Foi utilizado rabbitMQ

Frontend 
Foi utilizado React para frontend web
Conceito de mobile first


### Pontos a serem melhorados
Foi utilizado rabbitMQ para a auditoria das alterações de dados, esta parte está pronta até a parte que envia para a fila.
Porém é necessário um novo projeto listener da fila do rabbitMQ para inserção em banco noSQL Mongo DB. Preferência em node.js.

Acredito que para o backend ainda precisam ser feitos dois pontos principais que não foram feitos por falta de tempo:
	- Autenticação utilizando token, OAuth2, identityServer. Necessário um novo projeto, deixarei a API configurada.
	- TDD, ainda estou estudando a melhor forma de utilizar.

