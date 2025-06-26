# CorePlatform

## Vis�o Geral

O **CorePlatform** � uma plataforma desenvolvida em .NET 8 para gest�o de pacientes, estruturada com base em Clean Architecture e Domain-Driven Design (DDD). O projeto adota pr�ticas modernas de desenvolvimento, como uso de Use Cases, padr�es de reposit�rio, Result Pattern, testes automatizados (unit�rios e de integra��o), al�m de middlewares e componentes cross-cutting.

---

## Arquitetura

### Clean Architecture

O projeto segue a Clean Architecture, separando responsabilidades em camadas bem definidas:

- **Domain**: Entidades, agregados, interfaces de reposit�rio e regras de neg�cio.
- **Application**: Casos de uso (Use Cases), DTOs e interfaces de aplica��o.
- **Infrastructure**: Implementa��o dos reposit�rios, acesso a dados e integra��es externas.
- **Api**: Camada de apresenta��o, respons�vel por expor endpoints RESTful.

### Domain-Driven Design (DDD)

- **Entidades**: Representam conceitos do dom�nio, como `Patient`.
- **Reposit�rios**: Interfaces e implementa��es para persist�ncia de entidades.
- **Servi�os de Dom�nio**: (Se aplic�vel) encapsulam regras de neg�cio que n�o pertencem a uma �nica entidade.

---

## Design Patterns Utilizados

- **Repository Pattern**: Abstrai o acesso a dados, permitindo trocar a implementa��o sem afetar o dom�nio.
- **Result Pattern**: Padroniza o retorno de opera��es, encapsulando sucesso, falha e mensagens de erro.
- **DTO (Data Transfer Object)**: Utilizado para transportar dados entre camadas, desacoplando a API do dom�nio.
- **Dependency Injection**: Todas as depend�ncias s�o injetadas via construtor, facilitando testes e manuten��o.
- **Use Case Pattern**: Cada opera��o de neg�cio � encapsulada em um caso de uso, promovendo clareza e testabilidade.

---

## Principais Componentes

### Use Cases

- **ListPatientsUseCase**: Lista pacientes com filtros.
- **CreatePatientUseCase**: Cria um novo paciente.
- **UpdatePatientUseCase**: Atualiza dados de um paciente, apenas campos enviados s�o modificados.
- **DeactivatePatientUseCase**: Desativa um paciente.
- **GetPatientDashboardUseCase**: (Se aplic�vel) retorna dados agregados para dashboards.

### Controllers

- **PatientsController**: Exp�e endpoints REST para manipula��o de pacientes, utilizando os Use Cases.

### DTOs

- **CreatePatientDto**, **UpdatePatientDto**: Estruturas para entrada/sa�da de dados na API.

### Result Pattern

- **Result**: Objeto que encapsula o resultado de opera��es, indicando sucesso, falha e mensagens.

### Repositories

- **IPatientRepository**: Interface para opera��es de persist�ncia de pacientes.
- **PatientRepository**: Implementa��o concreta, usando Entity Framework Core.

---

## Testes

### Testes Unit�rios

- Cobrem casos de uso, valida��es e regras de neg�cio.
- Utilizam NUnit e Moq para mocks e asserts.

### Testes de Integra��o

- Testam reposit�rios e integra��o com banco de dados em mem�ria (`Microsoft.EntityFrameworkCore.InMemory`).
- Garantem que opera��es de CRUD funcionam corretamente.

---

## Middleware e Cross Cutting

- **Middleware**: (Adicionar detalhes se houver, como tratamento global de erros, logging, autentica��o, etc.)
- **Cross Cutting Concerns**: Componentes reutiliz�veis como logging, valida��o, tratamento de exce��es e mapeamento de objetos (ex: Mapster).

---

## Como Configurar e Executar

### Pr�-requisitos

- .NET 8 SDK instalado.

### Passos

1. **Restaurar depend�ncias**:dotnet restore
2. **Executar a aplica��o**:dotnet run
3. **Executar os testes**:

### Configura��o de Banco de Dados

- Por padr�o, a aplica��o pode ser configurada para usar um banco em mem�ria para testes.
- Para produ��o, configure a string de conex�o no arquivo `appsettings.json` do projeto `CorePlatform.Api`.

---

## Exemplos de Uso da API

- **Criar paciente**: `POST /api/patients`
- **Listar pacientes**: `GET /api/patients?name=...&cpf=...&isActive=...`
- **Atualizar paciente**: `PUT /api/patients/{cpf}`
- **Desativar paciente**: `PATCH /api/patients/{cpf}/deactivate`

---

## Depend�ncias Principais

- .NET 8
- Entity Framework Core
- NUnit, Moq, Coverlet (testes)
- Mapster (mapeamento de objetos)

---

## Observa��es

- O projeto � extens�vel e preparado para evoluir, seguindo boas pr�ticas de arquitetura e design.
- Para d�vidas ou sugest�es, abra uma issue no reposit�rio.

Desenvolvido para fins de teste t�cnico.

Lucas Duarte