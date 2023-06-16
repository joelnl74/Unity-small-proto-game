using Game.Settings;
using System.Collections;
using UnityEngine;


namespace Game
{
    /// <summary>
    /// Player Character
    /// </summary>
    public class Player : MonoBehaviour
    {
        public enum Direction
        {
            left, right
        }

        [Header("Components")]
        [SerializeField] private PlayerBulletSpawner m_player_bullet_spawner;
        [SerializeField] private ParticleSystem m_particle_system;

        [Header("Player parameters")]
        [SerializeField] private PlayerSettings m_player_settings;

        private Quaternion m_starting_rotation;
        private Quaternion m_max_rot;
        private Quaternion m_min_rot;

        private float m_ortho_width;
        private float m_current_move_speed;
        private float m_cooldown;

        private bool m_is_dodging = false;

        //------------------------------------------------------------------------------
        public void Start()
        {
            m_starting_rotation = Quaternion.Euler(-90, 0, 0);
            m_current_move_speed = m_player_settings.move_speed;
            m_ortho_width = Camera.main.orthographicSize;

            m_min_rot = Quaternion.Euler(new Vector3(0, m_player_settings.min_rotation, 0));
            m_max_rot = Quaternion.Euler(new Vector3(0, m_player_settings.max_rotation, 0));
        }

        /// <summary>
        /// Handle input and updating positions.
        /// </summary>
        private void Update()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            UpdatePosition(horizontalAxis, verticalAxis);
            UpdateRotation(horizontalAxis);

            // Shoot.
            if (Input.GetKeyDown(KeyCode.Z) && m_cooldown <= 0)
            {
                m_cooldown = m_player_settings.fire_cooldown;
                m_player_bullet_spawner.Get();
            }

            // Dodge.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(HandleDodgeRoll(Direction.left));
            }

            // Dodge.
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(HandleDodgeRoll(Direction.right));
            }

            m_cooldown -= Time.deltaTime;
        }

        /// <summary>
        /// Update rotation based on players input.
        /// </summary>
        /// <param name="horizontalAxis">Horizontal axis amount of input.</param>
        private void UpdateRotation(float horizontalAxis)
        {
            if (horizontalAxis == 0 && transform.eulerAngles.y != 0 && m_is_dodging == false)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_starting_rotation, m_player_settings.rotation_speed * Time.deltaTime);

                return;
            }

            float yRotation = m_player_settings.rotation_speed * -horizontalAxis * Time.deltaTime;

            if (transform.rotation.y > m_max_rot.y && horizontalAxis < 0
                || transform.rotation.y < m_min_rot.y && horizontalAxis > 0)
            {
                yRotation = 0;
            }

            transform.RotateAround(transform.position, Vector3.up, yRotation);
        }

        /// <summary>
        /// Update position based on players input.
        /// </summary>
        /// <param name="horizontalAxis">Horizontal axis amount of input.</param>
        /// <param name="verticalAxis">Vertical axis amount of input.</param>
        private void UpdatePosition(float horizontalAxis, float verticalAxis)
        {
            Vector3 position = transform.position;

            position.x += horizontalAxis * m_current_move_speed * Time.deltaTime;
            position.y += verticalAxis * m_current_move_speed * Time.deltaTime;

            transform.position = GetPositionWithinViewPort(position);

            if (horizontalAxis != 0 || verticalAxis != 0)
            {
                m_particle_system.Play();

                return;
            }

            m_particle_system.Stop();
        }

        /// <summary>
        /// Use the dodge role ability.
        /// </summary>
        /// <param name="direction">Direction used for the dodge roll.</param>
        /// <returns></returns>
        private IEnumerator HandleDodgeRoll(Direction direction)
        {
            if (m_is_dodging)
            {
                yield break;
            }

            Quaternion startRot = transform.rotation;
            float t = 0.0f;

            ChangeDodgeState(true, m_player_settings.dodge_speed);

            while (t < m_player_settings.dodge_duration)
            {
                Vector3 dodgeDirection = direction == Direction.left ? Vector3.forward : Vector3.back;

                t += Time.deltaTime;
                transform.rotation = startRot * Quaternion.AngleAxis(t / m_player_settings.dodge_duration * 360f, dodgeDirection);
                yield return null;
            }

            ChangeDodgeState(false, m_player_settings.move_speed);

            transform.rotation = startRot;
        }

        /// <summary>
        /// Clamp player position to screen.
        /// </summary>
        /// <param name="position">Position wanting to move.</param>
        /// <returns></returns>
        private Vector3 GetPositionWithinViewPort(Vector3 position)
        {
            float orthoAspectWidth = m_ortho_width * Camera.main.aspect;

            var clampedXPos = Mathf.Clamp(position.x, -orthoAspectWidth, orthoAspectWidth);
            var clampedYPos = Mathf.Clamp(position.y, -m_ortho_width, m_ortho_width);

            return new Vector3(clampedXPos, clampedYPos, 0);
        }

        /// <summary>
        /// Change character state
        /// </summary>
        /// <param name="dodging">is dodging</param>
        /// <param name="speed">player speed change</param>
        private void ChangeDodgeState(bool dodging, float speed)
        {
            m_is_dodging = dodging;
            m_current_move_speed = speed;
        }
    }
}
