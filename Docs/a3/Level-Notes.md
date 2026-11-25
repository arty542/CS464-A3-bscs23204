# Final level notes

All scenes are authored and saved through Unity MCP. Gameplay scripts do not construct scenes.

| Level | Additions |
|---|---|
| 01 | Coin score and gated finish, bounce balcony, normal jump, checkpoint, three lives, win/lose and restart. Simplified to match the sparse concrete/industrial appearance of the other levels. |
| 02 | Visible spikes followed by concealed approach-triggered spikes, shared knockback and i-frames, garden checkpoint and sideways finish jump. |
| 03 | Open loading yard with four-point loop guard, slower chase, longer give-up range, Return state and shared contact damage. |
| 04 | Jump-reactive sliding spike lane, warned crusher, checkpoint, guard and east wing spike bay. |
| 05 | Two five-second speed potions with refresh rule, ping-pong guard, both reactive spike types, crusher and final gap. |

The requested Level Devil / Trap Adventure 2 variations are the approach-triggered and jump-reactive spikes. They use the existing Hazard and Health components, with short warning phases before the damage collider is enabled.

Video recording was omitted at the user's request.
