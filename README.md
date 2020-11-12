# Wiz ZabbixToItop
Aplicativo desenvolvido para salvar tickets no Itop.

## Uso
```bash
dotnet run "{$ITOP_URL}" "{$ITOP_AUTH_USER}" "{$ITOP_AUTH_PWD}" "{$TEAMS_URL}" "{$CURRENT_DIR}" "{$Class}" "{$Description}" "{$Origin}" "{$CI_HOST}" "{$Urgency}" "{$Team}" "{$Impact}" 
```

## Exemplos de uso
```bash
dotnet run "{$ITOP_URL}" "{$ITOP_AUTH_USER}" "{$ITOP_AUTH_PWD}" "{$TEAMS_URL}" "{$CURRENT_DIR}" "UserRequest"  "Description"  "monitoring"  "Cluster1"  "4"  "Helpdesk"  "2"
```

## Configuração dos args e setar valores padrão
Através da classe ItopConfiguration.cs pode-se definir valores padrão para a requisição do itop e também fazer modificações em relação a configuração dos argumentos recebidos na execução.

## Ambiente de desenvolvimento
Uma instância do Itop no docker foi utilizada para desenvolvimento. Segue o link da [imagem](https://hub.docker.com/r/vbkunin/itop).