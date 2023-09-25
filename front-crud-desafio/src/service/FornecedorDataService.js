import axios from 'axios'

const FORNECEDOR_API_URL = 'http://localhost:9080/api/v1'

class FornecedorDataService {

    retrieveAllFornecedores() {
        return axios.get(`${FORNECEDOR_API_URL}/fornecedor`);
    }

    retrieveFornecedor(id) {
        return axios.get(`${FORNECEDOR_API_URL}/fornecedor/${id}`);
    }

    deleteFornecedor(id) {
        return axios.delete(`${FORNECEDOR_API_URL}/fornecedor/${id}`);
    }

    updateFornecedor(id, fornecedor) {
        return axios.put(`${FORNECEDOR_API_URL}/fornecedor/${id}`, fornecedor);
    }
    createFornecedor(fornecedor) {
        return axios.post(`${FORNECEDOR_API_URL}/fornecedor`, fornecedor);
    }

    /*findByName(nome) {
        return axios.get(`${FORNECEDOR_API_URL}/fornecedores?nome=${nome}`);
      }*/
  }
export default new FornecedorDataService()