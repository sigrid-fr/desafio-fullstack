# desafio-fullstack
O objetivo desse teste é entender quais são as suas habilidades de desenvolvimento, estética e técnicas

![image](https://raw.githubusercontent.com/sigrid-fr/desafio-fullstack/main/imagem.png)

# Configuração local e execução do aplicativo

<h2>Criar banco de dados e tabela</h2>

```CREATE DATABASE fornecedor;```

```
CREATE TABLE `Empresas` (
	`id` int NOT NULL AUTO_INCREMENT,
	`nome_fantasia` VARCHAR(100) NOT NULL,
	`cnpj` VARCHAR(14) NOT NULL,
	`cep` VARCHAR(11) NOT NULL,
	`estado` VARCHAR(2) NOT NULL,
	PRIMARY KEY(`id`)) ENGINE=InnoDB;

CREATE TABLE `Fornecedores` (
	`id` int NOT NULL AUTO_INCREMENT,
	`nome` VARCHAR(100) NOT NULL,
	`cnpj` VARCHAR(14) NOT NULL,
	`cpf` VARCHAR(11) NOT NULL,
	`email` VARCHAR(100) NOT NULL,
	`cep` VARCHAR(11) NOT NULL,
	`rg` VARCHAR(14) NOT NULL,
	`data_nascimento` datetime NOT NULL,
	`empresa_id` int NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`empresa_id`) REFERENCES `Empresas` (`id`)) ENGINE=InnoDB;
```

<h2> Baixe ou clone o código-fonte do GitHub para a máquina local </h2>

<h2> Backend</h2>

Você pode iniciar a API executando ```dotnet run``` na linha de comando na pasta raiz do projeto (onde o arquivo WebApi.csproj está localizado)

OR

Você também pode iniciar o aplicativo no modo de depuração no Visual Studio abrindo a pasta raiz do projeto no Visual Studio e pressionando F5 ou selecionando Debug -> Start Debugging no menu superior, executando no modo de depuração.

<h2>Frontend</h2>

```npm install```

```npm run serve -- --port 8081```

<h2>No navegador, chame o endpoint http://localhost:8081</h2>
