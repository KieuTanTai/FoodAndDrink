export interface BatchItemResult<TEntity> {
    input: TEntity;
    isSuccess: boolean;
    errorMessage: string | null;
}