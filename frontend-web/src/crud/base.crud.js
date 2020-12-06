import env from "../environments/environments";
import axios from "axios";

class BaseCrud {
  static serialize(data) {
    return `${Object.keys(data)
      .map((k) => encodeURIComponent(k) + "=" + encodeURIComponent(data[k]))
      .join("&")}`;
  }

  static trySerialize = (qs) => (qs ? BaseCrud.serialize(qs) : "");

  static async get(endpoint, spinner = true) {
    return axios.get(`${env.gestaoJogos.baseApi}/${endpoint}`, { spinner });
  }

  static async getById(endpoint, id, spinner = true) {
    return axios.get(`${env.gestaoJogos.baseApi}/${endpoint}(${id})`, {
      spinner,
    });
  }

  static async post(endpoint, data, spinner = true) {
    return axios.post(`${env.gestaoJogos.baseApi}/${endpoint}`, data, {
      spinner,
    });
  }

  static async put(endpoint, id, data, spinner = true) {
    console.log(data);
    return axios.put(`${env.gestaoJogos.baseApi}/${endpoint}/${id}`, data, {
      spinner,
    });
  }

  static async delete(endpoint, id, spinner = true) {
    return axios.delete(`${env.gestaoJogos.baseApi}/${endpoint}/${id}`, {
      spinner,
    });
  }
}

export default BaseCrud;
