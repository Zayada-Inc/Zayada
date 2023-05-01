import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import {
  IAuthenticationResponse,
  IGetAllUsersResponse,
  IGetGymResponse,
  IGetPersonalTrainerResponse,
  ILoginRequest,
  IRegisterRequest,
} from 'features/api/types/index';

import { BASE_URL } from 'features/api/constants/constants';

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
  tagTypes: ['Users'],
  endpoints: (builder) => ({
    getPersonalTrainers: builder.query<IGetPersonalTrainerResponse, any>({
      query: (options) => {
        const { Search, PageIndex } = options;
        const params: Record<string, string> = {};

        if (Search) {
          params.Search = Search;
        }

        if (PageIndex) {
          params.PageIndex = PageIndex;
        }

        return {
          url: '/PersonalTrainer',
          params,
        };
      },
    }),
    getGym: builder.query<IGetGymResponse, any>({
      query: (options) => {
        const { Search, PageIndex } = options;
        const params: Record<string, string> = {};

        if (Search) {
          params.Search = Search;
        }

        if (PageIndex) {
          params.PageIndex = PageIndex;
        }

        return {
          url: '/Gym',
          params,
        };
      },
    }),
    getAllUsers: builder.query<IGetAllUsersResponse, any>({
      query: (options) => {
        const { Search, PageIndex } = options;
        const params: Record<string, string> = {};

        if (Search) {
          params.Search = Search;
        }

        if (PageIndex) {
          params.PageIndex = PageIndex;
        }

        return {
          url: '/Account/getAllUsers',
          params,
        };
      },
      providesTags: ['Users'],
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
} = apiSlice;
