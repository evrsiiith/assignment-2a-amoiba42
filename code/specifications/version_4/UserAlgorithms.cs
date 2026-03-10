using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Version_4
{
    // tracks vertical look angle across frames
    // (static field is fine — one player, one session)

    public static class UserAlgorithms
    {
        private static float _xRot = 0f;
        private static bool  _cursorLocked = false;
        private static bool  _playerViewInitialized = false;
        private static GameObject _activeRotatingObject = null;
        private static readonly HashSet<int> _openedChestIds = new HashSet<int>();
        private static readonly Dictionary<int, CupboardAnimationState> _cupboardStates = new Dictionary<int, CupboardAnimationState>();
        private const float CupboardOpenAngle = 90f;
        private const float CupboardDegreesPerSecond = 180f;
        private const float PlayerMoveSpeed = 4f;
        private const float ObjectRotationSpeed = 360f;
        private static readonly string[] CollectibleBookNames = { "book_brown", "book_red", "book_golden", "book_green" };
        private const string ChestAudioAssetPath = "Assets/Audio/Chest.wav";

        private sealed class CupboardAnimationState
        {
            public Transform LeftDoor;
            public Transform RightDoor;
            public Quaternion LeftClosedRotation;
            public Quaternion RightClosedRotation;
            public float CurrentAngle;
            public float TargetAngle;
        }

        // ── Player Movement ──────────────────────────────────────────────────

        // Called every frame — always true so UpdatePlayer runs continuously
        public static bool IsPlayerActive()
        {
            return true;
        }

        // Handles WASD movement, Q/E vertical motion, and mouse look every frame
        public static void UpdatePlayer(GameObject obj)
        {
            if (obj == null) return;

            // Lock cursor on first run
            if (!_cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _cursorLocked = true;
            }

            Camera cam = EnsureFirstPersonCamera(obj);

            // Mouse look — rotate player body left/right, camera up/down
            float mouseX = Input.GetAxis("Mouse X") * 150f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 150f * Time.deltaTime;

            bool isRotatingObject = _activeRotatingObject != null && Input.GetKey(KeyCode.R);
            if (isRotatingObject)
            {
                RotateActiveObject(mouseX, mouseY);
            }
            else
            {
                obj.transform.Rotate(Vector3.up * mouseX);

                _xRot -= mouseY;
                _xRot = Mathf.Clamp(_xRot, -80f, 80f);
                if (cam != null)
                    cam.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }

            // WASD movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            bool cupboardInteractionRequested = IsCupboardInteractionRequested();
            float y = 0f;

            if (Input.GetKey(KeyCode.E) && !cupboardInteractionRequested)
                y += 1f;

            if (Input.GetKey(KeyCode.Q))
                y -= 1f;

            Vector3 move = obj.transform.right * x + obj.transform.forward * z + Vector3.up * y;
            if (move.sqrMagnitude > 1f)
                move.Normalize();

            obj.transform.Translate(move * PlayerMoveSpeed * Time.deltaTime, Space.World);

            if (obj.transform.position.y < 0f)
            {
                Vector3 clampedPosition = obj.transform.position;
                clampedPosition.y = 0f;
                obj.transform.position = clampedPosition;
                Debug.Log("[Version_4] Player Y clamped to floor at y=0.");
            }

            if (Input.GetKeyDown(KeyCode.E) && !cupboardInteractionRequested)
                Debug.Log("[Version_4] Player vertical input: up (E).");

            if (Input.GetKeyDown(KeyCode.Q))
                Debug.Log("[Version_4] Player vertical input: down (Q).");

            UpdateCupboardAnimations();
        }

        private static Camera EnsureFirstPersonCamera(GameObject player)
        {
            Camera cam = player.GetComponentInChildren<Camera>();
            if (cam == null)
                cam = Camera.main;

            if (cam == null)
            {
                GameObject camObj = new GameObject("Player Camera");
                cam = camObj.AddComponent<Camera>();
                cam.tag = "MainCamera";
                camObj.AddComponent<AudioListener>();
            }

            Transform camTransform = cam.transform;
            if (camTransform.parent != player.transform)
                camTransform.SetParent(player.transform, false);

            if (!cam.CompareTag("MainCamera"))
                cam.tag = "MainCamera";

            EnsureSingleAudioListener(cam);

            camTransform.localPosition = new Vector3(0f, 0.8f, 0f);

            if (!_playerViewInitialized)
            {
                Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                    renderer.enabled = false;

                _playerViewInitialized = true;
            }

            return cam;
        }

        // ── Trumpet ─────────────────────────────────────────────────────────

        // Condition: player presses T while the center of the camera view is aimed at the trumpet
        public static bool IsTrumpetHovered()
        {
            GameObject trumpet = GameObject.Find("Trumpet");
            if (!Input.GetKeyDown(KeyCode.T))
                return false;

            bool isLooking = trumpet != null && IsPlayerLookingAt(trumpet);
            Debug.Log("[Version_4] Trumpet input (T). Hit=" + GetLookedAtObjectName() + ", targetMatched=" + isLooking + ".");
            return isLooking;
        }

        // Action: play the melody and mark trumpet as played
        public static void PlayTrumpet(GameObject obj)
        {
            bool audioPlayed = PlayAttachedAudio(obj, true);
            VReqDV.StateAccessor.SetState("Trumpet", "played", obj, "Version_4");
            Debug.Log("[Version_4] Trumpet played. Audio=" + audioPlayed + ", state=played.");
            TryOpenChestIfReady("trumpet played");
        }

        // ── Book ─────────────────────────────────────────────────────────────

        // Condition: player looks at book and presses F
        public static bool IsBookGrabbed(GameObject obj)
        {
            if (!Input.GetKeyDown(KeyCode.F))
                return false;

            bool isLooking = IsPlayerLookingAt(obj);
            Debug.Log("[Version_4] Book input (F). Target=" + (obj != null ? obj.name : "null") + ", hit=" + GetLookedAtObjectName() + ", targetMatched=" + isLooking + ".");
            return isLooking;
        }

        // Action: play collect sound, mark as destroyed, remove from scene
        public static void CollectBook(GameObject obj)
        {
            if (obj == null) return;

            bool audioPlayed = PlayAttachedClipAtPoint(obj);
            VReqDV.StateAccessor.SetState(obj.name, "destroyed", obj, "Version_4");
            Debug.Log("[Version_4] Book collected: " + obj.name + ". Audio=" + audioPlayed + ", state=destroyed.");
            TryOpenChestIfReady("book collected: " + obj.name);
            GameObject.Destroy(obj);
        }

        public static bool AllBooksDestroyed()
        {
            foreach (string bookName in CollectibleBookNames)
            {
                GameObject book = GameObject.Find(bookName);
                if (book != null && !VReqDV.StateAccessor.IsState(bookName, "destroyed", book, "Version_4"))
                    return false;
            }

            return true;
        }

        // ── Chest ────────────────────────────────────────────────────────────

        public static bool ShouldOpenChest(GameObject obj)
        {
            if (obj == null)
                return false;

            if (_openedChestIds.Contains(obj.GetInstanceID()))
                return false;

            GameObject trumpet = GameObject.Find("Trumpet");
            bool trumpetPlayed = trumpet != null && VReqDV.StateAccessor.IsState("Trumpet", "played", trumpet, "Version_4");
            return trumpetPlayed && AllBooksDestroyed();
        }

        // Action: play chest audio once and remove the chest from the scene
        public static void OpenChest(GameObject obj)
        {
            if (obj == null) return;

            int chestId = obj.GetInstanceID();
            if (_openedChestIds.Contains(chestId))
                return;

            bool audioPlayed = PlayAttachedClipAtPoint(obj) || PlayEditorAudioFallback(obj, ChestAudioAssetPath);
            _openedChestIds.Add(chestId);
            GameObject.Destroy(obj);
            Debug.Log("[Version_4] Chest destroyed after unlock. Audio=" + audioPlayed + ".");
        }

        // ── Cupboard ─────────────────────────────────────────────────────────

        // Condition: player looks at cupboard and presses E
        public static bool IsCupboardInteracted(GameObject obj)
        {
            if (!Input.GetKeyDown(KeyCode.E))
                return false;

            bool isLooking = IsPlayerLookingAt(obj);
            Debug.Log("[Version_4] Cupboard input (E). Hit=" + GetLookedAtObjectName() + ", targetMatched=" + isLooking + ".");
            return isLooking;
        }

        // Action: toggle the cupboard doors and flip its state
        public static void ToggleCupboard(GameObject obj)
        {
            if (obj == null) return;

            CupboardAnimationState cupboard = GetOrCreateCupboardState(obj);
            if (cupboard == null)
            {
                Debug.LogWarning("[Version_4] Cupboard toggle skipped. Door transforms could not be resolved for " + obj.name + ".");
                return;
            }

            bool shouldOpen = VReqDV.StateAccessor.IsState("Cupboard", "closed", obj, "Version_4");
            cupboard.TargetAngle = shouldOpen ? CupboardOpenAngle : 0f;
            bool audioPlayed = PlayAttachedAudio(obj);

            if (shouldOpen)
                VReqDV.StateAccessor.SetState("Cupboard", "open", obj, "Version_4");
            else
                VReqDV.StateAccessor.SetState("Cupboard", "closed", obj, "Version_4");

            Debug.Log("[Version_4] Cupboard toggled. Audio=" + audioPlayed + ", state=" + (shouldOpen ? "open" : "closed") + ", targetAngle=" + cupboard.TargetAngle + ".");
        }

        // ── Statue ───────────────────────────────────────────────────────────

        // Condition: player looks at a rotatable object and presses R
        public static bool IsRotatableGrabbed(GameObject obj)
        {
            if (!Input.GetKeyDown(KeyCode.R))
                return false;

            bool isLooking = obj != null && IsPlayerLookingAt(obj);
            Debug.Log("[Version_4] Rotate input (R down). Target=" + (obj != null ? obj.name : "null") + ", hit=" + GetLookedAtObjectName() + ", targetMatched=" + isLooking + ".");
            return isLooking;
        }

        public static bool IsStatueGrabbed(GameObject obj)
        {
            return IsRotatableGrabbed(obj);
        }

        // Action: mark rotatable object as being rotated
        public static void StartRotatingObject(GameObject obj)
        {
            if (obj == null) return;

            _activeRotatingObject = obj;
            VReqDV.StateAccessor.SetState(obj.name, "rotating", obj, "Version_4");
            Debug.Log("[Version_4] Rotation started for " + obj.name + ". state=rotating.");
        }

        public static void StartRotatingStatue(GameObject obj)
        {
            StartRotatingObject(obj);
        }

        // Condition: player releases R while this rotatable object is rotating
        public static bool IsRotatableReleased(GameObject obj)
        {
            return Input.GetKeyUp(KeyCode.R) && _activeRotatingObject == obj;
        }

        public static bool IsStatueReleased(GameObject obj)
        {
            return IsRotatableReleased(obj);
        }

        // Action: return rotatable object to idle state
        public static void StopRotatingObject(GameObject obj)
        {
            if (obj == null) return;

            VReqDV.StateAccessor.SetState(obj.name, "idle", obj, "Version_4");
            if (_activeRotatingObject == obj)
                _activeRotatingObject = null;

            Debug.Log("[Version_4] Rotation stopped for " + obj.name + ". state=idle.");
        }

        public static void StopRotatingStatue(GameObject obj)
        {
            StopRotatingObject(obj);
        }

        // ── Utility ──────────────────────────────────────────────────────────

        // Returns true when a 3 m ray cast straight forward from the main camera hits this exact object
        private static bool IsPlayerLookingAt(GameObject obj)
        {
            if (!TryGetLookHit(out RaycastHit hit))
                return false;

            Transform hitTransform = hit.collider.transform;
            return hitTransform == obj.transform || hitTransform.IsChildOf(obj.transform);
        }

        private static bool TryGetLookHit(out RaycastHit hit)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                hit = default;
                return false;
            }

            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            return Physics.Raycast(ray, out hit, 3f);
        }

        private static string GetLookedAtObjectName()
        {
            if (!TryGetLookHit(out RaycastHit hit))
                return "nothing";

            return hit.collider != null ? hit.collider.gameObject.name : "nothing";
        }

        private static void UpdateCupboardAnimations()
        {
            if (_cupboardStates.Count == 0)
                return;

            List<int> missingKeys = null;

            foreach (KeyValuePair<int, CupboardAnimationState> pair in _cupboardStates)
            {
                CupboardAnimationState state = pair.Value;
                if (state.LeftDoor == null || state.RightDoor == null)
                {
                    if (missingKeys == null)
                        missingKeys = new List<int>();

                    missingKeys.Add(pair.Key);
                    continue;
                }

                state.CurrentAngle = Mathf.MoveTowards(state.CurrentAngle, state.TargetAngle, CupboardDegreesPerSecond * Time.deltaTime);
                state.LeftDoor.localRotation = state.LeftClosedRotation * Quaternion.Euler(0f, -state.CurrentAngle, 0f);
                state.RightDoor.localRotation = state.RightClosedRotation * Quaternion.Euler(0f, state.CurrentAngle, 0f);
            }

            if (missingKeys == null)
                return;

            foreach (int key in missingKeys)
                _cupboardStates.Remove(key);
        }

        private static CupboardAnimationState GetOrCreateCupboardState(GameObject obj)
        {
            int instanceId = obj.GetInstanceID();
            if (_cupboardStates.TryGetValue(instanceId, out CupboardAnimationState cachedState))
                return cachedState;

            if (!TryResolveCupboardDoors(obj, out Transform leftDoor, out Transform rightDoor))
                return null;

            bool isOpen = VReqDV.StateAccessor.IsState("Cupboard", "open", obj, "Version_4");
            CupboardAnimationState state = new CupboardAnimationState
            {
                LeftDoor = leftDoor,
                RightDoor = rightDoor,
                LeftClosedRotation = leftDoor.localRotation,
                RightClosedRotation = rightDoor.localRotation,
                CurrentAngle = isOpen ? CupboardOpenAngle : 0f,
                TargetAngle = isOpen ? CupboardOpenAngle : 0f
            };

            _cupboardStates[instanceId] = state;
            state.LeftDoor.localRotation = state.LeftClosedRotation * Quaternion.Euler(0f, -state.CurrentAngle, 0f);
            state.RightDoor.localRotation = state.RightClosedRotation * Quaternion.Euler(0f, state.CurrentAngle, 0f);
            return state;
        }

        private static bool TryResolveCupboardDoors(GameObject obj, out Transform leftDoor, out Transform rightDoor)
        {
            leftDoor = null;
            rightDoor = null;

            if (obj == null)
                return false;

            Transform[] transforms = obj.GetComponentsInChildren<Transform>(true);
            List<Transform> doorCandidates = new List<Transform>();

            foreach (Transform child in transforms)
            {
                if (child == obj.transform)
                    continue;

                string lowerName = child.name.ToLowerInvariant();
                if (leftDoor == null && lowerName == "doorleftgrp")
                    leftDoor = child;

                if (rightDoor == null && lowerName == "doorrightgrp")
                    rightDoor = child;

                bool isDoor = lowerName.Contains("door");

                if (leftDoor == null && lowerName.Contains("left") && isDoor)
                    leftDoor = child;

                if (rightDoor == null && lowerName.Contains("right") && isDoor)
                    rightDoor = child;

                if (isDoor)
                    doorCandidates.Add(child);
            }

            if (leftDoor == null || rightDoor == null)
            {
                foreach (Transform candidate in doorCandidates)
                {
                    if (leftDoor == null)
                    {
                        leftDoor = candidate;
                        continue;
                    }

                    if (rightDoor == null && candidate != leftDoor)
                    {
                        rightDoor = candidate;
                        break;
                    }
                }
            }

            if (leftDoor == null || rightDoor == null)
            {
                List<Transform> childCandidates = new List<Transform>();
                foreach (Transform child in transforms)
                {
                    if (child != obj.transform)
                        childCandidates.Add(child);
                }

                if (leftDoor == null && childCandidates.Count > 0)
                    leftDoor = childCandidates[0];

                if (rightDoor == null && childCandidates.Count > 1)
                    rightDoor = childCandidates[1];
            }

            return leftDoor != null && rightDoor != null;
        }

        private static bool IsCupboardInteractionRequested()
        {
            if (!Input.GetKeyDown(KeyCode.E))
                return false;

            GameObject cupboard = GameObject.Find("Cupboard");
            return cupboard != null && IsPlayerLookingAt(cupboard);
        }

        private static void TryOpenChestIfReady(string triggerSource)
        {
            GameObject chest = GameObject.Find("Chest");
            if (chest == null)
            {
                Debug.LogWarning("[Version_4] Chest readiness check skipped after " + triggerSource + ". Chest was not found in the scene.");
                return;
            }

            if (!ShouldOpenChest(chest))
            {
                Debug.Log("[Version_4] Chest readiness check after " + triggerSource + ": conditions not met yet.");
                return;
            }

            Debug.Log("[Version_4] Chest readiness check after " + triggerSource + ": conditions met, opening chest.");
            OpenChest(chest);
        }

        private static void RotateActiveObject(float mouseX, float mouseY)
        {
            if (_activeRotatingObject == null)
                return;

            float yawDelta = mouseX * ObjectRotationSpeed * Time.deltaTime;
            _activeRotatingObject.transform.Rotate(Vector3.up, yawDelta, Space.World);
        }

        private static bool PlayAttachedAudio(GameObject obj)
        {
            return PlayAttachedAudio(obj, false);
        }

        private static bool PlayAttachedAudio(GameObject obj, bool restartIfPlaying)
        {
            AudioSource audio = FindAttachedAudioSource(obj);
            if (audio == null || audio.clip == null)
            {
                Debug.LogWarning("[Version_4] No attached audio clip found for " + (obj != null ? obj.name : "null") + ".");
                return false;
            }

            if (restartIfPlaying && audio.isPlaying)
                audio.Stop();

            audio.Play();
            return true;
        }

        private static bool PlayAttachedClipAtPoint(GameObject obj)
        {
            AudioSource audio = FindAttachedAudioSource(obj);
            if (audio == null || audio.clip == null)
            {
                Debug.LogWarning("[Version_4] No attached audio clip found for " + (obj != null ? obj.name : "null") + ".");
                return false;
            }

            AudioSource.PlayClipAtPoint(audio.clip, obj.transform.position);
            return true;
        }

        private static AudioSource FindAttachedAudioSource(GameObject obj)
        {
            if (obj == null) return null;

            AudioSource[] audioSources = obj.GetComponentsInChildren<AudioSource>(true);
            foreach (AudioSource audio in audioSources)
            {
                if (audio != null && audio.clip != null)
                    return audio;
            }

            return null;
        }

        private static bool PlayEditorAudioFallback(GameObject obj, string assetPath)
        {
#if UNITY_EDITOR
            if (obj == null || string.IsNullOrEmpty(assetPath))
                return false;

            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);
            if (clip == null)
                return false;

            AudioSource.PlayClipAtPoint(clip, obj.transform.position);
            return true;
#else
            return false;
#endif
        }

        private static void EnsureSingleAudioListener(Camera activeCamera)
        {
            if (activeCamera == null)
                return;

            AudioListener activeListener = activeCamera.GetComponent<AudioListener>();
            if (activeListener == null)
                activeListener = activeCamera.gameObject.AddComponent<AudioListener>();

            AudioListener[] listeners = Object.FindObjectsOfType<AudioListener>(true);
            foreach (AudioListener listener in listeners)
            {
                if (listener == null)
                    continue;

                listener.enabled = listener == activeListener;
            }
        }
    }
}