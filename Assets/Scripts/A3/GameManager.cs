using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CS464.A3
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField, Min(1)] private int targetScore = 8;
        [SerializeField, Min(1)] private int startingLives = 3;
        [SerializeField] private PlayerController player;
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Button winRestart;
        [SerializeField] private Button loseRestart;
        [SerializeField, Min(0f)] private float respawnDelay = 0.8f;
        [SerializeField] private string levelLabel = "01 / TRAINING COURTYARD";
        [SerializeField] private string lesson = "COLLECT / BOUNCE / FINISH";
        [SerializeField] private string objective = "Collect 8 coins, ride the bounce pad, then reach FINISH.";
        [SerializeField] private TMP_Text levelHeading;
        [SerializeField] private TMP_Text lessonText;
        [SerializeField] private TMP_Text winEyebrow;
        [SerializeField] private TMP_Text loseEyebrow;
        [SerializeField] private TMP_Text winTitle;
        [SerializeField] private TMP_Text winDetail;
        [SerializeField] private TMP_Text boostText;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private TMP_Text nextLevelText;

        private Rigidbody body;
        private Health health;
        private int score;
        private int lives;
        private bool respawning;
        private bool ended;
        private float messageUntil;
        private int lastBoostSeconds = -1;

        public int Score => score;
        public int TargetScore => targetScore;
        public int Lives => lives;
        public bool HasEnded => ended;
        public bool IsRespawning => respawning;
        public Transform RespawnPoint => respawnPoint;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1f;
            lives = startingLives;
            body = player.GetComponent<Rigidbody>();
            health = player.GetComponent<Health>();
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            winRestart.onClick.AddListener(Restart);
            loseRestart.onClick.AddListener(Restart);
            if (nextLevelButton != null) nextLevelButton.onClick.AddListener(NextLevel);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Start()
        {
            // All Awake methods, including Health's, have completed now.
            UpdateScore();
            UpdateHealth();
            livesText.text = $"LIVES  {lives}";
            if (levelHeading != null) levelHeading.text = levelLabel;
            if (lessonText != null) lessonText.text = lesson;
            if (winEyebrow != null) winEyebrow.text = levelLabel;
            if (loseEyebrow != null) loseEyebrow.text = levelLabel;
            if (winTitle != null) winTitle.text = SceneManager.GetActiveScene().buildIndex == 4
                ? "ALL FIVE LEVELS CLEARED" : "LEVEL CLEARED";
            if (winDetail != null) winDetail.text = "You collected the coins and reached the gate.";
            if (nextLevelText != null) nextLevelText.text = SceneManager.GetActiveScene().buildIndex == 4
                ? "BACK TO LEVEL01" : "NEXT LEVEL";
            SetDefaultMessage();
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
                Restart();
            if (!ended && messageUntil > 0f && Time.unscaledTime >= messageUntil)
                SetDefaultMessage();
            int seconds = Mathf.CeilToInt(player.BoostSecondsRemaining);
            if (boostText != null && seconds != lastBoostSeconds)
            {
                boostText.text = seconds > 0 ? $"SPEED x{player.SpeedMultiplier:0.0} / {seconds}s" : "SPEED / READY";
                lastBoostSeconds = seconds;
            }
        }

        public void AddScore(int amount)
        {
            if (ended || respawning || amount <= 0) return;
            score += amount;
            UpdateScore();
            if (score == targetScore)
                ShowMessage("Gate unlocked. Follow the green FINISH marker.", 4f);
        }

        public bool TryFinish()
        {
            if (ended || respawning) return false;
            if (score < targetScore)
            {
                ShowMessage($"Find {targetScore - score} more coin(s), then return to the gate.", 3f);
                return false;
            }
            ended = true;
            StopPlayer();
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            winRestart.Select();
            return true;
        }

        public void LoseLife()
        {
            if (ended || respawning) return;
            respawning = true;
            lives = Mathf.Max(0, lives - 1);
            livesText.text = $"LIVES  {lives}";
            StopPlayer();
            if (lives <= 0)
            {
                ended = true;
                losePanel.SetActive(true);
                Time.timeScale = 0f;
                loseRestart.Select();
            }
            else
            {
                ShowMessage("Take a breath. Returning to your last checkpoint...", respawnDelay + 1f);
                StartCoroutine(Respawn());
            }
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(respawnDelay);
            body.position = respawnPoint.position;
            body.rotation = respawnPoint.rotation;
            body.isKinematic = false;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            health.Restore(1.5f);
            player.enabled = true;
            FindFirstObjectByType<CameraFollow>()?.SnapToTarget();
            respawning = false;
        }

        private void StopPlayer()
        {
            player.ClearBoost();
            player.enabled = false;
            if (!body.isKinematic) body.linearVelocity = Vector3.zero;
            body.isKinematic = true;
        }

        public void SetCheckpoint(Transform point)
        {
            if (ended || respawning || respawnPoint == point) return;
            respawnPoint = point;
            ShowMessage("Checkpoint reached. Your coins are safe if you fall.", 3f);
        }

        public void UpdateHealth()
        {
            if (health == null) health = player.GetComponent<Health>();
            healthText.text = $"HEARTS  {Mathf.Max(0, health.Current)} / {health.Maximum}";
        }

        public void ShowMessage(string message, float seconds = 3f)
        {
            messageText.text = message;
            messageUntil = Time.unscaledTime + seconds;
        }

        private void SetDefaultMessage()
        {
            messageUntil = 0f;
            messageText.text = score >= targetScore
                ? "Gate unlocked - head to FINISH."
                : objective;
        }

        private void UpdateScore() => scoreText.text = $"{score:00} <color=#99A9AF>/ {targetScore:00}</color>";

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }

        public void NextLevel()
        {
            Time.timeScale = 1f;
            int index = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(index);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}
