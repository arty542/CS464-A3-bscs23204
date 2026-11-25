# Level01 — Training Courtyard

Original A3 layout, authored directly through Unity MCP. The scene uses the same sparse industrial style as Levels02–05: Synty concrete floors, crates/barrels, teal checkpoint markings, an open background and plain world-space TMP signs. The earlier trees, planters, brick walls and large foundations were removed at the user's request.

- Starting platform: x −10…10, z −6…14, top y=0.
- Bounce balcony: x −5…5, z 18…28, top y=3; the bounce pad launches at 11.5 m/s using VelocityChange.
- Final platform: x −10…10, z 31…46, top y=3. A normal Space jump clears the 3 m gap.
- Ten coins, target eight; three hearts and three lives. No damaging hazards in this introductory level.
- The balcony checkpoint saves the landing position. Falling spends one life and retains coins; the final lost life displays Lose.
- Both result panels freeze time. Restart restores time before reloading, while Next Level opens Level02.

The saved scene contains all geometry and gameplay objects. Runtime scripts handle movement and game mechanics only; no scene-generation script is included.

See `Level01-PlayChecks.json` for the physical movement and UI checks.
