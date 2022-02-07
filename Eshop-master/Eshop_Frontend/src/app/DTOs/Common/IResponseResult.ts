export interface IResponseResult<TEntity> {
    status: string;
    data: TEntity;
}