# Cinema_Manager_ASP.NET_MVC
CRUD desenvolvido em ASP.NET Core MVC com EF (Entity Framework) e banco SQL Server (mssql).

## Description
CRUD de Gerenciamento de Cinema, onde é possível adicionar filmes, salas e sessões. Os filmes podem ser cadastrados, atualizados e removidos. Um filme só pode ser removido se não estiver vinculado a uma sessão ativa. As salas são para mera ilustração. Já as sessões, também podem ser cadastradas e removidas. Uma sessão não pode ser removida se faltar menos de 10 dias para sua exibição. Todas as sessões são vinculadas a um filme e uma sala.

## Installation
- SQL Server
	- Baixar o Microsoft SQL Server Management Studio caso não tenha.

- Visual Studio
	- Baixar e instalar o Visual Studio (caso não tenha, recomendado: 2022).
	- Baixar o projeto.
	- Extrair o projeto e abri-lo no Visual Studio.
	- Em "appsettings.json" atualizar os valores da config com os dados do seu banco de dados SQL Server (Server, User Id, Password).
	- Abrir Tools > NuGet Package Manager > Package Manager Console.
	- Entrar com o comando "add-migration CinemaManagerDB -context DBContext".
		- Espere realizar a criação do Migrations.
	- Entrar com o comando "update-database".
		- Pronto, o database foi criado em seu SQL Server Management Studio.
		
- SQL Server Management Studio
	- Entrar com o comando "insert into Users values ('Admin', 'admin@admin.com', 'Admin!2022', 'admin', 1, 'Administrador', CURRENT_TIMESTAMP, NULL)"
		- Este comando cria um usuário gerente/admin com permissões 'sudo'
	- Entrar com os 3 comandos abaixo para criar as salas:
		"insert into Theaters values ('Sala 1', 40, CURRENT_TIMESTAMP)"
		"insert into Theaters values ('Sala 2', 50, CURRENT_TIMESTAMP)"
		"insert into Theaters values ('Sala 3', 60, CURRENT_TIMESTAMP)"

## Usage
- Compilar o projeto no Visual Studio
	- Clean e Rebuild.
- Rodar o projeto.
- Fazer o login com a conta adm criada inicialmente.
