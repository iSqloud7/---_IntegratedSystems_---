using System.Text.RegularExpressions;

namespace TestExamIS.Tests.Utils;

public static class HttpResponseExtensions
{
    public static async Task<string> GetAntiForgeryTokenAsync(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        
        var match = Regex.Match(content, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        
        throw new Exception("Antiforgery token not found");
    }
}