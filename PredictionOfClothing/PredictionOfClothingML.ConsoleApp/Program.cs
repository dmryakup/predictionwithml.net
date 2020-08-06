using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using PredictionOfClothingML.Model;

namespace PredictionOfClothingML.ConsoleApp
{
    class Program
    {
        private const string DATA_FILEPATH = @"D:\PredictionOfClothing\PredictionOfClothing\PredictionOfClothing\Data\womenstrain.csv";

        static void Main(string[] args)
        {
            ModelInput sampleData = CreateMultipleDataSample(DATA_FILEPATH);
            Console.WriteLine("Please write your name:");
            string username = Console.ReadLine();
            Console.WriteLine("Please write your age:");
            string userage = Console.ReadLine();
            int user_age = Convert.ToInt32(userage);
            Console.WriteLine("What Type of clothes:");
            Console.WriteLine("blouses, dresses, finegauge, intimates, jackets, jeans, knits, layering, legwear, lounge, outerwear, pants, shorts, skirts, sleep, sweaters, swim, trend");
            string classname = Console.ReadLine();

            ModelInput input = new ModelInput()
            {
                //Age Clothing ID Rating Class Name
                Age = user_age,
                Clothing_ID = 1,
                Rating = 1,
                Class_Name = classname,
            };
            
            var predictionResult = ConsumeModel.Predict(input);

            Console.WriteLine("Using model to make prediction...\n\n");
            Console.WriteLine($"Age: {username}");
            Console.WriteLine($"Age: {userage}");
            Console.WriteLine($"Clothing ID: {sampleData.Clothing_ID}");
            Console.WriteLine($"Class Name: {sampleData.Class_Name}");
            Console.WriteLine($"\n\nActual Rating: {sampleData.Rating} \nPredicted Rating: {predictionResult.Score}\n\n");
            if (predictionResult.Score > 4)
            {
                Console.WriteLine($"CLOTHE " + sampleData.Clothing_ID + " FROM " + sampleData.Class_Name + " IS RECOMMENDED FOR MR/MRS. " + username);
            }
            else
            {
                Console.WriteLine($"CLOTHE " + sampleData.Clothing_ID + " FROM " + sampleData.Class_Name + " IS NOT RECOMMENDED FOR MR/MRS. " + username + " !!!\n\n");
            }
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

       
        #region CreateMultipleDataSample
        
        private static ModelInput CreateMultipleDataSample(string dataFilePath)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load dataset
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Use first line of dataset as model input
            // You can replace this with new test data (hardcoded or from end-user application)
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }
        #endregion
    }
}
