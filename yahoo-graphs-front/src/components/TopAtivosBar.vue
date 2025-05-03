<template>
  <div class="grafico-box">
    <div class="botoes-range">
      <button
        v-for="r in rangesDisponiveis"
        :key="r.value"
        :class="{ ativo: rangeAtual === r.value }"
        @click="rangeAtual = r.value"
      >
        {{ r.label }}
      </button>
    </div>
    <canvas ref="canvas"></canvas>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import { Chart, registerables } from "chart.js";
import { getStockHistory } from "../api";

Chart.register(...registerables);

const props = defineProps({
  ativos: { type: Array, required: true },
  titulo: { type: String, default: "Top ativos por variação" },
});

const canvas = ref(null);
let chart = null;

const rangesDisponiveis = [
  { value: "1mo", label: "1 mês" },
  { value: "3mo", label: "3 meses" },
  { value: "6mo", label: "6 meses" },
  { value: "1y", label: "1 ano" },
  { value: "5y", label: "5 anos" },
];
const rangeAtual = ref("6mo");

const cores = [
  "#ef4444",
  "#f97316",
  "#22c55e",
  "#3b82f6",
  "#8b5cf6",
  "#e11d48",
  "#0ea5e9",
];

const carregarVariações = async () => {
  const datasets = [];
  const labels = ["Variação (%)"];

  for (let i = 0; i < props.ativos.length; i++) {
    const simbolo = props.ativos[i];
    try {
      const data = await getStockHistory(simbolo, "1mo", rangeAtual.value);
      const valores = data.chart?.result?.[0]?.indicators.quote[0].close;
      if (!valores || valores.length < 2) continue;

      const precoInicial = valores[0];
      const precoFinal = valores[valores.length - 1];
      const variacao = ((precoFinal - precoInicial) / precoInicial) * 100;

      datasets.push({
        label: simbolo,
        data: [Number(variacao.toFixed(2))],
        backgroundColor: cores[i % cores.length],
      });
    } catch (e) {
      console.error("Erro ao buscar", simbolo);
    }
  }

  if (chart) chart.destroy();

  chart = new Chart(canvas.value, {
    type: "bar",
    data: {
      labels,
      datasets,
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: props.titulo,
        },
        tooltip: {
          callbacks: {
            label: (ctx) => `${ctx.dataset.label}: ${ctx.parsed.y.toFixed(2)}%`,
          },
        },
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            callback: (val) => val + "%",
          },
        },
      },
    },
  });
};

watch(rangeAtual, carregarVariações);
onMounted(carregarVariações);
</script>

<style scoped>
.grafico-box {
  max-width: 600px;
  margin-left: auto;
  margin-right: auto;
  background: white;
  padding: 1rem;
  border-radius: 1rem;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.botoes-range {
  display: flex;
  gap: 0.5rem;
  justify-content: center;
  margin-bottom: 1rem;
  flex-wrap: wrap;
}

.botoes-range button {
  padding: 0.5rem 1rem;
  border: 1px solid #ccc;
  background: #f1f5f9;
  border-radius: 8px;
  cursor: pointer;
  transition: 0.2s;
}

.botoes-range button:hover {
  background: #e2e8f0;
}

.botoes-range button.ativo {
  background: #0ea5e9;
  color: white;
  border-color: #0284c7;
}
</style>
