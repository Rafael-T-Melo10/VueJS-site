# 📈 Yahoo Finance Graphs com Vue.js + .NET 6

Este projeto é uma aplicação que consome dados da API do Yahoo Finance para exibir gráficos interativos de ações, FIIs e ETFs.  
Foi desenvolvido com **Vue.js no front-end** e **ASP.NET Core (.NET 6) no back-end** para lidar com cache local de dados (mock) e proteger a chave da API.

---

## 📊 Funcionalidades

- Gráfico de linha com histórico de preço
- Gráfico de barras com volume negociado
- Comparativo entre múltiplos ativos
- Top 5 ativos com maior variação
- Busca por ativo com autocomplete (versão mock/offline)
- Fallback: busca da API e salvamento automático do JSON local

---

## 🧰 Tecnologias utilizadas

### Front-end
- [Vue.js 3](https://vuejs.org/) (`^3.4`)
- [Chart.js](https://www.chartjs.org/) (`^4.x`)
- Axios (`^1.x`)

### Back-end
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- ASP.NET Core Web API
- HttpClient
- Suporte a CORS e arquivos estáticos (`wwwroot/mocks`)

---

## 🚀 Como rodar o projeto

### 🔧 1. Back-end (.NET)

```bash
cd yahoo-graphs-back
dotnet restore
dotnet run

Por padrão, o servidor estará em:
📍 http://localhost:5000/api/yahoo/{symbol}

### 🌐 2. Front-end (Vue)

cd vueSite
npm install
npm run serve

Acesse o front em:
📍 http://localhost:8080/ ou a porta exibida no terminal


## 🧠 Observações

    Todos os dados são buscados primeiro dos arquivos JSON (mock-{symbol}.json)

    Se o arquivo não existir, o back-end chama a API do Yahoo via RapidAPI e salva o mock automaticamente

    Isso economiza requisições e permite desenvolvimento offline

    A chave da RapidAPI fica protegida no back-end

## 📌 Requisitos

    Node.js ^18.x

    .NET 6 SDK

    Navegador moderno (Chrome/Firefox)

    Git (opcional)