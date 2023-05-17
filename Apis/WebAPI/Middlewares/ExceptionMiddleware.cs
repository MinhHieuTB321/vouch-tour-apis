namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // push notification & writing log
                Console.WriteLine("exception occur");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
