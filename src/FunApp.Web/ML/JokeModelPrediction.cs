using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;

namespace FunApp.Web.ML
{
    public class JokeModelPrediction
    {
        [ColumnName(DefaultColumnNames.PredictedLabel)]
        public string Category { get; set; }
    }
}