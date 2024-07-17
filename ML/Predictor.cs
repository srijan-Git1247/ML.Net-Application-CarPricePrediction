using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPricePrediction.ML.Base;
using CarPricePrediction.ML.Objects;
using Microsoft.ML;
using Newtonsoft.Json;

namespace CarPricePrediction.ML
{
    public class Predictor:BaseML //This provide prediction support in our project
    {
        public void Predict(string inputDataFile)
        {
            if(!File.Exists(ModelPath))
            {
                ////Verifying if the model exists prior to reading it
                Console.WriteLine($"Failed to find model at {ModelPath}");
                return;

            }
            if (!File.Exists(inputDataFile))
            {
                //Verifying if the input file exists before making predictions on it 
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }

            /*Loading the model  */
            //Then we define the ITransformer Object
            ITransformer mlModel;

            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                mlModel = MlContext.Model.Load(stream, out _);
                //Stream is disposed as a result of Using statement
            }
            if (mlModel == null)
            {
                Console.WriteLine("Failed to load the model");
                return;
            }



            // Create a prediction engine
            var predictionEngine = MlContext.Model.CreatePredictionEngine<CarInventory, CarInventoryPrediction>(mlModel);

            var json = File.ReadAllText(inputDataFile);

            //Given that we are no longer simply passing in the string and building an object on the fly,
            //We need to first read in the file as text.
            //We then deserialize the JSON into CarInventory object as follows
            // Call predict model on prediction engine class
            #pragma warning disable 8604
            var prediction = predictionEngine.Predict(JsonConvert.DeserializeObject<CarInventory>(json));

            //We need to adjust the output of our prediction to match our new CarInventoryPrediction properties

            Console.WriteLine($"Based on input json:{System.Environment.NewLine}" +
                                $"{json}{System.Environment.NewLine}" +
                                $"The car price is a {(prediction.PredictedLabel? "good" : "bad")} deal, with a {prediction.Probability:P0} confidence");

            

        }
    }
}
