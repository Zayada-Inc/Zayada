export interface IAPIError {
  data: {
    message: string;
    statusCode: number;
  };
  status: number;
}
