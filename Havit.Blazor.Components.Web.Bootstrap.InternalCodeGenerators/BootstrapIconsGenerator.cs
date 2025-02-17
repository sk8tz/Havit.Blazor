﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Havit.Blazor.Components.Web.Bootstrap.InternalCodeGenerators
{
	[Generator]
	public class BootstrapIconsGenerator : ISourceGenerator
	{
		public void Initialize(GeneratorInitializationContext context)
		{
		}

		public void Execute(GeneratorExecutionContext context)
		{
			foreach (var syntaxTree in context.Compilation.SyntaxTrees)
			{
				if ("BootstrapIcon" != Path.GetFileNameWithoutExtension(syntaxTree.FilePath))
				{
					continue;
				}

				string jsonFilename = Path.Combine(Path.GetDirectoryName(syntaxTree.FilePath), "bootstrap-icons.json");
				//JsonElement jsonRoot = JsonSerializer.Deserialize<JsonElement>(File.ReadAll(jsonFilename));

				string[] lines = File.ReadAllLines(jsonFilename);

				StringBuilder sb = new StringBuilder();
				sb.AppendLine("namespace Havit.Blazor.Components.Web.Bootstrap");
				sb.AppendLine("{");
				sb.AppendLine("	public partial class BootstrapIcon");
				sb.AppendLine("	{");
				foreach (string line in lines)
				{
					if (!line.Contains("\""))
					{
						continue;
					}

					string iconName = GetIconNameFromLine(line);
					string propertyName = GetPropertyNameFromIconName(iconName);
					string fieldName = GetFieldNameFromPropertyName(propertyName);
					sb.AppendLine($"		private static BootstrapIcon {fieldName};");
					sb.AppendLine($"		public static BootstrapIcon {propertyName} => {fieldName} ??= new BootstrapIcon(\"{iconName}\");");
				}
				sb.AppendLine("	}");
				sb.AppendLine("}");

				context.AddSource($"BootstrapIcon.generated.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
			}
		}

		private string GetIconNameFromLine(string line)
		{
			int s = line.IndexOf("\"");
			int e = line.LastIndexOf("\"");
			return line.Substring(s + 1, e - s - 1);
		}

		private string GetPropertyNameFromIconName(string iconName)
		{
			string[] segments = iconName.Split('-');
			return String.Join("", segments.Select(segment => segment.Substring(0, 1).ToUpper() + segment.Substring(1)));
		}

		private string GetFieldNameFromPropertyName(string propertyName)
		{
			// One of the icons is "new" which is a C# keyword. We are adding "_" to not conflict the field name with a keyword.
			return "_" + propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
		}

	}
}
