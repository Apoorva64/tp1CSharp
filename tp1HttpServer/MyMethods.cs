using System.Diagnostics;

namespace tp1HttpServer;

public class MyMethods
{
    public String HelloFrench(String param1, String param2)
    {
        return "<html><body> Bonjour " + param1 + " et " + param2 + "</body></html>";
    }
    
    public String HelloEnglish(String param1, String param2)
    {
        return "<html><body> Hello " + param1 + " and " + param2 + "</body></html>";
    }
    
    public String HelloSpanish(String param1, String param2)
    {
        return "<html><body> Hola " + param1 + " y " + param2 + "</body></html>";
    }

    public String ExternalProgram(String param1, String param2)
    {

        var start = new ProcessStartInfo
        {
            FileName = @"C:\Users\appad\Desktop\Midleware\tp1HttpServer\tp1ExternalApplication\bin\Debug\net7.0\tp1ExternalApplication.exe", // Specify exe name.
            Arguments = param1 + " " + param2, // Specify arguments.
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        using var process = Process.Start(start);
        using var reader = process?.StandardOutput;
        return reader?.ReadToEnd() ?? "Error in the external program";

    }
    public String JsonResponse(String param1, String param2)
    {
        return "{\"param1\":\"" + param1 + "\",\"param2\":\"" + param2 + "\"}";
    }
}