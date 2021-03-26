namespace Final.Logic
{
    public class MyRef<T> where T : struct
    {
        public MyRef(T value)
        {
            _Val = value;
        }

        private T _Val;

        public T Val
        {
            get
            {
                return _Val;
            }

            set
            {
                _Val = value;
            }
        }
    }
}