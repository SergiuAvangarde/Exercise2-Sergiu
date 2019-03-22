using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolynomOperations : MonoBehaviour
{
    public List<Monom> ResultedPolynomialEquation { get; set; }

    [Tooltip("Put all of the Operation Buttons in this list "), SerializeField]
    private Button[] operationButtons;
    [SerializeField]
    private Text resultedPolynomial;
    [SerializeField]
    private ConvertToPolynom polynomial1Input;
    [SerializeField]
    private ConvertToPolynom polynomial2Input;

    private List<Monom> resultedAddedEquation;

    //check if any of the equations are null, and if so, it makes the buttons inactive 
    private void Update()
    {
        if (polynomial1Input.PolynomialEquation == null || polynomial2Input.PolynomialEquation == null)
        {
            foreach (var Button in operationButtons)
            {
                Button.interactable = false;
            }
        }
        else
        {
            foreach (var Button in operationButtons)
            {
                Button.interactable = true;
            }
        }
    }

    //Adds two polynomial equations on a single Monom list 
    //If operation is false the second list is added with '-' sign for substract operation;
    //sorts it in descending order acording to exponent value
    private void AddOrSubPolynomials(bool operation)
    {
        ResultedPolynomialEquation = new List<Monom>();
        resultedAddedEquation = new List<Monom>(polynomial1Input.PolynomialEquation);

        foreach (Monom monomFrom2 in polynomial2Input.PolynomialEquation)
        {
            if (operation)
            {
                resultedAddedEquation.Add(monomFrom2);
            }
            else
            {
                var monomObj = new Monom(monomFrom2.Coefficient * monomFrom2.Sign * -1, monomFrom2.Exponent);
                //resultedAddedEquation.Add(MonomUtils.CreateMonomObj(monomFrom2.Coefficient * monomFrom2.Sign * -1, monomFrom2.Exponent));
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    resultedAddedEquation.Add(monomObj);
                }
            }
        }

        resultedAddedEquation.Sort((a, b) => -1 * a.Exponent.CompareTo(b.Exponent));
    }

    //function to search on two polynom equations, multiply the coeficients and add the exponents
    //it creates a new Monom List with the new values
    private void MultiplyArrays()
    {
        resultedAddedEquation = new List<Monom>();
        ResultedPolynomialEquation = new List<Monom>();
        float coeficientResult = 0;
        float exponentResult = 0;

        for (int i = 0; i <= polynomial1Input.PolynomialEquation.Count - 1; i++)
        {
            for (int j = 0; j <= polynomial2Input.PolynomialEquation.Count - 1; j++)
            {
                coeficientResult = (polynomial1Input.PolynomialEquation[i].Coefficient * polynomial1Input.PolynomialEquation[i].Sign) * (polynomial2Input.PolynomialEquation[j].Coefficient * polynomial2Input.PolynomialEquation[j].Sign);
                exponentResult = polynomial1Input.PolynomialEquation[i].Exponent + polynomial2Input.PolynomialEquation[j].Exponent;

                var monomObj = new Monom(coeficientResult, exponentResult);
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    resultedAddedEquation.Add(monomObj);
                }
            }
        }
        resultedAddedEquation.Sort((a, b) => -1 * a.Exponent.CompareTo(b.Exponent));
    }

    //this function takes two polynoms, checks the first coeficient of every equation and divides them if they can be divided
    //the function is recalled until the polynoms can't be divided anymore
    //and the function returns a remainder if there is any
    private List<Monom> DividePolynomials(List<Monom> polynomial1, List<Monom> polynomial2)
    {
        List<Monom> DivideEquation = new List<Monom>();
        List<Monom> resultedDivideEquation = new List<Monom>();
        List<Monom> remainder = new List<Monom>();
        float coeficientResult = 0;
        float exponentResult = 0;

        if (polynomial1[0].Exponent >= polynomial2[0].Exponent)
        {
            coeficientResult = (polynomial1[0].Coefficient * polynomial1[0].Sign) / (polynomial2[0].Coefficient * polynomial2[0].Sign);
            exponentResult = polynomial1[0].Exponent - polynomial2[0].Exponent;

            Monom DivideMonom = new Monom(coeficientResult, exponentResult);
            ResultedPolynomialEquation.Add(DivideMonom);

            foreach (var monom in polynomial2)
            {
                coeficientResult = monom.Coefficient * monom.Sign * (DivideMonom.Coefficient * DivideMonom.Sign);
                exponentResult = monom.Exponent + DivideMonom.Exponent;

                var monomObj = new Monom(coeficientResult, exponentResult);
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    DivideEquation.Add(monomObj);
                }
            }

            resultedAddedEquation = new List<Monom>(polynomial1);
            foreach (var monom in DivideEquation)
            {
                var monomObj = new Monom(monom.Coefficient * monom.Sign * -1, monom.Exponent);
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    resultedAddedEquation.Add(monomObj);
                }
            }
            resultedAddedEquation.Sort((a, b) => -1 * a.Exponent.CompareTo(b.Exponent));

            resultedDivideEquation = AddEquation(resultedAddedEquation);

            if (resultedDivideEquation.Count > 0)
            {
                if (resultedDivideEquation[0].Exponent >= polynomial2[0].Exponent)
                {
                    return DividePolynomials(resultedDivideEquation, polynomial2);
                }
                else
                {
                    remainder = resultedDivideEquation;
                    return remainder;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            ResultedPolynomialEquation = polynomial2;
            return polynomial1;
        }
    }

    //searches in Monom list for every object with the same exponent then adds the coeficients acording to operation value
    //it recalls itself until there is no objects with the same exponent
    private List<Monom> AddEquation(List<Monom> firstPolynomial)
    {
        List<Monom> secondPolynomial = new List<Monom>();
        float result = 0;

        for (int i = 0; i <= firstPolynomial.Count - 1; i++)
        {
            if (((i + 1) <= firstPolynomial.Count - 1) && (firstPolynomial[i].Exponent == firstPolynomial[i + 1].Exponent))
            {
                result = (firstPolynomial[i].Coefficient * firstPolynomial[i].Sign) + (firstPolynomial[i + 1].Coefficient * firstPolynomial[i + 1].Sign);

                if (result != 0)
                {
                    var monomObj = new Monom(result, firstPolynomial[i].Exponent);
                    if (!string.IsNullOrEmpty(monomObj.MonomString()))
                    {
                        secondPolynomial.Add(monomObj);
                    }
                }
                i++;
            }
            else
            {
                if (firstPolynomial[i].MonomString() != null)
                {
                    secondPolynomial.Add(firstPolynomial[i]);
                }
            }
        }

        if (secondPolynomial.Count == firstPolynomial.Count)
        {
            return secondPolynomial;
        }
        else
        {
            return AddEquation(secondPolynomial);
        }
    }

    //this function is called when you press Add button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnAddPress()
    {
        AddOrSubPolynomials(true);
        ResultedPolynomialEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomialEquation.Count > 0)
        {
            resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(ResultedPolynomialEquation));
        }
        else
        {
            resultedPolynomial.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Substract button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnSubstractPress()
    {
        AddOrSubPolynomials(false);
        ResultedPolynomialEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomialEquation.Count > 0)
        {
            resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(ResultedPolynomialEquation));
        }
        else
        {
            resultedPolynomial.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Multiply button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnMultiplyPress()
    {
        MultiplyArrays();
        ResultedPolynomialEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomialEquation.Count > 0)
        {
            resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(ResultedPolynomialEquation));
        }
        else
        {
            resultedPolynomial.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Divide button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnDividePress()
    {
        ResultedPolynomialEquation = new List<Monom>();

        if (polynomial1Input.PolynomialEquation.Count == 0 || polynomial2Input.PolynomialEquation.Count == 0)
        {
            resultedPolynomial.text = "Division by 0 is not posible";
        }
        else
        {
            var remainder = new List<Monom>(DividePolynomials(polynomial1Input.PolynomialEquation, polynomial2Input.PolynomialEquation));

            if (ResultedPolynomialEquation.Count > 0)
            {
                if (remainder != null)
                {
                    resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(ResultedPolynomialEquation)) + "\n with remainder: " + string.Join(" ", MonomUtils.PrintPolynomial(remainder));
                }
                else
                {
                    resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(ResultedPolynomialEquation));
                }
            }
            else
            {
                resultedPolynomial.text = "Polynom is 0.";
            }
        }
    }
}
