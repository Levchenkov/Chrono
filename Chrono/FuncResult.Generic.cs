namespace Chrono
{
    public class FuncResult<TValue>
    {
        public bool IsSuccessful
        {
            get;
            private set;
        }

        public TValue Value
        {
            get;
            private set;
        }

        public static FuncResult<TValue> Failed()
        {
            return new FuncResult<TValue>();
        }

        public static FuncResult<TValue> Successful(TValue value)
        {
            return new FuncResult<TValue>
            {
                IsSuccessful = true,
                Value = value
            };
        }  
    }
}
