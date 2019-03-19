using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolynomOperations : MonoBehaviour
{
    public List<Monom> ResultedPolynomEquation { get; set; }

    [SerializeField]
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
    //sorts it in descending order acording to exponent value
    private void AddArrays()
    {
        resultedAddedEquation = new List<Monom>();
        ResultedPolynomEquation = new List<Monom>();

        foreach (Monom polynomial1 in polynom1.PolynomialEquation)
        {
            resultedAddedEquation.Add(polynomial1);
        }
        foreach (Monom polynomial2 in polynom2.PolynomialEquation)
        {
            resultedAddedEquation.Add(polynomial2);
        }

        resultedAddedEquation.Sort((a, b) => -1 * a.exponent.CompareTo(b.exponent));
    }

    //searches on two polynom equations, multyplies the coeficients and adds the exponents
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
                coeficientResult = polynom1.PolynomialEquation[i].coefficient * polynom2.PolynomialEquation[j].coefficient;
                exponentResult = polynom1.PolynomialEquation[i].exponent + polynom2.PolynomialEquation[j].exponent;

                resultedAddedEquation.Add(Monom.CreateMonomObj(coeficientResult, exponentResult));
            }
        }
        resultedAddedEquation.Sort((a, b) => -1 * a.exponent.CompareTo(b.exponent));
    }

    //searches in Monom list for every object with the same exponent then adds or substracts the coeficients acording to operation value
    //it recalls itself until there is no objects with the same exponent
    private List<Monom> AddOrSub(List<Monom> first, bool operation)
    {
        List<Monom> second = new List<Monom>();
        float result = new float();

        for (int i = 0; i <= first.Count - 1; i++)
        {
            if (((i + 1) <= first.Count - 1) && (first[i].exponent == first[i + 1].exponent))
            {
                if (operation)
                {
                    result = first[i].coefficient + first[i + 1].coefficient;
                }
                else
                {
                    result = first[i].coefficient - first[i + 1].coefficient;
                }

                if (result != 0)
                {
                    second.Add(Monom.CreateMonomObj(result, first[i].exponent));
                }
                i++;
            }
            else
            {
                if (first[i].MonomString() != null)
                {
                    second.Add(first[i]);
                }
            }
        }

        if (second.Count == first.Count)
        {
            return second;
        }
        else
        {
            return AddOrSub(second, operation);
        }
    }

    //this function is called when you press Add button on UI, it needs refference in button component
    //it shows the resulted polynom on UI
    public void OnAddPress()
    {
        AddArrays();
        ResultedPolynomEquation = AddOrSub(resultedAddedEquation, true);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", Monom.PrintPolynom(ResultedPolynomEquation));
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
        AddArrays();
        ResultedPolynomEquation = AddOrSub(resultedAddedEquation, false);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", Monom.PrintPolynom(ResultedPolynomEquation));
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
        ResultedPolynomEquation = AddOrSub(resultedAddedEquation, true);

        if (ResultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", Monom.PrintPolynom(ResultedPolynomEquation));
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }
}
