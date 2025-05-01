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
  symbol: {
    type: String,
    required: true,
  },
  title: {
    type: String,
    default: "Cotação",
  },
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

const renderChart = async () => {
  try {
    const data = await getStockHistory(props.symbol, "1mo", rangeAtual.value);

    if (!data.chart?.result || !data.chart.result[0]) {
      console.error("Nenhum dado retornado para o ativo.");
      return;
    }

    const prices = data.chart.result[0].indicators.quote[0].close;
    const timestamps = data.chart.result[0].timestamp.map((ts) =>
      new Date(ts * 1000).toLocaleDateString()
    );

    if (chart) chart.destroy();

    chart = new Chart(canvas.value, {
      type: "line",
      data: {
        labels: timestamps,
        datasets: [
          {
            label: props.title,
            data: prices,
            borderWidth: 2,
            borderColor: "#3b82f6",
            fill: true,
            pointStyle: "circle",
            pointRadius: 5,
            pointHoverRadius: 7,
            tension: 0.3,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          title: {
            display: true,
            text: props.title,
          },
        },
        elements: {
          point: {
            pointStyle: "circle",
          },
        },
        interaction: {
          mode: "index",
          intersect: false,
        },
      },
    });
  } catch (error) {
    console.error("Erro ao carregar gráfico:", error);
  }
};

onMounted(renderChart);
watch([() => props.symbol, rangeAtual], renderChart);
</script>

<style scoped>
.grafico-box {
  max-width: 600px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 30px;
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
