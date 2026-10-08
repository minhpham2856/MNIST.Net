using CsvHelper;
using System.Globalization;

namespace MNIST.Core.Data;

public static class DataUtils
{
	public static Table Read(string filePath)
	{
		using var reader = new StreamReader(filePath);
		using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

		var table = new Table();

		while (csv.Read())
		{
			var row = csv.Parser.Record!.Select(int.Parse).ToArray();
			table.Add(row);
		}

		return table;
	}
}