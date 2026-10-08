using MNIST.Core.Data;

// Get data
var trainTable = DataUtils.Read("mnist_train.csv");
var testTable = DataUtils.Read("mnist_test.csv");


trainTable.Dimension();
testTable.Dimension();
