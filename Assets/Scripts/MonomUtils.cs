using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonomUtils
{
    //this function takes a list of Monoms and converts them to an Polynom equation string adding the coresponding signs
    public static List<string> PrintPolynomial(List<Monom> polynomList)
    {
        List<string> resultedPolynomialString = new List<string>();
        int i = 0;
        foreach (var monom in polynomList)
        {
            if (i == 0 && monom.Sign == +1)
            {
                resultedPolynomialString.Add(monom.MonomString());
                i++;
            }
            else if (monom.Sign == 1)
            {
                resultedPolynomialString.Add("+ " + monom.MonomString());
            }
            else if (monom.Sign == -1)
            {
                resultedPolynomialString.Add("- " + monom.MonomString());
            }
            i++;
        }
        return resultedPolynomialString;
    }
}
