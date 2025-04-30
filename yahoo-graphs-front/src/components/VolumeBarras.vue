<template>
  <div class="grafico-box">
    <canvas ref="canvas"></canvas>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import { Chart, registerables } from "chart.js";
import { getStockHistory } from "../api";

Chart.register(...registerables);

const props = defineProps({
  symbol: { type: String, required: true },
  titulo: { type: String, default: "Volume diário negociado" },
});

const canvas = ref(null);
let chart = null;

const carregarVolume = async () => {
  try {
    const data = await getStockHistory(props.symbol);

    const result = data.chart?.result?.[0];
    if (!result) return;

    const volumes = result.indicators.quote[0].volume;
    const datas = result.timestamp.map((ts) =>
      new Date(ts * 1000).toLocaleDateString()
    );

    if (chart) chart.destroy();

    chart = new Chart(canvas.value, {
      type: "bar",
      data: {
        labels: datas,
        datasets: [
          {
            label: "Volume negociado",
            data: volumes,
            backgroundColor: "#60a5fa",
          },
        ],
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
              label: (context) => {
                const valor = context.parsed.y;
                return `Volume: ${valor.toLocaleString("pt-BR")}`;
              },
            },
          },
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              callback: (val) => val.toLocaleString("pt-BR"),
            },
          },
        },
      },
    });
  } catch (e) {
    console.error("Erro ao carregar volume:", e);
  }
};

onMounted(carregarVolume);
watch(() => props.symbol, carregarVolume);
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
