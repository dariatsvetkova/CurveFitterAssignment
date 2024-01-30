using CurveFitter.Server.Models;

namespace CurveFitter.Server
{
    public class ServerUtils()
    {
        private static readonly int[] FitTypes = { 1, 2, 3 };

        public static readonly long centuryBegin = new DateTime(2001, 1, 1).Ticks;

        public static int GenerateId()
        {
            long timestamp = DateTime.Now.Ticks - centuryBegin;
            int id = (int)(timestamp % int.MaxValue);
            return id;
        }

        public static DataPoint[] StringToDataPoints(string pointsStr)
        {
            return pointsStr
                .Split(',')
                .Select(s => s.Split('y'))
                .Select(s => new DataPoint(Convert.ToDouble(s[0]), Convert.ToDouble(s[1])))
                .ToArray();
        }

        public static int StringToFitType(string fitTypeStr)
        {
            return Convert.ToInt32(fitTypeStr);
        }

        public static double[] StringToEquationArr(string equationStr)
        {
            return equationStr
                .Split(',')
                .Select(s => Convert.ToDouble(s))
                .ToArray();
        }

        private static (bool, string) ValidateFitType(int fitType)
        {
            if (FitTypes.Contains(fitType) == false)
            {
                return (false, $"Invalid fit type: {fitType}");
            }
            return (true, "");
        }

        private static (bool, string) ValidateDataPoints(DataPoint[] points, int fitType)
        {
            if (points.Length < fitType + 1)
            {
                return (false, $"Not enough data points: {points.Length}");
            }

            return (true, "");
        }

        private static (bool, string) ValidateEquation(double[] equation, int fitType)
        {
            if (equation.Length != fitType + 1)
            {
                return (false, "Equation doesn't contain enough coefficients");
            }

            return (true, "");
        }

        public static (bool, string) ValidateUserInputs(DataPoint[] points, int fitType)
        {
            (bool isFitValid, string fitError) = ValidateFitType(fitType);
            (bool arePointsValid, string pointsError) = ValidateDataPoints(points, fitType);

            return (isFitValid && arePointsValid, fitError + pointsError);
        }

        public static (bool, string) ValidateArchive(Archive archive)
        {
            bool isValid = true;
            string errorMessage = "";

            if (archive == null)
            {
                isValid = false;
                errorMessage = "Archive is null";
                return (isValid, errorMessage);
            }

            if (archive.Name.Length < 1)
            {
                isValid = false;
                errorMessage = "Archive name must be present";
            }
            
            (bool isFitValid, string fitError) = ValidateFitType(archive.FitType);
            if (!isFitValid)
            {
                isValid = false;
                errorMessage += fitError;
            }

            (bool areUserPointsValid, string userPointsError)
                = ValidateDataPoints(archive.UserDataPoints, archive.FitType);
            if (!areUserPointsValid)
            {
                isValid = false;
                errorMessage += userPointsError;
            }

            (bool areFitPointsValid, string fitPointsError)
                = ValidateDataPoints(archive.FitDataPoints, archive.FitType);
            if (!areFitPointsValid)
            {
                isValid = false;
                errorMessage += fitPointsError;
            }

            (bool isEquationValid, string equationError)
                = ValidateEquation(archive.Equation, archive.FitType);
            if (!isEquationValid)
            {
                isValid = false;
                errorMessage += equationError;
            }

            return (isValid, errorMessage);
        }
    }
}
