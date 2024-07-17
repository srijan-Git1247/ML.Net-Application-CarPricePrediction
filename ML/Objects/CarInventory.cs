using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace CarPricePrediction.ML.Objects
{
    //Class that contains the data to both predict and train our model
    public class CarInventory
    {
        [LoadColumn(0)]
        public float HasSunroof
        {
            get;
            set;
        }
        [LoadColumn(1)]
        public float HasAc
        {
            get;
            set;
        }
        [LoadColumn(2)]
        public float HasAutomaticTransmission
        {
            get;
            set;
        }
        [LoadColumn(3)]
        public float Amount
        { get; set; }
        [LoadColumn(4)]
        public bool Label
        {
            get;
            set;
        }
    }
}
