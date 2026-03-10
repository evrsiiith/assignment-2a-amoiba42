using UnityEngine;
using System.Collections.Generic;

namespace Version_2
{
    // tracks vertical look angle across frames
    // (static field is fine — one player, one session)

    public static class UserAlgorithms
    {
        private static float _xRot = 0f;
        private static bool  _cursorLocked = false;
        private static bool  _playerViewInitialized = false;
        private static readonly Dictionary<int, CupboardAnimationState> _cupboardStates = new Dictionary<int, CupboardAnimationState>();
        private const float CupboardOpenAngle = 270f;
        private const float CupboardDegreesPerSecond = 180f;

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

        // Handles WASD movement + mouse look every frame
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

            obj.transform.Rotate(Vector3.up * mouseX);

            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -80f, 80f);
            if (cam != null)
                cam.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);

            // WASD movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = obj.transform.right * x + obj.transform.forward * z;
            obj.transform.Translate(move * 4f * Time.deltaTime, Space.World);

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
            return trumpet != null && Input.GetKeyDown(KeyCode.T) && IsPlayerLookingAt(trumpet);
        }

        // Action: play the melody and mark trumpet as played
        public static void PlayTrumpet(GameObject obj)
        {
            bool audioPlayed = PlayAttachedAudio(obj);
            VReqDV.StateAccessor.SetState("Trumpet", "played", obj, "Version_2");
            Debug.Log("[Version_2] Trumpet played. Audio=" + audioPlayed + ", state=played.");
        }

        // ── Book ─────────────────────────────────────────────────────────────

        // Condition: player looks at book and presses F
        public static bool IsBookGrabbed(GameObject obj)
        {
            return Input.GetKeyDown(KeyCode.F) && IsPlayerLookingAt(obj);
        }

        // Action: play collect sound, mark as destroyed, remove from scene
        public static void CollectBook(GameObject obj)
        {
            if (obj == null) return;

            bool audioPlayed = PlayAttachedClipAtPoint(obj);
            VReqDV.StateAccessor.SetState("book_brown", "destroyed", obj, "Version_2");
            Debug.Log("[Version_2] Book collected: " + obj.name + ". Audio=" + audioPlayed + ", state=destroyed.");
            GameObject.Destroy(obj);
        }

        // ── Chest ────────────────────────────────────────────────────────────

        // Action: trigger open animation and mark chest as open
        public static void OpenChest(GameObject obj)
        {
            if (obj == null) return;

            Animator a = obj.GetComponent<Animator>();
            if (a != null) a.SetTrigger("Open");
            bool audioPlayed = PlayAttachedAudio(obj);
            VReqDV.StateAccessor.SetState("Chest", "open", obj, "Version_2");
            Debug.Log("[Version_2] Chest opened. AnimatorTriggered=" + (a != null) + ", Audio=" + audioPlayed + ", state=open.");
        }

        // ── Cupboard ─────────────────────────────────────────────────────────

        // Condition: player looks at cupboard and presses E
        public static bool IsCupboardInteracted(GameObject obj)
        {
            return Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAt(obj);
        }

        // Action: toggle the cupboard doors and flip its state
        public static void ToggleCupboard(GameObject obj)
        {
            if (obj == null) return;

            CupboardAnimationState cupboard = GetOrCreateCupboardState(obj);
            if (cupboard == null)
            {
                Debug.LogWarning("[Version_2] Cupboard toggle skipped. Door transforms could not be resolved for " + obj.name + ".");
                return;
            }

            bool shouldOpen = VReqDV.StateAccessor.IsState("Cupboard", "closed", obj, "Version_2");
            cupboard.TargetAngle = shouldOpen ? CupboardOpenAngle : 0f;
            bool audioPlayed = PlayAttachedAudio(obj);

            if (shouldOpen)
                VReqDV.StateAccessor.SetState("Cupboard", "open", obj, "Version_2");
            else
                VReqDV.StateAccessor.SetState("Cupboard", "closed", obj, "Version_2");

            Debug.Log("[Version_2] Cupboard toggled. Audio=" + audioPlayed + ", state=" + (shouldOpen ? "open" : "closed") + ", targetAngle=" + cupboard.TargetAngle + ".");
        }

        // ── Statue ───────────────────────────────────────────────────────────

        // Condition: player looks at statue and holds R
        public static bool IsStatueGrabbed(GameObject obj)
        {
            return Input.GetKeyDown(KeyCode.R) && IsPlayerLookingAt(obj);
        }

        // Action: mark statue as being rotated
        public static void StartRotatingStatue(GameObject obj)
        {
            VReqDV.StateAccessor.SetState("LionStatue", "rotating", obj, "Version_2");
            Debug.Log("[Version_2] Statue rotation started. state=rotating.");
        }

        // Condition: player releases R while statue is rotating
        public static bool IsStatueReleased(GameObject obj)
        {
            return Input.GetKeyUp(KeyCode.R);
        }

        // Action: return statue to idle state
        public static void StopRotatingStatue(GameObject obj)
        {
            VReqDV.StateAccessor.SetState("LionStatue", "idle", obj, "Version_2");
            Debug.Log("[Version_2] Statue rotation stopped. state=idle.");
        }

        // ── Utility ──────────────────────────────────────────────────────────

        // Returns true when a 3 m ray cast straight forward from the main camera hits this exact object
        private static bool IsPlayerLookingAt(GameObject obj)
        {
            Camera cam = Camera.main;
            if (cam == null) return false;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (!Physics.Raycast(ray, out RaycastHit hit, 3f))
                return false;

            Transform hitTransform = hit.collider.transform;
            return hitTransform == obj.transform || hitTransform.IsChildOf(obj.transform);
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

            bool isOpen = VReqDV.StateAccessor.IsState("Cupboard", "open", obj, "Version_2");
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

        private static bool PlayAttachedAudio(GameObject obj)
        {
            AudioSource audio = FindAttachedAudioSource(obj);
            if (audio == null || audio.clip == null)
                return false;

            audio.Play();
            return true;
        }

        private static bool PlayAttachedClipAtPoint(GameObject obj)
        {
            AudioSource audio = FindAttachedAudioSource(obj);
            if (audio == null || audio.clip == null)
                return false;

            AudioSource.PlayClipAtPoint(audio.clip, obj.transform.position);
            return true;
        }

        private static AudioSource FindAttachedAudioSource(GameObject obj)
        {
            if (obj == null) return null;

            AudioSource audio = obj.GetComponent<AudioSource>();
            if (audio != null && audio.clip != null)
                return audio;

            return obj.GetComponentInChildren<AudioSource>();
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