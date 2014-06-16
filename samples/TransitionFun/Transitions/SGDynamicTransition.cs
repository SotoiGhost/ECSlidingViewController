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
	public class SGDynamicTransition : NSObject, IUIViewControllerInteractiveTransitioning, IUIDynamicAnimatorDelegate, IECSlidingViewControllerDelegate
	{

		#region Properties

		public ECSlidingViewController SlidingViewController { get; set; }

		ECSlidingAnimationController default_animation_controller;
		ECSlidingAnimationController DefaultAnimationController {
			get {
				if (default_animation_controller != null)
					return default_animation_controller;

				default_animation_controller = new ECSlidingAnimationController ();
				return default_animation_controller;
			}
			set { default_animation_controller = value; }
		}

		NSMutableArray left_edge_queue;
		NSMutableArray LeftEdgeQueue {
			get {
				if (left_edge_queue != null)
					return left_edge_queue;

				left_edge_queue = new NSMutableArray (5);
				return left_edge_queue;
			}
			set { left_edge_queue = value; }
		}

		IUIViewControllerContextTransitioning TransitionContext { get; set; }

		UIDynamicAnimator animator;
		UIDynamicAnimator Animator {
			get {
				if (animator != null)
					return animator;

				animator = new UIDynamicAnimator (SlidingViewController.TopViewController.View) {
					Delegate = this
				};
				animator.UpdateItemUsingCurrentState (SlidingViewController.TopViewController.View);

				return animator;
			}
			set { animator = value; }
		}

		UICollisionBehavior collision_behavior;
		UICollisionBehavior CollisionBehavior { 
			get { 
				if (collision_behavior != null)
					return collision_behavior;

				collision_behavior = new UICollisionBehavior (new [] { SlidingViewController.TopViewController.View });

				float containerHeight = SlidingViewController.View.Bounds.Height;
				float containerWidth = SlidingViewController.View.Bounds.Width;
				float revealAmount = SlidingViewController.AnchorRightRevealAmount;

				collision_behavior.AddBoundary (new NSString ("LeftEdge"), new PointF (-1f, 0f), new PointF (-1f, containerHeight));
				collision_behavior.AddBoundary (new NSString ("RightEdge"), new PointF (revealAmount + containerWidth + 1f, 0f), new PointF (revealAmount + containerWidth + 1, containerHeight));

				return collision_behavior;
			}
			set { collision_behavior = value; }
		}

		UIGravityBehavior gravity_behavior;
		UIGravityBehavior GravityBehavior { 
			get {
				if (gravity_behavior != null)
					return gravity_behavior;

				gravity_behavior = new UIGravityBehavior (new [] { SlidingViewController.TopViewController.View });

				return gravity_behavior;
			}
			set { gravity_behavior = value; }
		}

		UIPushBehavior push_behavior;
		UIPushBehavior PushBehavior { 
			get {
				if (push_behavior != null)
					return push_behavior;

				push_behavior = new UIPushBehavior (new [] { SlidingViewController.TopViewController.View }, UIPushBehaviorMode.Instantaneous);

				return push_behavior;
			}
			set { push_behavior = value; }	
		}

		UIDynamicItemBehavior top_view_behavior;
		UIDynamicItemBehavior TopViewBehavior { 
			get {
				if (top_view_behavior != null)
					return top_view_behavior;

				UIView topView = SlidingViewController.TopViewController.View;
				top_view_behavior = new UIDynamicItemBehavior (new [] { topView }) {
					// the density ranges from 1 to 5 for iPad to iPhone
					Density = 908800f / (topView.Bounds.Width *topView.Bounds.Height),
					Elasticity = 0,
					Resistance = 1
				};

				return top_view_behavior;
			}
			set { top_view_behavior = value; }
		}

		UIDynamicBehavior composite_behavior;
		UIDynamicBehavior CompositeBehavior { 
			get {
				if (composite_behavior != null)
					return composite_behavior;

				composite_behavior = new UIDynamicBehavior ();
				composite_behavior.AddChildBehavior (CollisionBehavior);
				composite_behavior.AddChildBehavior (GravityBehavior);
				composite_behavior.AddChildBehavior (PushBehavior);
				composite_behavior.AddChildBehavior (TopViewBehavior);

				var copyThis = (SGDynamicTransition)Copy ();

				composite_behavior.Action = () => {
					// stop the dynamic animation when the value of the left edge is the same 5 times in a row
					NSNumber leftEdge = new NSNumber (copyThis.SlidingViewController.TopViewController.View.Frame.X);
					copyThis.LeftEdgeQueue.Insert (leftEdge, 0);

					if (copyThis.LeftEdgeQueue.Count == 6)
						copyThis.LeftEdgeQueue.RemoveLastObject ();

					if (copyThis.LeftEdgeQueue.Count == 5 && ((NSArray)copyThis.LeftEdgeQueue.ValueForKey (new NSString ("distinctUnionOfObjects"))).Count == 1)
						copyThis.Animator.RemoveAllBehaviors ();
				};

				return composite_behavior;
			}
			set { composite_behavior = value; }
		}

		bool PositiveLeftToRight { get; set; }
		bool IsPanningRight { get; set; }
		bool IsInteractive { get; set; }
		float FullWidth { get; set; }

		RectangleF InitialTopViewFrame { get; set; }

		#endregion

		public SGDynamicTransition ()
		{
		}

		#region ECSlidingViewControllerDelegate

		[Export ("slidingViewController:animationControllerForOperation:topViewController:")]
		public IUIViewControllerAnimatedTransitioning AnimationControllerForOperation (ECSlidingViewController slidingVC, 
											       ECSlidingViewControllerOperation operation, 
											       UIViewController topVC)
		{
			return DefaultAnimationController;
		}

		[Export ("slidingViewController:interactionControllerForAnimationController:")]
		public IUIViewControllerInteractiveTransitioning InteractionController (ECSlidingViewController slidingVC, IUIViewControllerAnimatedTransitioning animationController)
		{
			SlidingViewController = slidingVC;
			return this;
		}

		#endregion

		#region UIViewControllerInteractiveTransitioning



		#endregion
	}
}