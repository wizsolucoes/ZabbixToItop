
# Wiz ZabbixToItop
Aplicativo desenvolvido para salvar novos tickets no Itop.

## Uso
Para execução o projeto espera que três variáveis de ambiente estejam definidas {url} {auth_user}{auth_pwd}, 

```bash
dotnet run "*{$Class}" "*{$Description}" "*{$Origin}" "*{$CI_HOST}" "*{$Urgency}" "*{$Team}" "*{$Impact}" "{$Service_name}" "{$Service_subcategory_name}" "{$Resource_group_name}"
```
***Argumento obrigatório**

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
```bash
dotnet build
```

## Configuração de chaves secretas
A configuração das chaves deverão ser feitas no ZabbixToItop-template.conf, após definir as chaves basta retirar o '-template' do nome do arquivo e executr.
```bash
source ZabbixToItop.conf 
```

## Configuração dos args e setar valores padrão
Através da classe ItopConfiguration.cs pode-se definir valores padrão para a requisição do itop e também modificar os args no constructor da classe.

## Ambiente local
Uma instância do Itop no docker foi utilizada para desenvolvimento. Segue o link da [imagem](https://hub.docker.com/r/vbkunin/itop).