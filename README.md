# gRPC Proof of Concept

Este projeto demonstra o uso do **gRPC** em um ambiente .NET, com um **servidor gRPC** e uma **API REST** que consome o serviço gRPC.

## 📁 Estrutura do Projeto

```
GrpcProofOfConcept/
│── src/
│   ├── Contracts/        # Biblioteca compartilhada com contratos gRPC
│   ├── GrpcServer/       # Servidor gRPC
│   ├── WebApiClient/     # API REST que consome o gRPC
│── GrpcProofOfConcept.sln  # Solution file
```

## 🚀 Como Rodar o Projeto

### 1️⃣ **Rodar o Servidor gRPC**
Abra um terminal e execute:
```sh
dotnet run --project src/GrpcServer
```
O servidor gRPC estará rodando em `https://localhost:5087`

### 2️⃣ **Rodar a API REST (Cliente gRPC)**
Em outro terminal, execute:
```sh
dotnet run --project src/WebApiClient
```
A API REST estará rodando em `https://localhost:5286`

### 3️⃣ **Testar a Comunicação**
Agora você pode testar chamando a API REST, que se comunica com o servidor gRPC.

#### 📌 Teste com `curl`
```sh
curl -X GET "https://localhost:5002/api/accounts/123" -k
```
Resposta esperada:
```json
{
  "accountId": "123",
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

#### 📌 Teste pelo navegador
Acesse `https://localhost:5002/swagger/index.html` e teste a API via Swagger.

## 🛠️ Tecnologias Utilizadas
- **.NET 8**
- **gRPC**
- **ASP.NET Core Web API**
- **Protobuf** para comunicação eficiente

