import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

// TO-DO: change baseQuery to env variable
export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5000/api',
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
    register: builder.mutation<any, any>({
      query: (newUser) => ({
        url: `/Account/register`,
        method: 'POST',
        body: newUser,
      }),
    }),
  }),
});

export const { useGetPersonalTrainersQuery, useRegisterMutation } = apiSlice;
