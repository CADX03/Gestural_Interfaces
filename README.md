# Gestural Interfaces Unity Project

A small Unity project demonstrating gestural interfaces using a Kinect sensor. The repository includes several mini-games and examples that illustrate how body tracking and hand gestures can be used to spawn/throw projectiles, slice objects, and navigate a simple menu.

This project was developed for Windows (Kinect v2) using Unity 2022.3.x LTS.

---

## Contents

- Scenes
	- `MainSceneMenu` - Main menu for selecting games using hand-over zones.
	- `FireballBeatSaber` - Throw fireballs using hand throwing gestures.
	- `Game_Scene` / `Vegetable_Samurai` - Sword/Blade slicing game (vegetable samurai style) and simple object-spawning gameplay.
	- `SampleScene` - Example scene used in the project (may be a placeholder).

- Key folders
	- `Assets/KinectView` - Kinect integration and body tracking scripts.
	- `Assets/Scripts` - Game scripts and controllers for gameplay mechanics.
	- `Assets/Prefabs` - Prefabs used for projectiles, blades, characters, etc.

---

## Features and High-level Overview

- Kinect-based body tracking using `BodySourceManager` + `BodySourceView` (Windows.Kinect).
- Hand gesture recognition to trigger game actions (throwing, slicing, selecting menu items by holding a hand in a zone for 3 seconds).
- Example quantized games:
	- Fireball throwing (FireballBeatSaber): `FireballController.cs` creates fireball prefabs and launches them based on hand throw gestures.
	- Blade slicing (Vegetable_Samurai / Game_Scene): `BladeController.cs` spawns blades at the hands and provides direction for slicing.
	- Object shooter / Ball shooter: `BallShooter.cs` attached to instantiated objects to spawn projectiles using measured velocity.
	- Score tracking: `CubeController.cs` and `Score.cs` increment and display score when objects are destroyed.

---

## Prerequisites

- Windows 10/11 with Kinect for Windows v2 sensor (Kinect 2) connected.
- Kinect for Windows SDK v2.0 installed (runtime and drivers).
- Unity Editor 2022.3.20f1 (LTS) or compatible 2022.3.x LTS release (recommended).
- Visual Studio / editor with support for C# (if you want to modify or build the project).

Note about Kinect: The project depends on native Kinect libraries and the Standard Assets wrapping the `Windows.Kinect` namespace. If running on a system without Kinect hardware, the Kinect components may throw errors — open the project with no sensor attached if you only want to examine the scenes and scripts.

---

## Setup & How to Run

1. Install Unity 2022.3.x LTS (2022.3.20f1 recommended).
2. Install the Kinect for Windows SDK v2.0 and make sure the sensor is properly connected and showing up as an available device on your Windows machine.
3. Open Unity Hub and add this project folder: `Gestural_Interfaces`.
4. Open the project in Unity and allow Unity to import assets.
5. In the Unity Editor, open `MainSceneMenu` from the Scenes list or the top bar File -> Open Scene.
6. Assign the Kinect prefabs / plugins if required: ensure a `BodySourceManager` prefab from `Assets/KinectView` is present in the scene and that `BodySourceView` references are correctly set.
7. Press Play to run the scene inside the editor; keep Kinect plugged in to test gestures.

Notes for building:
- Build Settings -> Add Scenes to Build: include `MainSceneMenu`, `Game_Scene`, `FireballBeatSaber`, and any additional scenes you want.
- Choose Platform -> PC, Mac & Linux Standalone (Windows) and set 64-bit build target if your Kinect plugin is 64-bit.
- Ensure the `Plugins/x86` and `Plugins/x86_64` native DLLs are used according to your build settings.

---

## Controls & Gestures

- Menu navigation: Put your left or right hand inside one of the designated menu boxes (colliders with tags like `Game1`, `Game2`, `Game3`, `Game4`) for 3 seconds to select a scene.
- Fireball throwing (FireballBeatSaber): Quickly throw your right or left hand and release; `FireballController` detects a high magnitude movement followed by a reduce in velocity to spawn and launch a fireball in the throw direction.
- Blade slicing (Vegetable_Samurai / Game_Scene): Move your hand in a slicing motion; `BladeController` spawns blades at the hand transforms and tracks direction vectors for slicing.
- Projectile launch (BallShooter): The `BallShooter` script attached to spawned prefabs will fire a projectile based on measured hand-backwards movement (a measured z velocity less than 0 triggers shooting).
- Return to menu: Several controllers check left-hand vertical position to return to the main menu by holding a gesture for 3 seconds.

---

## Notable Scripts

- `Assets/KinectView/Scripts/BodySourceManager.cs` — Reads Kinect frames and returns `Kinect.Body[]` data.
- `Assets/KinectView/Scripts/BodySourceView.cs` — Builds GameObjects for tracked joints and exposes `Bodies` dictionary.
- `Assets/Scripts/FireballController.cs` — Tracks hand velocity to detect throw gestures and spawns fireball prefabs.
- `Assets/Controller.cs` — Example gesture based on a single `BodySourceView` reference; instantiates a prefab (Olaf) on hand and spawns projectiles.
- `Assets/Scripts/CubeController.cs` and `Assets/Scripts/Score.cs` — Basic scoring systems and object lifecycle.
- `Assets/KinectView/Scripts/BladeController.cs` — Uses a 'blade' attached to both hands and computes slicing direction.

---

## Troubleshooting & Tips

- If you see crashes or plugin issues, check `Assets/Plugins` and the appropriate `x86` or `x86_64` folder matches the build target.
- If Kinect frames are not detected, make sure the sensor is connected, the SDK runs, and the Kinect sensor is not used exclusively by another application.
- If the editor logs warnings about missing references, verify that the `BodySourceManager` GameObject is present in the scene and referenced on components that require it.

---

## Contributing

This project is intended as an educational / demo project for gestural interfaces and Kinect integration. Contributions are welcome in the form of bug fixes, new scenes, and additional gesture-based interactions.

If you plan to contribute:
- Use a branch for your changes.
- Include a minimal demo scene for new features.
- Test with Kinect runtime on the target platform.

---

## License

No license is currently provided in this repository. If you wish to release or reuse this project, please add a LICENSE file (MIT, Apache 2.0, etc.) or contact the owner for clarification.

---

## Acknowledgements

- The project uses Kinect for Windows SDK v2.0 and sample wrappers under `Assets/Standard Assets/Windows/Kinect`.
- Unity 2022 LTS and common Unity packages form the basis of the project.

If you want me to include contributor names or add build instructions for other platforms, tell me which specifics you'd like to add and I’ll update the README.
