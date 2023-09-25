<template>
  <div class="container">
    <h3>Lista de Empresas</h3>
    <div v-if="message" class="alert alert-success">{{ message }}</div>
    <div class="container">
      <table class="table">
        <thead>
          <tr>
            <th>Nome Fantasia</th>
            <th>CNPJ</th>
            <th>Estado</th>
            <th>CEP</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="empresa in empresas" :key="empresa.id">
            <td>{{ empresa.nomeFantasia }}</td>
            <td>{{ empresa.cnpj }}</td>
            <td>{{ empresa.estado }}</td>
            <td>{{ empresa.cep }}</td>
            <td>
              <button class="btn btn-warning" @click="updateEmpresa(empresa.id)">
                Atualizar
              </button>
              <button class="btn btn-danger" @click="deleteEmpresa(empresa.id)">
                Deletar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="row">
        <button class="btn btn-success" @click="addEmpresa">Adicionar</button>
      </div>
    </div>
  </div>
</template>

<script>
import EmpresaDataService from "../service/EmpresaDataService";

export default {
  name: "Empresas",
  data() {
    return {
      empresas: [],
      message: "",
    };
  },
  methods: {
    refreshEmpresas() {
      EmpresaDataService.retrieveAllEmpresas()
        .then((res) => {
          this.empresas = res.data;
        })
        .catch((error) => {
          console.error("Error fetching empresas:", error);
        });
    },
    addEmpresa() {
      // Check if this.$router is defined
      if (this.$router) {
        this.$router.push({ path: `/empresa/-1` });
      } else {
        console.error("this.$router is undefined");
      }
    },
    updateEmpresa(id) {
      // Check if this.$router is defined
      if (this.$router) {
        this.$router.push({ path: `/empresa/${id}` });
      } else {
        console.error("this.$router is undefined");
      }
    },
    deleteEmpresa(id) {
      EmpresaDataService.deleteEmpresa(id)
        .then(() => {
          this.refreshEmpresas();
        })
        .catch((error) => {
          console.error("Error deleting empresa:", error);
        });
    },
  },
  created() {
    this.refreshEmpresas();
  },
};
</script>