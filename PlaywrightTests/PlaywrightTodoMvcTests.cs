using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightTodoMvcTests : PageTest
{
    private string _testUrl = "https://demo.playwright.dev/todomvc/";

    [Test]
    public async Task OneItem()
    {
        await Page.GotoAsync(_testUrl);
        await AddItem("A");
        await AssertItemCount("1");
        await AssertNthItemText("A", "1");
    }

    [Test]
    public async Task TwoItem()
    {
        await Page.GotoAsync(_testUrl);
        await AddItem("A");
        await AssertItemCount("1");
        await AssertNthItemText("A", "1");
        await AddItem("B");
        await AssertItemCount("2");
        await AssertNthItemText("B", "2");
    }

    private async Task AddItem(string text)
    {
        await Page.GetByPlaceholder("What needs to be done?").ClickAsync();
        await Page.GetByPlaceholder("What needs to be done?").FillAsync(text);
        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
    }

    private async Task AssertItemCount(string count)
    {
        var unit = count == "1" ? "item" : "items";

        var itemCount = await Page.Locator(".todo-count").InnerTextAsync();
        Assert.AreEqual($"{count} {unit} left", itemCount);
    }

    private async Task AssertNthItemText(string extectedText, string index)
    {
        var bodySectionDivSectionUlLiNthChildDivLabel = $"ul.todo-list > li:nth-child({index}) > div > label";
        var expectedText                              = await Page.Locator(bodySectionDivSectionUlLiNthChildDivLabel).InnerTextAsync();
        Assert.AreEqual(extectedText, expectedText);
    }
}