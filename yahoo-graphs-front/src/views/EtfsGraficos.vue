<template>
  <div class="Etfs-container">
    <div v-if="loading" class="overlay-loading">
      <div class="spinner" />
      <p>Carregando gráficos, por favor aguarde...</p>
    </div>

    <div v-else class="Etfs-grafico">
      <StockChart symbol="^GSPC" title="Cotação do S&P500" />
      <TopEtfsBar :ativos="etfs" titulo="Top ETFs da Semana" />
      <VolumeBarras
        symbol="IVVB11.SA"
        titulo="Volume de IVVB11 nos últimos dias"
      />
      <ComparativoEtfs :ativos="etfs" titulo="Comparativo ETFs" />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import StockChart from "../components/StockChart.vue";
import VolumeBarras from "../components/VolumeBarras.vue";
import TopEtfsBar from "../components/TopAtivosBar.vue";
import ComparativoEtfs from "../components/ComparativoAtivos.vue";

const etfs = ["BOVA11.SA", "IVVB11.SA", "SMAL11.SA", "XFIX11.SA", "DIVO11.SA"];
const loading = ref(true);

onMounted(() => {
  setTimeout(() => {
    loading.value = false;
  }, 0);
});
</script>

<style scoped>
.Etfs-container {
  background-color: #cfe9f9;
  display: flex;
  justify-content: center;
  align-items: start;
  position: relative;
  width: 100%;
}

.Etfs-grafico {
  display: grid;
  width: 100%;
  height: 100%;
  padding: 1rem;
  background-color: #cfe9f9;
  z-index: 0;
  gap: 2rem;
  margin: 1rem auto;
  min-width: 1270px;
}

.Etfs-grafico :deep(canvas) {
  width: 100% !important;
  height: 320px !important;
}

@media (min-width: 768px) {
  .Etfs-grafico {
    grid-template-columns: 1fr 1fr;
  }

  .Etfs-grafico :deep(canvas) {
    max-height: 400px;
  }
}

@media (max-width: 767px) {
  .Etfs-grafico {
    grid-template-columns: 1fr;
  }
}

.overlay-loading {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 10;
  background: rgba(255, 255, 255, 0.95);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  font-size: 1.1rem;
  color: #334155;
  border-radius: 12px;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 5px solid #bae6fd;
  border-top: 5px solid #0284c7;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}
</style>
