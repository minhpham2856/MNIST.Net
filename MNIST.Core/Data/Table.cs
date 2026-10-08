using System;
using System.Collections.Generic;
using System.Text;

namespace MNIST.Core.Data;

public class Table
{
	public List<int[]> Rows { get; set; } = new();

	public int RowCount() => Rows.Count;
	public int ColumnCount() => IsEmpty() ? 0 : Rows[0].Length;
	public bool IsEmpty() => Rows.Count == 0;
	public void Dimension() => Console.WriteLine($"[{RowCount()}, {ColumnCount()}]");

	public void Add(int[] row) => Rows.Add(row);

	public List<int[]> Head(int rowCount = 5) => Rows.Take(rowCount).ToList();

	public Table Transpose()
	{
		if (IsEmpty()) return new();

		int rowCount = RowCount();
		int columnCount = ColumnCount();

		var transposed = new Table { Rows = new List<int[]>(columnCount) };

		for (int column = 0; column < columnCount; column++)
		{
			var row = new int[rowCount];
			for (int i = 0; i < rowCount; i++) row[i] = Rows[i][column];

			transposed.Rows.Add(row);
		}

		return transposed;
	}

	public override string ToString()
	{
		var previewRows = Head();

		var content = previewRows.Select(
			(row, index) => $"[{index}]: {string.Join(", ", row.Take(10))}" + (row.Length > 10 ? ", ..." : "")
		);

		return string.Join(Environment.NewLine, content);
	}

}
