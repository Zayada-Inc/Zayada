import { Tuple, DefaultMantineColor } from '@mantine/core';

export interface IAPIError {
  data: {
    message: string;
    statusCode: number;
  };
  status: number;
}

type ExtendedCustomColors = 'primaryColorName' | 'secondaryColors' | DefaultMantineColor;

declare module '@mantine/core' {
  export interface MantineThemeColorsOverride {
    colors: Record<ExtendedCustomColors, Tuple<string, 10>>;
  }
}
