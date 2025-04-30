import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/acoes",
    name: "acoes",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/AcoesGraficos.vue"),
  },
  {
    path: "/fiis",
    name: "fiis",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/FiisGraficos.vue"),
  },
  {
    path: "/etfs",
    name: "etfs",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/EtfsGraficos.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
