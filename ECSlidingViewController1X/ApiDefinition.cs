using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ECSlidingViewController1X
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     PointF Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_libraries
	//

	delegate void TopViewCenterMoved (float xPos);

	[BaseType (typeof (UIViewController))]
	interface ECSlidingViewController {
		/// <summary>
		/// Notification that gets posted when the underRight view will appear.
		/// </summary>
		[Field ("ECSlidingViewUnderRightWillAppear", "__Internal")]
		NSString ECUnderRightWillAppear { get; }

		/// <summary>
		/// Notification that gets posted when the underLeft view will appear.
		/// </summary>
		[Field ("ECSlidingViewUnderLeftWillAppear", "__Internal")]
		NSString ECUnderLeftWillAppear { get; }

		/// <summary>
		/// Notification that gets posted when the underLeft view will disappear.
		/// </summary>
		[Field ("ECSlidingViewUnderLeftWillDisappear", "__Internal")]
		NSString ECUnderLeftWillDisappear { get; }

		/// <summary>
		/// Notification that gets posted when the underRight view will disappear.
		/// </summary>
		[Field ("ECSlidingViewUnderRightWillDisappear", "__Internal")]
		NSString ECUnderRightWillDisappear { get; }

		/// <summary>
		/// Notification that gets posted when the top view is anchored to the left side of the screen.
		/// </summary>
		[Field ("ECSlidingViewTopDidAnchorLeft", "__Internal")]
		NSString ECTopDidAnchorLeft { get; }

		/// <summary>
		/// Notification that gets posted when the top view is anchored to the right side of the screen.
		/// </summary>
		[Field ("ECSlidingViewTopDidAnchorRight", "__Internal")]
		NSString ECTopDidAnchorRight { get; }

		/// <summary>
		/// Notification that gets posted when the top view will be centered on the screen.
		/// </summary>
		[Field ("ECSlidingViewTopWillReset", "__Internal")]
		NSString ECTopWillReset { get; }

		/// <summary>
		/// Notification that gets posted when the top view is centered on the screen.
		/// </summary>
		[Field ("ECSlidingViewTopDidReset", "__Internal")]
		NSString ECTopDidReset { get; }

		/// <summary>
		/// Gets or sets the view controller that will be visible when the top view is slide to the right.
		/// </summary>
		[Export ("underLeftViewController")]
		UIViewController UnderLeftViewController { get; set; }

		/// <summary>
		/// Gets or sets the view controller that will be visible when the top view is slide to the left.
		/// </summary>
		[Export ("underRightViewController")]
		UIViewController UnderRightViewController { get; set; }

		/// <summary>
		/// Gets or sets the top view controller.
		/// </summary>
		[Export ("topViewController")]
		UIViewController TopViewController { get; set; }

		/// <summary>
		/// Gets or sets the number of points the top view is visible when the top view is anchored to the left side.
		/// This value is fixed after rotation. If the number of points to reveal needs to be fixed, use anchorLeftRevealAmount.
		/// </summary>
		[Export ("topViewController", ArgumentSemantic.Assign)]
		float AnchorLeftPeekAmount { get; set; }

		/// <summary>
		/// Gets or sets the number of points the top view is visible when the top view is anchored to the right side.
		/// This value is fixed after rotation. If the number of points to reveal needs to be fixed, use anchorRightRevealAmount.
		/// </summary>
		[Export ("anchorRightPeekAmount", ArgumentSemantic.Assign)]
		float AnchorRightPeekAmount { get; set; }

		/// <summary>
		/// Gets or sets the number of points the under right view is visible when the top view is anchored to the left side.
		/// This value is fixed after rotation. If the number of points to peek needs to be fixed, use anchorLeftPeekAmount.
		/// </summary>
		[Export ("anchorLeftRevealAmount", ArgumentSemantic.Assign)]
		float AnchorLeftRevealAmount { get; set; }

		/// <summary>
		/// Gets or sets the number of points the under left view is visible when the top view is anchored to the right side.
		/// This value is fixed after rotation. If the number of points to peek needs to be fixed, use anchorRightPeekAmount.
		/// </summary>
		[Export ("anchorRightRevealAmount", ArgumentSemantic.Assign)]
		float AnchorRightRevealAmount { get; set; }

		/// <summary>
		/// Specifies whether or not the top view can be panned past the anchor point.
		/// Set to NO if you don't want to show the empty space behind the top and under view.
		/// By defaut, this is set to true
		/// </summary>
		[Export ("shouldAllowPanningPastAnchor", ArgumentSemantic.Assign)]
		bool AllowPanningPastAnchor { get; set; }

		/// <summary>
		/// Specifies if the user should be able to interact with the top view when it is anchored.
		/// By defaut, this is set to false
		/// </summary>
		[Export ("shouldAllowUserInteractionsWhenAnchored", ArgumentSemantic.Assign)]
		bool AllowUserInteractionsWhenAnchored { get; set; }

		/// <summary>
		/// Specifies if the top view snapshot requires a pan gesture recognizer.
		/// By default, this is set to false
		/// </summary>
		[Export ("shouldAddPanGestureRecognizerToTopViewSnapshot", ArgumentSemantic.Assign)]
		bool AddPanGestureRecognizerToTopViewSnapshot { get; set; }

		/// <summary>
		/// Specifies if the the child views should be shortened to accomodate the status bar. iOS 7 only.
		/// By default, this is set to false
		/// </summary>
		[Export ("shouldAdjustChildViewHeightForStatusBar", ArgumentSemantic.Assign)]
		bool AdjustChildViewHeightForStatusBar { get; set; }

		/// <summary>
		/// Specifies the behavior for the under left width.
		/// By default, this is set to ECViewWidthLayout.FullWidth
		/// </summary>
		[Export ("underLeftWidthLayout", ArgumentSemantic.Assign)]
		ECViewWidthLayout UnderLeftWidthLayout { get; set; }

		/// <summary>
		/// Specifies the behavior for the under right width
		/// By default, this is set to ECViewWidthLayout.FullWidth
		/// </summary>
		[Export ("underRightWidthLayout", ArgumentSemantic.Assign)]
		ECViewWidthLayout UnderRightWidthLayout { get; set; }

		/// <summary>
		/// Returns the strategy for resetting the top view when it is anchored.
		/// By default, this is set to ECPanning | ECTapping to allow both panning and tapping to reset the top view.
		/// If this is set to ECNone, then there must be a custom way to reset the top view otherwise it will stay anchored.
		/// </summary>
		[Export ("resetStrategy", ArgumentSemantic.Assign)]
		ECResetStrategy ResetStrategy { get; set; }

		/// <summary>
		/// Returns the magnitude of the X-axis velocity threshold used for determining whether or not to process a pan to the left or right
		/// By default, this is set to 100.
		/// </summary>
		[Export ("panningVelocityXThreshold", ArgumentSemantic.Assign)]
		ECResetStrategy PanningVelocityXThreshold { get; set; }

		/// <summary>
		/// Can be set to provide a continuous callback as the top view slides.
		/// Useful for animations synchronized to the sliding.
		/// </summary>
		[Export ("setTopViewCenterMoved:", ArgumentSemantic.Copy)]
		void SetTopViewCenterMovedHandler (TopViewCenterMoved topViewCenterMovedHandler);

		/// <summary>
		/// Gets or sets a view that is the same size and position as the status bar
		/// It is guaranteed to always be on top of the top and under views.
		/// </summary>
		[Export ("statusBarBackgroundView")]
		UIView StatusBarBackgroundView { get; set; }

		/// <summary>
		/// Returns a horizontal panning gesture for moving the top view.
		/// This is typically added to the top view or a top view's navigation bar.
		/// </summary>
		[Export ("panGesture")]
		UIPanGestureRecognizer panGesture ();

		/// <summary>
		/// Slides the top view in the direction of the specified side.
		/// A peek amount or reveal amount must be set for the given side. The top view will anchor to one of those specified values.
		/// </summary>
		/// <param name="side">The side for the top view to slide towards.</param>
		[Export ("anchorTopViewTo:")]
		void AnchorTopViewTo (ECSide side);

		/// <summary>
		/// Slides the top view in the direction of the specified side.
		/// A peek amount or reveal amount must be set for the given side. The top view will anchor to one of those specified values.
		/// </summary>
		/// <param name="side">The side for the top view to slide towards.</param>
		/// <param name="animationsHandler">Perform changes to properties that will be animated while top view is moved off screen. Can be nil.</param>
		/// <param name="onCompleteHandler">Executed after the animation is completed. Can be nil.</param>
		[Export ("anchorTopViewTo:animations:onComplete:")]
		void AnchorTopViewTo (ECSide side, Action animationsHandler, Action onCompleteHandler);

		/// <summary>
		/// Slides the top view off of the screen in the direction of the specified side.
		/// </summary>
		/// <param name="side">The side for the top view to slide off the screen towards.</param>
		[Export ("anchorTopViewOffScreenTo:")]
		void AnchorTopViewOffScreenTo (ECSide side);

		/// <summary>
		/// Slides the top view off of the screen in the direction of the specified side.
		/// </summary>
		/// <param name="side">The side for the top view to slide off the screen towards.</param>
		/// <param name="animationsHandler">Perform changes to properties that will be animated while top view is moved off screen. Can be nil.</param>
		/// <param name="onCompleteHandler">Executed after the animation is completed. Can be nil.</param>
		[Export ("anchorTopViewOffScreenTo:animations:onComplete:")]
		void AnchorTopViewOffScreenTo (ECSide side, Action animationsHandler, Action onCompleteHandler);

		/// <summary>
		/// Slides the top view back to the center.
		/// </summary>
		[Export ("resetTopView")]
		void ResetTopView ();

		/// <summary>
		/// Slides the top view back to the center.
		/// </summary>
		/// <param name="animationsHandler">Perform changes to properties that will be animated while top view is moved back to the center of the screen. Can be nil.</param>
		/// <param name="onCompleteHandler">Executed after the animation is completed. Can be nil.</param>
		[Export ("resetTopViewWithAnimations:onComplete:")]
		void ResetTopView (Action animationsHandler, Action onCompleteHandler);

		/// <summary>
		/// Returns true if the underLeft view is showing (even partially).
		/// </summary>
		[Export ("underLeftShowing")]
		void UnderLeftShowing ();

		/// <summary>
		/// Returns true if the underRight view is showing (even partially)
		/// </summary>
		[Export ("underRightShowing")]
		void UnderRightShowing ();

		/// <summary>
		/// Returns true if the top view is completely off the screen.
		/// </summary>
		[Export ("topViewIsOffScreen")]
		void TopViewIsOffScreen ();
	}


	[BaseType (typeof (UIViewController))]
	[Category]
	interface SlidingViewExtension {
		/// <summary>
		/// Convience method for getting access to the ECSlidingViewController instance.
		/// </summary>
		[Export ("slidingViewController")]
		ECSlidingViewController SlidingViewController ();
	}
}

