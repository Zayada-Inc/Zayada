import React from 'react';
import {
  Checkbox,
  Table as MantineTable,
  Pagination,
  ScrollArea,
  TextInput,
  rem,
} from '@mantine/core';
import { Search } from 'tabler-icons-react';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';

import { useTable } from 'components/Table/hooks/useTable';
import { isPrimitive } from 'utils/isPrimitive';
import { objectValues } from 'utils/objectValues';
import { IPaginatedResponse } from 'features/api/types';
import { isFetchBaseQueryError } from 'utils/isFetchBaseQueryError';
import { RootState } from 'store/store';
import { queryPage } from 'store/slices/search';

export interface ITableItem {
  id: string;
  [key: string]: any;
}

type TableHeadersType<T extends ITableItem> = Partial<Record<keyof T, string>>;
type CustomRendersType<T extends ITableItem> = Partial<
  Record<keyof T, (item: T) => React.ReactNode>
>;

interface TableProps<T extends ITableItem> {
  data: T[];
  headers: TableHeadersType<T>;
  pagination: Omit<IPaginatedResponse<T>, 'data'>;
  isFetching: boolean;
  error: FetchBaseQueryError | SerializedError | undefined;
  handleSearch: (e: React.ChangeEvent<HTMLInputElement>) => void;
  handlePagination: (e: number) => void;
  getQueryData: (state: RootState) => queryPage;
  onlySelectedHeaders?: boolean;
  customRenders?: CustomRendersType<T>;
  CustomRow?: React.FC<{ item: T }>;
}

export const Table = <T extends ITableItem>({
  data,
  headers,
  customRenders,
  CustomRow,
  onlySelectedHeaders = false,
  handleSearch,
  handlePagination,
  pagination,
  isFetching,
  error,
  getQueryData,
}: TableProps<T>) => {
  const { classes, cx, selection, toggleAll, toggleRow, activePage, searchQuery } = useTable(
    data,
    isFetching,
    getQueryData,
  );

  const renderRow = (item: T, i: number) => {
    const selected = selection.includes(item.id);

    return (
      <tr key={i} className={cx({ [classes.selectedRow]: selected })}>
        <td>
          <Checkbox
            checked={selection.includes(item.id)}
            onChange={() => toggleRow(item.id)}
            transitionDuration={0}
            color='orange'
          />
        </td>

        {Object.keys(onlySelectedHeaders ? headers : item).map((key, j) => {
          const customRender = customRenders?.[key];
          if (customRender) {
            return <td key={j}>{customRender(item)}</td>;
          }

          const shortenedValues =
            item[key] && item[key].length > 30
              ? item[key].slice(0, 5).concat(' ', '...')
              : item[key];

          return (
            <React.Fragment key={j}>
              {isPrimitive(item[key]) ? <td>{shortenedValues}</td> : ''}
            </React.Fragment>
          );
        })}
      </tr>
    );
  };

  const renderTableBody = () => {
    if (CustomRow && !error) {
      return data.map((item, i) => {
        const selected = selection.includes(item.id);

        return (
          <tr key={i} className={cx({ [classes.selectedRow]: selected })}>
            <td>
              <Checkbox
                checked={selection.includes(item.id)}
                onChange={() => toggleRow(item.id)}
                transitionDuration={500}
                color='orange'
              />
            </td>
            <CustomRow item={item} />
          </tr>
        );
      });
    }

    return !error && data.map(renderRow);
  };

  // TO-DO: make this a separate component
  const renderError = () => {
    if (isFetchBaseQueryError(error)) {
      const errorData = error.data as { message: string };
      const errorMessage = errorData.message || 'Unknown error occured';

      return <div className={classes.errorMessage}>{errorMessage}</div>;
    }
  };

  return (
    <div className={classes.tableWrapper}>
      <TextInput
        placeholder='Search by username'
        onChange={handleSearch}
        value={searchQuery}
        icon={<Search size='1.5rem' />}
        maw={350}
      />
      <div className={classes.tablePaginationWrapper}>
        <ScrollArea>
          <MantineTable w={500} horizontalSpacing='sm' highlightOnHover mb={rem(8)}>
            <thead>
              <tr className={classes.headers}>
                <th className={classes.checkboxHeader}>
                  <Checkbox
                    onChange={toggleAll}
                    checked={selection.length === data.length}
                    indeterminate={selection.length > 0 && selection.length !== data.length}
                    transitionDuration={0}
                    color='orange'
                  />
                </th>
                {objectValues(headers).map((header, i) => (
                  <th key={i} style={{ fontSize: rem(14) }}>
                    {header}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>{renderTableBody()}</tbody>
          </MantineTable>
        </ScrollArea>
        {!error && (
          <Pagination
            total={Math.ceil(pagination.count / pagination.pageSize)}
            onChange={handlePagination}
            value={activePage}
            size='sm'
          />
        )}
        {renderError()}
      </div>
    </div>
  );
};
