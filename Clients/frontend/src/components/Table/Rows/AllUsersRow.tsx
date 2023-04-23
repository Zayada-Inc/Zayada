import { Avatar, Badge, createStyles } from '@mantine/core';
import { IGetAllUsersResponse } from 'features/api/types';

const useStyles = createStyles((theme) => ({
  userContainer: {
    display: 'flex',
    gap: '8px',
    alignItems: 'center',
  },

  userDetails: {
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-between',
  },

  userBadge: {
    color: theme.colors.secondaryColors[4],
  },

  username: {
    fontSize: '14px',
    fontWeight: 'bold',
  },

  email: {
    fontSize: '12px',
    color: theme.colors.gray[6],
  },

  avatar: {
    height: '40px',
  },
}));

export interface AllUsersRowProps {
  item: IGetAllUsersResponse['data'][number];
}

export const AllUsersRow = ({ item }: AllUsersRowProps) => {
  const { classes } = useStyles();

  return (
    <>
      <td>{item.id.slice(0, 5).concat(' ', '...')}</td>
      <td>
        <div className={classes.userContainer}>
          {item?.photos.length ? (
            <Avatar src={item.photos[0].url} alt='user avatar' radius='xl' />
          ) : (
            <Avatar radius='xl' />
          )}
          <div className={classes.userDetails}>
            <p className={classes.username}>{item.username}</p>
            <p className={classes.email}>{item.email}</p>
          </div>
        </div>
      </td>
      <td>
        {item.personalTrainer ? (
          <Badge color='orange'>Coach</Badge>
        ) : (
          <Badge classNames={{ root: classes.userBadge }}>User</Badge>
        )}
      </td>
    </>
  );
};
