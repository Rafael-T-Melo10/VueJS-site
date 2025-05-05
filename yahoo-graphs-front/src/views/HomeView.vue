<template>
  <div class="home-container">
    <NoticiaMercado :noticias="noticiasDoMercado" />
    <MarketMovers />
  </div>
</template>

<script setup>
import MarketMovers from "../components/GetMarketMovers.vue";
import NoticiaMercado from "../components/NoticiaMercado.vue";
</script>

<style scoped>
.home-container {
  max-width: 1000px;
  margin: 1rem auto;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}
</style>
