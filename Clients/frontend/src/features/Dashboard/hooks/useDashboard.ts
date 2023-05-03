import { useDispatch, useSelector } from 'react-redux';
import { useDebouncedValue } from '@mantine/hooks';

import {
  useGetAllUsersQuery,
  useGetGymQuery,
  useGetPersonalTrainersQuery,
} from 'features/api/apiSlice';
import { setModal } from 'store/slices/modal';
import {
  getAllGym,
  getAllPt,
  getAllUsers,
  setAllUsersPage,
  setAllUsersSearch,
  setGymPage,
  setGymSearch,
  setPtPage,
  setPtSearch,
} from 'store/slices/search';

export const useDashboard = () => {
  const { query: usersQuery, activePage: usersActivePage } = useSelector(getAllUsers);
  const { query: gymQuery, activePage: gymActivePage } = useSelector(getAllGym);
  const { query: ptQuery, activePage: ptActivePage } = useSelector(getAllPt);
  const [usersDebouncedSearch] = useDebouncedValue(usersQuery, 400);
  const [gymDebouncedSearch] = useDebouncedValue(gymQuery, 400);
  const [ptDebouncedSearch] = useDebouncedValue(ptQuery, 400);
  const dispatch = useDispatch();
  const {
    data: usersRes,
    isLoading,
    isFetching,
    error,
  } = useGetAllUsersQuery({
    Search: usersDebouncedSearch,
    PageIndex: usersActivePage,
  });

  const {
    data: gymRes,
    isFetching: gymIsFetching,
    error: gymError,
  } = useGetGymQuery({
    Search: gymDebouncedSearch,
    PageIndex: gymActivePage,
  });

  const {
    data: ptRes,
    isFetching: ptIsFetching,
    error: ptError,
  } = useGetPersonalTrainersQuery({
    Search: ptDebouncedSearch,
    PageIndex: ptActivePage,
  });

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(setAllUsersSearch(e.target.value));
  };

  const handlePagination = (e: number) => {
    dispatch(setAllUsersPage(e));
  };
  const handleGymSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(setGymSearch(e.target.value));
  };

  const handleGymPagination = (e: number) => {
    dispatch(setGymPage(e));
  };

  const handlePtSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(setPtSearch(e.target.value));
  };

  const handlePtPagination = (e: number) => {
    dispatch(setPtPage(e));
  };

  const {
    count: usersCount = 0,
    pageIndex: usersPageIndex = 0,
    pageSize: usersPageSize = 0,
    data: usersData = [],
  } = usersRes ? usersRes : {};

  const {
    count: gymCount = 0,
    pageIndex: gymPageIndex = 0,
    pageSize: gymPageSize = 0,
    data: gymData = [],
  } = gymRes ? gymRes : {};

  const {
    count: ptCount = 0,
    pageIndex: ptPageIndex = 0,
    pageSize: ptPageSize = 0,
    data: ptData = [],
  } = ptRes ? ptRes : {};

  const ptPagination = {
    count: ptCount,
    pageIndex: ptPageIndex,
    pageSize: ptPageSize,
  };

  const usersPagination = {
    count: usersCount,
    pageIndex: usersPageIndex,
    pageSize: usersPageSize,
  };

  const gymPagination = {
    count: gymCount,
    pageIndex: gymPageIndex,
    pageSize: gymPageSize,
  };

  const handleAddUser = () => {
    dispatch(setModal({ type: 'addUser', isOpen: true }));
  };

  const handleAddGym = () => {
    dispatch(setModal({ type: 'addGym', isOpen: true }));
  };

  const handleAddPT = () => {
    dispatch(setModal({ type: 'addPT', isOpen: true }));
  };

  return {
    handleAddUser,
    handleAddGym,
    handleAddPT,
    isLoading,
    usersData,
    usersPagination,
    isFetching,
    handleSearch,
    handlePagination,
    error,
    gymData,
    gymIsFetching,
    gymError,
    gymPagination,
    handleGymPagination,
    handleGymSearch,
    ptData,
    ptPagination,
    ptIsFetching,
    ptError,
    handlePtSearch,
    handlePtPagination,
  };
};
