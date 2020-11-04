
# Wiz ZabbixToItop
Aplicativo desenvolvido para salvar tickets no Itop.

## Uso
```bash
dotnet run "arg0"  "arg1"  "arg2"  "arg3"  "arg4"  "arg5"  "arg6"  "arg7"  "arg8"  "arg9"
```

Abaixo estão listados os campos correspondentes no itop para cada arg:
***Argumento obrigatório**

arg0* = Class
arg1* = Description
arg2* = Origin
arg3* = CI/HOST
arg4* = Urgency
arg5* = Team
arg6* = Impact
arg7 = Service name
arg8 = Service subcategory name
arg9 = Resource group name

## Exemplos de uso

#### Utilizando resource group name
```bash
dotnet run "UserRequest"  "Description"  "monitoring"  "Cluster1"  "4"  "Helpdesk"  "2"  "Software"  "Microsoft Office Support"  "resourceGroupName"
```

#### Sem resource group name
```bash
dotnet run "UserRequest"  "Description"  "monitoring"  "Cluster1"  "4"  "Helpdesk"  "2"  "Software"  "Microsoft Office Support"
```

#### Sem resource group name, service name e service subcategory name
```bash
dotnet run "UserRequest"  "Description"  "monitoring"  "Cluster1"  "4"  "Helpdesk"  "2"  "Software"  "Microsoft Office Support"
```

## Build
Para fazer o build do projeto basta rodar o comando abaixo
```bash
dotnet build
```
Para fazer o build do projeto para ambiente de produção basta rodar este outro comando
```bash
dotnet build -c Release
```

## Configuração da conexão com o Itop
A conexão com o Itop do ambiente de desenvolvimento e produção devem ser definidas no arquivo App.config como mostrado abaixo:
```xml
<?xml version="1.0"?>
<configuration>
	<configSections>
		<section  name="Debug"  type="System.Configuration.AppSettingsSection"  />
		<section  name="Release"  type="System.Configuration.AppSettingsSection"/>
	</configSections>
	<Release>
		<add  key="url"  value="url release"  />
		<add  key="auth_pwd"  value="pwd release"  />
		<add  key="auth_user"  value="user release"  />
	</Release>
	<Debug>
		<add  key="url"  value="http://localhost:8000/webservices/rest.php?version=1.3"  />
		<add  key="auth_pwd"  value="Admin123_"  />
		<add  key="auth_user"  value="admin7"  />
	</Debug>
</configuration>
```

## Configuração dos args e setar valores padrão
Através da classe ItopConfiguration.cs pode-se definir valores padrão para a requisição do itop e também modificar os args no constructor da classe.

## Ambiente local
Uma instância do Itop no docker foi utilizada para desenvolvimento. Segue o link da [imagem](https://hub.docker.com/r/vbkunin/itop).