using DataAccess.Abstractions;
using Domain.Models;
using Domain.Models.Enums;
using Moq;

namespace Domain.Logic.Tests;

public class SimpleTaskPlannerTests
{
    [Fact]
    public void TestEmptyRepository()
    {
        var planner = CreatePlanner();
        var result = planner.CreatePlan();

        Assert.Empty(result);
    }

    [Fact]
    public void IsProperlySorted()
    {
        var alphaItem = WorkItem(DateTime.Now.AddDays(1), Priority.None, "Alpha");
        var betaItem = WorkItem(DateTime.Now, Priority.Urgent, "Beta");

        var planner = CreatePlanner(alphaItem, betaItem);

        var result = planner.CreatePlan();

        Assert.Equal(new WorkItem[] { betaItem, alphaItem }, result);
    }

    [Fact]
    public void AreCompletedAbsent()
    {
        var nonCompleted = WorkItem(DateTime.Now, Priority.None, "Alpha");
        var completed = WorkItem(DateTime.Now, Priority.High, "Beta", isCompleted: true);

        var planner = CreatePlanner(nonCompleted, completed);

        var result = planner.CreatePlan();

        Assert.Equal(new WorkItem[] { nonCompleted }, result);
    }

    private WorkItem WorkItem(DateTime dueDate, Priority priority, string title, bool isCompleted = false) => new WorkItem(
        id: null,
        creationDate: DateTime.Now,
        dueDate,
        Complexity.None,
        priority,
        title,
        "",
        isCompleted
    );

    private SimpleTaskPlanner CreatePlanner(params WorkItem[] items)
    {
        var mockRepostitory = new Mock<IWorkItemsRepository>();
        mockRepostitory.Setup(r => r.GetAll()).Returns(items);
        return new SimpleTaskPlanner(mockRepostitory.Object);
    }
}
