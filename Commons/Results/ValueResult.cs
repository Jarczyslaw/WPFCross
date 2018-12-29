namespace Commons
{
    public class ValueResult<T> : Result
    {
        public T Value { get; set; }

        public override void Clear()
        {
            Value = default(T);
            base.Clear();
        }
    }
}
