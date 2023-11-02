using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TodoTests : PageTest
{
    private readonly string _testUrl = "https://localhost:7222/Todo";

    [Test]
    public async Task OneItem()
    {
        await Page.GotoAsync(_testUrl);
        await AddItem("A");
        await AssertItemCount(1);
        await AssertNthItemText("A", 1);
    }

    [Test]
    public async Task TwoItem()
    {
        await Page.GotoAsync(_testUrl);
        await AddItem("A");
        await AssertItemCount(1);
        await AssertNthItemText("A", 1);
        await AddItem("B");
        await AssertItemCount(2);
        await AssertNthItemText("B", 2);
    }

    private async Task AddItem(string text)
    {
        await Page.Locator("#todoItem").ClickAsync();
        await Page.Locator("#todoItem").FillAsync(text);
        await Page.GetByRole(AriaRole.Button, new() { Name = "新增" }).ClickAsync();
    }

    private async Task AssertItemCount(int count)
    {
        var ul      = await Page.QuerySelectorAsync(".todo-items"); // 替换 "selector" 为你的 ul 元素的选择器
        var lis     = await ul.QuerySelectorAllAsync("li");
        var liCount = lis.Count;
        Assert.AreEqual(count, liCount);
    }

    private async Task AssertNthItemText(string extectedText, int index)
    {
        var selector     = $"ul.todo-items > li:nth-child({index})";
        var expectedText = await Page.Locator(selector).InnerTextAsync();
        Assert.AreEqual(extectedText, expectedText);
    }
}