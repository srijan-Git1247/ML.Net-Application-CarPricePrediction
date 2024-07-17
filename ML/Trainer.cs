using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPricePrediction.ML.Base;
using CarPricePrediction.ML.Objects;
using Microsoft.ML;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CarPricePrediction.Common;
namespace CarPricePrediction.ML
{
    public class Trainer:BaseML
    {
        public void Train(string trainingFileName, string testFileName)
        {
            // Check if training data exists
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find the training data file {trainingFileName}");
                return;
            }

            //Ensure that the test fileName exists
            if (!File.Exists(testFileName))
            {
                Console.WriteLine($"Failed to find test data file ({testFileName}");
                return;
            }

            //Loads training file into an IDataViewObject
            var trainingDataView = MlContext.Data.LoadFromTextFile<CarInventory>(trainingFileName, ',', hasHeader: false);

            //We then build the data process pipeline using the NormalizeMeanVariance transform method on the inputted values
            IEstimator<ITransformer> dataProcessPipeline = MlContext.Transforms.Concatenate("Features", typeof(CarInventory).ToPropertyList<CarInventory>(nameof(CarInventory.Label)))
                .Append(MlContext.Transforms.NormalizeMeanVariance(inputColumnName: "Features",
                    outputColumnName: "FeaturesNormalizedByMeanVar"));


            //Create the FastTree Trainer with the Label from the CarInventory Class and the normalized mean variance as follows:

            var trainer = MlContext.BinaryClassification.Trainers.FastTree(
                        labelColumnName: nameof(CarInventory.Label),
                        featureColumnName: "FeaturesNormalizedByMeanVar",
                        numberOfLeaves: 2,
                        numberOfTrees: 1000,
                        minimumExampleCountPerLeaf: 1,
                        learningRate: 0.2);


            //Complete our pipeline by appending the trainer we instantiated

            var trainingPipeLine = dataProcessPipeline.Append(trainer);


            //Train the model with the data set created Earlier
            var trainedModel = trainingPipeLine.Fit(trainingDataView);

            //Save created model to the filename specified matching training set's schema

            MlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath
                );

            //We evaluate the model we just trained, like this

            var evaluationPipeline = trainedModel.Append(MlContext.Transforms
               .CalculateFeatureContribution(trainedModel.LastTransformer)
               .Fit(dataProcessPipeline.Fit(trainingDataView).Transform(trainingDataView)));

            var testDataView = MlContext.Data.LoadFromTextFile <CarInventory>(testFileName, ',', hasHeader: false);
            var testSetTransform =evaluationPipeline.Transform(testDataView);

            var modelMetrics= MlContext.BinaryClassification.Evaluate(data :testSetTransform, labelColumnName: nameof(CarInventory.Label), scoreColumnName: "Score");


            //Finally we output all of the classification metrics. 
            Console.WriteLine($"Accuracy: {modelMetrics.Accuracy:P2}");
            Console.WriteLine($"Area Under Curve: {modelMetrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"Area under Precision recall Curve: {modelMetrics.AreaUnderPrecisionRecallCurve:P2}");
            Console.WriteLine($"F1Score: {modelMetrics.F1Score:P2}");
            Console.WriteLine($"LogLoss: {modelMetrics.LogLoss:#.##}");
            Console.WriteLine($"LogLossReduction: {modelMetrics.LogLossReduction:#.##}");
            Console.WriteLine($"PositivePrecision: {modelMetrics.PositivePrecision:#.##}");
            Console.WriteLine($"PositiveRecall: {modelMetrics.PositiveRecall:#.##}");
            Console.WriteLine($"NegativePrecision: {modelMetrics.NegativePrecision:#.##}");
            Console.WriteLine($"NegativeRecall: {modelMetrics.NegativeRecall:P2}");




        }



    }
}
