import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';

export function isFetchBaseQueryError(error: any): error is FetchBaseQueryError {
  return error && typeof error.status === 'number';
}
