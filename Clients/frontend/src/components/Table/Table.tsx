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

import { useTable } from 'components/Table/hooks/useTable';
import { isPrimitive } from 'utils/isPrimitive';
import { objectValues } from 'utils/objectValues';
import { IPaginatedResponse } from 'features/api/types';

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
  pagination,
  isFetching,
}: TableProps<T>) => {
  const {
    classes,
    cx,
    selection,
    toggleAll,
    toggleRow,
    activePage,
    searchQuery,
    handlePagination,
    handleSearch,
  } = useTable(data, isFetching);

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

  return (
    <div
      style={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'flex-start',
        maxWidth: '750px',
      }}
    >
      <TextInput
        placeholder='Search by username'
        onChange={handleSearch}
        value={searchQuery}
        icon={<Search size='1.5rem' />}
        maw={350}
      />
      <div
        style={{
          // border: '1px solid red',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <ScrollArea>
          <MantineTable w={750} horizontalSpacing='sm' highlightOnHover>
            <thead>
              <tr className={classes.header}>
                <th style={{ width: rem(40) }}>
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
            <tbody>
              {CustomRow
                ? data.map((item, i) => {
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
                  })
                : data.map(renderRow)}
            </tbody>
          </MantineTable>
        </ScrollArea>
        <Pagination
          total={Math.ceil(pagination.count / pagination.pageSize)}
          onChange={handlePagination}
          value={activePage}
          size='sm'
        />
      </div>
    </div>
  );
};
