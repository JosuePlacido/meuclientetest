# meuclientetest

## 🚀 Run project

### Altere a connection string em appsettings.json

para rodar migrações se o banco não estiver criado

```bash
	# acesse a pasta do projeto
	cd Api

	# aplique as migrations
	dotnet ef database update
```

### Com docker-compose?

```bash
 #na raiz do projeto
 docker-compose up --build

 # Por padrao vai encontrar a interface do swagger: http://localhost:5000
 yarn dev:server # or npm run dev:server
```

### Apenas dotnet

```bash
 #Acesse a pasta do projeto
 cd Api

 #baixe as dependências
 dotnet restore

 #faca o build
 dotnet build

 # Inicie a Api, por padrao vai encontrar a interface do swagger: http://localhost:5000
 dotnet run
```

## 🛠️ Construção

-   Desenvolvi seguindo os príncipios **SOLID**, e tentando manter o codigo mais simples e intuitivo o possível, utilizei como IDE o VSCode, SO: Linux e dotnet na versão 2.1.

-   Comecando por **Models**, implementei uma Entitdade global para permitir fazer um DAO com as operações básicas de CRUD genéricas, evitando repetição de codigo.

-   Implementei a class **InternalScoped** com os campos de controle interno que se repetia em 3 das 4 entidades.

-   **Order e ItemOrder** foram implementadas sendo **itemOrder** uma tabela separada numa relaçao **1-N**, ao alterar **Order** os itens **ItemOrder** são excluídos e reinseridos

-   **CNPJ** implementei como um valueObject para centralizar a validação do mesmo e o mapeamento em JSON desta informação.

-   Para **acesso a dados** implementei um **DAO** para cada **Entidade**, que extendia um generico e sobreescrevia algum metodo quando necessário por causa de algum comportamento específico por exemplo o **OrderDAO**.

-   Alguns desses **DAOs** tem interface com metodos únicos para aquela entidade, por exemplo **IDAOAsset** tem o metodo **GetAssetDetailed**.

-   Nos **controllers** tem apenas tratamento de recursos HTTP e chamada do serviço da aplicação.

-   Em **services** onde a plicação acontece, aqui implementei algumas validações mais complexas, de entrada de dados a **regras de negócios** (Validação do CNPJ em uso, ou do Desconto nao pode ser maior que o total do contrato); Além de validações também implementei alguns metodos com ações diferentes, por exemplo no serviço **AssetCRUD**, pode cadastrar informando o **ID** de um tipo previamente cadastrado ou criar um **Tipo de Ativo** no momento do cadastro.

-   Para manter o **padrão de retorno** em erros de validação, Criei um **ValidationError** e implementei um **ActionFilter**, para tratar exceção customizadas, padronizar o retorno da api e fazer log das exceções inesperadas.

-   em **Data** estão as **migrations** geradas e o **Context** com configurações de modelagens do banco. Aqui configurei um padrão de nomenclatura de tabelas e campos, visando facilitar a escrita(eventual) de longos codigos **SQL**

-   Por fim, separei em **extensions** para adicionar os **Serviços** e **DAOs** em DI e as configurações do **Swagger**. Desabilitei **redirecionamento HTTPS** para nao precisar configurar SSL.

## Autor

<a alt="Linkedin" href="https: //linkedin/in/josueplacido">
 <img style="border-radius: 50%;" src="https://github.com/josueplacido.png" width="100px;" alt=""/>
 <br />
 <sub><b>Josué Placido</b></sub></a>

Developed ❤️ by Josué Placido! 👋🏽

[![Linkedin Badge](https://img.shields.io/badge/-Josue%20Placido-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/josueplacido/)](https://www.linkedin.com/in/josueplacido/)
[![Gmail Badge](https://img.shields.io/badge/-juplacido.jnr@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:juplacido.jnr@gmail.com)](mailto:juplacido.jnr@gmail.com)
[![Hotmail Badge](https://img.shields.io/badge/-ozzyplacidojunior@hotmail.com-blue?style=flat-square&logo=data:image/svg+xml;base64,PCFET0NUWVBFIHN2ZyBQVUJMSUMgIi0vL1czQy8vRFREIFNWRyAxLjEvL0VOIiAiaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkIj4KDTwhLS0gVXBsb2FkZWQgdG86IFNWRyBSZXBvLCB3d3cuc3ZncmVwby5jb20sIFRyYW5zZm9ybWVkIGJ5OiBTVkcgUmVwbyBNaXhlciBUb29scyAtLT4KPHN2ZyB3aWR0aD0iODAwcHgiIGhlaWdodD0iODAwcHgiIHZpZXdCb3g9IjAgMCAzMiAzMiIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiBmaWxsPSIjZmZmZmZmIj4KDTxnIGlkPSJTVkdSZXBvX2JnQ2FycmllciIgc3Ryb2tlLXdpZHRoPSIwIi8+Cg08ZyBpZD0iU1ZHUmVwb190cmFjZXJDYXJyaWVyIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiLz4KDTxnIGlkPSJTVkdSZXBvX2ljb25DYXJyaWVyIj4KDTx0aXRsZT5maWxlX3R5cGVfb3V0bG9vazwvdGl0bGU+Cg08cGF0aCBkPSJNMTkuNDg0LDcuOTM3djUuNDc3TDIxLjQsMTQuNjE5YS40ODkuNDg5LDAsMCwwLC4yMSwwbDguMjM4LTUuNTU0YTEuMTc0LDEuMTc0LDAsMCwwLS45NTktMS4xMjhaIiBzdHlsZT0iZmlsbDojZmFmYWZhIi8+Cg08cGF0aCBkPSJNMTkuNDg0LDE1LjQ1N2wxLjc0NywxLjJhLjUyMi41MjIsMCwwLDAsLjU0MywwYy0uMy4xODEsOC4wNzMtNS4zNzgsOC4wNzMtNS4zNzhWMjEuMzQ1YTEuNDA4LDEuNDA4LDAsMCwxLTEuNDksMS41NTVIMTkuNDgzVjE1LjQ1N1oiIHN0eWxlPSJmaWxsOiNmYWZhZmEiLz4KDTxwYXRoIGQ9Ik0xMC40NCwxMi45MzJhMS42MDksMS42MDksMCwwLDAtMS40Mi44MzgsNC4xMzEsNC4xMzEsMCwwLDAtLjUyNiwyLjIxOEE0LjA1LDQuMDUsMCwwLDAsOS4wMiwxOC4yYTEuNiwxLjYsMCwwLDAsMi43NzEuMDIyLDQuMDE0LDQuMDE0LDAsMCwwLC41MTUtMi4yLDQuMzY5LDQuMzY5LDAsMCwwLS41LTIuMjgxQTEuNTM2LDEuNTM2LDAsMCwwLDEwLjQ0LDEyLjkzMloiIHN0eWxlPSJmaWxsOiNmYWZhZmEiLz4KDTxwYXRoIGQ9Ik0yLjE1Myw1LjE1NVYyNi41ODJMMTguNDUzLDMwVjJaTTEzLjA2MSwxOS40OTFhMy4yMzEsMy4yMzEsMCwwLDEtMi43LDEuMzYxLDMuMTksMy4xOSwwLDAsMS0yLjY0LTEuMzE4QTUuNDU5LDUuNDU5LDAsMCwxLDYuNzA2LDE2LjFhNS44NjgsNS44NjgsMCwwLDEsMS4wMzYtMy42MTZBMy4yNjcsMy4yNjcsMCwwLDEsMTAuNDg2LDExLjFhMy4xMTYsMy4xMTYsMCwwLDEsMi42MSwxLjMyMSw1LjYzOSw1LjYzOSwwLDAsMSwxLDMuNDg0QTUuNzYzLDUuNzYzLDAsMCwxLDEzLjA2MSwxOS40OTFaIiBzdHlsZT0iZmlsbDojZmFmYWZhIi8+Cg08L2c+Cg08L3N2Zz4=&link=mailto:ozzyplacidojunior@hotmail.com)](mailto:ozzyplacidojunior@hotmail.com)
