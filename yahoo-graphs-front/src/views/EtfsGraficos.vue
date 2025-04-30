<template>
  <div class="acoes-container">
    <div class="acoes-grafico">
      <StockChart symbol="^GSPC" title="Cotação do S&P500" />
      <TopAcoesBar :ativos="etfs" titulo="Top ETFs da Semana" />
      <VolumeBarras
        symbol="IVVB11.SA"
        titulo="Volume de PETR4 nos últimos dias"
      />
      <ComparativoAcoes :ativos="etfs" titulo="Comparativo ETFs" />
    </div>
  </div>
</template>

<script setup>
import StockChart from "../components/StockChart.vue";
import VolumeBarras from "../components/VolumeBarras.vue";
import TopAcoesBar from "../components/TopAtivosBar.vue";
import ComparativoAcoes from "../components/ComparativoAtivos.vue";

const etfs = ["BOVA11.SA", "IVVB11.SA", "SMAL11.SA"];
</script>

<style scoped>
.acoes-container {
  background-color: #f8fafc;
  min-height: 100vh;
  display: flex;
  justify-content: center;
  padding: 2rem;
}

.acoes-grafico {
  background-color: lightcyan;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 100%;
  height: 100%;
  max-width: 900px;
}
</style>
