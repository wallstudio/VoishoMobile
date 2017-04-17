using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum State : int {
        IDLE,
        MENU,
        // Mode
        TOUCH,
        FEED,
        CLEAN,
        GAME,
        GARALLY,
        COMMUNICATION,
        CONFIG
    }
    public static State GameState;
    public State gameState;


    static Text Title;
    static Text Left;
    static Text Center;
    static Text Right;
    static Text Instrundtion;
    static Text MenInd;
    static Text GarInd;
    static Text ModeText;

    static TextureRoll Hurt;
    static TextureRoll Life;
    static TextureRoll Mode;
    static TextureRoll Creature;
    static TextureRoll Food;
    static TextureRoll Cleaner;
    static TextureRoll Hand;
    static TextureRoll Meal;
    static TextureRoll Poison;
    static TextureRoll Dirt;

    static AudioSource Speaker;
    [SerializeField]
    AudioClip[] Audios;

    public int AutoBackTimer;

    static string[] ModeTextes = new string[] {
        "ﾅﾃﾞﾙ","ｺﾞﾊﾝ","ｿｳｼﾞ","ｱｿﾌﾞ","ｷﾞｬﾗﾘｰ","ﾂｳｼﾝ","ｾｯﾃｲ","ﾓﾄﾞﾙ"
    };

    bool IsSQuatting = true;
    bool IsHappy = false;

    int GFase = 0;

    // Use this for initialization
    void Start () {
        Title = GameObject.Find("TITLE").GetComponent<Text>();
        Left = GameObject.Find("LEFT").GetComponent<Text>();
        Center = GameObject.Find("CENTER").GetComponent<Text>();
        Right = GameObject.Find("RIGHT").GetComponent<Text>();
        Instrundtion = GameObject.Find("INSTRUCTION").GetComponent<Text>();
        MenInd = GameObject.Find("MEN_IND").GetComponent<Text>();
        GarInd = GameObject.Find("GAR_IND").GetComponent<Text>();
        ModeText = GameObject.Find("MODE_TEXT").GetComponent<Text>();

        Hurt = GameObject.Find("Hurt").GetComponent<TextureRoll>();
        Life = GameObject.Find("Life").GetComponent<TextureRoll>();
        Mode = GameObject.Find("Mode").GetComponent<TextureRoll>();
        Creature = GameObject.Find("Creature").GetComponent<TextureRoll>();
        Food = GameObject.Find("Food").GetComponent<TextureRoll>();
        Cleaner = GameObject.Find("Cleaner").GetComponent<TextureRoll>();
        Hand = GameObject.Find("Hand").GetComponent<TextureRoll>();
        Meal = GameObject.Find("Meal").GetComponent<TextureRoll>();
        Poison = GameObject.Find("Poison").GetComponent<TextureRoll>();
        Dirt = GameObject.Find("Dirt").GetComponent<TextureRoll>();
        Speaker = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Squat());
        StartCoroutine(LifeCycle(0.05f));
    }
	
	// Update is called once per frame
	void Update () {
        if (Dead) return;

        Hurt.TexNum = LifeData.Love;
        Life.TexNum = LifeData.Life;
        switch (GameState) {
            case State.IDLE:
                SetUI("", "SAVE", "MENU", "SLEEP", "", "", "", "");
                ZSet(Life.transform, 0);
                ZSet(Hurt.transform, 0);
                Creature.transform.localPosition = Vector3.zero;
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, LifeData.Hungery > 10 ? 0 : 1);
                ZSet(Poison.transform, LifeData.Sick > 0 ? 0 : 1);
                Dirt.TexNum = Dirt.GetLen() - 1 - LifeData.Dirty;
                ZSet(Dirt.transform, 0);
                IsSQuatting = true;
                if (!Dead && LifeData.Life == 0) {
                    Play("Dead");
                    Dead = true;
                }
                break;
            case State.MENU:
                SetUI("MENU", "<", "OK", ">", "", (Mode.TexNum + 1).ToString(), "", ModeTextes[Mode.TexNum]);
                ZSet(Life.transform, 0);
                ZSet(Hurt.transform, 0);
                Creature.transform.localPosition = new Vector3(0.25f, 0, 0);
                ZSet(Mode.transform, 0);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = false;
                IsSQuatting = true;
                break;
            case State.TOUCH:
                SetUI(ModeTextes[(int)GameState - 2], "", "", "", "", "", "", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                Creature.transform.localPosition = Vector3.zero;
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 0);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = true;
                IsSQuatting = true;
                break;
            case State.FEED:
                SetUI(ModeTextes[(int)GameState - 2], "", "", "", "", "", "", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                Creature.transform.localPosition = new Vector3(0.1f, 0, 0);
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 0);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = true;
                IsSQuatting = true;
                break;
            case State.CLEAN:
                SetUI(ModeTextes[(int)GameState - 2], "", "", "", "", "", "", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                ZSet(Creature.transform, 1);
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 0);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 0);
                IsHappy = false;
                break;
            case State.CONFIG:
                SetUI(ModeTextes[(int)GameState - 2], "UP", "OK", "DOWN", "", "", "", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                ZSet(Creature.transform, 1);
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = false;
                break;
            case State.GARALLY:
                SetUI(ModeTextes[(int)GameState - 2], "<", "BACK", ">", (Creature.TexNum + 1).ToString(), "", "", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                Creature.transform.localPosition = Vector3.zero;
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = false;
                IsSQuatting = false;
                break;
            case State.COMMUNICATION:
                SetUI(ModeTextes[(int)GameState - 2], "", "BACK", "", "", "", "せんようｽﾏﾎｱﾌﾟﾘ\nでよみこんでね‼", "");
                ZSet(Life.transform, 1);
                ZSet(Hurt.transform, 1);
                ZSet(Creature.transform, 1);
                ZSet(Mode.transform, 1);
                ZSet(Food.transform, 1);
                ZSet(Cleaner.transform, 1);
                ZSet(Hand.transform, 1);
                ZSet(Meal.transform, 1);
                ZSet(Poison.transform, 1);
                ZSet(Dirt.transform, 1);
                IsHappy = false;
                break;
            case State.GAME:
                switch (GFase) {
                    case 0:
                        SetUI(ModeTextes[(int)GameState - 2], "BACK", "OK", "", "", "", "たいおうするﾎﾞﾀﾝ\nでおいかけよう‼", "");
                        ZSet(Life.transform, 1);
                        ZSet(Hurt.transform, 1);
                        ZSet(Creature.transform, 1);
                        ZSet(Mode.transform, 1);
                        ZSet(Food.transform, 1);
                        ZSet(Cleaner.transform, 1);
                        ZSet(Hand.transform, 1);
                        ZSet(Meal.transform, 1);
                        ZSet(Poison.transform, 1);
                        ZSet(Dirt.transform, 1);
                        IsSQuatting = false;
                        IsHappy = false;
                        break;
                    case 1:
                        ZSet(Life.transform, 1);
                        ZSet(Hurt.transform, 1);
                        SetUI(ModeTextes[(int)GameState - 2], "^", "^", "^", "", "",
                            String.Format("TIME                          SCORE\n{0}                                {1}\n\n\n\n",AutoBackTimer , Point), "");
                        UnityEngine.Random.seed = DateTime.Now.Second;
                        Pos = UnityEngine.Random.Range(0, 3);
                        switch (Pos) {
                            case 0:
                                Creature.transform.localPosition = Vector3.zero;
                                break;
                            case 1:
                                Creature.transform.localPosition = Vector3.zero + Vector3.right * 0.3f;
                                break;
                            case 2:
                                Creature.transform.localPosition = Vector3.zero + Vector3.right * -0.3f;
                                break;
                        }
                        if(AutoBackTimer < 2) {
                            AutoBackTimer = 50;
                            GFase++;
                        }
                        ZSet(Mode.transform, 1);
                        ZSet(Food.transform, 1);
                        ZSet(Cleaner.transform, 1);
                        ZSet(Hand.transform, 1);
                        ZSet(Meal.transform, 1);
                        ZSet(Poison.transform, 1);
                        ZSet(Dirt.transform, 1);
                        IsSQuatting = false;
                        IsHappy = false;
                        break;
                    case 2:
                        SetUI(ModeTextes[(int)GameState - 2], "", "OK", "", "", "", String.Format("SCORE {0}", Point), "");
                        ZSet(Life.transform, 1);
                        ZSet(Hurt.transform, 1);
                        ZSet(Creature.transform, 1);
                        ZSet(Mode.transform, 1);
                        ZSet(Food.transform, 1);
                        ZSet(Cleaner.transform, 1);
                        ZSet(Hand.transform, 1);
                        ZSet(Meal.transform, 1);
                        ZSet(Poison.transform, 1);
                        ZSet(Dirt.transform, 1);
                        IsSQuatting = false;
                        IsHappy = false;
                        break;
                }
                break;
        }
    }

    void ZSet(Transform dest, float z) {
        dest.localPosition = new Vector3(dest.localPosition.x, dest.localPosition.y, z);
    }

    IEnumerator AutoBack(State toBack) {
        while (AutoBackTimer > 0) {
            yield return new WaitForSeconds(0.1f);
            AutoBackTimer--;
        }
        GameState = toBack;
    }

    void SetUI(string title, string left, string center, string right, string garInd, string menInd, string instruction, string modeText) {
        Title.text = title;
        Left.text = left;
        Center.text = center;
        Right.text = right;
        GarInd.text = garInd == "" ? "" : garInd + "/19";
        MenInd.text = menInd == "" ? "" : menInd + "/8";
        Instrundtion.text = instruction;
        ModeText.text = modeText;
    }

    IEnumerator Squat() {
        bool flip = false;
        for (;;) {
            yield return new WaitForSeconds(0.7f);
            if (!IsSQuatting) continue;
            Creature.TexNum = LifeData.Rank * 6 + (flip ? 0 : 1) 
                + (!IsHappy && LifeData.Dirty > 0 || LifeData.Hungery > 10 || LifeData.Sick > 0 ? 4 : 0) + (IsHappy ? 2 : 0);
            if (LifeData.Life <= 0) Creature.TexNum = 18;
            flip = !flip;
        }
    }

    public void ReciveInput(Button.Type t) {
        Debug.Log(t);
        switch (GameState) {
            case State.IDLE:
                Idele(t);
                break;
            case State.MENU:
                Menu(t);
                break;
            case State.TOUCH:
                Touch(t);
                break;
            case State.FEED:
                Feed(t);
                break;
            case State.CLEAN:
                Clean(t);
                break;
            case State.CONFIG:
                Config(t);
                break;
            case State.GARALLY:
                Garally(t);
                break;
            case State.COMMUNICATION:
                Communication(t);
                break;
            case State.GAME:
                Game(t);
                break;
        }
    }

    public void Idele(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("NG");
                break;
            case Button.Type.CENTER:
                GameState = State.MENU;
                AutoBackTimer = 50;
                StartCoroutine(AutoBack(State.IDLE));
                Play("OK");
                break;
            case Button.Type.RIGHT:
                Play("NG");
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    public void Menu(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("OK");
                Mode.TexNum += Mode.GetLen() - 1;
                Mode.TexNum %= Mode.GetLen();
                AutoBackTimer = 30;
                break;
            case Button.Type.CENTER:
                Play("OK");
                if (Mode.TexNum + 2 == (int)State.CONFIG) {
                    AutoBackTimer = 30;
                    break;
                };
                AutoBackTimer = Mode.TexNum + 2 >= (int)State.GAME ? 100 : 20;
                GameState = (State)((Mode.TexNum + 2) % 9);
                if(GameState == State.TOUCH) {
                    LifeData.Sick = 0;
                    Play("Gyun");
                }
                if (GameState == State.FEED) {
                    Play("Gyun");
                    LifeData.Hungery = 0;
                    StartCoroutine(FoodDicrease(20));
                }
                if (GameState == State.CLEAN) {
                    LifeData.Dirty = 0;
                    StartCoroutine(CleanerMove(20));
                    StartCoroutine(DirtDicrease(20));
                };
                if (GameState == State.GARALLY) Creature.TexNum = 0;
                if (GameState == State.TOUCH) StartCoroutine(TouchMove(20));
                break;
            case Button.Type.RIGHT:
                Play("OK");
                Mode.TexNum++;
                Mode.TexNum %= Mode.GetLen();
                AutoBackTimer = 30;
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    public void Touch(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("OK");
                break;
            case Button.Type.CENTER:
                Play("OK");
                break;
            case Button.Type.RIGHT:
                Play("OK");
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }
    IEnumerator TouchMove(int maxTime) {
        Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x, 0.05f, Hand.transform.localPosition.z);
        DateTime end = DateTime.Now.AddSeconds(maxTime * 0.1f);
        while (end > DateTime.Now) {
            yield return new WaitForSeconds(0.1f);
            Hand.transform.localPosition = new Vector3(
                Hand.transform.localPosition.x, 0.03f *(float)Math.Sin(DateTime.Now.Ticks/960f), Hand.transform.localPosition.z);
        }
    }

    public void Feed(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("OK");
                break;
            case Button.Type.CENTER:
                Play("OK");
                break;
            case Button.Type.RIGHT:
                Play("OK");
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }
    IEnumerator FoodDicrease(int maxTimer) {
        Food.TexNum = 0;
        for(int i = 0; i < Food.GetLen() - 1; i++) {
            yield return new WaitForSeconds(maxTimer * 0.1f / 4f);
            Food.TexNum++;
        }
    }

    public void Clean(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("OK");
                break;
            case Button.Type.CENTER:
                Play("OK");
                break;
            case Button.Type.RIGHT:
                Play("OK");
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }
    IEnumerator CleanerMove(int maxTime) {
        Cleaner.transform.localPosition = new Vector3(-0.45f, -0.02f, 0);
        DateTime end = DateTime.Now.AddSeconds(maxTime*0.1f);
        while(end > DateTime.Now) {
            yield return new WaitForSeconds(0.05f);
            if (Cleaner.transform.localPosition.x > 0.28f) break;
            Cleaner.transform.Translate(Vector3.right*0.11f);
        }
    }
    IEnumerator DirtDicrease(int maxTimer) {
        Dirt.TexNum = 0;
        for (int i = 0; i < Dirt.GetLen() - 1; i++) {
            yield return new WaitForSeconds(maxTimer * 0.1f / 3f);
            Dirt.TexNum++;
        }
    }

    public void Config(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                break;
            case Button.Type.CENTER:
                break;
            case Button.Type.RIGHT:
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    public void Garally(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("OK");
                Creature.TexNum += Creature.GetLen() - 1;
                Creature.TexNum %= Creature.GetLen();
                AutoBackTimer = 50;
                break;
            case Button.Type.CENTER:
                Play("OK");
                GameState = State.IDLE;
                AutoBackTimer = 50;
                break;
            case Button.Type.RIGHT:
                Play("OK");
                Creature.TexNum++;
                Creature.TexNum %= Creature.GetLen();
                AutoBackTimer = 50;
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    public void Communication(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                Play("NG");
                break;
            case Button.Type.CENTER:
                Play("OK");
                GameState = State.IDLE;
                break;
            case Button.Type.RIGHT:
                Play("NG");
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    int Pos;
    int Point = 0;
    public void Game(Button.Type t) {
        switch (t) {
            case Button.Type.LEFT:
                switch (GFase) {
                    case 0:
                        Play("OK");
                        GameState = State.IDLE;
                        break;
                    case 1:
                        if(Pos == 2) {
                            Play("OK");
                            Point += 1;
                        } else {
                            Play("NG");
                        }
                        break;
                    case 2:
                        break;
                }
                break;
            case Button.Type.CENTER:
                switch (GFase) {
                    case 0:
                        Play("OK");
                        GFase++;
                        Point = 0;
                        AutoBackTimer = 100;
                        break;
                    case 1:
                        if (Pos == 0) {
                            Play("OK");
                            Point += 1;
                        } else {
                            Play("NG");
                        }
                        break;
                    case 2:
                        Play("OK");
                        GFase = 0;
                        GameState = State.IDLE;
                        break;
                }
                break;
            case Button.Type.RIGHT:
                switch (GFase) {
                    case 0:
                        Play("NG");
                        break;
                    case 1:
                        if (Pos == 1) {
                            Play("OK");
                            Point += 1;
                        } else {
                            Play("NG");
                        }
                        break;
                    case 2:
                        Play("NG");
                        break;
                }
                break;
            default:
                throw new System.Exception("Invald Input" + t);
        }
    }

    bool Dead = false;
    IEnumerator LifeCycle(float min) {
        int HUNGRY_WEIGHT = 3;  // 1 .. 9
        int DIRTY_WEIGHT = 10;  // 1 .. 10
        int SICK_WAIGHT = 3;    // 1 .. 10
        for (;;) {
            yield return new WaitForSeconds(min*60);

            UnityEngine.Random.seed = (int)DateTime.Now.ToBinary();
            if (LifeData.Life == 0) {
                // Dead
            } else {
                // Advance
                UnityEngine.Random.seed = (int)DateTime.Now.ToBinary() % 211;
                if (LifeData.Rank < 2 && LifeData.Love == 6 && LifeData.Sick == 0 && LifeData.Hungery < 10 && LifeData.Dirty == 0 && LifeData.Life > 3 && UnityEngine.Random.Range(0, 3) == 0) {
                    LifeData.Rank++;
                    LifeData.Love = 0;
                    Play("Gyun");
                }
                // Good Metabolic 
                if (LifeData.Love < 6 && LifeData.Sick == 0 && LifeData.Hungery < 10 && LifeData.Dirty == 0 && LifeData.Life == 6) {
                    LifeData.Love++;
                    Play("Gyun");
                };
                if (LifeData.Life < 6 && LifeData.Sick == 0 && LifeData.Hungery < 10 && LifeData.Dirty == 0) LifeData.Life++;
                // Bad Metabolic 
                UnityEngine.Random.seed = (int)DateTime.Now.ToBinary() % 74;
                if (UnityEngine.Random.Range(0, 3) == 0) {
                    if (LifeData.Hungery < 100) LifeData.Hungery += HUNGRY_WEIGHT;
                    UnityEngine.Random.seed = (int)DateTime.Now.ToBinary() % 74;
                    if (LifeData.Dirty < 2 && UnityEngine.Random.Range(0, 10 / DIRTY_WEIGHT) == 0) LifeData.Dirty++;
                }
                // Event
                UnityEngine.Random.seed = (int)DateTime.Now.ToBinary() % 69;
                if (LifeData.Hungery > 10 && LifeData.Dirty > 1 && UnityEngine.Random.Range(0, 10 / SICK_WAIGHT) == 0) LifeData.Sick = 1;
                // Damage
                if (LifeData.Hungery == 100 || LifeData.Sick == 1) {
                    if (LifeData.Life > 0) LifeData.Life--;
                    if (LifeData.Love > 0) LifeData.Love--;
                }
                LifeData.Save();
            }
        }
    }

    void Play(string se) {
        float vol = 0.3f;
        switch (se) {
            case "Gyun":
                Speaker.PlayOneShot(Audios[2], vol);
                break;
            case "OK":
                Speaker.PlayOneShot(Audios[0], vol);
                break;
            case "NG":
                Speaker.PlayOneShot(Audios[1], vol);
                break;
            case "Dead":
                Speaker.PlayOneShot(Audios[3], vol);
                break;
            default:
                throw new Exception("Invald Audio");
        }
    }


    void OnGUI() {
        int i = 13;
        int h = 30;
        GUIStyle gs = new GUIStyle();
        gs.fontSize = h + 3;
        GUI.Label(new Rect(5, i++ * h, 500, 1000), "Timer " + AutoBackTimer, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 1000), "state " + GameState, gs);
        GUI.Label(new Rect(5, i++ * h, 500, 1000), "Pos " + Pos, gs);
    }
}
