namespace Portal.Portal.Common
{
    public class Result<T>
    {
        public bool Ok { get; set; }

        public Result<T>.ErrorModel? Errors { get; }

        public T Value { get; }

        public Result()
        {
            Ok = true;
        }

        public Result(T value) : this()
        {
            Value = value;
        }

        public Result(params string[] errors) : this(new ErrorModel(errors))
        {
            Ok = false;
        }

        public Result(ErrorModel errors)
        {
            Errors = errors;
        }

        public class ErrorModel
        {
            public ErrorModel(string[] messages)
            {
                Messages = messages;
            }

            public string[] Messages { get; }
        }
    }
}

