# Biblioteca - (aplicação em desenvolvimento)
<img width="1091" height="666" alt="image" src="https://github.com/user-attachments/assets/94e25290-24a8-4241-98af-580bf2147e79" />
<img width="1668" height="848" alt="image" src="https://github.com/user-attachments/assets/86047e83-7155-4271-bb0b-9e6bbcae7941" />
<img width="1670" height="801" alt="image" src="https://github.com/user-attachments/assets/9a7cb9d8-d340-4ae6-af27-f722ba2d14f8" />




## Descrição

O Biblioteca é um projeto em desenvolvimento cujo objetivo é disponibilizar uma API REST para gerenciamento de usuários e seus livros, permitindo o controle de coleção, status de leitura e informações detalhadas de cada obra.

O sistema está sendo construído com foco em organização de domínio, boas práticas de arquitetura e facilidade de evolução futura, servindo como base para aplicações web ou mobile.

Status do Projeto

Em desenvolvimento.

Funcionalidades, estrutura de banco de dados e regras de negócio estão em constante evolução.

## Tecnologias Utilizadas

.NET 8

ASP.NET Core Web API

Entity Framework Core

PostgreSQL

Cloudinary

Swagger (documentação da API)

Git e GitHub

## Estrutura do Projeto

O projeto segue uma separação em camadas para facilitar manutenção e escalabilidade:

API
Responsável por expor os endpoints HTTP.

Services
Contém as regras de negócio e validações.

Repositories
Responsável pelo acesso a dados e comunicação com o banco.

Models
Entidades do domínio e enums.

Infrastructure
Contexto do Entity Framework, migrations e configurações de persistência.

## Funcionalidades Planejadas

Cadastro e autenticação de usuários

Gerenciamento de livros por usuário

Controle de status de leitura

Organização por categorias

Histórico e anotações pessoais

Autorização baseada em perfis de usuário

## Banco de Dados

O banco de dados utiliza PostgreSQL, com mapeamento feito via Entity Framework Core e controle de versionamento através de migrations.

Relacionamento principal:

Um usuário pode possuir vários livros

Cada livro pertence a um único usuário
