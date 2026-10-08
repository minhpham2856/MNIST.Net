namespace MNIST.Core.Data;

public class Matrix
{
	public float[][] Values { get; set; } = Array.Empty<float[]>();

	public Matrix() { }
	public Matrix(float[][] values) { Values = values; }
	public Matrix(int rows, int columns)
	{
		Values = new float[rows][];
		for (int i = 0; i < rows; i++) Values[i] = new float[columns];
	}

	// Basic matrix info getters
	public int RowCount() => Values.Length;
	public int ColumnCount() => IsEmpty() ? 0 : Values[0].Length;
	public bool IsEmpty() => Values.Length == 0;
	public void Dimension() => Console.WriteLine($"[Rows: {RowCount()}, Columns: {ColumnCount()}]");
	public float[][] Head(int rowCount = 5) => Values.Take(rowCount).ToArray();

	// Add a row to the matrix
	public void Add(float[] row) => Values = Values.Append(row).ToArray();

	// Get a row/ column from the matrix
	public Matrix GetRow(int index) => new() { Values = new[] { Values[index] } };

	public Matrix GetColumn(int index)
	{
		var column = new float[RowCount()];
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
	public void Transpose()
	{
		if (IsEmpty()) return;

		int rowCount = RowCount();
		int columnCount = ColumnCount();

		var transposed = new float[columnCount][];
		for (int column = 0; column < columnCount; column++)
		{
			var row = new float[rowCount];
			for (int i = 0; i < rowCount; i++) row[i] = Values[i][column];

			transposed[column] = row;
		}

		Values = transposed;
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