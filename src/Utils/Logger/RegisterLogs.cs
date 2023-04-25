namespace Utils.Logger
{
    public class RegisterLogs
    {
        public async static Task CreateAsync(string message, string model)
        {
            string path = @$"C:\logs\{model}-{DateTime.Now.ToString("dd-MM-yy")}.log";

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                await streamWriter.WriteLineAsync();
            }
        }
    }
}
