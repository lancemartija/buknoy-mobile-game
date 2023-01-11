using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class QuizResultsManager : MonoBehaviour
{
    [SerializeField]
    public QuizManager quizmanager;
    public static QuizResultsManager instance;
    public QuizResultsList quizresultslist;

    [SerializeField]
    private float loadingtime = 2f;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        StartCoroutine(FileCreate());
    }

    void Awake()
    {
        instance = this;
        if (!Directory.Exists(Application.persistentDataPath + "/QuizResults/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/QuizResults/");
        }
    }

    IEnumerator FileCreate()
    {
        yield return new WaitForSeconds(loadingtime);
        if (File.Exists(Application.persistentDataPath + "/QuizResults/QuizResults.xml"))
        {
            Debug.Log("High Scores already exist!");
        }
        else
        {
            quizmanager.SetDefaultResults(0, 0, 1);
            quizmanager.SetDefaultResults(0, 0, 2);
            quizmanager.SetDefaultResults(0, 0, 3);
            quizmanager.SetDefaultResults(0, 0, 4);
            quizmanager.Save();
            Debug.Log("High Score file created!");
        }
    }

    public void SaveScores(List<QuizResults> results)
    {
        quizresultslist.resultslist = results;
        XmlSerializer serializer = new XmlSerializer(typeof(QuizResultsList));
        FileStream stream = new FileStream(
            Application.persistentDataPath + "/QuizResults/QuizResults.xml",
            FileMode.Create
        );
        serializer.Serialize(stream, quizresultslist);
        Debug.Log("High Scores saved!");
        stream.Close();
    }

    public List<QuizResults> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/QuizResults/QuizResults.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(QuizResultsList));
            FileStream stream = new FileStream(
                Application.persistentDataPath + "/QuizResults/QuizResults.xml",
                FileMode.Open
            );
            quizresultslist = serializer.Deserialize(stream) as QuizResultsList;
            Debug.Log("High Scores loaded!");
        }
        else
        {
            StartCoroutine(FileCreate());
            Debug.Log("High Scores file not found! Creating new one...");
        }
        return quizresultslist.resultslist;
    }

    public void DeleteHighScoreData()
    {
        foreach (
            var directory in Directory.GetDirectories(
                Application.persistentDataPath + "/QuizResults/"
            )
        )
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }

        foreach (var file in Directory.GetFiles(Application.persistentDataPath + "/QuizResults/"))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
    }
}

[System.Serializable]
public class QuizResultsList
{
    public List<QuizResults> resultslist = new List<QuizResults>();
}
