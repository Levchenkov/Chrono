namespace Chrono
{
    public static class FuncResult
    {
        public static FuncResult<TValue> Failed<TValue>()
        {
            return FuncResult<TValue>.Failed();
        }

        public static FuncResult<TValue> Successful<TValue>(TValue value)
        {
            return FuncResult<TValue>.Successful(value);
        }
    }
}