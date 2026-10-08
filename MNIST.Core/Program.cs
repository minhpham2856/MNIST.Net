using MNIST.Core.Data;

internal class Program
{
	private static void Main(string[] args)
	{
		// Get data
		var trainSet = DataUtils.Read("mnist_train.csv");
		var testSet = DataUtils.Read("mnist_test.csv");

		// Check datasets
		//trainSet.Dimension();
		//testSet.Dimension();

		// Shuffle datasets
		trainSet.Shuffle();
		testSet.Shuffle();

		// Split labels
		var Y_train = trainSet.GetColumn(0);
		trainSet.RemoveColumn(0);
		var X_train = trainSet;

		var Y_test = testSet.GetColumn(0);
		testSet.RemoveColumn(0);
		var X_test = testSet;

		X_train.Dimension();
		Y_train.Dimension();
		X_test.Dimension();
		Y_test.Dimension();
	}
}