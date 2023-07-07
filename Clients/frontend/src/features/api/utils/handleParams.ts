// TO-DO: strongly type this any
export function handleParams(options: any, url: string) {
  const { Search, PageIndex } = options;
  const params: Record<string, string> = {};

  if (Search) {
    params.Search = Search;
  }

  if (PageIndex) {
    params.PageIndex = PageIndex;
  }

  return {
    url,
    params,
  };
}
