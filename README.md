# Barricade

A turn-based strategy game inspired by [Barricade.gg](https://barricade.gg), built in C#.

Players race to reach the opposite side of the board while placing walls to block their opponent. The game enforces all core rules (besides side-stepping) — including ensuring no wall placement can completely cut off either player's path to their goal.

## Current Features

- **Two-player local play** on a 9×9 grid via keyboard controls
- **Wall placement** with a live preview, colour-coded yellow/red based on validity
- **A\* pathfinding** used in two ways:
  - `FindPath` — returns a full path between two nodes (there for future basic AI)
  - `FindPathToRow` — single-pass existence check confirming a player can still reach their target row, used to validate wall placements
- **Jump mechanic** — players can leap over each other when adjacent
- **Win detection** when a player reaches the opposing side

## Planned Features

- **Basic AI opponent** that navigates using the shortest path from `FindPath`
- **Wall count limit** per player, matching the original game's rules

## Controls

| Key | Action |
|---|---|
| Arrow keys | Move player |
| Space | Enter wall placement mode |
| Arrow keys (wall mode) | Move wall preview |
| R (wall mode) | Rotate wall |
| Enter (wall mode) | Place wall |

## How It Works

Wall validity is checked by temporarily placing the wall and running `FindPathToRow` for both players. If either has no route to their goal row, the placement is rejected. This uses an adapted A\* algorithm where the heuristic is the row distance to the target, and any cell in the goal row is treated as a valid destination.
