using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedOperations : MonoBehaviour
{
    [Tooltip("Put all of the Operation to Polynom buttons in this list "), SerializeField]
    private Button[] operationButtons;
    [Tooltip("The inputfield with the value of X"), SerializeField]
    private InputField xValue;
    [SerializeField]
    private GameObject graph;
    [SerializeField]
    private Text operationToPolynomial;
    [SerializeField]
    private Text resultedPolynomial;
    [Tooltip("This should be left null if the script is used with resulted polynom"), SerializeField]
    private ConvertToPolynom polynomialInput;
    [Tooltip("This should be left null if the script is used with polynom 1 or 2"), SerializeField]
    private PolynomOperations resultedPolynomialInput;
    [SerializeField]
    private DrawGraph graphPolynomial;

    private List<Monom> initialPolynomialEquation = new List<Monom>();
    private List<Monom> resultedPolynomialEquation;

    /// <summary>
    /// checks if the initial equation is one of the polynoms from input field or the resulted polynom
    /// if any of the equations are null, makes the buttons inactive
    /// </summary>
    private void Update()
    {
        if (polynomialInput != null)
        {
            initialPolynomialEquation = polynomialInput.PolynomialEquation;
        }
        else if (resultedPolynomialInput != null)
        {
            initialPolynomialEquation = resultedPolynomialInput.ResultedPolynomialEquation;
        }

        if (initialPolynomialEquation == null)
        {
            foreach (var Button in operationButtons)
            {
                Button.interactable = false;
            }
            xValue.interactable = false;
        }
        else
        {
            foreach (var Button in operationButtons)
            {
                Button.interactable = true;
            }
            xValue.interactable = true;
        }
    }

    /// <summary>
    /// checks for every monom in equation, and applies the Derivative operation
    /// the result is added to the resulted monoms list
    /// </summary>
    private void DerivativePolynomial()
    {
        resultedPolynomialEquation = new List<Monom>();
        float coeficientResult = 0;
        float exponentResult = 0;

        foreach (var monom in initialPolynomialEquation)
        {
            if (monom.Exponent > 0)
            {
                coeficientResult = monom.Coefficient * monom.Sign * monom.Exponent;
                exponentResult = monom.Exponent - 1;

                var monomObj = new Monom(coeficientResult, exponentResult);
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    resultedPolynomialEquation.Add(monomObj);
                }
            }
        }
    }

    /// <summary>
    /// checks for every monom in equation, and integrates it
    /// the result is added to the resulted monoms list
    /// </summary>
    private void IntegratePolynomial()
    {
        resultedPolynomialEquation = new List<Monom>();
        float coeficientResult = 0;
        float exponentResult = 0;

        foreach (var monom in initialPolynomialEquation)
        {
            coeficientResult = monom.Coefficient * monom.Sign / (monom.Exponent + 1);
            exponentResult = monom.Exponent + 1;

            var monomObj = new Monom(coeficientResult, exponentResult);
            if (!string.IsNullOrEmpty(monomObj.MonomString()))
            {
                resultedPolynomialEquation.Add(monomObj);
            }
        }
    }

    /// <summary>
    /// check for every monom in equation, and changes the value of X with the one provided in the input field
    /// the result is added to the resulted monoms list and printed to the interface
    /// </summary>
    public void SetXValue()
    {
        float XNumber = 0;
        float result = 0;

        if (!string.IsNullOrEmpty(xValue.text))
        {
            XNumber = float.Parse(xValue.text);

            float xSign = 0;
            if (XNumber < 0)
            {
                xSign = -1;
            }
            else
            {
                xSign = 1;
            }

            foreach (var monom in initialPolynomialEquation)
            {
                if (monom.Exponent % 2 == 0)
                {
                    result += monom.Coefficient * monom.Sign * Mathf.Pow(Mathf.Abs(XNumber), monom.Exponent);
                }
                else
                {
                    result += monom.Coefficient * monom.Sign * (Mathf.Pow(Mathf.Abs(XNumber), monom.Exponent) * xSign);
                }
            }

            operationToPolynomial.text = "Polynom with X value of " + XNumber.ToString() + " is: ";
            if (result != 0)
            {
                resultedPolynomial.text = result.ToString();
            }
            else
            {
                resultedPolynomial.text = " 0 ";
            }
        }
    }

    /// <summary>
    /// this function is called when the user presses the button Derivative
    /// it calls the Derivative funtion above and prints the result to interface
    /// </summary>
    public void OnDerivativePress()
    {
        DerivativePolynomial();
        operationToPolynomial.text = "Derivative polynomial is: ";

        if (resultedPolynomialEquation.Count > 0)
        {
            resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(resultedPolynomialEquation));
        }
        else
        {
            resultedPolynomial.text = "Polynomial is 0.";
        }
    }

    /// <summary>
    /// this function is called when the user presses the button Integrate
    /// it calls the Integrate funtion above and prints the result to interface
    /// </summary>
    public void OnIntegratePress()
    {
        IntegratePolynomial();
        operationToPolynomial.text = "Polynomial integrated is: ";

        if (resultedPolynomialEquation.Count > 0)
        {
            resultedPolynomial.text = string.Join(" ", MonomUtils.PrintPolynomial(resultedPolynomialEquation)) + " + C";
        }
        else
        {
            resultedPolynomial.text = "Polynomial is 0.";
        }
    }

    /// <summary>
    /// this function is called when the user presses the button Graph
    /// it activates the graph gameobject, sets the input polynom for the graph and calls the CreateGraph function
    /// </summary>
    public void OnGraphPress()
    {
        graph.SetActive(true);
        graphPolynomial.PolynomialEquation = new List<Monom>();
        graphPolynomial.PolynomialEquation = initialPolynomialEquation;
        graphPolynomial.CreateGraph();
    }
}
