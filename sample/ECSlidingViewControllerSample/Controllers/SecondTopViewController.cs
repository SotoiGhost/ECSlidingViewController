using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

using ECSlidingViewController1X;

namespace ECSlidingViewControllerSample
{
	partial class SecondTopViewController : UIViewController
	{
		public SecondTopViewController () : base ()
		{
		}

		public SecondTopViewController (IntPtr handle) : base (handle)
		{
		}

		partial void RevealMenu (UIBarButtonItem sender)
		{
			SlidingViewExtension.SlidingViewController (this).AnchorTopViewTo (ECSide.Right);
		}
	}
}
