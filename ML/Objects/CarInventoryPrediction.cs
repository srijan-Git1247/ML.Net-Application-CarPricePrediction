using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPricePrediction.ML.Objects
{
    //Class that contains the properties mapped to our prediction output
    public class CarInventoryPrediction
    {
        public bool Label { get; set; }
        public bool PredictedLabel { get; set; }//Contains our classification result

        //These two are for model evaluation
        public float Score {  get; set; }
        public float Probability { get; set; }  
    }
}
