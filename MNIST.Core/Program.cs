using MNIST.Core.Data;
using static MNIST.Core.Data.MathUtils;
using static MNIST.Core.Data.DataUtils;

internal class Program
{
	private static void Main(string[] args)
	{
		// Get data
		var trainSet = Read("mnist_train.csv");
		var testSet = Read("mnist_test.csv");

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

	public (Matrix W1, Matrix b1, Matrix W2, Matrix b2) InitParams()
	{
		var W1 = new Matrix(10, 784).FillRandom(-0.5);
		var b1 = new Matrix(10, 1).FillRandom(-0.5);
		var W2 = new Matrix(10, 10).FillRandom(-0.5);
		var b2 = new Matrix(10, 1).FillRandom(-0.5);
		return (W1, b1, W2, b2);
	}

	public (Matrix Z1, Matrix A1, Matrix Z2, Matrix A2) ForwardPass(Matrix W1, Matrix b1, Matrix W2, Matrix b2, Matrix X)
	{
		var Z1 = W1 * X + b1;
		var A1 = ActivateReLU(Z1);

		var Z2 = W2 * A1 + b2;
		var A2 = ActivateSoftmax(Z2);

		return (W1, A1, W2, A2);
	}

	public (Matrix Z1, Matrix A1, Matrix Z2, Matrix A2) BackwardPass(Matrix Z1, Matrix A1, Matrix Z2, Matrix A2, Matrix Y)
	{
		var m = Y.Values.Length;
		var OneHotY = OneHot(Y);
	}
}