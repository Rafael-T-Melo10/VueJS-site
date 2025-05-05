# 📈 Yahoo Finance Graphs com Vue.js + .NET 8

Este projeto é uma aplicação moderna que consome dados da API do Yahoo Finance para exibir gráficos interativos, notícias financeiras e informações do mercado brasileiro.

Criado com Vue.js 3 no front-end e ASP.NET Core (.NET 8) no back-end, ele usa cache local para otimizar performance e proteger sua chave de API.

---

## 📊 Funcionalidades

- 📈 Gráfico de linha com histórico de preço
- 📊 Gráfico de barras de volume
- 🏆 Top 5 ativos com maior variação
- 🧠 Fallback inteligente: se não houver JSON salvo, busca da API e armazena localmente
- 🗞️ Carrossel de notícias financeiras
- 📱 Menu lateral com perfil e animação, ideal para mobile
- 🌐 Resumo diário de ativos em alta, baixa e mais negociados (Brasil)

---

## 🧰 Tecnologias utilizadas

### Front-end
- [Vue.js 3](https://vuejs.org/) (`^3.4`)
- [Chart.js](https://www.chartjs.org/) (`^4.x`)
- Axios (`^1.x`)

### Back-end
- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
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
```

Por padrão, o servidor estará em:
📍 http://localhost:5000/api/yahoo/{Endpoint}

### 🌐 2. Front-end (Vue)
```bash
cd vueSite
npm install
npm run serve
```

Acesse o front em:
📍 http://localhost:8080/ ou a porta exibida no terminal

## 🧠 Observações

  -  Todos os dados são buscados primeiro dos arquivos JSON (mock-{symbol}.json)

  -  Se o arquivo não existir, o back-end chama a API do Yahoo via RapidAPI e salva o mock automaticamente

  -  Já que o limite de requisições dessa API é de 500 por mês 

  - Isso economiza requisições e permite desenvolvimento offline
