import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

// TO-DO: change baseQuery to env variable
export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5000/api' }),
  // tagTypes: ['PT'],
  endpoints: (builder) => ({
    getPersonalTrainers: builder.query<any, void>({
      query: () => '/PersonalTrainer',
      //   providesTags: ['PT'],
      // transformResponse: (res: any) => res.data,
    }),
  }),
});

export const { useGetPersonalTrainersQuery } = apiSlice;
