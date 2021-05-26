using System;

public enum IntervalType
{
    Open,
    Closed
};

// credits: Class Owner: https://gist.github.com/hongymagic/877f1e083d5f0855597a

public class Interval<T> where T : IComparable
{
    public T LowerBound { get; private set; }
    public T UpperBound { get; private set; }
    public IntervalType LowerBoundIntervalType { get; private set; }
    public IntervalType UpperBoundIntervalType { get; private set; }

    public Interval(
        T lowerBound, 
        T upperBound, 
        IntervalType lowerBoundIntervalType, 
        IntervalType upperBoundIntervalType
    )
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
        LowerBoundIntervalType = lowerBoundIntervalType;
        UpperBoundIntervalType = upperBoundIntervalType;

        if (!CheckBounds())
            SwapBounds();
    }

    private bool CheckBounds()
    {
        int comparison = LowerBound.CompareTo(UpperBound);
        return comparison < 0;
    }

    private void SwapBounds()
    {
        T temp = LowerBound;
        LowerBound = UpperBound;
        UpperBound = temp;
    }

    public bool Contains(T point)
    {
        if (LowerBound.GetType() != typeof(T)
            || UpperBound.GetType() != typeof(T))
        {
            throw new ArgumentException("Type mismatch", "point");
        }

        var lower = LowerBoundIntervalType == IntervalType.Open
            ? LowerBound.CompareTo(point) < 0
            : LowerBound.CompareTo(point) <= 0;
        var upper = UpperBoundIntervalType == IntervalType.Open
            ? UpperBound.CompareTo(point) > 0
            : UpperBound.CompareTo(point) >= 0;

        return lower && upper;
    }

    public override string ToString()
    {
        return string.Format(
            "{0}{1}, {2}{3}",
            LowerBoundIntervalType == IntervalType.Open ? "(" : "[",
            LowerBound,
            UpperBound,
            UpperBoundIntervalType == IntervalType.Open ? ")" : "]"
        );
    }

    public static Interval<double> Range(double lowerBound, double upperBound, IntervalType lowerBoundIntervalType = IntervalType.Closed, IntervalType upperBoundIntervalType = IntervalType.Closed)
    {
        return new Interval<double>(lowerBound, upperBound, lowerBoundIntervalType, upperBoundIntervalType);
    }

    public static Interval<float> Range(float lowerBound, float upperBound, IntervalType lowerBoundIntervalType = IntervalType.Closed, IntervalType upperBoundIntervalType = IntervalType.Closed)
    {
        return new Interval<float>(lowerBound, upperBound, lowerBoundIntervalType, upperBoundIntervalType);
    }

    public static Interval<int> Range(int lowerBound, int upperBound, IntervalType lowerBoundIntervalType = IntervalType.Closed, IntervalType upperBoundIntervalType = IntervalType.Closed)
    {
        return new Interval<int>(lowerBound, upperBound, lowerBoundIntervalType, upperBoundIntervalType);
    }
}
