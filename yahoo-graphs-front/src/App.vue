<template>
  <div>
    <div class="topo">
      <button class="menu-toggle" @click="menuAtivo = true">☰</button>

      <transition name="fade-slide">
        <nav class="nav-bar" :class="{ oculto: menuAtivo }">
          <router-link to="/" class="nav-link">Home</router-link>
          <router-link to="/fiis" class="nav-link"
            >Fundos Imobiliários</router-link
          >
          <router-link to="/acoes" class="nav-link">Ações</router-link>
          <router-link to="/etfs" class="nav-link">ETFs</router-link>
        </nav>
      </transition>
    </div>

    <transition name="slide-menu">
      <div class="menu-lateral" v-if="menuAtivo">
        <div class="perfil">
          <img src="./assets/imag-linkedin.jpg" alt="Perfil" />
          <p class="nome">Rafael Teixeira Melo</p>
          <button class="fechar" @click="menuAtivo = false">✕</button>
        </div>

        <router-link to="/" class="menu-link" @click="fecharMenu"
          >Home</router-link
        >
        <router-link to="/fiis" class="menu-link" @click="fecharMenu"
          >Fundos Imobiliários</router-link
        >
        <router-link to="/acoes" class="menu-link" @click="fecharMenu"
          >Ações</router-link
        >
        <router-link to="/etfs" class="menu-link" @click="fecharMenu"
          >ETFs</router-link
        >
      </div>
    </transition>

    <router-view />
  </div>
</template>

<script setup>
import { ref } from "vue";
const menuAtivo = ref(false);

const fecharMenu = () => {
  menuAtivo.value = false;
};
</script>

<style>
body {
  background-color: #cfe9f9;
  margin: 0;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  background-color: #cfe9f9;
}

.nav-bar {
  display: flex;
  justify-content: center;
  flex: 1;
  gap: 0.5rem;
  height: 35px;
  box-shadow: 0 2px 6px rgba(209, 143, 143, 0.05);
  border-radius: 0 0 12px 12px;
  opacity: 1;
  visibility: visible;
  transition: opacity 0.3s ease-in-out, visibility 0.3s ease-in-out;
}

.nav-bar.oculto {
  opacity: 0;
  visibility: hidden;
  pointer-events: none;
}

.nav-link {
  text-decoration: none;
  font-weight: 600;
  color: #1e3a8a;
  padding: 0.5rem 1rem;
  margin: 0px 5px;
  border-radius: 0px 0px 8px 8px;
  background-color: #78cff7;
  transition: background 0.2s, color 0.2s;
}

.nav-link:hover {
  background-color: #0084c1;
  color: white;
}

.router-link-exact-active {
  background-color: #0ea5e9;
  color: white !important;
}

.menu-toggle {
  font-size: 1.5rem;
  background: none;
  border: none;
  cursor: pointer;
  color: #1e3a8a;
}

.menu-lateral {
  position: fixed;
  top: 0;
  left: 0;
  height: 100vh;
  width: 185px;
  background-color: #ffffff;
  padding: 0.5rem 1rem 1rem 0rem;
  box-shadow: 2px 0 10px rgba(0, 0, 0, 0.1);
  z-index: 999;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.menu-link {
  width: 90%;
  text-align: left;
  text-decoration: none;
  font-weight: 600;
  color: #1e3a8a;
  background-color: #78cff7;
  padding: 0.6rem 0.6rem 0.6rem 1.2rem;
  margin-bottom: 0.5rem;
  border-radius: 0px 15px 15px 0px;
  transition: background 0.2s;
}

.menu-link:hover {
  background-color: #0084c1;
  color: white;
}

.perfil {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 0.5rem;
  position: relative;
  width: 100%;
}

.perfil img {
  width: 64px;
  height: 64px;
  border-radius: 50%;
  object-fit: cover;
  margin-bottom: 0.5rem;
}

.perfil .nome {
  font-weight: bold;
  color: #1e3a8a;
  text-align: center;
}

.fechar {
  position: absolute;
  top: 0;
  right: 0;
  background: none;
  border: none;
  font-size: 1.5rem;
  color: #ef4444;
  cursor: pointer;
}

.topo {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0rem 1rem;
}

.slide-menu-enter-active,
.slide-menu-leave-active {
  transition: transform 0.3s ease, opacity 0.3s ease;
}
.slide-menu-enter-from,
.slide-menu-leave-to {
  transform: translateX(-100%);
  opacity: 0;
}

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 0.3s ease;
}
.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
}
</style>
