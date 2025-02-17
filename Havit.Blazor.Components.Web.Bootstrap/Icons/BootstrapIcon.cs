﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// Bootstrap icon.
	/// </summary>
	/// <remarks>
	/// To bootstrap icons update you need:
	/// - Update wwwroot\fonts\bootstrap-icons.woff
	/// - Update wwwroot\fonts\bootstrap-icons.woff2
	/// - Update wwwroot\bootstrap-icons.css
	/// - Update bootstrap-icons.json in this folder.
	/// BootstrapIcon.Xy properties will be generated by a source generator.
	/// </remarks>
	public partial class BootstrapIcon : IconBase
	{
		/// <inheritdoc cref="IconBase.RendererComponentType" />
		public override Type RendererComponentType => typeof(HxBootstrapIcon);

		/// <summary>
		/// Name of the bootstrap icon.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Constructor. Private.
		/// </summary>
		private BootstrapIcon(string name)
		{
			Name = name;
		}

		// Next members are generated using a source generator.
		// Tho source for the generator is the bootstrap-icons.json file located next to this one.
	}
}
