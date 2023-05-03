import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import {
  IAuthenticationResponse,
  IGetAllUsersResponse,
  IGetGymResponse,
  IGetPersonalTrainerResponse,
  ILoginRequest,
  IPostGym,
  IPostPT,
  IRegisterRequest,
} from 'features/api/types/index';

import { BASE_URL } from 'features/api/constants/constants';
import { handleParams } from './utils/handleParams';

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
  tagTypes: ['Users', 'Gym', 'PT'],
  endpoints: (builder) => ({
    getPersonalTrainers: builder.query<IGetPersonalTrainerResponse, any>({
      query: (options) => handleParams(options, '/PersonalTrainer'),
      providesTags: ['PT'],
    }),
    getGym: builder.query<IGetGymResponse, any>({
      query: (options) => handleParams(options, '/Gym'),
      providesTags: ['Gym'],
    }),
    getAllUsers: builder.query<IGetAllUsersResponse, any>({
      query: (options) => handleParams(options, '/Account/GetAllUsers'),
      providesTags: ['Users'],
    }),
    addPersonalTrainer: builder.mutation<IPostPT, IPostPT>({
      query: (newPT) => ({
        url: '/PersonalTrainer',
        method: 'POST',
        body: newPT,
      }),
      invalidatesTags: ['PT'],
    }),
    addGym: builder.mutation<IPostGym, IPostGym>({
      query: (newGym) => ({
        url: '/Gym',
        method: 'POST',
        body: newGym,
      }),
      invalidatesTags: ['Gym'],
    }),
    register: builder.mutation<IAuthenticationResponse, IRegisterRequest>({
      query: (newUser) => ({
        url: `/Account/register`,
        method: 'POST',
        body: newUser,
      }),
      invalidatesTags: ['Users'],
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
  useGetGymQuery,
  useRegisterMutation,
  useLoginMutation,
  useAddGymMutation,
  useAddPersonalTrainerMutation,
} = apiSlice;
