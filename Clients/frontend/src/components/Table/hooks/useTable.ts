import { useEffect, useState } from 'react';
import { createStyles } from '@mantine/core';

import { ITableItem } from 'components/Table';
import { useDebouncedValue } from '@mantine/hooks';
import { useDispatch, useSelector } from 'react-redux';
import { getAllUsers, setAllUsersPage, setAllUsersSearch } from 'store/slices/search';

const useStyles = createStyles((theme) => ({
  header: {
    backgroundColor: theme.colors.gray[2],
  },

  selectedRow: {
    backgroundColor: theme.colors.gray[0],
  },
}));

export const useTable = <T extends ITableItem>(data: T[], isFetching: boolean) => {
  const [selection, setSelection] = useState<string[]>([]);
  const [hasQueryChanged, setHasQueryChanged] = useState<boolean>(false);
  const { query: searchQuery, activePage } = useSelector(getAllUsers);
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

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(setAllUsersSearch(e.target.value));
  };

  const handlePagination = (e: number) => {
    dispatch(setAllUsersPage(e));
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
    handleSearch,
    activePage,
    handlePagination,
  };
};
