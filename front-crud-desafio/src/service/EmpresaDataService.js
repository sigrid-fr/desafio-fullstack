import axios from 'axios'

const EMPRESA_API_URL = 'http://localhost:9080/api/v1'

class EmpresaDataService {

    retrieveAllEmpresas() {
        return axios.get(`${EMPRESA_API_URL}/empresa`);
    }

    retrieveEmpresa(id) {
        return axios.get(`${EMPRESA_API_URL}/empresa/${id}`);
    }

    deleteEmpresa(id) {
        return axios.delete(`${EMPRESA_API_URL}/empresa/${id}`);
    }

    updateEmpresa(id, empresa) {
        return axios.put(`${EMPRESA_API_URL}/empresa/${id}`, empresa);
    }
    createEmpresa(empresa) {
        return axios.post(`${EMPRESA_API_URL}/empresa`, empresa);
    }
  }
export default new EmpresaDataService()