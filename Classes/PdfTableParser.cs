using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{

	class PdfTableParser {
		public static string BorderOnly = @"[┌─┬└]{10,}";
		public static string Words = @"([\w -\./\\]+)";

		private static List<string> StringJoin(List<List<string>> strings) {
			List<string> str = new List<string>();
			foreach (var item in strings) {
				str.Add(String.Join("", item));
			}
			return str;
		}

		public static List<List<string>> Parse(string file_content) {
			var regexBorderOnly = new Regex(BorderOnly, RegexOptions.IgnoreCase);
			var regexWords = new Regex(Words, RegexOptions.IgnoreCase);

			bool tableStarted = false;
			List<List<string>> data = new List<List<string>>();
			List<List<string>> rowData = new List<List<string>>();
			foreach (var line in file_content.Split("\n")) {
				var match = regexBorderOnly.Match(line);
				if (match.Success) {
					tableStarted = true;

					if (rowData.Count > 0) {
						data.Add(StringJoin(rowData));
					}
					rowData = new List<List<string>>();
					rowData.Add(new List<string>());
					continue;
				}

				if (tableStarted) {
					var matchWords = regexWords.Matches(line);
					int i = 0;
					foreach (var matchWord in regexWords.Matches(line)) {
						if (rowData.Count <= i)
							rowData.Add(new List<string>());
						rowData[i].Add(matchWord.ToString().Trim().Replace(" ", ""));
						i++;
					}
				}
			}
			return data;
		}
	}
}
