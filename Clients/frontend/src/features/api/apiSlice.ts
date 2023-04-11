import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { IAuthenticationResponse, ILoginRequest, IRegisterRequest } from './types';

// TO-DO: change baseQuery to env variable
export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5001/api',
    prepareHeaders: (headers) => {
      if (localStorage.getItem('token')) {
        headers.set('authorization', `Bearer ${localStorage.getItem('token')}`);
      }

      return headers;
    },
  }),
  endpoints: (builder) => ({
    getPersonalTrainers: builder.query<any, void>({
      query: () => '/PersonalTrainer',
    }),
    register: builder.mutation<IAuthenticationResponse, IRegisterRequest>({
      query: (newUser) => ({
        url: `/Account/register`,
        method: 'POST',
        body: newUser,
      }),
    }),
    login: builder.mutation<IAuthenticationResponse, ILoginRequest>({
      query: (credentials) => ({
        url: '/Account/login',
        method: 'POST',
        body: credentials,
      }),
    }),
  }),
});

export const { useGetPersonalTrainersQuery, useRegisterMutation, useLoginMutation } = apiSlice;
