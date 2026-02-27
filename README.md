# panContas

O **panContas** é um sistema de gerenciamento moderno construído em **.NET 9**. Ele foi projetado para cadastro e controle eficiente de **Pessoas Físicas** (com integração de consultas via CEP) e **Pessoas Jurídicas**. O projeto adota a Clean Architecture (Arquitetura Limpa) para garantir a integridade dos dados, com interface rica e responsiva em Blazor Server.

## Tecnologias Utilizadas

- **Back-end:** C# com .NET 9
- **Front-end:** Blazor Server-side (.NET 9), Bootstrap 5, Bootstrap Icons
- **Banco de Dados:** PostgreSQL hospedado via AWS RDS
- **ORM:** Entity Framework Core 9 (Code-First)
- **Containerização:** Docker e Docker Compose
- **Integração Externa:** API Pública do ViaCEP

## Estrutura do Projeto (Clean Architecture)

- **`panContas.Domain`:** Entidades de núcleo (Interfaces, PessoaFisica, PessoaJuridica, Endereço).
- **`panContas.Application`:** Lógica de negócio, Serviços de Aplicação (Interfaces e Implementação).
- **`panContas.Infrastructure`:** Repositórios, acesso ao banco de dados EF Core (AppDbContext), adaptadores externos.
- **`panContas.API`:** Controladores RESTful (v1) e Swagger.
- **`panContas.Front`:** Interface Web em Blazor Interativo e componentes visuais.

## Como Executar o Projeto Localmente (via Docker)

O projeto foi configurado com Multi-stage builds do Docker para rodar API e Frontend isoladamente na sua máquina.

### Pré-requisitos
- Docker Desktop instalado e rodando.
- Git (Opcional, para clone).

### Passo a Passo

1. **Abra o terminal na pasta raiz do projeto.**
2. **Execute o construtor do Docker Compose em Background:**
   ```bash
   docker-compose up --build -d
   ```
3. O Compose vai baixar os SDKs necessários, compilar a solução completa e subir dois contêineres:
   - **Back-end (API e Swagger):** Acessível em [http://3.84.227.229:5262/swagger](http://3.84.227.229:5262/swagger)
   - **Front-end (Painel Blazor):** Acessível em [http://3.84.227.229:5100](http://3.84.227.229:5100)

4. **Para parar os servidores temporariamente**, execute:
   ```bash
   docker-compose stop
   ```
5. **Para desligar e remover os contêineres**, execute:
   ```bash
   docker-compose down
   ```

## Funcionalidades em Destaque

- ✅ **Validação Em Tempo Real:** Formulários Blazor configurados com Interceptação em JS `onkeypress` impedindo que caracteres ilegais ou numéricos entrem nos campos restritos, além de máscaras dinâmicas aplicadas "on the fly" em C#.
- ✅ **Consulta Externa:** Busca automática e inteligente pelo logradouro do usuário inserindo o CEP (ViaCep).
- ✅ **Tratamento de FK Inteligente:** Sistema EF Core imune a colisões de Foreign Keys não-planejadas de UUIDs, preservando as ligações das tabelas pai e filho de maneira cirúrgica.

## Desenvolvimento Contínuo Local (.NET Nativo)

Se optar por rodar pelo Visual Studio em vez de Docker, garanta que ambas as aplicações sejam iniciadas simultaneamente:
1. Inicie o Server Project de inicialização `panContas.API`.
2. Em um novo terminal na pasta `/panContas.Front`, digite `dotnet watch run` para aplicar as modificações visuais do Blazor imediatamente.
