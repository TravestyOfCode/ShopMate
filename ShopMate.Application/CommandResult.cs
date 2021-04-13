namespace ShopMate.Application
{
    public class CommandResult
    {
        public string StatusMessage { get; }

        public int StatusCode { get; }

        public bool IsSuccess => StatusCode >= 200 && StatusCode <= 299;

        private CommandResult() { }

        private CommandResult(int statusCode, string statusMessage = null)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }

        public static CommandResult Ok()
        {
            return new CommandResult(200);
        }

        public static CommandResult Ok(string message)
        {
            return new CommandResult(200, message);
        }

        public static CommandResult Created()
        {
            return new CommandResult(201);
        }

        public static CommandResult Created(string message)
        {
            return new CommandResult(201, message);
        }

        public static CommandResult BadRequest()
        {
            return new CommandResult(400, "Bad Request");
        }

        public static CommandResult BadRequest(string message)
        {
            return new CommandResult(400, message);
        }

        public static CommandResult NotFound()
        {
            return new CommandResult(404);
        }

        public static CommandResult NotFound(string message)
        {
            return new CommandResult(404, message);
        }

        public static CommandResult ServerError()
        {
            return new CommandResult(500);
        }

        public static CommandResult ServerError(string message)
        {
            return new CommandResult(500, message);
        }

        public static implicit operator bool(CommandResult result)
        {
            return result.IsSuccess;
        }
    }
}
