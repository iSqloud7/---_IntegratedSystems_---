using Microsoft.Playwright;
using NUnit.Framework;

namespace TestExamIS.Tests.Setup;

public class TestBase
{
    protected static IBrowser Browser { get; private set; }
    protected IPage Page { get; private set; }
    protected IBrowserContext Context { get; private set; }
    private IPlaywright _playwright;

    [OneTimeSetUp]
    public async Task SetUpBrowser()
    {
        var projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
        var browserPath = Path.Combine(
            projectDir,
            "Browser/chromium-1155/chrome-mac/Chromium.app/Contents/MacOS/Chromium"
        );

        
        _playwright = await Playwright.CreateAsync();
        var options = new BrowserTypeLaunchOptions
        {
            ExecutablePath = browserPath,
            Headless = true
        };

        Browser = await _playwright.Chromium.LaunchAsync(options);
    }

    [SetUp]
    public async Task SetUpContext()
    {
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task Cleanup()
    {
        await Context?.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalCleanup()
    {
        if (Browser != null)
            await Browser.DisposeAsync();
        if (_playwright != null)
            _playwright.Dispose();
    }
}