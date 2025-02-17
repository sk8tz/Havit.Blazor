﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// Checkbox input.
	/// </summary>
	public class HxInputCheckbox : HxInputBase<bool>
	{
		[Inject] protected IStringLocalizer<HxInputCheckbox> Localizer { get; set; }

		/// <inheritdoc cref="LabelValueRenderOrder" />
		protected override LabelValueRenderOrder RenderOrder => LabelValueRenderOrder.ValueLabel;

		/// <inheritdoc cref="HxInputBase{TValue}.CoreCssClass" />
		private protected override string CoreCssClass => !String.IsNullOrWhiteSpace(Label) ? "form-check" : null;

		/// <inheritdoc cref="HxInputBase{TValue}.CoreInputCssClass" />
		private protected override string CoreInputCssClass => "form-check-input";

		/// <inheritdoc cref="HxInputBase{TValue}.CoreLabelCssClass" />
		private protected override string CoreLabelCssClass => "form-check-label";

		/// <inheritdoc />
		protected override void BuildRenderInput(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "input");
			BuildRenderInput_AddCommonAttributes(builder, "checkbox");

			builder.AddAttribute(1000, "checked", BindConverter.FormatValue(CurrentValue));
			builder.AddAttribute(1001, "onchange", value: EventCallback.Factory.CreateBinder<bool>(this, value => CurrentValue = value, CurrentValue));
			builder.AddEventStopPropagationAttribute(1002, "onclick", true);
			builder.AddElementReferenceCapture(1003, elementReferece => InputElement = elementReferece);

			builder.CloseElement();
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
		{
			throw new NotSupportedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");
		}

		protected override void RenderChipValue(RenderTreeBuilder builder)
		{
			builder.AddContent(0, CurrentValue ? Localizer["ChipValueTrue"] : Localizer["ChipValueFalse"]);
		}
	}
}
