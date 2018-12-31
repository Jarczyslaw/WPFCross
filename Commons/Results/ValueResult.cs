namespace Commons
{
    public class ValueResult<T> : Result
    {
        public T Value { get; set; }

        public ValueResult()
        {
        }

        public ValueResult(T value)
        {
            Value = value;
        }

        public ValueResult(Result result)
            : base(result)
        {
        }

        public override void Clear()
        {
            Value = default(T);
            base.Clear();
        }
    }
}
