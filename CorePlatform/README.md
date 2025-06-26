# CorePlatform

## Visão Geral

O **CorePlatform** é uma plataforma desenvolvida em .NET 8 para gestão de pacientes, estruturada com base em Clean Architecture e Domain-Driven Design (DDD). O projeto adota práticas modernas de desenvolvimento, como uso de Use Cases, padrões de repositório, Result Pattern, testes automatizados (unitários e de integração), além de middlewares e componentes cross-cutting.

---

## Arquitetura

### Clean Architecture

O projeto segue a Clean Architecture, separando responsabilidades em camadas bem definidas:

- **Domain**: Entidades, agregados, interfaces de repositório e regras de negócio.
- **Application**: Casos de uso (Use Cases), DTOs e interfaces de aplicação.
- **Infrastructure**: Implementação dos repositórios, acesso a dados e integrações externas.
- **Api**: Camada de apresentação, responsável por expor endpoints RESTful.

### Domain-Driven Design (DDD)

- **Entidades**: Representam conceitos do domínio, como `Patient`.
- **Repositórios**: Interfaces e implementações para persistência de entidades.
- **Serviços de Domínio**: (Se aplicável) encapsulam regras de negócio que não pertencem a uma única entidade.

---

## Design Patterns Utilizados

- **Repository Pattern**: Abstrai o acesso a dados, permitindo trocar a implementação sem afetar o domínio.
- **Result Pattern**: Padroniza o retorno de operações, encapsulando sucesso, falha e mensagens de erro.
- **DTO (Data Transfer Object)**: Utilizado para transportar dados entre camadas, desacoplando a API do domínio.
- **Dependency Injection**: Todas as dependências são injetadas via construtor, facilitando testes e manutenção.
- **Use Case Pattern**: Cada operação de negócio é encapsulada em um caso de uso, promovendo clareza e testabilidade.

---

## Principais Componentes

### Use Cases

- **ListPatientsUseCase**: Lista pacientes com filtros.
- **CreatePatientUseCase**: Cria um novo paciente.
- **UpdatePatientUseCase**: Atualiza dados de um paciente, apenas campos enviados são modificados.
- **DeactivatePatientUseCase**: Desativa um paciente.
- **GetPatientDashboardUseCase**: (Se aplicável) retorna dados agregados para dashboards.

### Controllers

- **PatientsController**: Expõe endpoints REST para manipulação de pacientes, utilizando os Use Cases.

### DTOs

- **CreatePatientDto**, **UpdatePatientDto**: Estruturas para entrada/saída de dados na API.

### Result Pattern

- **Result**: Objeto que encapsula o resultado de operações, indicando sucesso, falha e mensagens.

### Repositories

- **IPatientRepository**: Interface para operações de persistência de pacientes.
- **PatientRepository**: Implementação concreta, usando Entity Framework Core.

---

## Testes

### Testes Unitários

- Cobrem casos de uso, validações e regras de negócio.
- Utilizam NUnit e Moq para mocks e asserts.

### Testes de Integração

- Testam repositórios e integração com banco de dados em memória (`Microsoft.EntityFrameworkCore.InMemory`).
- Garantem que operações de CRUD funcionam corretamente.

---

## Middleware e Cross Cutting

- **Middleware**: (Adicionar detalhes se houver, como tratamento global de erros, logging, autenticação, etc.)
- **Cross Cutting Concerns**: Componentes reutilizáveis como logging, validação, tratamento de exceções e mapeamento de objetos (ex: Mapster).

---

## Como Configurar e Executar

### Pré-requisitos

- .NET 8 SDK instalado.

### Passos

1. **Restaurar dependências**:dotnet restore
2. **Executar a aplicação**:dotnet run
3. **Executar os testes**:

### Configuração de Banco de Dados

- Por padrão, a aplicação pode ser configurada para usar um banco em memória para testes.
- Para produção, configure a string de conexão no arquivo `appsettings.json` do projeto `CorePlatform.Api`.

---

## Exemplos de Uso da API

- **Criar paciente**: `POST /api/patients`
- **Listar pacientes**: `GET /api/patients?name=...&cpf=...&isActive=...`
- **Atualizar paciente**: `PUT /api/patients/{cpf}`
- **Desativar paciente**: `PATCH /api/patients/{cpf}/deactivate`

---

## Dependências Principais

- .NET 8
- Entity Framework Core
- NUnit, Moq, Coverlet (testes)
- Mapster (mapeamento de objetos)

---

## Observações

- O projeto é extensível e preparado para evoluir, seguindo boas práticas de arquitetura e design.
- Para dúvidas ou sugestões, abra uma issue no repositório.

Desenvolvido para fins de teste técnico.

Lucas Duarte