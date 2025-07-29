# StockFuelRemover

## Introduction

This plugin automatically removes deprecated fuel resources when loading craft files in VAB/SPH. When installing the [Chemical Technologies](https://forum.kerbalspaceprogram.com/topic/227705-112x-chemical-technologies-a-chemically-based-resource-overhaul-2025-07-22/) mod and its dependencies, previously saved craft files contain legacy fuel resources (MonoPropellant, LiquidFuel, Oxidizer) that are no longer used by the new system. Instead of manually adjusting each part, this plugin automatically detects and removes these obsolete fuels upon craft loading, ensuring your vessels are compatible with the Chemical Propulsion mod without any manual intervention.

Features:

- Automatically removes MonoPropellant, LiquidFuel, and Oxidizer from loaded craft
- Works in both VAB and SPH
- No user interaction required - runs silently in the background
- Saves time by eliminating the need to manually clean up each part

## Installation
The installation process is the same as other mods. Just put the **StockFuelRemover** folder from GameData into the GameData in the game root directory. 
