<template>
  <div id="app">
    <nav class="navbar navbar-expand navbar-dark bg-dark">
      <router-link to="/" class="navbar-brand">CRUD DESAFIO</router-link>
      <div class="navbar-nav mr-auto">
        <li class="nav-item">
          <router-link to="/empresa/-1" class="nav-link">Adicionar Empresa</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/empresa-list" class="nav-link">Lista de Empresas</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/fornecedor/-1" class="nav-link">Adicionar Fornecedor</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/fornecedor-list" class="nav-link">Lista de Fornecedores</router-link>
        </li>
      </div>
    </nav>

    <div class="container mt-3">
      <router-view />
    </div>
  </div>
</template>

<script>
export default {
  name: 'MainPage',
  props: {
    msg: String
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
