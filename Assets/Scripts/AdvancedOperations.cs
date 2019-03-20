using System.Collections;
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
    private GameObject Graph;
    [SerializeField]
    private Text operationToPolynom;
    [SerializeField]
    private Text resultedPolynom;
    [Tooltip("This should be left null if the script is used with resulted polynom"), SerializeField]
    private ConvertToPolynom polynomInput;
    [Tooltip("This should be left null if the script is used with polynom 1 or 2"), SerializeField]
    private PolynomOperations resultedPolynomInput;
    [SerializeField]
    private DrawGraph GraphPolynom;

    private List<Monom> initialPolynomEquation;
    private List<Monom> resultedPolynomEquation;

    //initialize the initial polynom equation wich will be used for operations
    private void Start()
    {
        initialPolynomEquation = new List<Monom>();
    }

    //checks if the initial equation is one of the polynoms from input field or the resulted polynom
    //if any of the equations are null, makes the buttons inactive
    private void Update()
    {
        if (polynomInput != null)
        {
            initialPolynomEquation = polynomInput.PolynomialEquation;
        }
        else if (resultedPolynomInput != null)
        {
            initialPolynomEquation = resultedPolynomInput.ResultedPolynomEquation;
        }

        if (initialPolynomEquation == null)
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

    //checks for every monom in equation, and derivates it
    //the result is added to the resulted monoms list
    private void DerivatePolynom()
    {
        resultedPolynomEquation = new List<Monom>();
        float coeficientResult = new float();
        float exponentResult = new float();

        foreach (var monom in initialPolynomEquation)
        {
            if (monom.exponent > 0)
            {
                coeficientResult = monom.coefficient * monom.sign * monom.exponent;
                exponentResult = monom.exponent - 1;

                resultedPolynomEquation.Add(MonomFactory.CreateMonomObj(coeficientResult, exponentResult));
            }
        }
    }

    //checks for every monom in equation, and integrates it
    //the result is added to the resulted monoms list
    private void IntegratePolynom()
    {
        resultedPolynomEquation = new List<Monom>();
        float coeficientResult = new float();
        float exponentResult = new float();

        foreach (var monom in initialPolynomEquation)
        {
            coeficientResult = monom.coefficient * monom.sign / (monom.exponent + 1);
            exponentResult = monom.exponent + 1;

            resultedPolynomEquation.Add(MonomFactory.CreateMonomObj(coeficientResult, exponentResult));
        }
    }

    //check for every monom in equation, and changes the value of X with the one provided in the input field
    //the result is added to the resulted monoms list and printed to the interface
    public void SetXValue()
    {
        float XNumber = new float();
        float result = new float();
        XNumber = float.Parse(xValue.text);

        float xSign = new float();
        if (XNumber < 0)
        {
            xSign = -1;
        }
        else
        {
            xSign = 1;
        }

        foreach (var monom in initialPolynomEquation)
        {
            if (monom.exponent % 2 == 0)
            {
                result += monom.coefficient * monom.sign * Mathf.Pow(Mathf.Abs(XNumber), monom.exponent);
            }
            else
            {
                result += monom.coefficient * monom.sign * (Mathf.Pow(Mathf.Abs(XNumber), monom.exponent) * xSign);
            }
        }

        operationToPolynom.text = "Polynom with X value of " + XNumber.ToString() + " is: ";
        if (result != 0)
        {
            resultedPolynom.text = result.ToString();
        }

    }

    //this function is called when the user presses the button Derivate
    //it calls the Derivate funtion above and prints the result to interface
    public void OnDerivatePress()
    {
        DerivatePolynom();
        operationToPolynom.text = "Polynom derivated is: ";

        //resultedPolynom.text = string.Join(" ", Monom.PrintPolynom(resultedPolynomEquation));

        if (resultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(resultedPolynomEquation));
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }

    //this function is called when the user presses the button Ierivate
    //it calls the Integrate funtion above and prints the result to interface
    public void OnIntegratePress()
    {
        IntegratePolynom();
        operationToPolynom.text = "Polynom integrated is: ";
        //resultedPolynom.text = string.Join(" ", Monom.PrintPolynom(resultedPolynomEquation)) + " + C";

        if (resultedPolynomEquation.Count > 0)
        {
            resultedPolynom.text = string.Join(" ", MonomFactory.PrintPolynom(resultedPolynomEquation)) + " + C";
        }
        else
        {
            resultedPolynom.text = "Polynom is 0.";
        }
    }

    //this function is called when the user presses the button Graph
    //it activates the graph gameobject, sets the input polynom for the graph and calls the CreateGraph function
    public void OnGraphPress()
    {
        Graph.SetActive(true);
        GraphPolynom.PolynomEquation = new List<Monom>();
        GraphPolynom.PolynomEquation = initialPolynomEquation;
        GraphPolynom.CreateGraph();
    }
}
