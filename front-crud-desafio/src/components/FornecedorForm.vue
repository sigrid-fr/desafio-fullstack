<template>
  <div>
    <h3>Adicionar/Editar Fornecedor</h3>
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
          <label>Nome</label>
          <input type="text" class="form-control" v-model="nome" />
        </fieldset>
        <fieldset class="form-group">
          <label>CNPJ</label>
          <input type="text" class="form-control" v-model="cnpj" />
        </fieldset>
        <fieldset class="form-group">
          <label>CPF</label>
          <input type="text" class="form-control" v-model="cpf" />
        </fieldset>
        <fieldset class="form-group">
          <label>Email</label>
          <input type="text" class="form-control" v-model="email" @blur="validarEmail"/>
        </fieldset>
        <fieldset class="form-group">
          <label>CEP</label>
          <input type="text" class="form-control" v-model="cep" @blur="validarCep" />
        </fieldset>
        <fieldset class="form-group">
          <label>RG (Opcional para PJ)</label>
          <input type="text" class="form-control" v-model="rg" />
        </fieldset>
        <fieldset class="form-group">
          <label>Data de Nascimento (Opcional para PJ)</label>
          <input type="text" class="form-control" v-model="dataNascimento" />
        </fieldset>
        <button class="btn btn-success" type="submit">Salvar</button>
      </form>
    </div>
  </div>
</template>
<script>
import FornecedorDataService from "../service/FornecedorDataService";

export default {
  name: "Fornecedor",
  data() {
    return {
      nome: "",
      cnpj: "",
      cpf: null,
      email: "",
      cep: "",
      rg: null,
      dataNascimento: null,
      errors: [],
    };
  },
  computed: {
    id() {
      return this.$route.params.id;
    },
  },
  methods: {
    refreshFornecedorDetails() {
      FornecedorDataService.retrieveFornecedor(this.id).then((res) => {
        this.nome = res.data.nome;
        this.cnpj = res.data.cnpj;
        this.cpf = res.data.cpf;
        this.email = res.data.email;
        this.cep = res.data.cep;
        this.rg = res.data.rg;
        this.dataNascimento = res.data.dataNascimento;
      });
    },
    
    validateAndSubmit(e) {
      e.preventDefault();
      this.errors = [];
      if (!this.nome) {
        this.errors.push("Digite um valor válido para Nome");
      } else if (this.nome.length < 5) {
        this.errors.push("Digite ao menos 5 caracteres no Nome");
      }
      if (!this.cnpj) {
        this.errors.push("Digite um valor válido para CNPJ");
      } else if (this.cnpj.length < 14) {
        this.errors.push("Digite 14 caracteres em Cnpj");
      }
      if (!this.cep) {
        this.errors.push("Digite um valor válido para CEP");
      } else if (this.cep.length < 8) {
        this.errors.push("Digite 8 caracteres em CEP");
      }
      if (this.errors.length === 0) {
        if (this.id == -1 && this.id != undefined) {
          FornecedorDataService.createFornecedor({
            nome: this.nome,
            cnpj: this.cnpj,
            cpf: this.cpf,
            email: this.email,
            cep: this.cep,
            rg: this.rg,
            dataNascimento: this.dataNascimento,
          }).then(() => {
            this.$router.push("/fornecedor-list");
          }, err => this.errors.push(err.response.data.errors));
        } else {
          FornecedorDataService.updateFornecedor(this.id, {
            id: this.id,
            nome: this.nome,
            cnpj: this.cnpj,
            cpf: this.cpf,
            email: this.email,
            cep: this.cep,
            rg: this.rg,
            dataNascimento: this.dataNascimento,
          }).then(() => {
            this.$router.push("/fornecedor-list");
          }, err => this.errors.push(err.response.data.errors));
        }
      }
    },
  },
  created() {
    this.refreshFornecedorDetails();
  },

  async validarCep() {
      // Remova caracteres não numéricos do CEP
      const cep = this.cep.replace(/\D/g, '');

      if (cep.length !== 8) {
        this.erro = 'CEP inválido';
        this.endereco = null;
        return;
      }

      try {
        const response = await fetch(`http://viacep.com.br/ws/${cep}/json/`, {
          method: 'GET',
        });

        if (!response.ok) {
          this.erro = 'Erro ao buscar CEP';
          this.endereco = null;
          return;
        }

        const data = await response.json();

        if (data.erro) {
          this.erro = 'CEP não encontrado';
          this.endereco = null;
        } else {
          this.endereco = data;
          this.erro = null;
        }
      } catch (error) {
        console.error('Erro ao validar CEP:', error);
        this.erro = 'Erro ao buscar CEP';
        this.endereco = null;
      }
    },

    validarEmail() {
    if (/^\w+([\\.-]?\w+)*@\w+([\\.-]?\w+)*(\.\w{2,3})+$/.test(this.email)) {
        this.msg['email'] = 'Please enter a valid email address';
    } else {
        this.msg['email'] = '';
    }
}

};
</script>