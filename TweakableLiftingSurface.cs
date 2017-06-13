using UnityEngine;
using System;

namespace TweakableLiftingSurface
{
	[KSPAddon(KSPAddon.Startup.MainMenu, false)]
	public class TLSHook : MonoBehaviour
	{
		public void Start()
		{
		Controller.HookModule("ModuleLiftingSurface", "TweakableLiftingSurface");
		}
	}

	public class TweakableLiftingSurface : PartModule
	{
		[KSPField(isPersistant = true, guiName = "Lift Enabled", guiActive = true, guiActiveEditor = true), UI_Toggle(enabledText = "True", disabledText = "False")]
		public bool liftOn = true;
		[KSPAction("Toggle Lift")]
		public void toggle(KSPActionParam param)
		{
			if (liftOn)
			{
				liftOn = false;
			}
			else
			{
				liftOn = true;
			}
		}
		[KSPAction("Enable Lift")]
		public void enable(KSPActionParam param)
		{
			liftOn = true;
		}
		[KSPAction("Disable Lift")]
		public void disable(KSPActionParam param)
		{
			liftOn = false;
		}
		ModuleLiftingSurface liftingSurface;
		float deflectionLiftCoeff;
		float oneOrZero = 1.0f;

		public override void OnStart(StartState state)
		{
			try
			{
				base.OnStart(state);
				liftingSurface = part.FindModuleImplementing<ModuleLiftingSurface>();
				deflectionLiftCoeff = liftingSurface.deflectionLiftCoeff;
				liftingSurface.deflectionLiftCoeff = deflectionLiftCoeff * oneOrZero;
			}
			catch (Exception ex)
			{
				Debug.LogError("PROBLEM.\n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		public void FixedUpdate()
		{
			if (liftOn)
			{
				oneOrZero = 1.0f;
			}
			else
			{
				oneOrZero = 0.0f;
			}
			liftingSurface.deflectionLiftCoeff = deflectionLiftCoeff * oneOrZero;
		}
	}
}