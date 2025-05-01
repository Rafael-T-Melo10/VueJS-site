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
import { ref, watch, onMounted } from "vue";
import { Chart, registerables } from "chart.js";
import { getStockHistory } from "../api";

Chart.register(...registerables);

const props = defineProps({
  ativos: { type: Array, required: true },
  titulo: { type: String, default: "Comparativo de Ativos" },
});

const canvas = ref(null);
let chart = null;

const cores = ["#10b981", "#f59e0b", "#3b82f6", "#ef4444", "#6366f1"];

const rangesDisponiveis = [
  { value: "1mo", label: "1 mês" },
  { value: "3mo", label: "3 meses" },
  { value: "6mo", label: "6 meses" },
  { value: "1y", label: "1 ano" },
  { value: "5y", label: "5 anos" },
];
const rangeAtual = ref("6mo");

const carregar = async () => {
  const datasets = [];
  let labels = [];

  for (let i = 0; i < props.ativos.length; i++) {
    const simbolo = props.ativos[i];
    try {
      const data = await getStockHistory(simbolo, "1mo", rangeAtual.value);
      const res = data.chart?.result?.[0];
      const valores = res?.indicators.quote[0].close;
      const datas = res?.timestamp.map((ts) =>
        new Date(ts * 1000).toLocaleDateString()
      );

      if (!valores || !datas) continue;

      if (labels.length === 0) labels = datas;

      datasets.push({
        label: simbolo,
        data: valores,
        borderColor: cores[i % cores.length],
        fill: false,
        tension: 0.3,
      });
    } catch (e) {
      console.error("Erro em", simbolo);
    }
  }

  if (chart) chart.destroy();

  chart = new Chart(canvas.value, {
    type: "line",
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
      },
    },
  });
};

watch(rangeAtual, () => {
  carregar();
});

onMounted(carregar);
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
