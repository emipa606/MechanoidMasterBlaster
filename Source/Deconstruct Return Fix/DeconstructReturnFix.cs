using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Deconstruct_Return_Fix
{
	// Token: 0x02000002 RID: 2
	[StaticConstructorOnStartup]
	internal static class DeconstructReturnFix
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static DeconstructReturnFix()
		{
			Harmony harmonyInstance = new Harmony("com.dninemfive.deconstructreturnfix");
			harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
			Log.Message("Deconstruct Return Fix harmony patch successfully loaded", false);
		}

		// Token: 0x02000003 RID: 3
		[HarmonyPatch(typeof(GenLeaving), "DoLeavingsFor", new Type[]
		{
			typeof(Thing),
			typeof(Map),
			typeof(DestroyMode),
			typeof(CellRect),
			typeof(Predicate<IntVec3>),
            typeof(List<Thing>)
		})]
		private class CalcFix
		{
			// Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
			[HarmonyPrefix]
			public static bool DoLeavingsForPrefix(Thing diedThing, Map map, DestroyMode mode, CellRect leavingsRect, Predicate<IntVec3> nearPlaceValidator = null, List<Thing> listOfLeavingsOut = null)
			{
				bool flag = mode == DestroyMode.Deconstruct;
				bool result;
				if (flag)
				{
					ThingOwner<Thing> thingOwner = new ThingOwner<Thing>();
					bool flag2 = GenLeaving.CanBuildingLeaveResources(diedThing, mode);
					if (flag2)
					{
						Frame frame = diedThing as Frame;
						bool flag3 = frame != null;
						if (flag3)
						{
							for (int i = frame.resourceContainer.Count - 1; i >= 0; i--)
							{
								int num = DeconstructReturnFix.CalcFix.GBRLC(diedThing, mode)(frame.resourceContainer[i].stackCount);
								bool flag4 = num > 0;
								if (flag4)
								{
									frame.resourceContainer.TryTransferToContainer(frame.resourceContainer[i], thingOwner, num, true);
								}
							}
							frame.resourceContainer.ClearAndDestroyContents(mode);
						}
						else
						{
							List<ThingDefCountClass> list = diedThing.CostListAdjusted();
							for (int j = 0; j < list.Count; j++)
							{
								ThingDefCountClass thingDefCountClass = list[j];
								int num2 = DeconstructReturnFix.CalcFix.GBRLC(diedThing, mode)(thingDefCountClass.count);
								bool flag5 = num2 > 0;
								if (flag5)
								{
									Thing thing = ThingMaker.MakeThing(thingDefCountClass.thingDef, null);
									thing.stackCount = num2;
									thingOwner.TryAdd(thing, true);
								}
							}
						}
					}
					List<IntVec3> list2 = leavingsRect.Cells.InRandomOrder(null).ToList<IntVec3>();
					int num3 = 0;
					for (;;)
					{
						bool flag6 = thingOwner.Count > 0;
						if (!flag6)
						{
							goto IL_1B4;
						}
						ThingOwner<Thing> thingOwner2 = thingOwner;
						Thing thing2 = thingOwner[0];
						IntVec3 dropLoc = list2[num3];
						ThingPlaceMode mode2 = ThingPlaceMode.Near;
						Thing thing3 = null;
						bool flag7 = thingOwner2.TryDrop(thing2, dropLoc, map, mode2, out thing3, null, nearPlaceValidator);
						if (!flag7)
						{
							break;
						}
						num3++;
						bool flag8 = num3 >= list2.Count;
						if (flag8)
						{
							num3 = 0;
						}
					}
					Log.Warning(string.Concat(new object[]
					{
						"Deconstruct Return Fix: Failed to place all leavings for destroyed thing ",
						diedThing,
						" at ",
						leavingsRect.CenterCell
					}), false);
					return false;
					IL_1B4:
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}

			// Token: 0x06000003 RID: 3 RVA: 0x00002290 File Offset: 0x00000490
			public static Func<int, int> GBRLC(Thing t, DestroyMode d)
			{
				bool flag = !GenLeaving.CanBuildingLeaveResources(t, d);
				if (!flag)
				{
					switch (d)
					{
					case DestroyMode.Vanish:
						return (int count) => 0;
					case DestroyMode.KillFinalize:
						return (int count) => GenMath.RoundRandom((float)count * 0.5f);
					case DestroyMode.Deconstruct:
						return (int count) => GenMath.RoundRandom(Mathf.Min((float)count * t.def.resourcesFractionWhenDeconstructed, (float)count));
					case DestroyMode.FailConstruction:
						return (int count) => GenMath.RoundRandom((float)count * 0.5f);
					case DestroyMode.Cancel:
						return (int count) => GenMath.RoundRandom((float)count * 1f);
					case DestroyMode.Refund:
						return (int count) => count;
					}
					throw new ArgumentException("Unknown destroy mode " + d + " (Deconstruct Return Fix error)");
				}
				return (int count) => 0;
			}
		}
	}
}
