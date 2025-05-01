<template>
  <div>
    <nav class="nav-bar">
      <router-link to="/" class="nav-link">Home</router-link>
      <router-link to="/fiis" class="nav-link">Fundos Imobiliários</router-link>
      <router-link to="/acoes" class="nav-link">Ações</router-link>
      <router-link to="/etfs" class="nav-link">ETFs</router-link>
    </nav>
    <router-view />
  </div>
</template>

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
  color: #2c3e50;
  background-color: #cfe9f9;
}

.nav-bar {
  display: flex;
  justify-content: center;
  background-color: #ffffff;
  padding: 0.5rem;
  border-bottom: 2px solid #93c5fd;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
  border-radius: 0 0 12px 12px;
}

.nav-link {
  text-decoration: none;
  font-weight: 600;
  color: #1e3a8a;
  padding: 0.5rem 1rem;
  margin: 0px 5px;
  border-radius: 8px;
  transition: background 0.2s, color 0.2s;
}

.nav-link:hover {
  background-color: #0ea5e9;
  color: white;
  border-color: #0284c7;
}

.router-link-exact-active {
  background-color: #0ea5e9;
  color: white !important;
}
</style>
