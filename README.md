# CurveFitter

![image](https://github.com/dariatsvetkova/CurveFitterAssignment/assets/68360696/892ffe0d-14cd-483c-b45d-346c73906e3a)

## Description

> **Update** 1 Feb 2024: the persistence feature has been added. You can access the original submission [here](https://github.com/dariatsvetkova/CurveFitterAssignment/tree/og-submission) and the [diff](https://github.com/dariatsvetkova/CurveFitterAssignment/pull/1/files) here.

### The following functionality has been implemented:

* Client app that collects form data for 2D points and fit type, then queries the API and displays a plot with the user's points and approximated data; plots can be saved to archive, viewed and deleted.

* API endpoint that takes form input and returns lists of data points for the approximated curve and the equation for it

* API endpoints to create new users, save the user's plots to the database, get list of all plots and delete a plot

### Functionality that hasn't been implemented:

Due to the limited timeframe, I had to focus on implementing key features and leave out more time-consuming ones. If given more time, I would do the following:

- Research and implement .NET API best practices, including CORS policy configuration
- Add automated tests
- Improve the front-end functionality by using appropriate libraries for handling form inputs and formatting data
- Implement more robust TypeScript types
- Add a CSS framework and improve styles

## Key files and implementation details

[CurveFitController.cs](https://github.com/dariatsvetkova/CurveFitterAssignment/blob/80b62d2d3f938d996126d366dee08ef5cf380518/CurveFitter.Server/Controllers/CurveFitController.cs) handles the backend logic

  - `MathNet.Numerics` is used as the curve fitting algorithm
    > **Assumption: all incoming data values are within the range of the `int` format**
  
  - Query parameters are validated for length and value correctness, then converted to a shape that `Math.Numerics` consumes
  - The final data point values are calculated using the resulting polynomial coefficients

[PlotView.tsx](https://github.com/dariatsvetkova/CurveFitterAssignment/blob/80b62d2d3f938d996126d366dee08ef5cf380518/curvefitter.client/src/components/PlotView.tsx) handles user inputs and queries the API

  - The component renders the required amount of data points based on the selected curve fit
    > **Assumption: inputs are limited to positive, non-comma-separated integer values** (a more robust implementation would require text inputs with a 3-rd party formatting solution and, therefore, more thorough validation)
  - Form is validated on submission for completeness, duplicate data points
  - Graphs are implemented using `Recharts`
    > **Assumption: The equation is displayed with numbers rounded to the 3rd digit. Negligibly small coefficients are assumed to be a side-effect of floating-point arithmetic and not included in the displayed equation**
