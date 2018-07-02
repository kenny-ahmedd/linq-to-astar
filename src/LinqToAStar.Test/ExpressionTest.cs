using System.Drawing;
using System.Linq;
using Xunit;

namespace LinqToAStar.Test
{
    using Core;

    public class ExpressionTest
    {
        private readonly Point start = new Point(2, 2);
        private readonly Point goal = new Point(18, 18);
        private readonly Rectangle boundary = new Rectangle(0, 0, 20, 20);
        private const int unit = 1;

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void LetTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var solution = from step in queryable.Contains(boundary.GetAvailablePoints(unit))
                           let m = step.GetManhattanDistance(goal)
                           let e = step.GetChebyshevDistance(goal)
                           orderby m, e
                           select step;
            var actual = solution.ToArray();

            Assert.Equal(actual.First(), queryable.From);
            Assert.Equal(actual.Last(), queryable.To);
        }

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void SelectManyTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var obstacles = new[] { new Point(5, 5), new Point(6, 6), new Point(7, 7), new Point(8, 8), new Point(9, 9) };
            var solution = from step in queryable
                           from obstacle in obstacles
                           where step != obstacle
                           orderby step.GetManhattanDistance(goal)
                           select step;
            var actual = solution.ToArray();

            Assert.Empty(actual.Intersect(obstacles));
            Assert.Equal(actual.First(), queryable.From);
            Assert.Equal(actual.Last(), queryable.To);
        }

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void ContainsTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var solution = from step in queryable.Contains(boundary.GetAvailablePoints(unit))
                           orderby step.GetManhattanDistance(goal)
                           select step;
            var actual = solution.ToArray();

            Assert.Equal(actual.First(), queryable.From);
            Assert.Equal(actual.Last(), queryable.To);
        }

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void ReverseFromInsideTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var solution = from step in queryable.Reverse()
                           where boundary.Contains(step)
                           orderby step.GetManhattanDistance(goal)
                           select step;
            var actual = solution.ToArray();

            Assert.Equal(actual.First(), queryable.To);
            Assert.Equal(actual.Last(), queryable.From);
        }

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void ReverseFromOutsideTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var solution = from step in queryable
                           where boundary.Contains(step)
                           orderby step.GetManhattanDistance(goal)
                           select step;
            var actual = solution.Reverse().ToArray();

            Assert.Equal(actual.First(), queryable.To);
            Assert.Equal(actual.Last(), queryable.From);
        }

        [Theory]
        [InlineData(nameof(AStar))]
        [InlineData(nameof(BestFirstSearch))]
        [InlineData(nameof(IterativeDeepeningAStar))]
        [InlineData(nameof(RecursiveBestFirstSearch))]
        public void OrderByThenByComparerTest(string algorithmName)
        {
            var queryable = HeuristicSearch.Use(algorithmName, start, goal, (step, lv) => step.GetFourDirections(unit));
            var solution = from step in queryable
                           where boundary.Contains(step)
                           orderby step.GetManhattanDistance(goal), step.GetEuclideanDistance(goal)
                           select step;
            var actual = solution.ToArray();

            Assert.IsType<CombinedComparer<Point, Point>>(solution.NodeComparer);
            Assert.Equal(actual.First(), queryable.From);
            Assert.Equal(actual.Last(), queryable.To);
        }
    }
}