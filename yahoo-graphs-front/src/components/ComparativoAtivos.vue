<template>
  <div class="grafico-box">
    <canvas ref="canvas"></canvas>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
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

const carregar = async () => {
  const datasets = [];
  let labels = [];

  for (let i = 0; i < props.ativos.length; i++) {
    const simbolo = props.ativos[i];
    try {
      const data = await getStockHistory(simbolo);
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

onMounted(carregar);
</script>

<style scoped>
.grafico-box {
  max-width: 800px;
  margin: 2rem auto;
  background: white;
  padding: 1rem;
  border-radius: 1rem;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}
</style>
