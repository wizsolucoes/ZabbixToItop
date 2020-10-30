# Wiz ZabbixToItop

Aplicativo desenvolvido para salvar tickets no Itop de acordo com os parâmetros passados.

## Uso

```bash
dotnet run "arg0" "arg1" "arg2" "arg3" "arg4" "arg5" "arg6" "arg7" "arg8" "arg9"
```

*Todos argumentos são obrigatórios
Abaixo estão listados os campso correspodentes no itop para cada arg:
arg0 = Class
arg1 = Description
arg2 = Origin
arg3 = Resource group name
arg4 = CI/HOST
arg5 = Urgency
arg6 = Team
arg7 = Impact
arg8 = Service name
arg9 = Service subcategory name

## Exemplo de uso

```bash
dotnet run "UserRequest" "Description" "monitoring" "resourceGroupName" "Cluster1" "4" "Helpdesk" "2" "Software" "Microsoft Office Support"
```

## Configuração da conexão com o Itop
A conexão com o Itop deve ser definida através do arquivo App.config como mostrado abaixo:
```xml
<?xml version="1.0"?>
<configuration>
	<appSettings>
		<add key="url" value="{sua_url}" />
		<add key="auth_pwd" value="{sua_senha}" />
		<add key="auth_user" value="{seu_usuario}" />
	</appSettings>
</configuration>
```

## Configuração dos args e setar valores padrão
Através da classe ItopConfiguration.cs pode-se definir valores padrão para a requisição do itop e também modificar os args no constructor da classe.

## Ambiente local
Uma instância do Itop no docker foi utilizada para desenvolvimento. Abaixo segue o link da
[imagem]: https://hub.docker.com/r/vbkunin/itop .
