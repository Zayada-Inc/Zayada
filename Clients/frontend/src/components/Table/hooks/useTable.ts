import { useEffect, useState } from 'react';
import { createStyles, rem } from '@mantine/core';

import { ITableItem } from 'components/Table';
import { useDispatch, useSelector } from 'react-redux';
import { getAllUsers, queryPage, setAllUsersPage, setAllUsersSearch } from 'store/slices/search';
import { RootState } from 'store/store';

const useStyles = createStyles((theme) => ({
  tableWrapper: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-start',
  },

  tablePaginationWrapper: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    marginTop: rem(8),
  },

  checkboxHeader: {
    width: rem(40),
  },

  headers: {
    backgroundColor: theme.colors.gray[2],
  },

  selectedRow: {
    backgroundColor: theme.colors.gray[0],
  },

  errorMessage: {
    color: theme.colors.red[5],
    fontSize: theme.fontSizes.md,
    marginTop: '1.5rem',
  },
}));

export const useTable = <T extends ITableItem>(
  data: T[],
  isFetching: boolean,
  getQueryData: (state: RootState) => queryPage,
) => {
  const [selection, setSelection] = useState<string[]>([]);
  const [hasQueryChanged, setHasQueryChanged] = useState<boolean>(false);
  const { query: searchQuery, activePage } = useSelector(getQueryData);
  const dispatch = useDispatch();

  const { classes, cx } = useStyles();

  const toggleRow = (id: string) => {
    setSelection((current) =>
      current.includes(id) ? current.filter((item) => item !== id) : [...current, id],
    );
  };

  const toggleAll = () => {
    data &&
      setSelection((current) =>
        current.length === data.length ? [] : data.map((item) => item.id),
      );
  };

  // corner cases covering paginated search
  useEffect(() => {
    if (searchQuery && isFetching && activePage > 1 && hasQueryChanged) {
      dispatch(setAllUsersPage(1));
      // setActivePage(1);
      setHasQueryChanged(false);
    }
  }, [isFetching]);

  useEffect(() => {
    if (searchQuery && activePage > 1) {
      setHasQueryChanged(true);
    }
  }, [searchQuery]);

  return {
    classes,
    cx,
    selection,
    setSelection,
    toggleAll,
    toggleRow,
    searchQuery,
    activePage,
  };
};
