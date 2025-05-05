import axios from "axios";
const API_BACK_URL = "http://localhost:5000/api/yahoo";

export const getStockHistory = async (
  symbol,
  interval = "1mo",
  range = "6mo"
) => {
  const url = `${API_BACK_URL}/${symbol}?interval=${interval}&range=${range}`;

  try {
    const response = await fetch(url);
    if (!response.ok) throw new Error(`Erro ao buscar dados de ${symbol}`);
    return await response.json();
  } catch (e) {
    console.error("Erro ao buscar dados:", e);
    throw e;
  }
};

export const getMarketIndices = async () => {
  try {
    const response = await axios.get(`${API_BACK_URL}/indices`);
    return response.data;
  } catch (error) {
    console.error("Erro ao buscar índices do mercado:", error);
    throw error;
  }
};

export const getNoticias = async () => {
  try {
    const response = await axios.get(`${API_BACK_URL}/noticias`);
    return response.data;
  } catch (error) {
    console.error("Erro ao buscar notícias:", error);
    throw error;
  }
};
