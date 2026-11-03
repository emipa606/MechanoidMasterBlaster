# GitHub Copilot Instructions for Mechanoid Master Blaster (Continued)
## Mod Overview and Purpose
Mechanoid Master Blaster (Continued) is an update of Trunkens mod for RimWorld. It adds a variety of new turrets to the game, including the Shockwave Coil, Nitro Laser, Salvo Blaster, Multi Burst, and Devastator. The mod also includes a Trinity Gun, which requires a Minigun to build and Heavy Turrets Research.
The mod is designed to add an epic quest theme to the game, where players can collect a Power Cell after defeating alien robots. This Power Cell can be used to unlock new features such as passive energy after research and a variety of cool weapons.
## Key Features and Systems
The mod includes several key features and systems, including:
- A new turret system with superior targeting, laser pew pew, slow reload, and plated properties
- A Trinity Gun that requires a Minigun to build and Heavy Turrets Research
- Compatibility with other mods such as Turret Extensions, Mechanoid Power Source, and Combat Extended
- Upgrades for the turrets with Turret Extensions
- Hundreds of textures for the turrets
## Coding Patterns and Conventions
The mod follows standard coding patterns and conventions for RimWorld modding, including:
- Using XML to define game objects and their properties
- Using C# to implement custom game logic
- Using Harmony patches to modify existing game code
- Using namespaces and classes to organize code
## XML Integration
The mod uses XML to define game objects and their properties. The following XML files are included in the mod:
- Damages_Environmental.xml
- ResearchProjects_Highend_Turrets.xml
- World_Oneshots_Turrets.xml
- Buildings_Guards.xml
- Buildings_Heavy.xml
These files define damage types, research projects, sound definitions, and building definitions, among other things.
## Harmony Patching
The mod uses Harmony patches to modify existing game code. The following Harmony patch is included in the mod:
- DamageWorker_BombNoCamShake.cs
This patch modifies the ExplosionStart method of the DamageWorker_BombNoCamShake class to remove the camera shake effect.
## Suggestions for Copilot
To get the most out of Copilot when working on Mechanoid Master Blaster (Continued), we recommend:
- Using XML to define game objects and their properties
- Using C# to implement custom game logic
- Using Harmony patches to modify existing game code
- Using namespaces and classes to organize code
- Following standard coding patterns and conventions for RimWorld modding
- Using Copilot to generate code snippets and suggestions for XML and C# files
- Using Copilot to generate code snippets and suggestions for Harmony patches
- Using Copilot to generate code snippets and suggestions for organizing code into namespaces and classes

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

