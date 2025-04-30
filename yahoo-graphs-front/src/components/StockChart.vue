<template>
  <div>
    <h2>{{ title }}</h2>
    <canvas ref="canvas"></canvas>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import { Chart, registerables } from "chart.js";
import { getStockHistory } from "../api";

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

Chart.register(...registerables);

const canvas = ref(null);
let chart = null;

const renderChart = async () => {
  try {
    const data = await getStockHistory(props.symbol);

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
            borderColor: "rgba(75, 192, 192, 1)",
            backgroundColor: "rgba(75, 192, 192, 0.2)",
            fill: true,
            pointStyle: "circle",
            pointRadius: 5,
            pointHoverRadius: 7,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            labels: {
              usePointStyle: true,
            },
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
watch(() => props.symbol, renderChart);
</script>
