export function objectValues<T extends object>(obj: T) {
  return Object.keys(obj).map((key) => obj[key as keyof T]);
}
