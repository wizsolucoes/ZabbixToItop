# Wiz ZabbixToItop
Aplicativo desenvolvido para facilitar a integração com o Itop.

## Uso
```bash
dotnet run "{$ITOP_URL}" "{$ITOP_AUTH_USER}" "{$ITOP_AUTH_PWD}" "{$TEAMS_URL}" "{$CURRENT_DIR}" "{$Class}" "{$Description}" "{$String}"
```

## Exemplos de uso
```bash
dotnet run "{$ITOP_URL}" "{$ITOP_AUTH_USER}" "{$ITOP_AUTH_PWD}" "{$TEAMS_URL}" "{$CURRENT_DIR}" "UserRequest" "Description" "Problem started at 17:10:52 on 2020.11.19^M Problem name: ping^M Host: Cluster1^M Severity: Disaster^M ^M Original problem ID: 3058^M ^M ^M Equipe: Helpdesk^MHost: Cluster1^M Severidade: Disaster^M Impacto: 1"
```

## Configuração dos args e setar valores padrão
Através da classe ItopConfiguration.cs pode-se definir valores padrão para a requisição do itop e também fazer modificações em relação a configuração dos argumentos recebidos na execução.

## Ambiente de desenvolvimento
Uma instância do Itop no docker foi utilizada para desenvolvimento. Segue o link da [imagem](https://hub.docker.com/r/vbkunin/itop).