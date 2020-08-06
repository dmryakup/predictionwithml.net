
using Microsoft.ML.Data;

namespace PredictionOfClothingML.Model
{
    public class ModelInput
    {
        [ColumnName("Age"), LoadColumn(0)]
        public float Age { get; set; }


        [ColumnName("Clothing ID"), LoadColumn(1)]
        public float Clothing_ID { get; set; }


        [ColumnName("Rating"), LoadColumn(2)]
        public float Rating { get; set; }


        [ColumnName("Class Name"), LoadColumn(3)]
        public string Class_Name { get; set; }

    }
}
