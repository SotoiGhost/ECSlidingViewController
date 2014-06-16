using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ECSlidingViewControllerSDK;
using MonoTouch.CoreAnimation;

namespace TransitionFun
{
	public class SGFoldAnimationController : NSObject, IUIViewControllerAnimatedTransitioning, IECSlidingViewControllerDelegate
	{
		public SGFoldAnimationController ()
		{
		}

		#region ECSlidingViewControllerDelegate

		[Export ("slidingViewController:animationControllerForOperation:topViewController:")]
		public IUIViewControllerAnimatedTransitioning AnimationControllerForOperation (ECSlidingViewController slidingVC, 
											       ECSlidingViewControllerOperation operation, 
											       UIViewController topVC)
		{
			return this;
		}

		#endregion

		#region UIViewControllerAnimatedTransitioning

		public double TransitionDuration (IUIViewControllerContextTransitioning transitioningContext)
		{
			return 0.25;
		}

		public void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{

			UIViewController topVC = transitionContext.GetViewControllerForKey (ECTransitionContext.TopViewControllerKey);
			UIViewController toVC = transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey);
			UIView containerView = transitionContext.ContainerView;
			RectangleF topViewInitialFrame = transitionContext.GetInitialFrameForViewController (topVC);
			RectangleF topViewFinalFrame = transitionContext.GetFinalFrameForViewController (topVC);
			float revealWidth;
			bool isResetting = false;

			topVC.View.Frame = topViewInitialFrame;

			CATransform3D transform = CATransform3D.Identity;
			transform.m34 = -0.002f;
			containerView.Layer.SublayerTransform = transform;

			UIViewController underVC;

			if (topVC == toVC) {
				underVC = transitionContext.GetViewControllerForKey (ECTransitionContext.UnderLeftControllerKey);
				revealWidth = transitionContext.GetInitialFrameForViewController (topVC).X;
				isResetting = true;
			} else {
				underVC = transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey);
				revealWidth = transitionContext.GetFinalFrameForViewController (topVC).X;
				isResetting = false;
			}

			RectangleF underViewFrame;

			RectangleF underViewInitialFrame = transitionContext.GetInitialFrameForViewController (underVC);
			RectangleF underViewFinalFrame = transitionContext.GetFinalFrameForViewController (underVC);

			if (underViewInitialFrame.IsEmpty)
				underViewFrame = underViewFinalFrame;
			else
				underViewFrame = underViewInitialFrame;

			UIView underView = underVC.View;

			underView.Frame = underViewFrame;
			underView.RemoveFromSuperview ();

			float underViewHalfwayPoint = revealWidth / 2;
			RectangleF leftSideFrame = new RectangleF (0, 0, underViewHalfwayPoint, underView.Bounds.Height);
			RectangleF rightSideFrame = new RectangleF (underViewHalfwayPoint, 0, underViewHalfwayPoint, underView.Bounds.Height);

			UIView leftSideView = underView.ResizableSnapshotView (leftSideFrame, true, UIEdgeInsets.Zero);
			UIView rightSideView = underView.ResizableSnapshotView (rightSideFrame, true, UIEdgeInsets.Zero);

			leftSideView.Layer.AnchorPoint = new PointF (0f, 0.5f);
			leftSideView.Frame = leftSideFrame;

			rightSideView.Layer.Frame = rightSideFrame;
			rightSideView.Layer.AnchorPoint = new PointF (1, 0);

			if (isResetting)
				UnfoldLayers (leftSideView.Layer, rightSideView.Layer);
			else
				FoldLayers (leftSideView.Layer, rightSideView.Layer);

			containerView.Layer.InsertSublayerBelow (leftSideView.Layer, topVC.View.Layer);
			containerView.Layer.InsertSublayerBelow (rightSideView.Layer, topVC.View.Layer);

			double duration = TransitionDuration (transitionContext);
			UIView.Animate (duration, () => {
				UIView.SetAnimationCurve (UIViewAnimationCurve.Linear);

				topVC.View.Frame = topViewFinalFrame;

				if (isResetting)
					FoldLayers (leftSideView.Layer, rightSideView.Layer);
				else
					UnfoldLayers (leftSideView.Layer, rightSideView.Layer);
			}, () => {
				containerView.Layer.SublayerTransform = CATransform3D.Identity;

				leftSideView.RemoveFromSuperview ();
				rightSideView.RemoveFromSuperview ();

				bool topViewReset = (isResetting && !transitionContext.TransitionWasCancelled) || 
						    (!isResetting && transitionContext.TransitionWasCancelled);

				if (transitionContext.TransitionWasCancelled)
					topVC.View.Frame = transitionContext.GetInitialFrameForViewController (topVC);
				else
					topVC.View.Frame = transitionContext.GetFinalFrameForViewController (topVC);

				if (topViewReset)
					underView.RemoveFromSuperview ();
				else {
					if (transitionContext.TransitionWasCancelled)
						underView.Frame = transitionContext.GetInitialFrameForViewController (underVC);
					else
						underView.Frame = transitionContext.GetFinalFrameForViewController (underVC);

					containerView.InsertSubviewBelow (underView, topVC.View);
				}

				transitionContext.CompleteTransition (true);
			});
		}

		#endregion

		#region Private Methods

		void FoldLayers (CALayer leftSide, CALayer rightSide)
		{
			leftSide.Transform = CATransform3D.MakeRotation ((float)Math.PI / 2f, 0, 1, 0);

			rightSide.Position = new PointF (0, 0);
			rightSide.Transform = CATransform3D.MakeRotation ((float)-Math.PI / 2f, 0, 1, 0);
		}

		void UnfoldLayers (CALayer leftSide, CALayer rightSide)
		{
			leftSide.Transform = CATransform3D.MakeRotation (0, 0, 1, 0);

			rightSide.Position = new PointF (rightSide.Bounds.Width * 2, 0);
			rightSide.Transform = CATransform3D.MakeRotation (0, 0, 1, 0);
		}

		#endregion
	}
}