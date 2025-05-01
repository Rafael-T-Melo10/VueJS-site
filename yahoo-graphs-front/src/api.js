export const getStockHistory = async (
  symbol,
  interval = "1mo",
  range = "6mo"
) => {
  const url = `http://localhost:5000/api/yahoo/${symbol}?interval=${interval}&range=${range}`;

  try {
    const response = await fetch(url);
    if (!response.ok) throw new Error(`Erro ao buscar dados de ${symbol}`);
    return await response.json();
  } catch (e) {
    console.error("Erro ao buscar dados:", e);
    throw e;
  }
};
