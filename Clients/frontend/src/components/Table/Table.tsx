import { Table as MantineTable, ScrollArea } from '@mantine/core';

import { isPrimitive } from 'utils/isPrimitive';
import { objectValues } from 'utils/objectValues';

interface ITableItem {
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
  customRenders?: CustomRendersType<T>;
}

export const Table = <T extends ITableItem>({ data, headers, customRenders }: TableProps<T>) => {
  const renderRow = (item: T, i: number) => {
    return (
      <tr key={i}>
        {Object.keys(headers).map((key, j) => {
          const customRender = customRenders?.[key];

          if (customRender) {
            return <td key={j}>{customRender(item)}</td>;
          }

          return <td key={j}>{isPrimitive(item[key]) ? item[key] : ''}</td>;
        })}
      </tr>
    );
  };

  return (
    <ScrollArea>
      <MantineTable>
        <thead>
          <tr>
            {objectValues(headers).map((header, i) => (
              <th key={i}> {header} </th>
            ))}
          </tr>
        </thead>
        <tbody>{data.map(renderRow)}</tbody>
      </MantineTable>
    </ScrollArea>
  );
};
