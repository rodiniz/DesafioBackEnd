# Desafio Técnico - Backend

O propósito desse desafio é a criação de uma API que fará a persistência de dados de um quadro de kanban. Esse quadro possui listas, que contém cards.



## Rodando o Backend e o FrontEnd no .net aspire

 Após clonar o projeto navegue até  diretório back/KanbanHost/KanbanHost
Execute os comandos abaixo:
```bash
dotnet user-secrets set "LoginSettings:Username" "letscode"
dotnet user-secrets set "LoginSettings:Password" "lets@123"
dotnet user-secrets set "JwtSettings:JWTSecret" "mjdowej2318nTBVBD423Nheu3wg4RVDE2334ufnGRDFBV3EEWM4MXSDHQWEIRWNEHBUBneer"
```
```bash
dotnet run
```
O Front end não lida bem com erros e mostra uma página em branco quando tento criar um card sem as informações obrigatórias

