namespace Chrono
{
    public static class ObjectExtensions
    {
        public static FuncResult<TValue> AsFuncResult<TValue>(this TValue value)
        {
            return FuncResult.Successful(value);
        }
    }
}