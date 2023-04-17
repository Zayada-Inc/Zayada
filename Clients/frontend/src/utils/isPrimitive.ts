export type PrimitiveType = string | number | boolean;

export function isPrimitive(value: unknown): value is PrimitiveType {
  return typeof value === 'string' || typeof value === 'number' || typeof value === 'boolean';
}
