# Polynomials - Sergiu

This program takes two inputs from the user and converts them into polynomials of the type: a(i)*x^(n-i)

These two polynomials can be Added, Substracted or Multiplyed between them, or you could do operations like derivate, integrate, calculate the equation with the value of X from the user input, or draw a graph with the values of X between -10 to 10 on each of the polynom inputs or on the resulted polynom from add, substract or multiply operations.

## Scripts
The program takes an array of numbers separated by spaces from the input field, the numbers can be separated by more than one space, and the numbers can have negative values. The array is compiled only when the user finished writing the numbers and pressed 'Enter'. This is done to avoid errors when writing negative numbers.

To create the Polynom equation I use a Monom Class wich takes every number from the array and creates the coresponding Monom. Every monom has an coeficient wich is the number introduced, an exponent wich is calculated by the total of numbers introduced and the position where the number is located according to the other numbers, and a sign wich is taken from the input numbers. It creates a list with those monoms, then makes a string from that list and adds the coresponding signs from every monom to create the polynomial equation. The String is shown on the UI with the Text component from Unity.

The sign is taken separately into consideration because if the first element of the polynom has the sign "+" it's not necessary to be shown, also if the input number is 0 the program skips adding it to the final equation. 
If the exponent is 1 the final polynom will show only "x" not "x^1" and if the exponent is 0 the program will show only the coeficient value. Also, if the coeficient is 1 the polynom will show only the sign and the x value, but if the "x" exponent is 0 and the coeficient is 1, the program will show the value 1 for the coeficient.

Every button on the interface becomes available only when that equation has all of the necesary inputs introduced by the user.
If the input is 0 or null and the user tries make some operation with that value, the program will print the message "Polynom is 0".

To apply the add, substract or multiply operations to the input polynoms, it adds the monom lists from every polynom to a new monom list, sorts that list in descending order acording to the exponent values, then aplies the coresponding operation.
If the added list with the multiply result has 2 monoms with the same exponent value it recalls the add function until the result is corected.

For Derivate, Integrate and Set value of X operations it takes the coresponding input polynom or resulted polynom, and aplies the coresponding math operation.

The Graph is calculated by setting the value of X with values ranging from -10 to 10, At every value of X it sets the exact point for the line on the graph, for a smoother curve I incremented the value of X by 0.1 at every point. To match the graph scale with the point coordinates I scaled the line in the inspector, and this way if i give the coordinates X = 2.5f and Y = 5.0f, that point will be exactly at 2.5 on the X scale and 5 on Y scale according to the graph background.

## Further improvements wich could be done:

- I could make the posibility to select wich polynoms to add, substract or multiply, currently I can only operate on the input polynomyals, not the resulted polynomyals from derivate or integrate functions.
- The UI can be improved to look nicer
