using Microsoft.Playwright;

namespace TestExamIS.Tests.Pages;

public class EnrollmentFormPage
{
    private readonly IPage _page;
    private readonly ILocator _semesterSelect;
    private readonly ILocator _semesterOptions;
    private readonly ILocator _courseIdInput;
    private readonly ILocator _submitButton;

    public EnrollmentFormPage(IPage page)
    {
        _page = page;
        _semesterSelect = page.Locator("#semesters");
        _semesterOptions = page.Locator("#semesters option");
        _courseIdInput = page.Locator("#course-id");
        _submitButton = page.Locator("input[type='submit']");
    }

    public async Task WaitForFormLoadAsync()
    {
        await _page.WaitForSelectorAsync("form", new() { State = WaitForSelectorState.Visible });
    }

    public async Task<int> GetSemesterCountAsync()
    {
        return await _semesterOptions.CountAsync();
    }

    public ILocator GetSemesterOption(int index)
    {
        return _semesterOptions.Nth(index);
    }

    public ILocator SemesterOptions => _semesterOptions;

    public async Task<string> GetCourseIdAsync()
    {
        return await _courseIdInput.GetAttributeAsync("value");
    }

    public async Task SelectSemesterByValueAsync(string value)
    {
        await _semesterSelect.SelectOptionAsync(new[] { new SelectOptionValue { Value = value } });
    }

    public async Task<List<string>> GetSemesterValuesAsync()
    {
        var count = await GetSemesterCountAsync();
        var values = new List<string>();
        
        for (int i = 0; i < count; i++)
        {
            var value = await GetSemesterOption(i).GetAttributeAsync("value");
            values.Add(value);
        }
        
        return values;
    }

    public async Task SelectSemesterByPositionAsync(int position)
    {
        var values = await GetSemesterValuesAsync();
        if (position < 0 || position >= values.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(position), 
                $"Position must be between 0 and {values.Count - 1}");
        }
        
        await SelectSemesterByValueAsync(values[position]);
    }

    public async Task SubmitFormAsync()
    {
        await _submitButton.ClickAsync();
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}