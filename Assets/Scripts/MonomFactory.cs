using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonomFactory
{
    //this function creates an Object of class Monom wich will be added to a list of monoms
    public static Monom CreateMonomObj(float coef, float exp)
    {
        Monom monomObject = new Monom();
        if (coef >= 0)
        {
            monomObject.Sign = Mathf.Sign(+1);
        }
        else
        {
            monomObject.Sign = Mathf.Sign(-1);
        }
        monomObject.Coefficient = Mathf.Abs(coef);
        monomObject.Exponent = exp;

        if (!string.IsNullOrEmpty(monomObject.MonomString()))
        {
            return monomObject;
        }
        else
        {
            return null;
        }
    }

    //this function takes a list of Monoms and converts them to an Polynom equation string adding the coresponding signs
    public static List<string> PrintPolynom(List<Monom> polynomList)
    {
        List<string> ResultedPolynomialString = new List<string>();
        int i = 0;
        foreach (var monom in polynomList)
        {
            if (i == 0 && monom.Sign == +1)
            {
                ResultedPolynomialString.Add(monom.MonomString());
                i++;
            }
            else if (monom.Sign == 1)
            {
                ResultedPolynomialString.Add("+ " + monom.MonomString());
            }
            else if (monom.Sign == -1)
            {
                ResultedPolynomialString.Add("- " + monom.MonomString());
            }
            i++;
        }
        return ResultedPolynomialString;
    }
}
