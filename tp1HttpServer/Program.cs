using System.Net;
using System.Web;
using tp1HttpServer;

using var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8001/");
listener.Start();
Console.WriteLine("Listening on port 8001...");
var type = typeof(MyMethods);
var c = new MyMethods();

while (true)
{
    
    var ctx = listener.GetContext();
    using var response = ctx.Response;
    var request = ctx.Request;
    // get URI
    var methodName = request.Url?.Segments[1];

    if (methodName == null || methodName == "/" || request.Url == null)
    {
        response.StatusCode = (int) HttpStatusCode.NotFound;
        response.StatusDescription = "Provide a method name";
        response.ContentType = "text/plain";
        continue;
    }

    // get method
    var method = type.GetMethod(methodName);
    if (method == null)
    {
        response.StatusCode = (int) HttpStatusCode.NotFound;
        response.StatusDescription = $"Method {methodName} not found";
        response.ContentType = "text/plain";
        continue;
        
    }
    
    // get params
    var param1 = HttpUtility.ParseQueryString(request.Url.Query).Get("param1");
    var param2 = HttpUtility.ParseQueryString(request.Url.Query).Get("param2");
    if (param1 == null || param2 == null)
    {
        response.StatusCode = (int) HttpStatusCode.BadRequest;
        response.StatusDescription = "Provide two params in the URL";
        response.ContentType = "text/plain";
        continue;
    }
    
    
    var result =  method.Invoke(c, new object[] {param1, param2});
    // send response
    response.StatusCode = (int) HttpStatusCode.OK;
    response.ContentType = "text/html";
    if (methodName== "JsonResponse")
    {
        response.ContentType = "application/json";
    }
    var buffer = System.Text.Encoding.UTF8.GetBytes((string) result!);
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
}