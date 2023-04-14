const DEV_URL = 'http://localhost:5001/api';
const PROD_URL = 'https://zayadaapi.azurewebsites.net/api';

export const BASE_URL = import.meta.env.PROD ? PROD_URL : DEV_URL;
