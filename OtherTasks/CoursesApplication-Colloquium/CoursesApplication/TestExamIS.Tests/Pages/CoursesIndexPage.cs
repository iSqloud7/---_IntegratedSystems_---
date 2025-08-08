using Microsoft.Playwright;

namespace TestExamIS.Tests.Pages;

public class CoursesPage
{
    private readonly IPage _page;
    private readonly ILocator _enrollButtons;
    
    public ILocator CoursesTable => _page.Locator("table.table");
    public ILocator CoursesRows => _page.Locator("table.table tbody tr");
    

    public async Task<int> GetFlightRowCountAsync()
    {
        return await CoursesRows.CountAsync();
    }

    public CoursesPage(IPage page)
    {
        _page = page;
        _enrollButtons = page.Locator("a.enroll-btn");
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("http://localhost:5164/Courses");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task<int> GetEnrollButtonCountAsync()
    {
        return await _enrollButtons.CountAsync();
    }

    public async Task<List<string>> GetEnrollButtonHrefsAsync()
    {
        var count = await GetEnrollButtonCountAsync();
        var hrefs = new List<string>();
        
        for (int i = 0; i < count; i++)
        {
            var href = await _enrollButtons.Nth(i).GetAttributeAsync("href");
            hrefs.Add(href);
        }
        
        return hrefs;
    }

    public async Task ClickFirstEnrollButtonAsync()
    {
        await _enrollButtons.First.ClickAsync();
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}