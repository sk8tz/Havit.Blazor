﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Havit.Blazor.Components.Web.Bootstrap.Documentation.Shared.Components
{
	public partial class ComponentApiDocCssVariable
	{
		/// <summary>
		/// Name of the css variable.
		/// </summary>
		[Parameter] public string Name { get; set; }
		/// <summary>
		/// Default value for the css variable.
		/// </summary>
		[Parameter] public string Default { get; set; }
		[Parameter] public RenderFragment ChildContent { get; set; }
	}
}
