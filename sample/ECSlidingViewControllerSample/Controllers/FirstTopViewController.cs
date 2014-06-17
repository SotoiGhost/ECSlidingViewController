using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

using ECSlidingViewController1X;

namespace ECSlidingViewControllerSample
{
	partial class FirstTopViewController : UIViewController
	{
		public FirstTopViewController () : base ()
		{
		}

		public FirstTopViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			View.Layer.ShadowOpacity = 0.75f;
			View.Layer.ShadowRadius = 10f;
			View.Layer.ShadowColor = UIColor.Black.CGColor;

			if (!(SlidingViewExtension.SlidingViewController (this).UnderLeftViewController is MenuViewController)) {
				SlidingViewExtension.SlidingViewController (this).UnderLeftViewController = Storyboard.InstantiateViewController ("MenuViewControllerID") as MenuViewController;
			}

			// UnderRightViewController

			View.AddGestureRecognizer (SlidingViewExtension.SlidingViewController (this).PanGesture ());
		}

		partial void RevealMenu (UIBarButtonItem sender)
		{
			SlidingViewExtension.SlidingViewController (this).AnchorTopViewTo (ECSide.Right);
		}
	}
}
