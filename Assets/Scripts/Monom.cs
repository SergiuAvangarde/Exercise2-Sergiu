using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monom
{
    public float coefficient;
    public float exponent;
    public float sign;

    //this function takes the coeficient and the exponent and creates a string of Monom type with them
    public string MonomString()
    {
        var coef = coefficient;
        var exp = exponent;
        string result = null;

        if (coef != 0)
        {
            if (Mathf.Abs(coef) == 1)
            {
                if (exp > 1)
                {
                    result = "x^" + exp.ToString();
                }
                else if(exp == 1)
                {
                    result = "x";
                }
                else if (exp == 0)
                {
                    result = coef.ToString();
                }
            }
            else
            {
                if (exp > 1)
                {
                    result = coef.ToString() + "x^" + exp.ToString();
                }
                else if (exp == 1)
                {
                    result = coef.ToString() + "x";
                }
                else if (exp == 0)
                {
                    result = coef.ToString();
                }
            }
        }
        return result;
    }

    //this function creates an Object of this class wich will be added to a list of monoms
    public static Monom CreateMonomObj(float coef, float exp)
    {
        Monom polynomial = new Monom();
        if (coef >= 0)
        {
            polynomial.sign = Mathf.Sign(+1);
        }
        else
        {
            polynomial.sign = Mathf.Sign(-1);
        }
        polynomial.coefficient = Mathf.Abs(coef);
        polynomial.exponent = exp;

        if (polynomial.MonomString() != null)
        {
            return polynomial;
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
        foreach (var polynomial in polynomList)
        {
            if (i == 0 && polynomial.sign == +1)
            {
                ResultedPolynomialString.Add(polynomial.MonomString());
                i++;
            }
            else if (polynomial.sign == +1)
            {
                ResultedPolynomialString.Add("+ " + polynomial.MonomString());
            }
            else if (polynomial.sign == -1)
            {
                ResultedPolynomialString.Add("- " + polynomial.MonomString());
            }
        }
        return ResultedPolynomialString;
    }
}
