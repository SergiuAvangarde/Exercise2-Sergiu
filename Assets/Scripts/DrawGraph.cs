using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGraph : MonoBehaviour
{
    public List<Monom> PolynomEquation { get; set; }
    private List<Vector3> pointsList;
    private LineRenderer Line;

    //sets a reference for the line renderer component
    private void Awake()
    {
        Line = GetComponent<LineRenderer>();
    }

    //this function takes the input polynom and creates a graph with X values of -10 to 10
    //it is incrementing by 0.1 for a smoother curve on the graph
    public void CreateGraph()
    {
        pointsList = new List<Vector3>();

        for (float i = -10; i < 10; i += 0.1f)
        {
            float sign = new float();
            if((i) < 0)
            {
                sign = -1;
            }
            else
            {
                sign = 1;
            }

            float result = new float();
            foreach (var polynom in PolynomEquation)
            {
                if (polynom.exponent % 2 == 0)
                {
                    result += polynom.coefficient * Mathf.Pow(Mathf.Abs(i), polynom.exponent);
                }
                else
                {
                    result += polynom.coefficient * (Mathf.Pow(Mathf.Abs(i), polynom.exponent) * sign);
                }
            }
            pointsList.Add(new Vector3(i, result, 0));
        }

        Line.positionCount = pointsList.Count;
        Line.SetPositions(pointsList.ToArray());
    }
}
