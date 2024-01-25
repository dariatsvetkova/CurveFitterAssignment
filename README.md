# CurveFitter

![Screenshot 2024-01-24 200826](https://github.com/dariatsvetkova/CurveFitterAssignment/assets/68360696/792b0153-0017-461e-b8fa-6a17e4f0ffcf)

## Description

[Client demo](https://www.loom.com/share/3620fddfc8404a5e8ea4cf3958441c11?sid=e1de9eac-0ac2-4bdd-8090-dd85289714db)

### The following functionality has been implemented:

* Client app that collects form data for 2D points and fit type, then queries the API and displays a graph with the user's points and approximated data

* API endpoint that takes form input and returns lists of data points for the approximated curve and the equation for it

### Functionality that hasn't been implemented:

Due to the limited timeframe, I had to focus on implementing key features and leave out more time-consuming ones. If given more time, I would do the following:

- Add the persistence functionality described in the assignment (you can find the beginnings of SQLite and the Entity Framework implementation in this codebase, but it is a work in progress and isn't functional so far)
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
