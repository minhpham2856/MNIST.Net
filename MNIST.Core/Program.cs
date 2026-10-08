using MNIST.Core.Data;
using System;
using System.IO;

string baseDirectory = AppContext.BaseDirectory;

string trainPath = Path.Combine(baseDirectory, "DataSets", "mnist_train.csv");
string testPath = Path.Combine(baseDirectory, "DataSets", "mnist_train.csv");

var trainTable = DataUtils.Read(trainPath);

Console.WriteLine(trainTable.Head(5));
