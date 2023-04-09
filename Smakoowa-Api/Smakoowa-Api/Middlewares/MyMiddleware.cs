namespace Smakoowa_Api.Middlewares
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Perform some async operation here
            await Wait();

            await next(context);
        }

        private static async Task Wait()
        {
            await Task.Delay(10000);
        }
    }

}
