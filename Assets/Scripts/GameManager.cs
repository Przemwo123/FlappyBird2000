using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Biom
{
    public string name;
    public GameObject ground;
    public List<GameObject> tubeList = new List<GameObject>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _info1Text;
    [SerializeField] private GameObject _newRecordText;
    [SerializeField] private Image _blackPanel;
    [SerializeField] private Text _textScoreGameOverPanel;
    [SerializeField] private Text _textBestGameOverPanel;
    [SerializeField] private Text _textScore;

    public GameObject birdPrefab;
    public TubeSpawn tubeSpawn;
    public bool isGameOver = false;
    public List<Biom> biomList = new List<Biom>();

    private int _repeating = 0;
    private int _score;
    private int _currentBiom = 0;
    private float _timeRestart = 0;
    private bool _isActive = false;
    private bool _isRestartLevel = false;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else if(S != this)
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsActive() => _isActive;
    public int GetCurrentBiom() => _currentBiom;

    void Start()
    {
        _blackPanel.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        _info1Text.SetActive(true);

        if (_gameOverCanvas.activeSelf) _gameOverCanvas.SetActive(false);
        Instantiate(birdPrefab);
    }

    private void Update()
    {
        if(isGameOver)
        {
            _timeRestart += Time.deltaTime;

            if(_timeRestart > 1 && (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Space))))
                ResetLevel();
        }
    }

    public void AddRepeating()
    {
        if (isGameOver || !_isActive) return;

        _repeating++;

        if (_repeating >= 4)
        {
            _repeating = 0;
            _currentBiom++;

            if (_currentBiom >= biomList.Count)
                _currentBiom = 0;
        }
    }

    public void Score()
    {
        if (isGameOver) return;

        _score++;
        _textScore.text = _score.ToString();
    }

    public void Die()
    {
        isGameOver = true;
        _gameOverCanvas.SetActive(true);
        _textScore.gameObject.SetActive(false);
        _textScoreGameOverPanel.text = _score.ToString();
        if(!HighScore.TrySetNewHighScore(_score))
        {
            _newRecordText.SetActive(false);
        }
        _textBestGameOverPanel.text = HighScore.GetHighScore().ToString();
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = _curve.Evaluate(t);
            _blackPanel.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    public void ResetLevel()
    {
        if (!_isRestartLevel)
        {
            _isRestartLevel = true;
            StartCoroutine(_ResetLevel());
        }
    }

    IEnumerator _ResetLevel()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = _curve.Evaluate(t);
            _blackPanel.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IsActive()
    {
        _isActive = true;
        _info1Text.GetComponent<Animator>().SetTrigger("isTurnOff");
    }
}
