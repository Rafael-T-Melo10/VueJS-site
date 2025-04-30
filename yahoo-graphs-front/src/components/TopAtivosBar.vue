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
  titulo: { type: String, default: "Top ativos por variação" },
});

const canvas = ref(null);
let chart = null;

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
  const variacoes = [];

  for (let i = 0; i < props.ativos.length; i++) {
    const simbolo = props.ativos[i];
    try {
      const data = await getStockHistory(simbolo);
      const valores = data.chart?.result?.[0]?.indicators.quote[0].close;
      if (!valores || valores.length < 2) continue;

      const precoInicial = valores[0];
      const precoFinal = valores[valores.length - 1];
      const variacao = ((precoFinal - precoInicial) / precoInicial) * 100;

      variacoes.push({ simbolo, valor: Number(variacao.toFixed(2)) });
    } catch (e) {
      console.error("Erro ao buscar", simbolo);
    }
  }

  variacoes.sort((a, b) => b.valor - a.valor);
  const labels = variacoes.map((v) => v.simbolo);
  const valores = variacoes.map((v) => v.valor);

  if (chart) chart.destroy();

  chart = new Chart(canvas.value, {
    type: "bar",
    data: {
      labels,
      datasets: [
        {
          label: "Variação (%) últimos dias",
          data: valores,
          backgroundColor: cores.slice(0, labels.length),
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

onMounted(carregarVariações);
</script>

<style scoped>
.grafico-box {
  max-width: 600px;
  margin: 2rem auto;
  background: white;
  padding: 1rem;
  border-radius: 1rem;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}
</style>
