using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using ECSlidingViewControllerSDK;

namespace TransitionFun
{
	public class SGZoomAnimationController : NSObject, IUIViewControllerAnimatedTransitioning, IECSlidingViewControllerDelegate, IECSlidingViewControllerLayout
	{

		static readonly float ScaleFactor = 0.75f;
		public ECSlidingViewControllerOperation Operation { get; set; }

		public SGZoomAnimationController ()
		{
		}

		#region ECSlidingViewControllerDelegate

		[Export ("slidingViewController:animationControllerForOperation:topViewController:")]
		public IUIViewControllerAnimatedTransitioning AnimationControllerForOperation (ECSlidingViewController slidingVC, 
											       ECSlidingViewControllerOperation operation, 
											       UIViewController topVC)
		{
			Operation = operation;
			return this;
		}

		[Export ("slidingViewController:layoutControllerForTopViewPosition:")]
		public IECSlidingViewControllerLayout LayoutController (ECSlidingViewController slidingVC, 
									ECSlidingViewControllerTopViewPosition topViewPosition)
		{
			return this;
		}

		#endregion

		#region ECSlidingViewControllerLayout

		[Export ("slidingViewController:frameForViewController:topViewPosition:")]
		public RectangleF FrameForViewController (ECSlidingViewController slidingVC, 
							  UIViewController viewController, 
							  ECSlidingViewControllerTopViewPosition topViewPosition)
		{
			if (topViewPosition == ECSlidingViewControllerTopViewPosition.AnchoredRight &&
			    viewController == slidingVC.TopViewController)
				return TopViewAnchoredRightFrame (slidingVC);
			else
				return MonoTouch.CoreImage.CIImage.EmptyImage.Extent;
		}

		#endregion

		#region UIViewControllerAnimatedTransitioning

		public double TransitionDuration (IUIViewControllerContextTransitioning transitionContext)
		{
			return 0.25;
		}

		public void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{
			UIViewController topVC = transitionContext.GetViewControllerForKey (ECTransitionContext.TopViewControllerKey);
			UIViewController underLeftVC = transitionContext.GetViewControllerForKey (ECTransitionContext.UnderLeftControllerKey);
			UIView containerView = transitionContext.ContainerView;

			UIView topView = topVC.View;
			topView.Frame = containerView.Bounds;

			underLeftVC.View.Layer.Transform = CATransform3D.Identity;

			if (Operation == ECSlidingViewControllerOperation.Right) {
				containerView.InsertSubviewBelow (underLeftVC.View, topView);

				TopViewStartingState (topView, containerView.Bounds);
				UnderLeftViewStartingState (underLeftVC.View, containerView.Bounds);

				double duration = TransitionDuration (transitionContext);
				UIView.Animate (duration, () => {
					UnderLeftViewEndState (underLeftVC.View);
					TopViewAnchorRightEndState (topView, transitionContext.GetFinalFrameForViewController (underLeftVC));
				}, () => {
					if (transitionContext.TransitionWasCancelled) {
						underLeftVC.View.Frame = transitionContext.GetFinalFrameForViewController (underLeftVC);
						underLeftVC.View.Alpha = 1;
						TopViewStartingState (topView, containerView.Bounds);
					}

					transitionContext.CompleteTransition (true);
				});
			} else if (Operation == ECSlidingViewControllerOperation.ResetFromRight) {
				TopViewAnchorRightEndState (topView, transitionContext.GetInitialFrameForViewController (topVC));
				UnderLeftViewEndState (underLeftVC.View);

				double duration = TransitionDuration (transitionContext);
				UIView.Animate (duration, () => {
					UnderLeftViewStartingState (underLeftVC.View, containerView.Bounds);
					TopViewStartingState (topView, containerView.Bounds);
				}, () => {
					if (transitionContext.TransitionWasCancelled) {
						UnderLeftViewEndState (underLeftVC.View);
						TopViewAnchorRightEndState (topView, transitionContext.GetInitialFrameForViewController (topVC));
					} else {
						underLeftVC.View.Alpha = 1;
						underLeftVC.View.Layer.Transform = CATransform3D.Identity;
						underLeftVC.View.RemoveFromSuperview ();
					}

					transitionContext.CompleteTransition (true);
				});
			}
		}

		#endregion

		#region Private Methods

		RectangleF TopViewAnchoredRightFrame (ECSlidingViewController slidingVC)
		{
			RectangleF frame = slidingVC.View.Bounds;

			frame.X = slidingVC.AnchorRightRevealAmount;
			frame.Width = frame.Width * SGZoomAnimationController.ScaleFactor;
			frame.Height = frame.Height * SGZoomAnimationController.ScaleFactor;
			frame.Y = (slidingVC.View.Bounds.Height - frame.Height) / 2f;

			return frame;
		}

		void TopViewStartingState (UIView topView, RectangleF containerFrame)
		{
			topView.Layer.Transform = CATransform3D.Identity;
			topView.Layer.Position = new PointF (containerFrame.Width / 2f, containerFrame.Height / 2f);
		}

		void TopViewAnchorRightEndState (UIView topView, RectangleF anchoredFrame)
		{
			topView.Layer.Transform = CATransform3D.MakeScale (SGZoomAnimationController.ScaleFactor, 
									   SGZoomAnimationController.ScaleFactor, 
									   1);
			topView.Frame = anchoredFrame;
			topView.Layer.Position = new PointF (anchoredFrame.X + ((topView.Layer.Bounds.Width * SGZoomAnimationController.ScaleFactor) / 2f), 
							     topView.Layer.Position.Y);
		}

		void UnderLeftViewStartingState (UIView leftView, RectangleF containerFrame)
		{
			leftView.Alpha = 0;
			leftView.Frame = containerFrame;
			leftView.Layer.Transform = CATransform3D.MakeScale (1.25f, 1.25f, 1);
		}

		void UnderLeftViewEndState (UIView leftView)
		{
			leftView.Alpha = 1;
			leftView.Layer.Transform = CATransform3D.Identity;
		}

		#endregion
	}
}