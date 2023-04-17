import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import {
  IAuthenticationResponse,
  IGetAllUsersResponse,
  ILoginRequest,
  IRegisterRequest,
} from 'features/api/types/index';

import { BASE_URL } from 'features/api/constants/constants';

// TO-DO: change baseQuery to env variable
export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: BASE_URL,
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
    getAllUsers: builder.query<IGetAllUsersResponse[], void>({
      query: () => '/Account/getAllUsers',
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

export const {
  useGetPersonalTrainersQuery,
  useGetAllUsersQuery,
  useRegisterMutation,
  useLoginMutation,
} = apiSlice;