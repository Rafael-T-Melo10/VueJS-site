<template>
  <div class="resumo-movers">
    <div
      v-for="grupo in dados.result"
      :key="grupo.title"
      class="grupo-card"
      :class="{
        baixa: grupo.title.toLowerCase().includes('baixa'),
        alta: !grupo.title.toLowerCase().includes('baixa'),
      }"
    >
      <h3>
        {{ grupo.title }}
        <span class="tooltip-wrapper">
          <span class="tooltip-icon">?</span>
          <div class="tooltip-box">{{ grupo.description }}</div>
        </span>
      </h3>
      <div class="lista-ativos">
        <div v-for="quote in grupo.quotes" :key="quote.symbol" class="ativo">
          <span class="symbol">{{ quote.symbol }}</span>
          <span class="preco">
            R$ {{ quote.regularMarketPrice?.toFixed(2) || "0.00" }}
          </span>
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
.tooltip-icon {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 18px;
  height: 18px;
  font-size: 12px;
  font-weight: bold;
  color: white;
  background-color: #94a3b8;
  border-radius: 50%;
  cursor: help;
  margin-left: 6px;
  transition: background-color 0.2s ease;
}
.tooltip-box {
  position: absolute;
  bottom: 125%;
  left: 50%;
  transform: translateX(-50%);
  background-color: #0f172a;
  color: #ffffff;
  padding: 0.5rem 0.7rem;
  border-radius: 6px;
  font-size: 0.75rem;
  white-space: normal;
  max-width: 220px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  opacity: 0;
  pointer-events: none;
  transition: opacity 0.2s ease;
  z-index: 1000;
  width: 250px;
}

.tooltip-box::after {
  content: "";
  position: absolute;
  top: 100%;
  left: 50%;
  margin-left: -6px;
  border-width: 6px;
  border-style: solid;
  border-color: #0f172a transparent transparent transparent;
}

.tooltip-wrapper:hover .tooltip-box {
  opacity: 1;
}
.tooltip-wrapper {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  position: relative;
}

.tooltip-icon:hover {
  background-color: #64748b;
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
  display: flex;
  align-items: center;
  justify-content: center;
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

.baixa {
  border-color: #f87171;
  background-color: #fff1f2;
}

.baixa h3 {
  color: #b91c1c;
}

.baixa .preco {
  color: #dc2626;
}
.alta {
  border-color: #69f767;
  background-color: #fff1f2;
}

.alta h3 {
  color: #1cb91f;
}

.alta .preco {
  color: #059669;
}
</style>
