# Desafio Técnico - Backend

O propósito desse desafio é a criação de uma API que fará a persistência de dados de um quadro de kanban. Esse quadro possui listas, que contém cards.

## Rodando o Frontend

Um frontend de exemplo foi disponibilizado na pasta FRONT.

Para rodá-lo, faça:

```console
> cd FRONT
> yarn
> yarn start
```

## Rodando o Backend

 Após clonar o projeto navegue até  diretório back/KanbanApi/KanbanApi
Execute os comandos abaixo:
```bash
dotnet user-secrets set "LoginSettings:Username" "letscode"
dotnet user-secrets set "LoginSettings:Password" "lets@123"
dotnet user-secrets set "JwtSettings:JWTSecret" "mjdowej2318nTBVBD423Nheu3wg4RVDE2334ufnGRDFBV3EEWM4MXSDHQWEIRWNEHBUBneer"
```
```bash
dotnet run
```
Não tive tempo de configurar docker e docker compose.
O Front end não lida bem com erros e mostra uma página em branco quando tento criar um card sem as informações obrigatórias
O projeto possui dois branches
Para executar o projeto com .net aspire selecione o branch features/netapire e vá até o diretório \BACK\KanbanHost\KanbanHost 
crie os user secrets usando as instruções acima e execute
```bash
dotnet un
```
