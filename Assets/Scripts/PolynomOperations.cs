using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolynomOperations : MonoBehaviour
{
    public List<Monom> ResultedPolynomEquation { get; set; }

    [Tooltip("Put all of the Operation Buttons in this list "), SerializeField]
    private Button[] operationButtons;
    [SerializeField]
    private Text resultedPolynom;
    [SerializeField]
    private ConvertToPolynom polynom1;
    [SerializeField]
    private ConvertToPolynom polynom2;

    private List<Monom> resultedAddedEquation;

    //check if any of the equations are null, and if so, it makes the buttons inactive 
    private void Update()
    {
        if (polynom1.PolynomialEquation == null || polynom2.PolynomialEquation == null)
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

    //Adds two polynom equations on a single Monom list 
    //If operation is false the second list is added with '-' sign for substract operation;
    //sorts it in descending order acording to exponent value
    private void AddOrSubPolynoms(bool operation)
    {
        resultedAddedEquation = new List<Monom>();
        ResultedPolynomEquation = new List<Monom>();

        foreach (Monom monomFrom1 in polynom1.PolynomialEquation)
        {
            resultedAddedEquation.Add(monomFrom1);
        }
        foreach (Monom monomFrom2 in polynom2.PolynomialEquation)
        {
            if (operation)
            {
                resultedAddedEquation.Add(monomFrom2);
            }
            else
            {
                if (monomFrom2.sign == -1)
                {
                    resultedAddedEquation.Add(MonomFactory.CreateMonomObj(monomFrom2.coefficient, monomFrom2.exponent));
                }
                else
                {
                    resultedAddedEquation.Add(MonomFactory.CreateMonomObj(monomFrom2.coefficient * -1, monomFrom2.exponent));
                }
            }
        }

        resultedAddedEquation.Sort((a, b) => -1 * a.exponent.CompareTo(b.exponent));
    }

    //function to search on two polynom equations, multiply the coeficients and add the exponents
    //it creates a new Monom List with the new values
    private void MultiplyArrays()
    {
        resultedAddedEquation = new List<Monom>();
        ResultedPolynomEquation = new List<Monom>();
        float coeficientResult = new float();
        float exponentResult = new float();

        for (int i = 0; i <= polynom1.PolynomialEquation.Count - 1; i++)
        {
            for (int j = 0; j <= polynom2.PolynomialEquation.Count - 1; j++)
            {
                coeficientResult = (polynom1.PolynomialEquation[i].coefficient * polynom1.PolynomialEquation[i].sign) * (polynom2.PolynomialEquation[j].coefficient * polynom2.PolynomialEquation[j].sign);
                exponentResult = polynom1.PolynomialEquation[i].exponent + polynom2.PolynomialEquation[j].exponent;

                resultedAddedEquation.Add(MonomFactory.CreateMonomObj(coeficientResult, exponentResult));
            }
        }
        resultedAddedEquation.Sort((a, b) => -1 * a.exponent.CompareTo(b.exponent));
    }

    //this function takes two polynoms, checks the first coeficient of every equation and divides them if they can be divided
    //the function is recalled until the polynoms can't be divided anymore
    //and the function returns a remainder if there is any
    private List<Monom> DividePolynoms(List<Monom> polynom1, List<Monom> polynom2)
    {
        Monom DivideMonom = new Monom();
        List<Monom> DivideEquation = new List<Monom>();
        List<Monom> resultedDivideEquation = new List<Monom>();
        List<Monom> remainder = new List<Monom>();
        resultedAddedEquation = new List<Monom>();
        float coeficientResult = new float();
        float exponentResult = new float();

        if (polynom1[0].exponent >= polynom2[0].exponent)
        {
            coeficientResult = (polynom1[0].coefficient * polynom1[0].sign) / (polynom2[0].coefficient * polynom2[0].sign);
            exponentResult = polynom1[0].exponent - polynom2[0].exponent;

            DivideMonom = MonomFactory.CreateMonomObj(coeficientResult, exponentResult);
            ResultedPolynomEquation.Add(DivideMonom);

            foreach (var monom in polynom2)
            {
                coeficientResult = monom.coefficient * monom.sign * (DivideMonom.coefficient * DivideMonom.sign);
                exponentResult = monom.exponent + DivideMonom.exponent;

                if (MonomFactory.CreateMonomObj(coeficientResult, exponentResult) != null)
                {
                    DivideEquation.Add(MonomFactory.CreateMonomObj(coeficientResult, exponentResult));
                }
            }

            foreach (var monom in polynom1)
            {
                resultedAddedEquation.Add(monom);
            }
            foreach (var monom in DivideEquation)
            {
                if (monom.sign == -1)
                {
                    resultedAddedEquation.Add(MonomFactory.CreateMonomObj(monom.coefficient, monom.exponent));
                }
                else
                {
                    resultedAddedEquation.Add(MonomFactory.CreateMonomObj(monom.coefficient * -1, monom.exponent));
                }
            }
            resultedAddedEquation.Sort((a, b) => -1 * a.exponent.CompareTo(b.exponent));

            resultedDivideEquation = AddEquation(resultedAddedEquation);

            if (resultedDivideEquation.Count > 0)
            {
                //counter is for debugging
                if (resultedDivideEquation[0].exponent >= polynom2[0].exponent)
                {
                    return DividePolynoms(resultedDivideEquation, polynom2);
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
            ResultedPolynomEquation = polynom2;
            return polynom1;
        }
    }

    //searches in Monom list for every object with the same exponent then adds the coeficients acording to operation value
    //it recalls itself until there is no objects with the same exponent
    private List<Monom> AddEquation(List<Monom> firstPolynom)
    {
        List<Monom> secondPolynom = new List<Monom>();
        float result = new float();

        for (int i = 0; i <= firstPolynom.Count - 1; i++)
        {
            if (((i + 1) <= firstPolynom.Count - 1) && (firstPolynom[i].exponent == firstPolynom[i + 1].exponent))
            {
                result = (firstPolynom[i].coefficient * firstPolynom[i].sign) + (firstPolynom[i + 1].coefficient * firstPolynom[i + 1].sign);

                if (result != 0)
                {
                    secondPolynom.Add(MonomFactory.CreateMonomObj(result, firstPolynom[i].exponent));
                }
                i++;
            }
            else
            {
                if (firstPolynom[i].MonomString() != null)
                {
                    secondPolynom.Add(firstPolynom[i]);
                }
            }
        }

        if (secondPolynom.Count == firstPolynom.Count)
        {
            return secondPolynom;
        }
        else
        {
            return AddEquation(secondPolynom);
        }
    }

    //this function is called when you press Add button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnAddPress()
    {
        AddOrSubPolynoms(true);
        ResultedPolynomEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(ResultedPolynomEquation));
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Substract button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnSubstractPress()
    {
        AddOrSubPolynoms(false);
        ResultedPolynomEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(ResultedPolynomEquation));
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Multiply button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnMultiplyPress()
    {
        MultiplyArrays();
        ResultedPolynomEquation = AddEquation(resultedAddedEquation);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(ResultedPolynomEquation));
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }

    //this function is called when you press Divide button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnDividePress()
    {
        ResultedPolynomEquation = new List<Monom>();
        var remainder = new List<Monom>();

        remainder = DividePolynoms(polynom1.PolynomialEquation, polynom2.PolynomialEquation);

        if (ResultedPolynomEquation.Count > 0)
        {
            if (remainder != null)
            {
                resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(ResultedPolynomEquation)) + "\n with remainder: " + string.Join(" ", MonomFactory.PrintPolynom(remainder));
            }
            else
            {
                resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(ResultedPolynomEquation));
            }
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }
}
