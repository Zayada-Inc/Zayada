import { ContentLayout } from 'components/Layout';
import { Table } from 'components/Table';
import { AllUsersRow } from 'components/Table/Rows/AllUsersRow';
import { useGetAllUsersQuery } from 'features/api/apiSlice';

export const Dashboard = () => {
  const { data: res, isLoading } = useGetAllUsersQuery();

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
            data={res ? res.data : []}
            CustomRow={AllUsersRow}
            customRenders={{
              personalTrainer: (it) => (
                <>
                  {it?.personalTrainer?.certifications ? it?.personalTrainer?.certifications : '-'}
                </>
              ),
            }}
          />
        )}
      </div>
    </ContentLayout>
  );
};
