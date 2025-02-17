﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// Application-wide defaults for <see cref="HxCard"/>.
	/// </summary>
	public class CardDefaults
	{
		/// <summary>
		/// Additional CSS classes for the card-container.
		/// </summary>
		public string CssClass { get; set; }

		/// <summary>
		/// Additional CSS class for the header.
		/// </summary>
		public string HeaderCssClass { get; set; }

		/// <summary>
		/// Additional CSS class for the body.
		/// </summary>
		public string BodyCssClass { get; set; }

		/// <summary>
		/// Additional CSS class for the footer.
		/// </summary>
		public string FooterCssClass { get; set; }
	}
}
