using System.Collections.Generic;

public class MonomUtils
{
    /// <summary>
    /// this function takes a list of Monoms and converts them to an Polynom equation string adding the coresponding signs
    /// </summary>
    /// <param name="polynomList">A list of monoms wich forms the polynomial equation</param>
    /// <returns>A string of polynomial equation type</returns>
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
