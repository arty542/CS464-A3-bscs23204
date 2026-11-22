# Synty asset selection for Assignment 03

## Direction

Use the instructor-provided `Assets/Synty/PolygonGeneric` and `Assets/Synty/PolygonPrototype` packages for an overgrown industrial courtyard setting. Combine concrete walkways and brick walls with crates, pipes, trees and ivy. Give each of the five original layouts a distinct space within the same setting.

This is a verified asset shortlist for the next build phase. No new level geometry has been placed yet. Prefab files, material assignments and several source textures were inspected; final scale, appearance, animation and collider behavior still need checking in Unity.

## Main environment kit

Base building folder: `Assets/Synty/PolygonGeneric/Prefabs/Base/`.

| Prefab | Intended use |
|---|---|
| `SM_Bld_Base_Floor_01.prefab` | Main walkways and courtyard surfaces. |
| `SM_Bld_Base_Floor_Half_01.prefab` | Short landings and optional coin detours. |
| `SM_Bld_Base_Floor_Hole_01.prefab` | Clearly framed fall openings, after checking its collider. |
| `SM_Bld_Base_Stairs_01.prefab` | Visible changes in height; use a smooth collision ramp if individual steps catch the player. |
| `SM_Bld_Base_Wall_01.prefab` | Brick and concrete room boundaries. |
| `SM_Bld_Base_Wall_Half_01.prefab` | Low boundaries that preserve the follow camera's view. |
| `SM_Bld_Base_Wall_Destroyed_01.prefab` | Broken garden/depot edges and landmarks. |
| `SM_Bld_Base_Wall_Door_Double_01.prefab` | Generous entrances and exit framing. |
| `SM_Bld_Base_Pillar_01.prefab` | Crusher supports and repeated architectural landmarks. |

Additional geometry folder: `Assets/Synty/PolygonPrototype/Prefabs/Buildings/Simple/`.

| Prefab | Intended use |
|---|---|
| `SM_Buildings_Floor_5x5_01.prefab` | Simple surfaces when exact modular construction is useful. |
| `SM_Buildings_Ramp_25_1x1_01.prefab` | Gentle ramps, resized only after checking dimensions and slope in Unity. |

Prefer Generic's building kit for finished surfaces. Prototype's simple kit has grid/atlas UVs: keep its matching materials or author a suitable variant instead of blindly substituting a repeating brick/concrete texture.

## Dressing and landmarks

| Folder under `Assets/Synty/PolygonGeneric/Prefabs/` | Prefab | Intended use |
|---|---|---|
| `Props/` | `SM_Gen_Prop_Crate_01.prefab` | Small stacks around the loading yard; retain clear running lanes. |
| `Props/` | `SM_Gen_Prop_Barrel_Metal_01.prefab` | Industrial accents outside hazard footprints. |
| `Props/` | `SM_Gen_Prop_Light_Wall_01.prefab` | Exit and workshop landmarks. |
| `Building/` | `SM_Gen_Bld_Pipe_Straight_01.prefab` | Workshop edges and crusher framing. |
| `Building/` | `SM_Gen_Bld_Beam_01.prefab` | Overhead structural details. Its supplied collider is narrow, so do not assume it is a fair walking bridge. |
| `Building/` | `SM_Gen_Bld_Background_01.prefab` | Distant skyline, outside playable routes. |
| `Environment/` | `SM_Gen_Env_Rock_02.prefab` | Garden borders and blocked-route silhouettes. |
| `Environment/` | `SM_Gen_Env_Tree_01.prefab` | Courtyard boundaries, away from the camera and patrol lane. |
| `Environment/` | `SM_Gen_Env_Ivy_01.prefab` | Wall dressing with no gameplay collision. |

## Gameplay visuals

| Package folder under `Assets/Synty/` | Prefab | Gameplay wrapper to build |
|---|---|---|
| `PolygonGeneric/Prefabs/Props/` | `SM_Gen_Prop_Coin_01.prefab` | Coin with a generous trigger, Player tag check, score callback and optional spin. |
| `PolygonPrototype/Prefabs/Props/` | `SM_Prop_BoostPad_01.prefab` | Bounce pad: preserve its physical top and add a separate trigger for the upward force. |
| `PolygonGeneric/Prefabs/Props/` | `SM_Gen_Prop_Potion_01.prefab` | Timed speed pickup, with its timer on the player rather than the bottle. |
| `PolygonPrototype/Prefabs/Characters/` | `Character_Dummy_Male_01_FixedScale.prefab` | Guard visual beneath a kinematic patrol/chase root. Character animation is not assumed to be included. |
| `PolygonPrototype/Prefabs/Primitives/` | `SM_Primitive_Cone_01.prefab` | Repeated spike visuals above one readable hazard footprint. |
| `PolygonPrototype/Prefabs/Primitives/` | `SM_Primitive_Cube_01.prefab` | Crusher slab, with a visible resting/warning/striking sequence. |
| `PolygonPrototype/Prefabs/Icons/` | `SM_Icon_Warning_01.prefab` | Crusher warning marker, paired with a visible footprint and timing cue. |
| `PolygonPrototype/Prefabs/Icons/` | `SM_Icon_Arrow_Small_01.prefab` | Direction cues at junctions. |
| `PolygonPrototype/Prefabs/Icons/` | `SM_Icon_Text_Finish_01.prefab` | Readable goal marker at the exit. |

These are visual assets, not ready-made A3 gameplay scripts. The inspected Coin, Potion and BoostPad prefabs have solid mesh colliders. Configure trigger/collision roles in our own wrapper prefabs so collectibles do not block movement. Spike visuals should not trap the player: use a separate damage trigger. Keep supplied source assets intact and put gameplay wrappers in `Assets/Prefabs/A3/` when building them.

## Materials and readability

| Supplied material | Use |
|---|---|
| `Assets/Synty/PolygonGeneric/Materials/Generic_Concrete.mat` | Quiet, neutral surfaces that make coins and hazard markings stand out. |
| `Assets/Synty/PolygonGeneric/Materials/Generic_Brick.mat` | Warm boundary walls and workshop exteriors. |
| `Assets/Synty/PolygonGeneric/Materials/Generic_Grass.mat` | Garden edges and landscape; keep it off manufactured walkways. |
| `Assets/Synty/PolygonGeneric/Materials/Generic_Wood.mat` | Compatible wood surfaces and small details. |
| `Assets/Synty/PolygonGeneric/Materials/Alts/Generic_01_A.mat` | Retain on the coin, potion, crates, barrels and other props designed for this shared atlas. |
| `Assets/Synty/PolygonPrototype/Materials/PolygonPrototype_01.mat` | Matching atlas for boost pad, icons, primitive hazard visuals and dummy character. |
| `Assets/Synty/PolygonPrototype/Materials/PolygonPrototype_Grid_01.mat` | Measuring/blockout aid on matching Prototype geometry. |

The concrete, grass and brick source textures and Prototype atlas were visually inspected. Keep prop atlas assignments, material slots and UV mapping intact. Create A3-specific material copies only where needed, then verify the actual result under the project's URP lighting.

Use gold coin silhouettes, cyan boost markers, red hazard outlines, yellow/black crusher warnings and a green exit marker as the planned gameplay vocabulary. Pair color with shape, text or motion: a coin disk, a potion bottle, pointed spikes, a warning symbol and FINISH text. Environmental grass is scenery, not a goal cue. Keep decorative effects quieter than gameplay feedback.

## Five new spaces

| Scene | Proposed space | Asset emphasis | New mechanic |
|---|---|---|---|
| Level01 | Training courtyard | Concrete paving, low brick walls, trees, a raised landing and the Synty boost pad. | Coins, goal and bounce pad; safe first demonstration. |
| Level02 | Broken garden walk | Destroyed walls, rocks and ivy around wide ledges and an optional coin detour. | Spikes and recovery after damage. |
| Level03 | Loading yard | Crates, barrels and open concrete lanes, with a clear escape route. | Guard patrol/chase/return. Keep its route clear because direct MoveTowards does not navigate around walls. |
| Level04 | Press workshop | Pillars, pipes, wall lights and a suspended Synty slab with a marked strike zone. | Warned crusher, then a combination with a guard. |
| Level05 | Escape yard | Broad connected lanes, two potion pickups and an unmistakable final gate. | Timed speed boost and repeat pickup behavior, mixed with earlier challenges. |

Build each layout from scratch. Do not copy Synty demo scenes or earlier assignment/lab layouts. Reuse the selected source meshes, materials and our player/camera foundation.
