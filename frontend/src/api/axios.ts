import axios from "axios";

// Configura la instancia de Axios con la URL base de tu backend
const clienteAxios = axios.create({
  baseURL: "https://localhost:7040/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default clienteAxios;