using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGraph : MonoBehaviour
{
    public List<Monom> PolynomialEquation { get; set; }
    private List<Vector3> pointsList;
    private LineRenderer line;

    //sets a reference for the line renderer component
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    //this function takes the input polynom and creates a graph with X values of -10 to 10
    //it is incrementing by 0.1 for a smoother curve on the graph
    public void CreateGraph()
    {
        pointsList = new List<Vector3>();

        for (float i = -14; i <= 14; i += 0.02f)
        {
            float xSign = 0;
            if (i < 0)
            {
                xSign = -1;
            }
            else
            {
                xSign = 1;
            }

            float result = 0;
            foreach (var monom in PolynomialEquation)
            {
                if (monom.Exponent % 2 == 0)
                {
                    result += monom.Coefficient * monom.Sign * Mathf.Pow(Mathf.Abs(i), monom.Exponent);
                }
                else
                {
                    result += monom.Coefficient * monom.Sign * (Mathf.Pow(Mathf.Abs(i), monom.Exponent) * xSign);
                }
            }

            //this commented section should not let the graph go outside the bounds of the background
            //if(result >= -10.5f && result <= 10.5f)
            //{
                pointsList.Add(new Vector3(i, result, 0));
            //}
        }

        line.positionCount = pointsList.Count;
        line.SetPositions(pointsList.ToArray());
    }
}
