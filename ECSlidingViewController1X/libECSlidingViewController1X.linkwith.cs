using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libECSlidingViewController1X.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64, Frameworks = "UIKit CoreGraphics QuartzCore", ForceLoad = true)]
