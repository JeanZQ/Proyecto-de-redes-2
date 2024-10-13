namespace Contaminados.Models.Common
{
    public class CustomException : Exception
    {
        public int Status { get; set;}

        public CustomException(int status, string message) : base(message)
        {
            Status = status;
        }
    }
    public class ClientException : CustomException
    {
        public ClientException() : base(400, "Client Error") { }
    }

    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException() : base(401, "Invalid credentials") { }
    }

    public class ForbiddenException : CustomException
    {
        public ForbiddenException() : base(403, "Not part of the game") { }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException() : base(404, "The specified resource was not found") { }
    }

    public class ConflictException : CustomException
    {
        public ConflictException() : base(409, "Asset already exists") { }
    }

    public class PreconditionRequiredException : CustomException
    {
        public PreconditionRequiredException() : base(428, "This action is not allowed at this time") { }
    }
}
