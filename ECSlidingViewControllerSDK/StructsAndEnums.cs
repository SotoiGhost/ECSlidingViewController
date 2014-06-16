using System;
using MonoTouch.Foundation;

namespace ECSlidingViewControllerSDK
{
	public enum ECSlidingViewControllerOperation
	{
		None = 0,
		Left,
		Right,
		ResetFromLeft,
		ResetFromRight
	}

	public enum ECSlidingViewControllerTopViewPosition
	{
		AnchoredLeft = 0,
		AnchoredRight,
		Centered
	}

	[Flags]
	public enum ECSlidingViewControllerAnchoredGesture
	{
		None = 0,
		Panning  = 1 << 0,
		Tapping  = 1 << 1,
		Custom   = 1 << 2,
		Disabled = 1 << 3
	}

	public static class ECTransitionContext
	{
		public static NSString TopViewControllerKey { get { return new NSString ("ECTransitionContextTopViewControllerKey"); } }

		public static NSString UnderLeftControllerKey { get { return new NSString ("ECTransitionContextUnderLeftControllerKey"); } }

		public static NSString UnderRightControllerKey { get { return new NSString ("ECTransitionContextUnderRightControllerKey"); } }
	}
}