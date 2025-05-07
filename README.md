# 💸 WLConsultings - Carteira Digital API

API RESTful desenvolvida em **.NET 9** para gerenciamento de carteiras digitais e transações financeiras entre usuários. A solução inclui autenticação via JWT, persistência com **PostgreSQL** e uso de containers com **Docker**.

---

## ✅ Funcionalidades

- Registro e autenticação de usuários via JWT.
- Consulta de saldo da carteira.
- Depósito de valores na carteira.
- Transferência entre carteiras de diferentes usuários.
- Listagem de transferências com filtro opcional por período (data inicial/final).
- API segura e organizada, seguindo boas práticas e arquitetura em camadas.

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura baseada em **DDD (Domain-Driven Design)** com as seguintes camadas:

- **Domain**: Entidades e regras de negócio.
- **Domain.Core**: Interfaces para contratos genéricos e de repositório.
- **Domain.Services**: Serviços de domínio e orquestração de lógica.
- **Infrastructure**: Implementações de repositórios e persistência (EF Core com PostgreSQL).
- **Application**: Camada de aplicação (pode conter DTOs, validadores etc).
- **API**: Camada de apresentação com controllers e configuração geral.

---

## 🐳 Executando com Docker

### ⚙️ Pré-requisitos

- Docker e Docker Compose instalados

### ▶️ Como executar

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/wlconsultings-api.git
cd wlconsultings-api
```

2. Suba os containers:

```bash

docker-compose up --build
```

3. Acesse a API em: http://localhost:5001/swagger

- O Swagger UI estará disponível para testar os endpoints.

4. Banco de Dados (PostgreSQL):

        Host: localhost

        Porta: 5433

        Usuário: postgres

        Senha: postgres

        Banco: wlconsultingsdb

5. PGAdmin (gerenciador opcional):

        Acesse: http://localhost:15632

        Email: dev@gmail.com

        Senha: 12341

### 🔐 Autenticação JWT
Todas as rotas protegidas exigem o envio do token JWT no header:

```bash
Authorization: Bearer <seu_token>
```
Ao criar um usuário e autenticar, você receberá o token no login.

📦 Populando o banco de dados com dados fictícios
Após subir os containers, execute o endpoint de seed para gerar dados de demonstração:

```bash
POST /api/dev/seed
```

Esse endpoint irá:

        Criar usuários fictícios

        Criar carteiras associadas

        Realizar transferências entre elas

### 📚 Endpoints Principais

| Método | Rota                     | Descrição                           |
| ------ | ------------------------ | ----------------------------------- |
| POST   | /api/auth/register       | Criação de usuário                  |
| POST   | /api/auth/login          | Autenticação e geração do token JWT |
| GET    | /api/wallet/balance      | Consulta do saldo da carteira       |
| POST   | /api/wallet/deposit      | Realizar um depósito                |
| POST   | /api/wallet/transfer     | Transferir entre carteiras          |
| GET    | /api/wallet/transactions | Listar transferências do usuário    |

📌 Tecnologias Utilizadas
        ASP.NET 9

        PostgreSQL

        Entity Framework Core

        JWT Authentication

        Docker e Docker Compose

        Swagger

🤔 Justificativas de Escolhas
        C# / ASP.NET 9: Escolhido por sua robustez, segurança, alta performance e suporte nativo à arquitetura em camadas com DDD.

        PostgreSQL: Banco relacional maduro e com ótima compatibilidade com .NET. Suporta transações complexas e segurança.

        Docker: Facilita a configuração do ambiente, testes locais e deploys em qualquer ambiente.