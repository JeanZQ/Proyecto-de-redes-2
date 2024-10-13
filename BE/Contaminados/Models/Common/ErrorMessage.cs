namespace Contaminados.Models.Common
{
    public class ErrorMessages
    {
        public int Status { get; set; }
        public required string Message { get; set; }
        public static readonly Dictionary<int, ErrorMessages> Errors = new Dictionary<int, ErrorMessages>
        {
            { 401, new ErrorMessages { Status = 401, Message = "Invalid credentials" } }, //Unauthorized()
            { 403, new ErrorMessages { Status = 403, Message = "Not part of the game" } }, //StatusCode(403)
            { 404, new ErrorMessages { Status = 404, Message = "The specified resource was not found" } }, //NotFound()
            { 409, new ErrorMessages { Status = 409, Message = "Asset already exists" } }, //Conflict()
            { 428, new ErrorMessages { Status = 428, Message = "This action is not allowed at this time" } }, //StatusCode(428)
            { 100, new ErrorMessages { Status = 100, Message = "this error does not exist in the dictionary" } } //StatusCode(100)
        };

        public static ErrorMessages GetErrorMessage(int statusCode)
        {
            if (Errors.ContainsKey(statusCode))
            {
                return Errors[statusCode];
            }
            return Errors[100];
        }
    }
}