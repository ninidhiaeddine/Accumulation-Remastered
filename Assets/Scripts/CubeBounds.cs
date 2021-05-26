using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBounds
{
    public Interval<float> XInterval { get; private set; }
    public Interval<float> YInterval { get; private set; }
    public Interval<float> ZInterval { get; private set; }

    private GameObject cube;

    public CubeBounds(GameObject cube)
    {
        this.cube = cube;

        ComputeXBounds();
        ComputeYBounds();
        ComputeZBounds();
    }

    private void ComputeXBounds()
    {
        float xPosition = cube.transform.position.x;
        float xScale = cube.transform.localScale.x;

        float lowerBound = xPosition - (xScale / 2.0f);
        float upperBound = xPosition + (xScale / 2.0f);

        XInterval = Interval<float>.Range(lowerBound, upperBound, IntervalType.Open, IntervalType.Open);
    }

    private void ComputeYBounds()
    {
        float yPosition = cube.transform.position.y;
        float yScale = cube.transform.localScale.y;

        float lowerBound = yPosition - (yScale / 2.0f);
        float upperBound = yPosition + (yScale / 2.0f);

        YInterval = Interval<float>.Range(lowerBound, upperBound, IntervalType.Open, IntervalType.Open);
    }

    private void ComputeZBounds()
    {
        float zPosition = cube.transform.position.z;
        float zScale = cube.transform.localScale.z;

        float lowerBound = zPosition - (zScale / 2.0f);
        float upperBound = zPosition + (zScale / 2.0f);

        ZInterval = Interval<float>.Range(lowerBound, upperBound, IntervalType.Open, IntervalType.Open);
    }
}
