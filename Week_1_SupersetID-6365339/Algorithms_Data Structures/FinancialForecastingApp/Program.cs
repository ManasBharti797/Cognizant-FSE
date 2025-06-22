using System;

class FinancialForecasting
{
   
    // Recursive Approach
    public static double CalculateFutureValueRecursive(double initialValue, double growthRate, int periods)
    {
        if (periods <= 0)
            return initialValue;

        double nextValue = initialValue * (1 + growthRate);
        return CalculateFutureValueRecursive(nextValue, growthRate, periods - 1);
    }

    // Iteration Approach
    public static double CalculateFutureValueIterative(double initialValue, double growthRate, int periods)
    {
        return initialValue * Math.Pow(1 + growthRate, periods);
    }

    // Analysis
    public static void AnalyzePerformance()
    {
        Console.WriteLine("=== Time Complexity Analysis ===");

        var watch = System.Diagnostics.Stopwatch.StartNew();

        // Test recursive approach
        watch.Restart();
        double recursiveResult = CalculateFutureValueRecursive(1000, 0.05, 20);
        watch.Stop();
        Console.WriteLine($"Recursive Result: {recursiveResult:C2} - Time: {watch.ElapsedTicks} ticks");

        // Test iterative approach
        watch.Restart();
        double iterativeResult = CalculateFutureValueIterative(1000, 0.05, 20);
        watch.Stop();
        Console.WriteLine($"Iterative Result: {iterativeResult:C2} - Time: {watch.ElapsedTicks} ticks");

        /*
         * Time Complexity:
         * - Recursive: O(n) time, O(n) space due to call stack
         * - Iterative: O(n) time, O(1) space
         * Iterative version is preferred for larger inputs to avoid stack overflow
         */
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Financial Forecasting Tool ===\n");

        double initialInvestment = 10000;
        double annualGrowthRate = 0.07;
        int years = 10;

        Console.WriteLine("1. Recursive Future Value Calculation:");
        double futureValueRecursive = FinancialForecasting.CalculateFutureValueRecursive(
            initialInvestment, annualGrowthRate, years);
        Console.WriteLine($"Initial Investment: {initialInvestment:C2}");
        Console.WriteLine($"Annual Growth Rate: {annualGrowthRate:P2}");
        Console.WriteLine($"Time Period: {years} years");
        Console.WriteLine($"Future Value (Recursive): {futureValueRecursive:C2}\n");

        Console.WriteLine("2. Iterative Future Value Calculation:");
        double futureValueIterative = FinancialForecasting.CalculateFutureValueIterative(
            initialInvestment, annualGrowthRate, years);
        Console.WriteLine($"Future Value (Iterative): {futureValueIterative:C2}\n");

        // Step 4: Performance analysis
        FinancialForecasting.AnalyzePerformance();
    }
}
