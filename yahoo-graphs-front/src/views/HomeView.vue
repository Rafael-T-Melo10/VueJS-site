<template>
  <div class="home">
    <HomeComponent msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
import HomeComponent from "@/components/HomeComponent.vue";

export default {
  name: "HomeView",
  components: {
    HomeComponent,
  },
};
</script>
