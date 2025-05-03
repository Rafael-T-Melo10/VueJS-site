<template>
  <div class="home-container">
    <MarketMovers />
    <!-- <AtivosEmAltaBaixa />
    <NoticiaMercado /> -->
  </div>
</template>

<script setup>
import MarketMovers from "../components/GetMarketMovers.vue";
</script>

<style scoped>
.home-container {
  max-width: 1000px;
  margin: 1rem auto;
  display: flex;
  flex-direction: column;
  gap: 2rem;
}
</style>
