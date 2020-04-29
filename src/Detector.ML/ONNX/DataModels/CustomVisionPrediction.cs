using Microsoft.ML.Data;

namespace Detector.ML
{
    public class CustomVisionPrediction : IOnnxObjectPrediction
    {
        [ColumnName("model_outputs0")]
        public float[] PredictedLabels { get; set; }
    }
}
