using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libECSlidingViewController1X.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, Frameworks = "UIKit CoreGraphics QuartzCore", ForceLoad = true)]
