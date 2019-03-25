using UnityEngine;

public class Monom
{
    public float Coefficient { get; set; }
    public float Exponent { get; set; }
    public float Sign { get; set; }

    /// <summary>
    /// this function takes the coeficient and the exponent and creates a monom
    /// </summary>
    /// <returns>the resulted monom as a string</returns>
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
                else if (exp == 1)
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

    /// <summary>
    /// monom default constructor
    /// </summary>
    public Monom()
    {
    
    }

    /// <summary>
    /// monom constructor with parameters
    /// </summary>
    /// <param name="coef">the coeficient of the monom</param>
    /// <param name="exp">the exponent of the monom</param>
    public Monom(float coef, float exp)
    {
        if (coef >= 0)
        {
            Sign = Mathf.Sign(+1);
        }
        else
        {
            Sign = Mathf.Sign(-1);
        }
        Coefficient = Mathf.Abs(coef);
        Exponent = exp;
    }
}
