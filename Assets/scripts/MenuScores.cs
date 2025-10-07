using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;


    public class MenuScores : MonoBehaviour
    {
        [SerializeField] public TMP_Text[] _entryTextObjects;
        [SerializeField] public TMP_InputField _usernameInputField;



        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>
        
            Leaderboards.board.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            });
        }
        
    }

