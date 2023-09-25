<template>
  <div>
    <h3>Adicionar/Editar Empresa</h3>
    <div class="container">
      <form @submit="validateAndSubmit">
        <div v-if="errors.length">
          <div
            class="alert alert-danger"
            v-bind:key="index"
            v-for="(error, index) in errors"
          >
            {{ error }}
          </div>
        </div>
        <fieldset class="form-group">
          <label>Nome Fantasia</label>
          <input type="text" class="form-control" v-model="nomeFantasia" />
        </fieldset>
        <fieldset class="form-group">
          <label>CNPJ</label>
          <input type="text" class="form-control" v-model="cnpj" v-mask="'##.###.###/####-##'" />
        </fieldset>
        <fieldset class="form-group">
          <label>Estado</label>
          <input type="text" class="form-control" v-model="estado" />
        </fieldset>
        <fieldset class="form-group">
          <label>CEP</label>
          <input type="text" class="form-control" v-model="cep" v-mask="'#####-###'" @blur="validarCep" />
        </fieldset>
        <button class="btn btn-success" type="submit">Salvar</button>
      </form>
    </div>
  </div>
</template>
<script>
import EmpresaDataService from "../service/EmpresaDataService";
import VueMask from 'v-mask'

export default {
  directives: {
    mask: VueMask.directive,
  },
  name: "Empresa",
  data() {
    return {
      nomeFantasia: "",
      cnpj: "",
      estado: "",
      cep: "",
      errors: [],
    };
  },
  computed: {
    id() {
      return this.$route.params.id;
    },
  },
  methods: {
    refreshEmpresaDetails() {
      EmpresaDataService.retrieveEmpresa(this.id).then((res) => {
        this.nomeFantasia = res.data.nomeFantasia;
        this.cnpj = res.data.cnpj;
        this.estado = res.data.estado;
        this.cep = res.data.cep;
      });
    },
    validateAndSubmit(e) {
      e.preventDefault();
      this.errors = [];
      if (!this.nomeFantasia) {
        this.errors.push("Digite um valor válido para Nome Fantasia");
      } else if (this.nomeFantasia.length < 5) {
        this.errors.push("Digite ao menos 5 caracteres no Nome Fantasia");
      }
      if (!this.cnpj) {
        this.errors.push("Digite um valor válido para CNPJ");
      } else if (this.cnpj.length < 14) {
        this.errors.push("Digite 14 caracteres em Cnpj");
      }
      if (!this.estado) {
        this.errors.push("Digite um valor válido para Estado/UF");
      } else if (this.cep.length < 2) {
        this.errors.push("Digite 2 caracteres em Estado/UF");
      }
      if (this.errors.length === 0) {
        if (this.id == -1 && this.id != undefined) {
          EmpresaDataService.createEmpresa({
            nomeFantasia: this.nomeFantasia,
            cnpj: this.cnpj,
            estado: this.estado,
            cep: this.cep,
          }).then(() => {
            this.$router.push("/empresa-list");
          }, err => this.errors.push(err.response.data.errors));
        } else {
          EmpresaDataService.updateEmpresa(this.id, {
            id: this.id,
            nomeFantasia: this.nomeFantasia,
            cnpj: this.cnpj,
            estado: this.estado,
            cep: this.cep,
          }).then(() => {
            this.$router.push("/empresa-list");
          }, err => this.errors.push(err.response.data.errors));
        }
      }
    },
  },

  created() {
    this.refreshEmpresaDetails();
  },
};

</script>