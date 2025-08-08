using Microsoft.Playwright;

namespace TestExamIS.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public ILocator UsernameInput => _page.Locator("input[type='email']");
    public ILocator PasswordInput => _page.Locator("input[type='password']");
    public ILocator SubmitButton => _page.Locator("button[type='submit']");

    public async Task GotoAsync()
    {
        await _page.GotoAsync("http://localhost:5164/Identity/Account/Login");
    }

    public async Task LoginAsync(string username, string password)
    {
        await GotoAsync();
        await UsernameInput.FillAsync("teststudent@example.com");
        await PasswordInput.FillAsync("Test123!");
        await SubmitButton.ClickAsync();
        await _page.WaitForURLAsync("**/");
    }
}
