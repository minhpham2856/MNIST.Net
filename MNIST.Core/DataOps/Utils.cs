using CsvHelper;
using System.Globalization;
using System;

namespace MNIST.Core.Data;

public static class DataUtils
{
	// Read csv from DataSets/...
	public static Matrix Read(string fileName)
	{
		string filePath = Path.Combine(AppContext.BaseDirectory, "DataSets", fileName);

		using var reader = new StreamReader(filePath);
		using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

		var matrix = new Matrix();

		while (csv.Read())
		{
			var row = csv.Parser.Record!.Select(double.Parse).ToArray();
			matrix.Add(row);
		}

		return matrix;
	}
}

public static class MathUtils
{
	// Fills a matrix with random 0s and 1s
	public static Matrix FillRandom(this Matrix matrix, double balancer = 0)
	{
		var random = Random.Shared;

		for (int i = 0; i < matrix.RowCount(); i++)
		{
			for (int j = 0; j < matrix.ColumnCount(); j++)
			{
				matrix.Values[i][j] = random.Next(2) + balancer;
			}
		}

		return matrix;
	}

	public static Matrix ActivateReLU(Matrix matrix)
	{
		for (int i = 0; i < matrix.RowCount(); i++)
		{
			for (int j = 0; j < matrix.ColumnCount(); j++)
			{
				matrix.Values[i][j] = Math.Max(0, matrix.Values[i][j]);
			}
		}
		return matrix;
	}

	public static Matrix ActivateSoftmax(Matrix matrix)
	{
		// e^Xi / sum(e^Xj)

		var sum = 0.0;
		for (int i = 0; i < matrix.RowCount(); i++)
		{
			for (int j = 0; j < matrix.ColumnCount(); j++)
			{
				sum += Math.Exp(matrix.Values[i][j]);
			}
		}

		for (int i = 0; i < matrix.RowCount(); i++)
		{
			for (int j = 0; j < matrix.ColumnCount(); j++)
			{
				matrix.Values[i][j] = Math.Exp(matrix.Values[i][j]) / sum;
			}
		}


		return matrix;
	}
}