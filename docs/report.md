# Report


## Metrics

| Item | Value |
|---|---|
| Total time taken | 320 mins |
| Number of development iterations before final submission | 28 |
| Number of correctly implemented modifications | 24 |

## Iteration Log

| Iteration | Modification(s) implemented | Correctly implemented modifications (count) | Requirements mismatched after iteration (count) | Notes |
| ---: | --- | ---: | ---: | --- |
| 1 | Imported the required prefabs and used all the coordinates from Phase 1 to arrange the initial scene layout, then hardened `objectHandler.cs` against missing optional/inherited article fields and missing XR data | 1 | 10 | This was the point where the base scene was assembled properly and generation became more stable, but many interaction mismatches still remained. |
| 2 | Added exception handling in `VReqDV.cs` scene opening flow | 1 | 10 | Prevented editor UI failure cascades during scene generation. |
| 3 | Removed Version 2 dependency on a missing `Cupboard` component | 1 | 9 | Cupboard interaction no longer hard-failed when the component was absent. |
| 4 | Added missing Version 1 book interaction methods and shared camera-ray helper | 1 | 9 | Restored generated Version 1 behavior compatibility. |
| 5 | Fixed `ClearObjects()` to destroy only active-scene root objects from a stable snapshot | 1 | 8 | Eliminated cleanup-time `MissingReferenceException` issues. |
| 6 | Converted Version 2 player view to a first-person camera attached to `Player` | 1 | 7 | Movement became aligned with expected first-person behavior. |
| 7 | Improved Version 2 interaction reliability by checking child colliders and attached/child audio sources | 1 | 7 | Solved several false negatives caused by prefab collider and audio placement. |
| 8 | Reworked cupboard door logic to scripted rotation and improved cupboard sound fallback | 1 | 6 | Replaced unreliable prefab-side animation assumptions for cupboard behavior. |
| 9 | Moved object audio configuration into article/spec data and generator setup | 1 | 6 | Audio became spec-driven instead of hardcoded in gameplay logic. |
| 10 | Moved Version 2 cupboard behavior fully into spec-side algorithms and added interaction logs | 1 | 5 | Centralized behavior in the VReqDV pipeline, but later versions still needed equivalent cleanup. |
| 11 | Enforced a single `AudioListener` and treated child-collider hits as valid interaction hits | 1 | 5 | Fixed missing-listener warnings and improved prefab interaction reliability. |
| 12 | Updated export flow so only active-scene root objects are serialized into later versions | 1 | 4 | Prevented prefab child objects from being exported as independent scene articles. |
| 13 | Improved Version 3 movement with `Q`/`E` vertical motion and cleaned stale copied state/log identifiers | 1 | 4 | Brought Version 3 behavior closer to the intended traversal model. |
| 14 | Preserved Version 3 audio fields during export and added extra interaction diagnostics | 1 | 4 | Helped expose which object the center ray was actually hitting. |
| 15 | Corrected Version 4 baseline gameplay: stale copied identifiers, floor clamp, cupboard angle, rotation model, and per-book state names | 1 | 3 | Established the main Version 4 logic foundation. |
| 16 | Expanded Version 4 behavior rules for extra books and rotatable objects, and gated chest unlock behind all-book completion | 1 | 3 | Requirement coverage improved, but unlock behavior still had generator-state fragility. |
| 17 | Added explicit Version 4 article-state/audio data for the missing collectible books | 1 | 3 | Brought article data in line with behavior expectations. |
| 18 | Refined Version 4 world-`Y` rotation behavior and allowed repeated trumpet triggering | 1 | 2 | Interaction behavior became more usable during play testing. |
| 19 | Cleaned Version 5 stale copied identifiers and added event-driven chest readiness checks after trumpet/book events | 1 | 2 | Eliminated the earlier delayed/polling-style unlock behavior. |
| 20 | Added Version 5 destroy-state and trumpet-state logging plus immediate book hide/disable before destruction | 1 | 2 | Greatly improved debugging visibility and pickup responsiveness. |
| 21 | Added generated player collider settings and scale handling for Version 5 | 0 | 2 | Capsule collider settings were enforced, but the intended scale mapping was initially misapplied and needed correction. |
| 22 | Reworked Version 4 chest unlock to use spec-side `ShouldOpenChest` with local opened-chest tracking | 1 | 1 | Avoided generated chest-state reads that were causing registration crashes. |
| 23 | Applied the same spec-side chest workaround to Version 5 | 1 | 1 | Kept Version 5 aligned with the safer Version 4 pattern. |
| 24 | Tried solving the book-floor issue by turning Version 4 books into non-physics placed objects | 0 | 1 | This was only a partial diagnostic step; it did not fully explain the observed scene problem. |
| 25 | Fixed generator-added fallback colliders so prefabs only get a root `BoxCollider` when no collider exists anywhere in the hierarchy | 1 | 1 | This resolved the unnecessary `RoundTable` root-collider behavior and improved interaction/collision fidelity. |
| 26 | Adjusted brown-book placement upward to compensate for its lower-looking placement in scene | 1 | 1 | This was a local placement correction rather than a systemic prefab fix. |
| 27 | Investigated chest audio/animation failures, broadened audio lookup, removed generated chest-state writes, and corrected the chest prefab path | 0 | 1 | Audio was recovered, but animation still failed reliably because the live scene object/prefab arrangement did not behave as expected. |
| 28 | Replaced chest-opening animation requirement in Version 4 with a destroy-on-unlock action after conditions are met | 1 | 0 | Final workaround matched gameplay intent even though the chest animation itself could not be made reliable in time. |

## Changes from initial requirements
In the VReqDV version, the chest is removed once the required conditions are satisfied instead of playing an unlock animation, because I was not able to get the animation working properly in time.

Here is a **more natural, human-written version** of your section. I kept the same ideas but made the tone smoother and more reflective, like something a student would realistically write in a report.

---

## Challenges

While working with the VReqDV-based specification-and-generation approach, I encountered several technical and workflow challenges throughout development.

### 1. Fragility in Generator and Runtime State Initialization

One of the recurring issues was how sensitive the generated state system was to initialization order. For example, the chest unlock logic occasionally crashed with a `KeyNotFoundException` when the generated behavior attempted to access `ChestStateStorage` before the chest had been registered in the state system. This suggested that the runtime and generator were not always synchronizing correctly. To address this, I redesigned the chest unlock logic so that it relied more on specification-side algorithms and local runtime checks rather than directly querying generated chest-state values.

### 2. Prefab Structure Assumptions

Another challenge came from differences between the assumptions made by the generated logic and the actual structure of imported prefabs. The generator often expected important components such as colliders, audio sources, or animation controllers to exist on the root object. In reality, many prefabs had these components nested deeper within their hierarchy. This caused a variety of problems:

* Ray interactions sometimes failed because the ray hit a child collider rather than the expected root object.
* Audio warnings appeared even though the prefab visibly had audio sources configured.
* Chest animation logic occasionally failed because the runtime component structure did not match what the generated code expected.

To work around this, I had to improve how scripts searched for components in child objects and, in some cases, implement gameplay-side workarounds when the chest animation continued to behave inconsistently.

### 3. Mismatch Between Specification Data and Scene Instances

Another difficulty was that updating specification files such as `article.json` or `UserAlgorithms.cs` did not always immediately resolve issues. In several cases, the active Unity scene still contained outdated generated instances. This made some bugs appear to be logic errors when the real problem was simply that the scene had not been regenerated with the updated object definitions or prefab paths. As a result, I had to frequently regenerate the scene to ensure that the runtime environment reflected the latest specification changes.

### 4. Indirect Debugging of Physics and Colliders

Debugging physics and collider-related issues was particularly time-consuming. For example, problems with book placement and table interactions could originate from several different sources at once, including rigidbody settings, collider overlap, missing table geometry, automatically injected fallback colliders from the generator, or incorrect prefab pivot points. Diagnosing the brown-book placement issue and the round-table collider behavior required several iterations of inspecting the article data, reviewing the generator code, and observing the runtime scene behavior in Unity.

### 5. JSON Workflow Limitations

Finally, the JSON-based workflow introduced its own challenges during iterative development. At one point, the Version 4 `article.json` file ended up containing commented-out object definitions even though strict JSON does not support comments. Although this did not immediately break every step of the pipeline, it technically made the file invalid and introduced unnecessary noise during validation. I eventually removed those commented JSON blocks, but the issue still highlighted a limitation of using raw JSON for iterative scene editing without stronger validation or tooling support.

### 6. Chest Animation Could Not Be Made Reliable in Time

The final major limitation was the chest open behavior. Even after broadening component lookups, improving audio fallback, and correcting the chest prefab path, the chest animation still did not behave reliably enough in the generated Version 4 scene. Since the real gameplay requirement was that the chest should show some behavior changes once the books and trumpet conditions were satisfied, I finally changed the behavior so the chest gets destroyed on unlock instead of trying to animate it.


## VReqDV System Changes and Limitations Observed

While working on the final version of the scene, I ended up having to touch quite a few parts of the VReqDV system itself, not just the scene content. Some of this was just making things more stable, and some of it was figuring out where the system had gaps that I needed to work around.

### Improvements I made to VReqDV

* **`Assets/VReqDV/Editor/objectHandler.cs` (L28–35, L73–85, L173, L230–231)** — I added safety checks for missing optional fields like rigidbody data, XR rigid object references, and state definitions. I also moved audio configuration to be fully spec-driven (reading from article data instead of hardcoding), and fixed the fallback collider logic so that prefabs only get a root `BoxCollider` added if they don't already have a collider anywhere in their hierarchy. Before this fix, objects like `RoundTable` were getting an extra collider added on top of the ones they already had.

* **`Assets/VReqDV/Editor/VReqDV.cs` (L264–278, L297–299, L308–312)** — I added a try-catch block around the `OpenScene` method so that if something goes wrong during generation, the editor doesn't just crash out of a usable state. I also fixed `ClearObjects()` to use a snapshot of root objects from the active scene, and updated the export flow so only root-level objects get serialized into new versions — meaning prefab child objects no longer accidentally end up as standalone articles in later versions.

* **`Assets/VReqDV/specifications/version_4/UserAlgorithms.cs` (L18, L213–236, L572–580)** — I ended up using spec-side algorithms quite a bit as a workaround when the generated state system caused crashes. For example, `ShouldOpenChest` (L213) and `OpenChest` (L227) were written entirely in the user algorithm file, with a local `_openedChestIds` set (L18) to track whether a chest had already been opened, instead of calling into the generated `ChestStateStorage`. `FindAttachedAudioSource` (L572) was also updated to do a child component search instead of only checking the root object.

* **`Assets/VReqDV/specifications/version_4/article.json` (L209, L261, L313, L365, L417–466)** — I added missing state and audio configuration for all four collectible books (`book_red`, `book_golden`, `book_green`, `book_brown`) and corrected the chest prefab path from `Assets/my prefabs/Chest.prefab` to `Assets/my prefabs/Prefabs/Chest.prefab` (L466), which was the actual prefab that had the animation and audio components on it.

### Limitations I ran into

* **Generated state registration order** (`version_4/UserAlgorithms.cs` L213–236): If code tries to read from a generated state class before the object has been registered, it just throws a `KeyNotFoundException`. There's no built-in safety net for this, so I had to move all the chest state logic into the user algorithm file and manage it locally.

* **Component location assumptions** (`objectHandler.cs` L73, `UserAlgorithms.cs` L572): Both the generator and user algorithm code originally assumed that components like `AudioSource` and `Collider` would be on the root object. For imported prefabs that's often not true, and it caused a lot of confusing failures. I had to update both files to use child component searches.

* **Scene regeneration being required after every spec change**: Updating `article.json` or `UserAlgorithms.cs` doesn't do anything until you actually regenerate the scene. I lost time more than once debugging what looked like a code problem, but was actually just an outdated scene instance.

* **Raw JSON with no comment support** (`version_4/article.json`): Editing JSON by hand is error-prone, and strict JSON doesn't support comments. This caused some validation noise during development that I had to clean up later.

* **Multi-layer debugging**: When something goes wrong it could be the article data, the behavior rules, the user algorithms, the generated code, or the prefab structure. Tracing where the actual problem was took a lot longer than debugging a normal Unity script would.

---

## Reflection

Overall, the workflow was useful but not always smooth.

One of the biggest strengths of this approach was that it encouraged defining behavior at a higher level using `article.json`, `behavior.json`, and `UserAlgorithms.cs`. Once a working pattern was established, it became easier to reuse that pattern across different versions instead of constantly modifying scene scripts directly. This worked particularly well for features like first-person movement, collectible book interactions, and the specification-driven audio setup.

However, when something broke, it was not always obvious where the problem came from. A single gameplay issue could originate from several different layers of the system: the article data, the behavior rules, the user algorithms, the generated code, the prefab structure, or even an outdated generated scene. Because of this, debugging sometimes felt confusing. In some cases, even correct code changes would not appear to fix the issue until the scene had been regenerated and tested again.

My confidence in the final solution improved once I stopped trying to preserve every original mechanism and instead focused on the underlying requirement. The chest behavior is a good example of this. Initially, the goal was to unlock the chest through an animation. However, repeated attempts showed that the animation path was not reliable with the current VReqDV setup and prefab structure. Switching the unlock effect to simply destroying the chest was less visually impressive, but it was much more dependable and still satisfied the gameplay requirement.

Overall, the development process felt moderately challenging. The system eventually became productive, but it required several iterations to improve robustness, remove hidden assumptions, and introduce practical workarounds when the toolchain or prefab setup did not support a cleaner solution.

---

## Assumptions

* The final Version 4 chest behavior is considered acceptable because the chest is destroyed when the unlock condition is met, even though the original animated opening behavior could not be made reliable.
* The temporary validation issues caused by commented JSON blocks in Version 4 were treated as a tooling or formatting issue rather than a gameplay requirement mismatch, and those commented sections were later removed.

# Drive Link 
[Unity package link](https://iiithydstudents-my.sharepoint.com/:u:/g/personal/gracy_garg_students_iiit_ac_in/IQB0u9SPTfQvSqoiTwG3zy9TARbAWZ3A8UiZPk_eIOrbXDo?e=6mEV2p)
