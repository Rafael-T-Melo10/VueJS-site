export const getStockHistory = async (symbol) => {
  const response = await fetch(`http://localhost:5000/api/yahoo/${symbol}`);
  return await response.json();
};
