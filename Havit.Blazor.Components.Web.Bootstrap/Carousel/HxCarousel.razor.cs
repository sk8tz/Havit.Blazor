﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// A slideshow component for cycling through elements—images or slides of text—like a carousel.
	/// </summary>
	public partial class HxCarousel : IAsyncDisposable
	{
		internal const string ItemsRegistrationCascadingValueName = "ItemsRegistration";

		/// <summary>
		/// Content of the carousel.
		/// </summary>
		[Parameter] public RenderFragment ChildContent { get; set; }

		/// <summary>
		/// Carousel CSS class. Added to root div <c>.carousel</c>.
		/// </summary>
		[Parameter] public string CssClass { get; set; }

		/// <summary>
		/// Display controls to switch between slides.
		/// </summary>
		[Parameter] public bool Controls { get; set; }

		/// <summary>
		/// Display indicators showing which slide is active. Can also be used to switch between slides.
		/// </summary>
		[Parameter] public bool Indicators { get; set; }

		/// <summary>
		/// Show controls, captions, etc. to dark colors.
		/// </summary>
		[Parameter] public bool Dark { get; set; }

		/// <summary>
		/// Animate slides with a fade transition instead of slide.
		/// </summary>
		[Parameter] public bool Crossfade { get; set; }

		/// <summary>
		/// Delay for automatically switching slides. Default is <c>3000 ms</c>.
		/// </summary>
		[Parameter] public int? Interval { get; set; } = 3000;

		/// <summary>
		/// Enable or disable swiping left/right on touchscreen devices to move between slides.
		/// Default is <c>true</c> (enabled).
		/// </summary>
		[Parameter] public bool TouchSwiping { get; set; } = true;

		/// <summary>
		/// Is fired when the current slide is changed (at the very start of the sliding transition).
		/// </summary>
		[Parameter] public EventCallback OnSlide { get; set; }

		/// <summary>
		/// Is fired when the current slide is changed (once the transition is completed).
		/// </summary>
		[Parameter] public EventCallback OnSlid { get; set; }

		/// <summary>
		/// Carousel ride (autoplay) behavior. Default is <see cref="CarouselRide.Carousel"/> (autoplays the carousel on load).
		/// </summary>
		[Parameter] public CarouselRide Ride { get; set; } = CarouselRide.Carousel;


		[Inject] protected IJSRuntime JSRuntime { get; set; }


		private IJSObjectReference jsModule;
		private string id = "hx" + Guid.NewGuid().ToString("N");
		private DotNetObjectReference<HxCarousel> dotnetObjectReference;
		private List<HxCarouselItem> items;
		private CollectionRegistration<HxCarouselItem> itemsRegistration;
		private bool isDisposed = false;

		public HxCarousel()
		{
			dotnetObjectReference = DotNetObjectReference.Create(this);
			items = new List<HxCarouselItem>();
			itemsRegistration = new CollectionRegistration<HxCarouselItem>(items,
				this.StateHasChanged,
				() => isDisposed);
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender)
			{
				await EnsureJsModule();
				await jsModule.InvokeVoidAsync("Initialize", id, dotnetObjectReference, Ride.ToString().ToLower());
			}
		}

		protected async Task EnsureJsModule()
		{
			jsModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Havit.Blazor.Components.Web.Bootstrap/" + nameof(HxCarousel) + ".js");
		}


		[JSInvokable("HxCarousel_HandleSlide")]
		public async Task HandleSlide()
		{
			await OnSlide.InvokeAsync();
		}

		[JSInvokable("HxCarousel_HandleSlid")]
		public async Task HandleSlid()
		{
			await OnSlid.InvokeAsync();
		}

		/// <summary>
		/// Slides to an element with the coresponding index.
		/// </summary>
		public async Task SlideToAsync(int index)
		{
			await EnsureJsModule();
			await jsModule.InvokeVoidAsync("SlideTo", id, index);
		}

		/// <summary>
		/// Slides to the previous item (to the left).
		/// </summary>
		public async Task SlideToPreviousItemAsync()
		{
			await EnsureJsModule();
			await jsModule.InvokeVoidAsync("Previous", id);
		}

		/// <summary>
		/// Slides to the next item (to the right).
		/// </summary>
		public async Task SlideToNextItemAsync()
		{
			await EnsureJsModule();
			await jsModule.InvokeVoidAsync("Next", id);
		}

		/// <summary>
		/// Start cycling between slides.
		/// </summary>
		public async Task CycleAsync()
		{
			await EnsureJsModule();
			await jsModule.InvokeVoidAsync("Cycle", id);
		}

		/// <summary>
		/// Pause cycling.
		/// </summary>
		public async Task PauseAsync()
		{
			await EnsureJsModule();
			await jsModule.InvokeVoidAsync("Pause", id);
		}

		/// <summary>
		/// Dispose the carousel.
		/// </summary>
		public async ValueTask DisposeAsync()
		{
			isDisposed = true;

			if (jsModule is not null)
			{
				await jsModule.InvokeVoidAsync("Dispose", id);
				await jsModule.DisposeAsync();
			}
		}
	}
}
