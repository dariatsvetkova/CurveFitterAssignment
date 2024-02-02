import { PRECISION } from '../utility/constants';

interface EquationProps {
    Equation: number[];
}

export default function Equation({
    Equation,
}: EquationProps) {
    let eqString = ""

    Equation.forEach((curr: number, index: number) => {
        const roundedCurr = Math.round(curr * Math.pow(10, PRECISION)) / Math.pow(10, PRECISION);

        if (roundedCurr !== 0) {
            const formatCurr = (curr > 0 && eqString.length !== 0) ?
                "+" + roundedCurr :
                roundedCurr

            if (index === 0) {
                eqString += roundedCurr;
            } else if (index === 1) {
                eqString += `${formatCurr}x`;
            } else {
                eqString += `${formatCurr}x^${index}`;
            }
        }
    })

    if (eqString === "") {
        return (
            <p>
                <i>y(x) = 0</i>
            </p>
        )
    }

    return (
        <p>
            <i>{`y(x) = ${eqString}`}</i>
        </p>
    )
}
