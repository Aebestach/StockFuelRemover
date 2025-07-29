using KSP.UI.Screens;
using System.Collections.Generic;
using UnityEngine;

[KSPAddon(KSPAddon.Startup.EditorAny, false)]
public class StockFuelRemover : MonoBehaviour
{
    // Type of fuel to be deleted
    private readonly HashSet<string> fuelTypesToRemove = new HashSet<string>
    {
        "MonoPropellant",
        "LiquidFuel",
        "Oxidizer",
        "SolidFuel"
    };

    private void Start()
    {
        // Listening to aircraft loading events
        GameEvents.onEditorShipModified.Add(OnShipModified);
        GameEvents.onEditorLoad.Add(OnEditorLoad);

        // Print Log
        // Debug.Log("[StockFuelRemover] Plugin activated");
    }

    private void OnDestroy()
    {
        // Clear event listeners
        GameEvents.onEditorShipModified.Remove(OnShipModified);
        GameEvents.onEditorLoad.Remove(OnEditorLoad);
    }

    private void OnEditorLoad(ShipConstruct ship, CraftBrowserDialog.LoadType loadType)
    {
        // Debug.Log("[StockFuelRemover] Craft loading event detected");
        RemoveFuelsFromShip(ship);
    }

    private void OnShipModified(ShipConstruct ship)
    {
        // Only processed when the complete craft is loaded, to avoid frequent triggering during editing
        if (ship != null && ship.parts != null && ship.parts.Count > 0)
        {
            RemoveFuelsFromShip(ship);
        }
    }

    private void RemoveFuelsFromShip(ShipConstruct ship)
    {
        // Check if it's in the editor
        if (!HighLogic.LoadedSceneIsEditor)
        {
            return;
        }

        if (ship == null || ship.parts == null)
        {
            return;
        }

        int totalRemovedResources = 0;

        foreach (Part part in ship.parts)
        {
            if (part.Resources != null)
            {
                // Creating a list of resources to delete
                List<PartResource> resourcesToRemove = new List<PartResource>();

                foreach (PartResource resource in part.Resources)
                {
                    if (fuelTypesToRemove.Contains(resource.resourceName))
                    {
                        resourcesToRemove.Add(resource);
                    }
                }

                // Deletion of found resources
                foreach (PartResource resource in resourcesToRemove)
                {
                    part.Resources.Remove(resource);
                    totalRemovedResources++;
                }
            }
        }

        if (totalRemovedResources > 0)
        {
            // Refresh the editor interface
            if (EditorLogic.fetch != null)
            {
                EditorLogic.fetch.ship = ship;
            }
        }
    }
}
