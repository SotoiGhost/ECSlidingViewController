using System;

namespace ECSlidingViewController1X
{
	/// <summary>
	/// Width of under views
	/// </summary>
	public enum ECViewWidthLayout {
		/// <summary>
		/// Under view will take up the full width of the screen
		/// </summary>
		FullWidth,

		/// <summary>
		/// Under view will have a fixed width equal to anchorRightRevealAmount or anchorLeftRevealAmount.
		/// </summary>
		FixedRevealWidth,

		/// <summary>
		/// Under view will have a variable width depending on rotation equal to the screen's width - anchorRightPeekAmount or anchorLeftPeekAmount.
		/// </summary>
		VariableRevealWidth
	}

	/// <summary>
	/// Side of screen
	/// </summary>
	public enum ECSide {
		/// <summary>
		/// Left side of screen
		/// </summary>
		Left,

		/// <summary>
		/// Right side of screen
		/// </summary>
		Right
	}

	/// <summary>
	/// Top view behavior while anchored
	/// </summary>
	[Flags]
	public enum ECResetStrategy {
		/// <summary>
		/// No reset strategy will be used
		/// </summary>
		None = 0,

		/// <summary>
		/// Tapping the top view will reset it
		/// </summary>
		Tapping = 1 << 0,

		/// <summary>
		/// Panning will be enabled on the top view. If it is panned and released towards the reset position it will reset, otherwise it will slide towards the anchored position.
		/// </summary>
		Panning = 1 << 1
	}
}

