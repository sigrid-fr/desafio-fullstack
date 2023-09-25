import { createWebHistory, createRouter } from "vue-router";
import EmpresaList from '@/components/EmpresaList.vue';
import EmpresaForm from '@/components/EmpresaForm.vue';
import FornecedorList from '@/components/FornecedorList.vue';
import FornecedorForm from '@/components/FornecedorForm.vue';
import MainPage from '@/components/MainPage.vue';

const routes = [
    { path: '/', component: MainPage },
    { path: '/empresa-list', component: EmpresaList },
    { path: '/empresa/:id', component: EmpresaForm },
    { path: '/empresa', component: EmpresaForm },

    { path: '/fornecedor-list', component: FornecedorList },
    { path: '/fornecedor/:id', component: FornecedorForm },
    { path: '/fornecedor', component: FornecedorForm },
  ];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;