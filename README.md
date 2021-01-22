# distribuicao-lucros

O objetivo deste projeto é armazenar uma lista de funcionários e em seguida calcular a participação nos lucros de cada funcionário.

A arquitetura da aplicação foi organizada em um DDD, utilizando técnicas de inversão de controle, inversão de dependencia e injeção de dependencia, possibilitando
criar testes de unidade e controlar com mais fácilidade o ciclo de vida dos objetos.

Para executar a aplicação é necessário possuir visual studio e .net 5.

Após abrir o arquivo `distribuicao-lucros.sln` com o visual studio, clique com o botão direito do mouse no projeto `distribuicao-lucros-api` e selecione `Set as startup Project`.

Em seguida execute o projeto através da tecla F5. A conexão com o Firebase Database(já está configurada no arquivo appsettings.json).

Será aberta uma nova janela do navegador com a página inicial do Swagger, possibilitando executar testes em todas as rotas da aplicação.

Todas as rotas estão sendo documentadas automaticamente pelo Swagger, dessa forma, a cada Controller/Route adicionada, a UI do swagger será atualizada automaticamente.
