using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Services
{
	public class MarkdownWriter : IDisposable
	{
		private readonly StreamWriter writer;
		private bool disposedValue;

		public MarkdownWriter(string fileName)
		{
			this.writer = new StreamWriter(fileName);
		}


		public MarkdownWriter Write(object value)
		{
			if (this.disposedValue)
				throw new ObjectDisposedException(GetType().Name);

			writer.Write(value);
			return this;
		}
		public MarkdownWriter WriteLine(object value = null)
		{
			if (this.disposedValue)
				throw new ObjectDisposedException(GetType().Name);

			writer.WriteLine(value);
			return this;
		}

		public MarkdownWriter WriteParagraph(string text)
		{
			writer.WriteLine(text);
			writer.WriteLine();
			return this;
		}


		public MarkdownWriter WriteHeader1(string value)
		{
			if (this.disposedValue)
				throw new ObjectDisposedException(GetType().Name);

			writer.Write("# ");
			writer.WriteLine(value);
			writer.WriteLine();
			return this;
		}


		public MarkdownWriter WriteHeader2(string value)
		{
			if (this.disposedValue)
				throw new ObjectDisposedException(GetType().Name);

			writer.Write("## ");
			writer.WriteLine(value);
			writer.WriteLine();
			return this;
		}


		public MarkdownWriter WriteHeader3(string value)
		{
			if (this.disposedValue)
				throw new ObjectDisposedException(GetType().Name);

			writer.Write("### ");
			writer.WriteLine(value);
			writer.WriteLine();
			return this;
		}


		public MarkdownWriter WriteList(params string[] lines)
		{
			return WriteList(0, lines);
		}
		public MarkdownWriter WriteList(int indentLevel, params string[] lines)
		{
			var indent = new string(' ', indentLevel * 2);

			foreach (var line in lines)
			{
				writer.Write(indent);
				writer.Write("- ");
				writer.WriteLine(line);
			}
			writer.WriteLine();
			return this;
		}


		public MarkdownWriter WriteTable<TRow>(IReadOnlyList<TRow> collection, string[] rowHeaders, Func<TRow, string[]> rowData)
		{
			return WriteTable(collection, () => rowHeaders, rowData);
		}

		public MarkdownWriter WriteTable<TRow>(IReadOnlyList<TRow> collection, Func<string[]> rowHeaders, Func<TRow, string[]> rowData)
		{
			var headers = rowHeaders();
			var rows = collection.Select(rowData).ToList();

			var columnWidths = new int[headers.Length];
			for (var i = 0; i < headers.Length; i++)
			{
				columnWidths[i] = Math.Max(headers[i].Length, rows.Max(row => row[i]?.Length ?? 0));
			}

			var header = "| " + string.Join(" | ", headers.Select((col, i) => col.PadRight(columnWidths[i]))) + " |";
			var separator = "|-" + string.Join("-|-", columnWidths.Select(colWidth => new string('-', colWidth))) + "-|";
			var body = string.Join(Environment.NewLine,
				 rows.Select(
					row => "| " + string.Join(" | ", row.Select((col, i) => col?.PadRight(columnWidths[i]))) + " |"
				 )
			);

			writer.WriteLine(header + Environment.NewLine + separator + Environment.NewLine + body);
			writer.WriteLine();
			return this;
		}

		public MarkdownWriter WriteCodeBlock(string code, string language = null)
		{
			writer.Write("```");
			if (language != null)
				writer.Write(language);
			writer.WriteLine();
			writer.WriteLine(code);
			writer.WriteLine("```");
			writer.WriteLine();
			return this;
		}
		public MarkdownWriter WriteCodeBlockStart(string language)
		{
			writer.Write("```");
			writer.Write(language);
			writer.WriteLine();
			return this;
		}
		public MarkdownWriter WriteCodeBlockEnd()
		{
			writer.WriteLine("```");
			return this;
		}




		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					writer.Dispose();
				}

				disposedValue = true;
			}
		}
	}
}
