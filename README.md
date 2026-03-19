Projeto ainda em desenvolvimento!

# 📚 Sistema de Biblioteca Online

Aplicação fullstack para gerenciamento de livros, usuários e empréstimos, com foco em organização, controle e escalabilidade de dados.

 **Acesse a aplicação:**
https://bibliotecaapp-ak62.onrender.com/

---

##  Problema

Bibliotecas (mesmo pequenas) enfrentam dificuldades para controlar:

* Cadastro e organização de livros
* Disponibilidade de exemplares
* Histórico de empréstimos
* Controle de usuários

Muitos desses processos ainda são feitos manualmente ou com sistemas pouco eficientes.

---

##  Solução

Este projeto propõe um sistema web para gerenciamento de biblioteca, permitindo:

* Cadastro e consulta de livros
* Controle de usuários
* Registro de empréstimos
* Organização centralizada das informações

Com uma API backend estruturada e um frontend integrado, a aplicação simula um cenário real de uso.

---

##  Arquitetura

O sistema foi dividido em duas camadas principais:

### 🔹 Backend (.NET / C#)

* API REST
* Estrutura em camadas (Controller, Service, Repository)
* Uso de DTOs para transporte de dados
* Integração com banco de dados PostgreSQL

### 🔹 Frontend

* Interface web para interação com o sistema
* Consumo da API backend

---

##  Tecnologias utilizadas

### Backend

* C# / .NET
* Entity Framework
* PostgreSQL

### Frontend

* (adicione aqui: Vue.js, Blazor, etc — importante você especificar)

### Outros

* Git & GitHub
* Render (deploy)

---

##  Funcionalidades

*  Cadastro de livros
*  Cadastro de usuários
*  Operações CRUD completas
*  Integração entre frontend e backend
*  Deploy em ambiente real

---

##  Possíveis melhorias (roadmap)

Este projeto pode evoluir com:

* Autenticação com JWT
* Controle de permissões (admin/usuário)
* Paginação e filtros avançados
* Logs de operações
* Tratamento de erros mais robusto
* Testes automatizados

---

##  Como rodar o projeto localmente

### Backend

```bash
git clone https://github.com/carolinefreitasalegre/Biblioteca
cd Biblioteca

# configurar connection string no appsettings
dotnet restore
dotnet run
```

### Frontend

```bash
# ajustar conforme tecnologia usada
npm install
npm run dev
```

---

##  Demonstração

<img width="1091" height="666" alt="image" src="https://github.com/user-attachments/assets/94e25290-24a8-4241-98af-580bf2147e79" />
<img width="1668" height="848" alt="image" src="https://github.com/user-attachments/assets/86047e83-7155-4271-bb0b-9e6bbcae7941" />
<img width="1670" height="801" alt="image" src="https://github.com/user-attachments/assets/9a7cb9d8-d340-4ae6-af27-f722ba2d14f8" />




---

##  Sobre o projeto

Este projeto foi desenvolvido com foco em prática de desenvolvimento backend com .NET, integração com banco de dados relacional e construção de APIs REST.

Também demonstra experiência com:

* Estruturação de aplicações reais
* Separação de responsabilidades
* Consumo de API no frontend
* Deploy de aplicação

---

##  Autora

Caroline Freitas
https://github.com/carolinefreitasalegre

