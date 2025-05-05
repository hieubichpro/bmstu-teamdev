import axios from "axios";

export const API = axios.create({
  baseURL: process.env.VITE_API_URL || "http://localhost:8081",
});
