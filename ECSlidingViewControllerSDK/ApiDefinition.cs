using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ECSlidingViewControllerSDK {

	[BaseType (typeof (NSObject))]
	interface ECPercentDrivenInteractiveTransition : IUIViewControllerInteractiveTransitioning {
		[Export ("animationController")]
		IUIViewControllerAnimatedTransitioning AnimationController { get; set; }

		[Export ("percentComplete", ArgumentSemantic.Assign)]
		float PercentComplete { get; }

		[Export ("cancelInteractiveTransition")]
		void CancelInteractiveTransition ();

		[Export ("finishInteractiveTransition")]
		void FinishInteractiveTransition ();
	}

	[BaseType (typeof (NSObject))]
	interface ECSlidingAnimationController : IUIViewControllerAnimatedTransitioning {
		[Export ("defaultTransitionDuration", ArgumentSemantic.Assign)]
		double DefaultTransitionDuration { get; set; }
	}

	[BaseType (typeof (ECPercentDrivenInteractiveTransition))]
	interface ECSlidingInteractiveTransition {
		[Export ("initWithSlidingViewController:")]
		IntPtr Constructor (ECSlidingViewController slidingVC);

		[Export ("updateTopViewHorizontalCenterWithRecognizer:")]
		void UpdateTopViewHorizontalCenter (UIPanGestureRecognizer recognizer);
	}

	[BaseType (typeof (UIStoryboardSegue))]
	interface ECSlidingSegue {
		[Export ("skipSettingTopViewController", ArgumentSemantic.Assign)]
		bool SkipSettingTopVC { get; set; }
	}

	interface IECSlidingViewControllerLayout { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface ECSlidingViewControllerLayout {
		[Export ("slidingViewController:frameForViewController:topViewPosition:")]
		RectangleF FrameForViewController (ECSlidingViewController slidingVC, UIViewController viewController, ECSlidingViewControllerTopViewPosition topViewPosition);
	}

	interface IECSlidingViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface ECSlidingViewControllerDelegate {
		[Export ("slidingViewController:animationControllerForOperation:topViewController:")]
		IUIViewControllerAnimatedTransitioning AnimationControllerForOperation (ECSlidingViewController slidingVC, ECSlidingViewControllerOperation operation, UIViewController topVC);

		[Export ("slidingViewController:interactionControllerForAnimationController:")]
		IUIViewControllerInteractiveTransitioning InteractionController (ECSlidingViewController slidingVC, IUIViewControllerAnimatedTransitioning animationController);

		[Export ("slidingViewController:layoutControllerForTopViewPosition:")]
		IECSlidingViewControllerLayout LayoutController (ECSlidingViewController slidingVC, ECSlidingViewControllerTopViewPosition topViewPosition);
	}

	delegate void ECSlidingViewControllerOnComplete ();

	[BaseType (typeof (UIViewController))]
	interface ECSlidingViewController : IUIViewControllerTransitionCoordinator, IUIViewControllerTransitionCoordinatorContext {
		[Static, Export ("slidingWithTopViewController:")]
		ECSlidingViewController SlidingWithTopViewController (UIViewController topVC);

		[Export ("initWithTopViewController:")]
		IntPtr Costructor (UIViewController topVC);

		[Export ("topViewController")]
		UIViewController TopViewController { get; set; }

		[Export ("underLeftViewController")]
		UIViewController UnderLeftViewController { get; set; }

		[Export ("underRightViewController")]
		UIViewController UnderRightViewController { get; set; }

		[Export ("anchorLeftPeekAmount", ArgumentSemantic.Assign)]
		float AnchorLeftPeekAmount { get; set; }

		[Export ("anchorLeftRevealAmount", ArgumentSemantic.Assign)]
		float AnchorLeftRevealAmount { get; set; }

		[Export ("anchorRightPeekAmount", ArgumentSemantic.Assign)]
		float AnchorRightPeekAmount { get; set; }

		[Export ("anchorRightRevealAmount", ArgumentSemantic.Assign)]
		float AnchorRightRevealAmount { get; set; }

		[Export ("anchorTopViewToRightAnimated:")]
		void AnchorTopViewToRight (bool animated);

		[Export ("anchorTopViewToRightAnimated:onComplete:")]
		void AnchorTopViewToRight (bool animated, ECSlidingViewControllerOnComplete onComplete);

		[Export ("anchorTopViewToLeftAnimated:")]
		void AnchorTopViewToLeft (bool animated);

		[Export ("anchorTopViewToLeftAnimated:onComplete:")]
		void AnchorTopViewToLeft (bool animated, ECSlidingViewControllerOnComplete onComplete);

		[Export ("resetTopViewAnimated:")]
		void ResetTopView (bool animated);

		[Export ("resetTopViewAnimated:onComplete:")]
		void ResetTopView (bool animated, ECSlidingViewControllerOnComplete onComplete);

		[Export ("topViewControllerStoryboardId")]
		string TopViewControllerStoryboardId { get; set; }

		[Export ("underLeftViewControllerStoryboardId")]
		string UnderLeftViewControllerStoryboardId { get; set; }

		[Export ("underRightViewControllerStoryboardId")]
		string UnderRightViewControllerStoryboardId { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)] [NullAllowed]
		IECSlidingViewControllerDelegate Delegate { get; set; }

		[Export ("topViewAnchoredGesture", ArgumentSemantic.Assign)]
		ECSlidingViewControllerAnchoredGesture TopViewAnchoredGesture { get; set; }

		[Export ("currentTopViewPosition", ArgumentSemantic.Assign)]
		ECSlidingViewControllerTopViewPosition CurrentTopViewPosition { get; }

		[Export ("panGesture")]
		UIPanGestureRecognizer PanGesture { get; }

		[Export ("resetTapGesture")]
		UITapGestureRecognizer ResetTapGesture { get; }

		[Export ("customAnchoredGestures")]
		UIGestureRecognizer[] CustomAnchoredGestures { get; set; }

		[Export ("defaultTransitionDuration", ArgumentSemantic.Assign)]
		double DefaultTransitionDuration { get; set; }
	}

	[BaseType (typeof (UIViewController))]
	[Category]
	interface ECSlidingViewControllerExtension {
		[Export ("slidingViewController")]
		ECSlidingViewController SlidingViewController ();
	}

}