import axios from "axios";

export const API = axios.create({
  baseURL: "http://localhost/api/v1",
  // baseURL: "/api/v1",
});
// export const BASE_URL = "/api/v1";