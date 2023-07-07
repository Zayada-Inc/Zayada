import { createStyles } from '@mantine/core';
import { UserPlus, CirclePlus } from 'tabler-icons-react';

import { Button } from 'components/Button';
import { ContentLayout } from 'components/Layout';
import { Modal } from 'components/Modal';
import { Table } from 'components/Table';
import { AllUsersRow } from 'components/Table/Rows/AllUsersRow';
import { getAllGym, getAllPt, getAllUsers } from 'store/slices/search';
import { useDashboard } from '../hooks/useDashboard';

const useStyles = createStyles(() => ({
  relativeContainer: {
    position: 'relative',
  },

  btnWrapper: {
    position: 'absolute',
    right: 0,
  },

  tablesWrapper: {
    display: 'flex',
    justifyContent: 'space-evenly',
    marginTop: '30px',
  },
}));

export const Dashboard = () => {
  const { classes } = useStyles();
  const {
    handleAddUser,
    handleAddGym,
    handleAddPT,
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
  } = useDashboard();

  return (
    <ContentLayout>
      <div className={classes.tablesWrapper}>
        <div className={classes.relativeContainer}>
          <div className={classes.btnWrapper}>
            <Button text='Add user' Icon={UserPlus} onClick={handleAddUser} />
          </div>
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
        </div>
        <div className={classes.relativeContainer}>
          <div className={classes.btnWrapper}>
            <Button text='Add gym' Icon={CirclePlus} onClick={handleAddGym} />
          </div>
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
        <div className={classes.relativeContainer}>
          <div className={classes.btnWrapper}>
            <Button text='Add Trainer' Icon={UserPlus} onClick={handleAddPT} />
          </div>
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
      </div>
      <Modal />
    </ContentLayout>
  );
};
