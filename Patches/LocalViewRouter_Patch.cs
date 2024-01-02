using HarmonyLib;
using Kitchen;
using TMPro;
using UnityEngine;

namespace KitchenPatiencePercent.Patches
{
    [HarmonyPatch]
    static class LocalViewRouter_Patch
    {
        [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
        [HarmonyPostfix]
        static void GetPrefab_Postfix(ViewType view_type, ref GameObject __result)
        {
            if ((view_type == ViewType.CustomerIndicator || view_type == ViewType.QueueIndicator) &&
                __result != null &&
                __result.GetComponent<PatiencePercentController>() == null)
            {
                PatiencePercentController controller = __result.AddComponent<PatiencePercentController>();
                controller.Indicator = __result.GetComponent<CustomerIndicatorView>();

                GameObject textGO = __result.transform.Find("Container")?.Find("GameObject")?.Find("Container")?.Find("GameObject (1)")?.Find("Text")?.gameObject;
                if (textGO != null)
                {
                    controller.Icon = textGO.transform;
                    controller.DefaultIconPosition = textGO.transform.localPosition;

                    GameObject tmpGO = GameObject.Instantiate(textGO);
                    tmpGO.transform.SetParent(textGO.transform.parent);
                    tmpGO.transform.Reset();
                    controller.TMP = tmpGO.GetComponent<TextMeshPro>();
                    if (controller.TMP != null)
                    {
                        controller.TMP.enabled = false;
                        controller.TMP.color = Color.white;
                    }
                }
            }
        }
    }
}
