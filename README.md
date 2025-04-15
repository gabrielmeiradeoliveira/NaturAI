
# 💪 NaturAI - Gerenciador de Treinos com IA

NaturAI é uma aplicação que usa inteligência artificial para gerar treinos personalizados baseados nos treinos anteriores e nas informações do usuário. A IA usada é o **DeepSeek Coder**, executada localmente via **Ollama**, tornando o projeto gratuito e com performance local.

---

## ⚙️ Tecnologias

- ASP.NET Core
- Dapper
- PostgreSQL
- DeepSeek Coder (via Ollama)
- JWT Authentication

---

## 🧠 Funcionalidades com IA

A IA gera automaticamente **15 dias de treinos** com base:
- Nos **últimos 15 dias de treino**
- Nas **informações do usuário** (peso, sexo, intensidade, nível etc.)

A IA sempre retorna o treino no mesmo formato JSON.

---

## 🏗️ Arquitetura

```
Controller -> Service -> Repository -> Database
                             ↘
                       DeepSeek Coder (IA)
```

---

## 🔐 Autenticação

- Usuários podem se registrar, fazer login e receber um JWT.
- Permissões de usuário: padrão ou professor.
- Professores podem vincular alunos à sua conta.

---

## 📦 Como rodar o projeto

### 1. Banco de Dados

Crie o banco PostgreSQL e configure a connection string no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=senha;Database=naturai"
}
```

### 2. Migrations (se estiver usando)

```bash
dotnet ef database update
```

---

## 🤖 Como rodar a IA (DeepSeek Coder)

A IA roda localmente usando o [Ollama](https://ollama.com), via Docker.

### 1. Instale o Ollama

- https://ollama.com/download

> Certifique-se de ter o Docker instalado

### 2. Baixe o modelo:

```bash
ollama pull deepseek-coder
```

### 3. Rode o modelo:

```bash
ollama run deepseek-coder
```

> A IA estará disponível via `http://localhost:11434/api/generate`

---

## 📡 Exemplo de chamada para a IA

```http
POST http://localhost:11434/api/generate
Content-Type: application/json

{
  "model": "deepseek-coder",
  "prompt": "### Gere 15 dias de treino com base neste JSON: {DADOS_DO_USUARIO}",
  "stream": false
}
```

---

## 📁 Estrutura de Diretórios

```
NaturAI/
├── Controller/
│   └── UsuarioController.cs
│   └── GeradorTreinoController.cs
├── Service/
│   └── UsuarioService.cs
│   └── GeradorTreinoService.cs
├── Repository/
│   └── UsuarioRepository.cs
│   └── GeradorTreinoRepository.cs
├── Model/
│   └── UsuarioDTO.cs
│   └── TreinoDTO.cs
├── Program.cs
├── appsettings.json
└── README.md
```

---

## 🧪 Teste rápido da API (Requisições via Swagger)

1. Execute a aplicação:
```bash
dotnet run
```

2. Acesse: `https://localhost:{PORT}/swagger`

---

## 🧔 Permissão de Professor

- Professores podem vincular alunos com base no e-mail.
- Alunos vinculados podem ter seus treinos acompanhados pelo professor.

---

## 🔐 Autenticação JWT

- Após o login, o token JWT é retornado.
- Incluir o token nas requisições autenticadas:

```http
Authorization: Bearer {seu_token}
```

---

## 🧾 Licença

Projeto pessoal/educacional - sinta-se livre para usar e contribuir.
