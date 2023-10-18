var client = new HttpClient();

while (true)
{
    Console.WriteLine("Enter method name:");
    var methodName = Console.ReadLine();
    Console.WriteLine("Enter param1:");
    var param1 = Console.ReadLine();
    Console.WriteLine("Enter param2:");
    var param2 = Console.ReadLine();

    var url = $"http://localhost:8001/{methodName}?param1={param1}&param2={param2}";
    var response = await client.GetAsync(url);
    var responseString = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseString);
}