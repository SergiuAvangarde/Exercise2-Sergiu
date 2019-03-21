using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monom
{
    public float Coefficient;
    public float Exponent;
    public float Sign;

    //this function takes the coeficient and the exponent and creates a string of Monom type with them
    public string MonomString()
    {
        var coef = Coefficient;
        var exp = Exponent;
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
}
