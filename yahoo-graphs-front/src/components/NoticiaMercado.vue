<template>
  <div class="noticias-box">
    <h2>Notícias Recentes</h2>
    <div v-if="loading" class="loading">Carregando notícias...</div>

    <div class="carrossel" v-else>
      <button @click="voltar" class="navegar">&#8592;</button>

      <transition name="fade" mode="out-in">
        <div class="noticia" :key="noticiaAtual?.uuid" v-if="noticiaAtual">
          <img
            v-if="noticiaAtual.main_image?.resolutions?.[0]?.url"
            :src="noticiaAtual.main_image.resolutions[0].url"
            class="imagem"
            alt="Imagem da notícia"
          />
          <div class="conteudo">
            <h3>{{ noticiaAtual.title }}</h3>
            <p class="fonte">{{ noticiaAtual.publisher }}</p>
            <p class="descricao">
              {{ noticiaAtual.summary || "Sem resumo disponível." }}
            </p>
            <a :href="noticiaAtual.link" target="_blank">Leia mais</a>
          </div>
        </div>
      </transition>

      <button @click="avancar" class="navegar">&#8594;</button>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { getNoticias } from "@/api";

const noticias = ref([]);
const loading = ref(true);
const index = ref(0);

const noticiaAtual = computed(() => noticias.value[index.value]);

const avancar = () => {
  if (index.value < noticias.value.length - 1) index.value++;
};
const voltar = () => {
  if (index.value > 0) index.value--;
};

onMounted(async () => {
  try {
    const { items } = await getNoticias();
    noticias.value = items.result;
  } catch (e) {
    console.error("Erro ao carregar notícias:", e);
  } finally {
    loading.value = false;
  }
});
</script>

<style scoped>
.noticias-box {
  max-width: 1100px;
  height: 300px;
  margin: 1rem;
  background-color: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.08);
  text-align: center;
}

h2 {
  font-size: 1.5rem;
  margin-bottom: 1.5rem;
  color: #0f172a;
}

.carrossel {
  display: flex;
  margin-bottom: 2rem;
  align-items: center;
  justify-content: center;
  gap: 1rem;
}

.navegar {
  background-color: transparent;
  border: none;
  font-size: 2rem;
  cursor: pointer;
  color: #1e3a8a;
}

.noticia {
  display: flex;
  align-items: flex-start;
  gap: 1rem;
  padding: 1rem;
  border-radius: 10px;
  border: 1px solid #e2e8f0;
  max-width: 700px;
}

.imagem {
  width: 140px;
  height: 140px;
  object-fit: cover;
  border-radius: 8px;
  flex-shrink: 0;
}

.conteudo {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.conteudo h3 {
  font-size: 1.1rem;
  color: #1e3a8a;
  margin: 0 0 0.5rem;
}

.fonte {
  font-size: 0.75rem;
  color: #64748b;
  margin-bottom: 0.3rem;
}

.descricao {
  font-size: 0.9rem;
  color: #334155;
  margin-bottom: 0.5rem;

  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
}

.conteudo a {
  font-size: 0.85rem;
  color: #2563eb;
  text-decoration: underline;
  margin-top: auto;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.4s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
