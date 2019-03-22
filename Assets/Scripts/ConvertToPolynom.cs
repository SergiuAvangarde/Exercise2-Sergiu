using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertToPolynom : MonoBehaviour
{
    public List<Monom> PolynomialEquation { get; set; }

    [SerializeField]
    private Text resultText;

    private InputField inputNumbers;
    private string[] numbersString;
    private float[] numbersArray;

    //initialize InputField component
    private void Start()
    {
        inputNumbers = GetComponent<InputField>();
    }

    //Splits the string from the input field at every space and adds the elements to a float array
    //converts the values from the array to a list of Monoms
    //prints the polynom to a text field
    public void NumbersToPolynom()
    {
        PolynomialEquation = new List<Monom>();
        numbersString = null;
        numbersString = inputNumbers.text.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        if (numbersString != null)
        {
            numbersArray = new float[numbersString.Length];

            for (int i = 0; i < numbersString.Length; i++)
            {
                numbersArray[i] = float.Parse(numbersString[i].Trim());
            }

            for (int i = numbersArray.Length - 1; i >= 0; i--)
            {
                var monomObj = new Monom(numbersArray[numbersArray.Length - 1 - i], i);
                if (!string.IsNullOrEmpty(monomObj.MonomString()))
                {
                    PolynomialEquation.Add(monomObj);
                }
            }

            if (PolynomialEquation.Count > 0)
            {
                resultText.text = string.Join(" ", MonomUtils.PrintPolynomial(PolynomialEquation));
            }
            else
            {
                resultText.text = "Polynom is 0.";
            }
        }
    }
}
