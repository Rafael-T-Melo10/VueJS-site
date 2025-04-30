<template>
  <div class="acoes-container">
    <div class="acoes-grafico">
      <StockChart symbol="XPML11.SA" title="Cotação do XPML11" />
      <TopAcoesBar :ativos="fiis" titulo="Top FIIs da Semana" />
      <VolumeBarras
        symbol="HGLG11.SA"
        titulo="Volume do HGLG11 nos últimos dias"
      />
      <ComparativoAcoes :ativos="fiis" titulo="Comparativo FIIs" />
    </div>
  </div>
</template>

<script setup>
import StockChart from "../components/StockChart.vue";
import VolumeBarras from "../components/VolumeBarras.vue";
import TopAcoesBar from "../components/TopAtivosBar.vue";
import ComparativoAcoes from "../components/ComparativoAtivos.vue";

const fiis = ["HGLG11.SA", "KNRI11.SA", "XPML11.SA"];
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
