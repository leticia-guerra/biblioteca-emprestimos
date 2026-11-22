# Biblioteca: empréstimos e devoluções

Trabalho de testes automatizados da disciplina de Gestão da Qualidade de Software.
Grupo: Letícia, Sara e Yasmin.

## Domínio

Biblioteca de classes em C# que gerencia o empréstimo e a devolução de livros: quem pode pegar
um livro, por quanto tempo, quantos livros cada pessoa pode ter ao mesmo tempo e quanto paga se
atrasar. Não há interface gráfica nem banco de dados; o foco está nas regras de negócio e nos
testes unitários.

### Regras de negócio

| Regra | Descrição |
|-------|-----------|
| R1 | Cada usuário pode ter no máximo 3 empréstimos ativos. |
| R2 | O prazo de devolução é de 7 dias a partir do empréstimo. |
| R3 | Devolução depois do prazo gera multa de R$ 2,00 por dia de atraso. |
| R4 | Usuário com multa pendente não pode fazer novo empréstimo. |
| R5 | Livro já emprestado não pode ser emprestado de novo. |
| R6 | Ao devolver, o livro volta a ficar disponível. |

Exemplo: a usuária pega um livro no dia 1 (prazo até o dia 8). Se devolver no dia 11, são 3 dias
de atraso e a multa é de R$ 6,00. Enquanto não pagar, não consegue novo empréstimo.

### Classes

| Classe | Responsabilidade |
|--------|------------------|
| `Livro` | Livro do acervo e sua disponibilidade. |
| `Usuario` | Pessoa que pega livros e sua multa pendente. |
| `Emprestimo` | Registro de um empréstimo: datas de empréstimo, prevista e de devolução. |
| `CalculadoraDeMulta` | Cálculo da multa por dias de atraso (R3). |
| `Biblioteca` | Serviço com as operações de emprestar, devolver e pagar multa. |

As datas sempre entram como parâmetro (não usamos a data atual do computador), para que os
testes controlem o tempo e deem o mesmo resultado em qualquer dia.

## Tecnologias

- C# com .NET 9
- [xUnit](https://xunit.net/) como framework de testes

## Estrutura

```
biblioteca-emprestimos/
├── Biblioteca.sln
├── src/Biblioteca.Core/      # regras de negócio
└── tests/Biblioteca.Tests/   # testes unitários (xUnit)
```

## Como rodar os testes

Pré-requisito: [.NET SDK 9](https://dotnet.microsoft.com/download) ou mais novo instalado.

```bash
git clone https://github.com/leticia-guerra/biblioteca-emprestimos.git
cd biblioteca-emprestimos
dotnet test
```

Para ver o nome de cada teste na saída:

```bash
dotnet test --logger "console;verbosity=detailed"
```

## Testes implementados

| ID | Cartão | Regra | Cenário | Resultado esperado |
|----|--------|-------|---------|--------------------|
| T01 | K01 | R2, R5 | Emprestar livro disponível no dia 1 | Empréstimo criado; livro indisponível; data prevista = dia 8 |
| T02 | K01 | R5 | Emprestar livro já emprestado | Falha com mensagem clara; livro continua indisponível |
| T03 | K01 | – | Emprestar com livro ou usuário nulo | Exceção de argumento nulo |
| T04 | K02 | R6 | Devolver livro emprestado no dia 5 | Livro disponível; data de devolução registrada |
| T05 | K02 | – | Devolver empréstimo já devolvido | Erro de operação inválida |
| T06 | K03 | R3 | Devolver no dia 8 (último dia do prazo) | Multa R$ 0,00 |
| T07 | K03 | R3 | Devolver no dia 9 (1 dia de atraso) | Multa R$ 2,00 |
| T08 | K03 | R3 | Devolver no dia 11 (3 dias de atraso) | Multa R$ 6,00 |
| T09 | K04 | R1 | Pegar o 3º livro (dentro do limite) | Empréstimo realizado |
| T10 | K04 | R1 | Tentar o 4º livro (acima do limite) | Falha por limite; livro continua disponível |
| T11 | K05 | R4 | Emprestar com multa pendente | Falha por multa pendente |
| T12 | K05 | R4 | Pagar a multa e emprestar | Multa zerada; empréstimo realizado |

Os casos T06 a T08 são um único teste `[Theory]` com três `[InlineData]`.

## Gitflow

| Branch | Uso |
|--------|-----|
| `main` | Versão estável (tags `v1.0` e `v1.0.1`). |
| `develop` | Integração das funcionalidades em desenvolvimento. |
| `feature/*` | Uma branch por cartão do Trello, com merge na `develop` por pull request revisado. |
| `release/1.0` | Preparação da versão 1.0. |
| `hotfix/1.0.1` | Correção feita a partir da `main` depois da release. |

Commits seguem prefixos curtos: `feat:`, `test:`, `fix:`, `docs:` e `chore:`.

## Organização

Quadro Kanban no Trello: https://trello.com/b/o7cbuZji/biblioteca-sprint-12-11-a-24-11

| Integrante | Papel | Cartões |
|------------|-------|---------|
| Letícia | Product Owner | K00, K02, K03 |
| Sara | Scrum Master | K01, K04 |
| Yasmin | Líder técnica | K05, K06 |

## Versão 1.0

A versão 1.0 reúne as funcionalidades de empréstimo, devolução, cálculo de multas, limite de empréstimos e bloqueio de usuários com multas pendentes.

A validação da versão foi realizada com `dotnet test`, com **14 testes aprovados e nenhuma falha**.