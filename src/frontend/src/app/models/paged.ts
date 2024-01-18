export class PagedModel<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  pageCount: number;
  isNext: boolean;
}
