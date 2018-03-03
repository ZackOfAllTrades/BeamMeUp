namespace MathZ
{
    public class MathZ
    {
        public static float NormalizeScalar(float dataSetMin, float dataSetMax, float targetRangeMin, float targetRangeMax, float scalar)
        {
            return targetRangeMin + (scalar - dataSetMin) * (targetRangeMax - targetRangeMin) / (dataSetMax - dataSetMin);
        }

    }
    public class ScalarNormalizer
    {
        //a + (x - A)(b - a) / (B-A)
        //A = datasetmin, B = datasetMax, a = targetRangeMin, b = targetRangeMax, x = scalar

        float DataSetMin;
        float DataSetMax;
        float TargetRangeMin;
        float TargetRangeMax;

        public ScalarNormalizer(float dataSetMin, float dataSetMax, float targetRangeMin, float targetRangeMax)
        {
            DataSetMin = dataSetMin;
            DataSetMax = dataSetMax;
            TargetRangeMin = targetRangeMin;
            TargetRangeMax = targetRangeMax;
        }

        public float NormalizeScalar(float scalar)
        {
           return TargetRangeMin + (scalar - DataSetMin) * (TargetRangeMax - TargetRangeMin) / (DataSetMax - DataSetMin);
        }
    }
}
