using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using ECSlidingViewControllerSDK;

namespace LayoutDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public override UIWindow Window {
			get;
			set;
		}

		#region Instance Variables

		ECSlidingViewController SlidingViewController { get; set; }
		UIBarButtonItem btnAnchorRight;
		UIBarButtonItem btnAnchorLeft;

		#endregion

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);

			var topVC = new UIViewController ();
			var underLeftVC = new UIViewController ();
			var underRightVC = new UIViewController ();

			// Configure top view controller
			btnAnchorLeft = new UIBarButtonItem ("Left", UIBarButtonItemStyle.Plain, MoveViewToRight);
			btnAnchorRight = new UIBarButtonItem ("Right", UIBarButtonItemStyle.Plain, MoveViewToLeft);

			topVC.NavigationItem.Title = "Layout Demo";
			topVC.NavigationItem.LeftBarButtonItem = btnAnchorLeft;
			topVC.NavigationItem.RightBarButtonItem = btnAnchorRight;
			topVC.View.BackgroundColor = UIColor.White;

			var navController = new UINavigationController (topVC);

			// Configure under left view controller
			underLeftVC.View.Layer.BorderWidth = 20;
			underLeftVC.View.Layer.BackgroundColor = UIColor.FromWhiteAlpha (0.3f, 1f).CGColor;
			underLeftVC.View.Layer.BorderColor = UIColor.FromWhiteAlpha (0.6f, 1f).CGColor;
			underLeftVC.EdgesForExtendedLayout = UIRectEdge.Top | UIRectEdge.Bottom | UIRectEdge.Left; // don't go under the top view

			// Configure under right view controller
			underRightVC.View.Layer.BorderWidth = 20;
			underRightVC.View.Layer.BackgroundColor = UIColor.FromWhiteAlpha (0.3f, 1f).CGColor;
			underRightVC.View.Layer.BorderColor = UIColor.FromWhiteAlpha (0.6f, 1f).CGColor;
			underRightVC.EdgesForExtendedLayout = UIRectEdge.Top | UIRectEdge.Bottom | UIRectEdge.Right; // don't go under the top view

			// Configure sliding view controller
			SlidingViewController = ECSlidingViewController.SlidingWithTopViewController (navController);
			SlidingViewController.UnderLeftViewController = underLeftVC;
			SlidingViewController.UnderRightViewController = underRightVC;

			// Enable swiping on the top view
			navController.View.AddGestureRecognizer (SlidingViewController.PanGesture);

			// Configure anchored layout
			SlidingViewController.AnchorRightPeekAmount = 20f;
			SlidingViewController.AnchorLeftRevealAmount = 250f;

			Window.RootViewController = SlidingViewController;

			Window.MakeKeyAndVisible ();
			return true;
		}

		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

		void MoveViewToRight (object sender, EventArgs e)
		{
			SlidingViewController.AnchorTopViewToRight (true);
		}

		void MoveViewToLeft (object sender, EventArgs e)
		{
			SlidingViewController.AnchorTopViewToLeft (true);
		}
	}
}

