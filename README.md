<h1 align="center">Freelando - site de projetos freelancer ✅ </h1>

## 📚 Sobre o projeto

Bem-vindo ao repositório do Freelando, um site desenvolvido para conectar freelancers a clientes que necessitam de serviços especializados. Este projeto tem como objetivo simplificar o processo de contratação e oferta de serviços freelance, oferecendo uma plataforma intuitiva e eficiente tanto para freelancers quanto para clientes.

## 📝 Conteúdo

- [Sobre o projeto](#-sobre-o-projeto)

## Configuração do ambiente

### 📋 Pré-requisitos

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### 🎲 Banco de dados
A configuração do banco de dados é feita através do arquivo appsettings.json, que fica na raiz do projeto Freelando.API. 
O arquivo já está configurado para o banco de dados **Sql Server** local, mas caso queira utilizar outro banco de dados, basta alterar a string de conexão.

### ⚓ Docker
Opcionalmente você pode baixar uma imagem docker com uma versão inicial do banco, para isso é necessário ter instalado em sua máquina o **Docker Desktop**, que você consegue [fazer o download através do site oficial](https://www.docker.com/products/docker-desktop/).

Para executar a imagem gerando um container o comando que você pode utilizar é o seguinte:
`docker run -e "ACCEPT_EULA=Y" -e MSSQL_SA_PASSWORD=SENHA#2024 -p 1433:1433 -d aluracursos/freelando_bd_image`.Agora você já tem sua imagem de testes com um banco de dados pré-populado com informações.

No projeto inicial, certifique-se que o seu arquivo `appsettings.json` está da seguinte maneira:
```jSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {    
    "DefaultConnection": "Server=localhost,1433;Database=Freelando;User Id=sa;Password=SENHA#2024;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=true;"
  }
}

```

## 🚀 Como executar o projeto

```bash
# Clone este repositório
$ git clone https://github.com/bessax/freelando-avancando-ef-core.git

# Acesse a pasta do projeto no terminal o projeto Web /cmd
$ cd ./src/Freelando.API [Executar o projeto Web]

# Execute a aplicação em modo de desenvolvimento
$ dotnet run

# O servidor inciará localmente na porta:7177 - acesse https://localhost:7177
```

## 🛠 Tecnologias e Ferramentas

As seguintes ferramentas foram usadas na construção e manutenção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/) - Linguagem de programação.
- [.NET](https://docs.microsoft.com/pt-br/dotnet/) - Framework
- [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet/apis) - Asp.NET Core WebAPI
- [EF Core](https://learn.microsoft.com/en-us/ef/core/) - EntityFramework Core
- [SQL Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) - Ferramenta para manipulação do banco de dados.
- [Swagger](https://swagger.io/) - Documentação da API

## ✒️ Colaborador(as/es)

- **Jeniffer Bittencourt** - _Desenvolvedora_  - [jeniblodev ](https://github.com/jeniblodev)
- **André Bessa da Silva** - _Desenvolvedor_  - [bessax](https://github.com/bessax)
- **Daniel Portugal** - _Desenvolvedor_  - [dpcosta](https://github.com/dpcosta)

