<template>
  <div class="container">
    <h3>Lista de Fornecedores</h3>
    <div v-if="message" class="alert alert-success">{{ message }}</div>
    <!-- <div class="list row">
    <div class="col-md-12">
      <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Busque por nome"
          v-model="nome"/>
        <div class="input-group-append">
            <button class="btn btn-outline-secondary" type="button"
            @click="searchName"
          >
            Buscar
          </button> 
        </div>
      </div> 
    </div> 
  </div> --> 
    <div class="container">
      <table class="table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>CNPJ</th>
            <th>CPF</th>
            <th>Email</th>
            <th>CEP</th>
            <th>RG</th>
            <th>Dt Nascimento</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="fornecedor in fornecedores" :key="fornecedor.id">
            <td>{{ fornecedor.nome }}</td>
            <td>{{ fornecedor.cnpj }}</td>
            <td>{{ fornecedor.cpf }}</td>
            <td>{{ fornecedor.email }}</td>
            <td>{{ fornecedor.cep }}</td>
            <td>{{ fornecedor.rg }}</td>
            <td>{{ fornecedor.dataNascimento }}</td>
            <td>
              <button class="btn btn-warning" @click="updateFornecedor(fornecedor.id)">
                Atualizar
              </button>
              <button class="btn btn-danger" @click="deleteFornecedor(fornecedor.id)">
                Deletar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="row">
        <button class="btn btn-success" @click="addFornecedor">Adicionar</button>
      </div>
    </div>
  </div>
</template>

<script>
import FornecedorDataService from "../service/FornecedorDataService";

export default {
  name: "Fornecedores",
  data() {
    return {
      fornecedores: [],
      message: "",
    };
  },
  methods: {
    refreshFornecedores() {
      FornecedorDataService.retrieveAllFornecedores()
        .then((res) => {
          this.fornecedores = res.data;
        })
        .catch((error) => {
          console.error("Error fetching fornecedores:", error);
        });
    },
    addFornecedor() {
      // Check if this.$router is defined
      if (this.$router) {
        this.$router.push({ path: `/fornecedor/-1` });
      } else {
        console.error("this.$router is undefined");
      }
    },
    updateFornecedor(id) {
      // Check if this.$router is defined
      if (this.$router) {
        this.$router.push({ path: `/fornecedor/${id}` });
      } else {
        console.error("this.$router is undefined");
      }
    },
    deleteFornecedor(id) {
      FornecedorDataService.deleteFornecedor(id)
        .then(() => {
          this.refreshFornecedores();
        })
        .catch((error) => {
          console.error("Error deleting fornecedor:", error);
        });
    },
  },
  created() {
    this.refreshFornecedores();
  },

  /*searchName() {
      FornecedorDataService.findByName(this.nome)
        .then(response => {
          this.nome = response.data;
          //this.setActiveNome(null);
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
    },
  mounted() {
    //this.retrieveAllFornecedores();
  }*/
};

</script>