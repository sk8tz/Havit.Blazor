﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Havit.Blazor.Components.Web.Bootstrap.Internal
{
	/// <summary>
	/// Extends <see cref="IFormValueComponent"/> with properties for rendering input groups.
	/// </summary>
	public interface IFormValueComponentWithInputGroups : IFormValueComponent
	{
		/// <summary>
		/// Custom CSS class to render with input-group span.
		/// </summary>
		string InputGroupCssClass => null;

		/// <summary>
		/// Input group size.
		/// </summary>
		InputSize InputGroupSize => InputSize.Regular;

		/// <summary>
		/// Input-group-text at the beginning of the input.
		/// In comparison to <see cref="InputGroupStartTemplate"/> this property is rendered as <code>.input-group-text</code>.
		/// </summary>
		string InputGroupStartText => null;

		/// <summary>
		/// Input-group at the beginning of the input.
		/// </summary>
		RenderFragment InputGroupStartTemplate => null;

		/// <summary>
		/// Input-group-text at the end of the input.
		/// In comparison to <see cref="InputGroupEndTemplate"/> this property is rendered as <code>.input-group-text</code>.
		/// </summary>
		string InputGroupEndText => null;

		/// <summary>
		/// Input-group at the end of the input.
		/// </summary>
		RenderFragment InputGroupEndTemplate => null;
	}
}
