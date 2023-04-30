import { useDebouncedValue } from '@mantine/hooks';
import { ContentLayout } from 'components/Layout';
import { Table } from 'components/Table';
import { AllUsersRow } from 'components/Table/Rows/AllUsersRow';
import { useGetAllUsersQuery } from 'features/api/apiSlice';
import { useSelector } from 'react-redux';
import { getAllUsers } from 'store/slices/search';

export const Dashboard = () => {
  const { query, activePage } = useSelector(getAllUsers);
  const [debouncedSearch] = useDebouncedValue(query, 400);
  const {
    data: res,
    isLoading,
    isFetching,
    error,
  } = useGetAllUsersQuery({
    Search: debouncedSearch,
    PageIndex: activePage,
  });

  const { count = 0, pageIndex = 0, pageSize = 0, data = [] } = res ? res : {};
  const pagination = {
    count,
    pageIndex,
    pageSize,
  };

  return (
    <ContentLayout>
      <div className='h-full'>
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
            {...{
              data,
              pagination,
              isFetching,
              error,
            }}
          />
        )}
      </div>
    </ContentLayout>
  );
};
