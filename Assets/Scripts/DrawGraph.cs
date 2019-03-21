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

        for (float i = -14; i <= 14; i += 0.02f)
        {
            float xSign = new float();
            if (i < 0)
            {
                xSign = -1;
            }
            else
            {
                xSign = 1;
            }

            float result = new float();
            foreach (var monom in PolynomEquation)
            {
                if (monom.exponent % 2 == 0)
                {
                    result += monom.coefficient * monom.sign * Mathf.Pow(Mathf.Abs(i), monom.exponent);
                }
                else
                {
                    result += monom.coefficient * monom.sign * (Mathf.Pow(Mathf.Abs(i), monom.exponent) * xSign);
                }
            }

            if(result >= -10.5f && result <= 10.5f)
            {
                pointsList.Add(new Vector3(i, result, 0));
            }
        }

        Line.positionCount = pointsList.Count;
        Line.SetPositions(pointsList.ToArray());
    }
}
