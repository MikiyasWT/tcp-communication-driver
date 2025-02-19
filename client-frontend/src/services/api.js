import axios from "axios";

const API_BASE_URL = "http://localhost:5282/api/tcp"; // Adjust this to match your Web API URL

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export const getTemperature = async () => {
  try {
    const response = await api.get("/get-temp");
    return response.data;
  } catch (error) {
    console.error("Error fetching temperature:", error);
    throw error;
  }
};

export const getStatus = async () => {
  try {
    const response = await api.get("/get-status");
    return response.data;
  } catch (error) {
    console.error("Error fetching status:", error);
    throw error;
  }
};
