The trainer used in this project uses FastTreeBinaryTrainer.
Fast Tree is based on the Multiple Additive Regression Trees (MART) gradient boosting algorithm. Here, series of trees are built in a step wise manner before ultimately selecting the best tree.
NuGet Package used is Microsoft.ML.FastTree

The sampledata.csv file contains 18 rows of random data. Feel free to adjust the data to fit your own observation or to adjust the trained model. 
Each of these rows contains the value for the properties in the CarInventory Class.
1. HasSunroof
2. HasAC
3. HasAutomaticTransmission
4. Amount
5. Label indicating whether price is a good deal or not.





Here is a snippet of the data:



![image](https://github.com/user-attachments/assets/e7e9247e-3d5f-429b-9532-fc0ad7f37efe)


The testdata.csv file contains additional datga points to test the trained and evaluate:



![image](https://github.com/user-attachments/assets/d2c72a18-e030-494d-8f78-0c36ce9c4cc4)

Run the Console Application with commandline arguments:
1. Train and test-evaluate the model using sampledata.csv and testdata.csv 
> D:\Machine Learning Projects\CarPricePrediction\bin\Debug\net8.0 train
>"D:\Machine Learning Projects\CarPricePrediction\Data\sampledata.csv"
>"D:\Machine Learning Projects\CarPricePrediction\Data\testdata.csv"

Output
>Accuracy: 88.89%
>Area Under Curve: 100.00%
>Area under Precision recall Curve: 100.00%
>F1Score: 87.50%
>LogLoss: 2.19
>LogLossReduction: -1.19
>PositivePrecision: 1
>PositiveRecall: .78
>NegativePrecision: .82
>NegativeRecall: 100.00%


2. After training the model, build a sample JSON file and save it as input.json as follows:
   ![image](https://github.com/user-attachments/assets/6096cc67-6e57-445e-8281-2158e9066baf)


3. To run the model with the input.json, simply pass in the filename to built application and the predicted out will appear:
   > D:\Machine Learning Projects\CarPricePrediction\bin\Debug\net8.0\CarPricePrediction.exe predict
   >"D:\Machine Learning Projects\CarPricePrediction\Data\input.json"

   ![image](https://github.com/user-attachments/assets/afb38ce0-9cc8-43b7-894b-7b760f8f1f1c)






