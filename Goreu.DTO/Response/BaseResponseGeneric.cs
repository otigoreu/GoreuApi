namespace Goreu.Dto.Response
{
    public class BaseResponseGeneric<T> : BaseResponse
    {
        public T? Data { get; set; }

        public static BaseResponseGeneric<T> Ok(T data)
        {
            return new BaseResponseGeneric<T>
            {
                Success = true,
                Data = data
            };
        }

        public static BaseResponseGeneric<T> Fail(string errorMessage)
        {
            return new BaseResponseGeneric<T>
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
