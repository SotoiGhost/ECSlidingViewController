using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

using ECSlidingViewController1X;

namespace ECSlidingViewControllerSample
{
	partial class InitialSlidingViewController : ECSlidingViewController
	{
		#region Constructors

		public InitialSlidingViewController () : base ()
		{
		}

		public InitialSlidingViewController (IntPtr handle) : base (handle)
		{
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (UIDevice.CurrentDevice.CheckSystemVersion (6, 1)) {
				AdjustChildViewHeightForStatusBar = true;
				StatusBarBackgroundView.BackgroundColor = UIColor.Black;
			}

			TopViewController = Storyboard.InstantiateViewController ("FirstTopViewControllerID") as UIViewController;
			AddPanGestureRecognizerToTopViewSnapshot = true;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}
