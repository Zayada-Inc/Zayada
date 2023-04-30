import { useDebouncedValue } from '@mantine/hooks';
import { ContentLayout } from 'components/Layout';
import { Table } from 'components/Table';
import { AllUsersRow } from 'components/Table/Rows/AllUsersRow';
import {
  useGetAllUsersQuery,
  useGetGymQuery,
  useGetPersonalTrainersQuery,
} from 'features/api/apiSlice';
import { useDispatch, useSelector } from 'react-redux';
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

export const Dashboard = () => {
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

  return (
    <ContentLayout>
      <div>
        {isLoading ? (
          <p>loading...</p>
        ) : (
          <Table
            headers={{
              id: 'ID',
              username: 'User',
              email: 'Role',
            }}
            CustomRow={AllUsersRow}
            data={usersData}
            pagination={usersPagination}
            getQueryData={getAllUsers}
            {...{
              isFetching,
              handleSearch,
              handlePagination,
              error,
            }}
          />
        )}
      </div>
      <div>
        <Table
          headers={{
            id: 'ID',
            gymName: 'Gym Name',
            gymAddress: 'Gym Address',
          }}
          data={gymData}
          isFetching={gymIsFetching}
          error={gymError}
          pagination={gymPagination}
          handlePagination={handleGymPagination}
          handleSearch={handleGymSearch}
          getQueryData={getAllGym}
        />
      </div>
      <div>
        <Table
          headers={{
            id: 'ID',
            certifications: 'Certifications',
            gymName: 'Gym Name',
            username: 'Username',
            email: 'Email',
          }}
          data={ptData}
          pagination={ptPagination}
          isFetching={ptIsFetching}
          error={ptError}
          getQueryData={getAllPt}
          handleSearch={handlePtSearch}
          handlePagination={handlePtPagination}
        />
      </div>
    </ContentLayout>
  );
};
