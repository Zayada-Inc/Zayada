import { ContentLayout } from 'components/Layout';
import { Table } from 'components/Table';
import { useGetAllUsersQuery } from 'features/api/apiSlice';

export const Dashboard = () => {
  const { data = [], isLoading } = useGetAllUsersQuery();

  return (
    <ContentLayout>
      <div className='h-full'>
        {isLoading ? (
          <p>loading...</p>
        ) : (
          <Table
            headers={{
              id: 'Id',
              email: 'Email',
              personalTrainer: 'Certifications',
            }}
            data={data}
            customRenders={{
              personalTrainer: (it) => <p>{it?.personalTrainer?.certifications}</p>,
            }}
          />
        )}
      </div>
    </ContentLayout>
  );
};
