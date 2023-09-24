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
        var alphaItem = WorkItem(Priority.None, DateTime.Now.AddDays(1), "Alpha");
        var betaItem = WorkItem(Priority.Urgent, DateTime.Now, "Beta");

        var planner = CreatePlanner(alphaItem, betaItem);

        var result = planner.CreatePlan();

        Assert.Equal(new WorkItem[] { betaItem, alphaItem }, result);
    }

    [Fact]
    public void AreCompletedAbsent()
    {
        var nonCompleted = WorkItem(Priority.None, DateTime.Now, "Alpha");
        var completed = WorkItem(Priority.High, DateTime.Now, "Beta", isCompleted: true);

        var planner = CreatePlanner(nonCompleted, completed);

        var result = planner.CreatePlan();

        Assert.Equal(new WorkItem[] { nonCompleted }, result);
    }

    private WorkItem WorkItem(Priority priority, DateTime dueDate, string title, bool isCompleted = false)
    {
        var workItem = new WorkItem();
        workItem.Priority = priority;
        workItem.DueDate = dueDate;
        workItem.Title = title;
        workItem.IsCompleted = isCompleted;
        return workItem;
    }

    private SimpleTaskPlanner CreatePlanner(params WorkItem[] items)
    {
        var mockRepostitory = new Mock<IWorkItemsRepository>();
        mockRepostitory.Setup(r => r.GetAll()).Returns(items);
        return new SimpleTaskPlanner(mockRepostitory.Object);
    }
}
