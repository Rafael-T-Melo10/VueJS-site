<template>
  <div class="resumo-movers">
    <div v-for="grupo in dados.result" :key="grupo.title" class="grupo-card">
      <h3>{{ grupo.title }}</h3>
      <div class="lista-ativos">
        <div v-for="quote in grupo.quotes" :key="quote.symbol" class="ativo">
          <span class="symbol">{{ quote.symbol }}</span>
          <span class="preco"
            >R$ {{ quote.regularMarketPrice?.toFixed(2) || "0.00" }}</span
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
const dados = ref({ result: [] });

onMounted(async () => {
  try {
    const res = await fetch("http://localhost:5000/api/yahoo/movers");
    const json = await res.json();
    dados.value = json.finance;
  } catch (e) {
    console.error("Erro ao carregar dados de movers:", e);
  }
});
</script>
<style scoped>
.resumo-movers {
  max-width: 1100px;
  margin: 1rem auto;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: 1.5rem;
}

.grupo-card {
  flex: 1 1 300px;
  background: #ffffff;
  border-radius: 16px;
  padding: 1.5rem;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.06);
  border: 1px solid #e2e8f0;
}

.grupo-card h3 {
  font-size: 1.2rem;
  color: #0f172a;
  margin-bottom: 1rem;
  border-bottom: 2px solid #e2e8f0;
  padding-bottom: 0.5rem;
}

.lista-ativos {
  display: flex;
  flex-direction: column;
  gap: 0.6rem;
}

.ativo {
  display: flex;
  justify-content: space-between;
  font-size: 0.95rem;
  padding: 0.4rem 0.6rem;
  border-radius: 8px;
  background-color: #f8fafc;
  border: 1px solid #e2e8f0;
  transition: background 0.2s ease;
}

.ativo:hover {
  background-color: #e0f2fe;
}

.symbol {
  font-weight: bold;
  color: #1e3a8a;
}

.preco {
  font-weight: 500;
  color: #059669;
}
</style>
