namespace MNIST.Core.Data;

public class Matrix
{
	public double[][] Values { get; set; } = Array.Empty<double[]>();

	public Matrix() { }
	public Matrix(double[][] values) { Values = values; }
	public Matrix(int rows, int columns)
	{
		Values = new double[rows][];
		for (int i = 0; i < rows; i++) Values[i] = new double[columns];
	}

	// Basic matrix info getters
	public int RowCount() => Values.Length;
	public int ColumnCount() => IsEmpty() ? 0 : Values[0].Length;
	public int Size() => RowCount() * ColumnCount();
	public bool IsEmpty() => Values.Length == 0;
	public void Dimension() => Console.WriteLine($"[Rows: {RowCount()}, Columns: {ColumnCount()}]");
	public double[][] Head(int rowCount = 5) => Values.Take(rowCount).ToArray();
	public double Sum()
	{
		var sum = 0.0;
		for(int i = 0; i < Values.Length; i++)
		{
			for(int j = 0;  j < Values[i].Length; j++)
			{
				sum += Values[i][j];
			}
		}

		return sum;
	}

	// Add a row to the matrix
	public void Add(double[] row) => Values = Values.Append(row).ToArray();

	// Get a row/ column from the matrix
	public Matrix GetRow(int index) => new() { Values = new[] { Values[index] } };

	public Matrix GetColumn(int index)
	{
		var column = new double[RowCount()];
		for (int i = 0; i < RowCount(); i++) column[i] = Values[i][index];

		return new() { Values = column.Select(x => new[] { x }).ToArray() };
	}

	// Remove a row/ column from the matrix
	public void RemoveRow(int index) => Values = Values.Where((_, i) => i != index).ToArray();
	public void RemoveColumn(int index)
	{
		for (int i = 0; i < RowCount(); i++)
			Values[i] = Values[i].Where((_, j) => j != index).ToArray();
	}

	// Shuffle the rows in the matrix
	public void Shuffle()
	{
		var random = Random.Shared;

		for (int i = Values.Length - 1; i > 0; i--)
		{
			int j = random.Next(i + 1);
			(Values[i], Values[j]) = (Values[j], Values[i]);
		}
	}

	// Transpose the matrix
	public Matrix Transpose()
	{
		if (IsEmpty()) return new();

		int rowCount = RowCount();
		int columnCount = ColumnCount();

		var transposed = new Matrix(columnCount, rowCount);
		for (int i = 0; i < rowCount; i++)
		{
			for (int j = 0; j < columnCount; j++)
			{
				transposed.Values[j][i] = Values[i][j];
			}
		}

		return transposed;
	}

	// Operator for addition of 2 matrices
	public static Matrix operator +(Matrix matrixA, Matrix matrixB)
	{
		bool sameRowSize = matrixA.RowCount() != matrixB.RowCount();
		bool sameColumnSize = matrixA.ColumnCount() != matrixB.ColumnCount();

		if (sameRowSize || sameColumnSize) throw new InvalidOperationException("[E] Incompatible dimensions");

		int rows = matrixA.RowCount();
		int cols = matrixA.ColumnCount();

		double[][] resultData = new double[rows][];
		for (int i = 0; i < rows; i++)
		{
			resultData[i] = new double[cols];
			for (int j = 0; j < cols; j++)
				resultData[i][j] = matrixA.Values[i][j] + matrixB.Values[i][j];
		}

		return new Matrix(resultData);
	}

	// Operator for subtraction of 2 matrices
	public static Matrix operator -(Matrix matrixA, Matrix matrixB)
	{
		bool differentRowSize = matrixA.RowCount() != matrixB.RowCount();
		bool differentColumnSize = matrixA.ColumnCount() != matrixB.ColumnCount();

		if (differentRowSize || differentColumnSize) throw new InvalidOperationException("[E] Incompatible dimensions");

		int rows = matrixA.RowCount();
		int cols = matrixA.ColumnCount();

		double[][] resultData = new double[rows][];
		for (int i = 0; i < rows; i++)
		{
			resultData[i] = new double[cols];
			for (int j = 0; j < cols; j++)
				resultData[i][j] = matrixA.Values[i][j] - matrixB.Values[i][j];
		}

		return new Matrix(resultData);
	}

	// Operator for scalar matrix
	public static Matrix operator *(double scalar, Matrix matrix)
	{
		int rows = matrix.RowCount();
		int cols = matrix.ColumnCount();

		double[][] resultData = new double[rows][];
		for (int i = 0; i < rows; i++)
		{
			resultData[i] = new double[cols];
			for (int j = 0; j < cols; j++)
				resultData[i][j] = scalar * matrix.Values[i][j];
		}

		return new(resultData);
	}

	public static Matrix operator *(Matrix matrix, double scalar) => scalar * matrix;

	// Operator for dot product of 2 matrices
	public static Matrix operator *(Matrix matrixA, Matrix matrixB)
	{
		if (matrixA.ColumnCount() != matrixB.RowCount()) throw new InvalidOperationException($"[E] Incompatible dimensions");

		int rows = matrixA.RowCount();
		int cols = matrixB.ColumnCount();
		int innerDim = matrixA.ColumnCount(); // matrixB.Rows

		// Init result matrix
		double[][] resultData = new double[rows][];
		for (int i = 0; i < rows; i++) resultData[i] = new double[cols];

		// Get dot product
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				double sum = 0;
				for (int k = 0; k < innerDim; k++)
					sum += matrixA.Values[i][k] * matrixB.Values[k][j];

				resultData[i][j] = sum;
			}
		}

		return new Matrix(resultData);
	}

	// Matrix summary
	public override string ToString()
	{
		var previewRows = Head();

		var content = previewRows.Select(
			(row, index) => $"[{index}]: {string.Join(", ", row.Take(10))}" + (row.Length > 10 ? ", ..." : "")
		);

		// More to come
		return string.Join(Environment.NewLine, content);
	}
}