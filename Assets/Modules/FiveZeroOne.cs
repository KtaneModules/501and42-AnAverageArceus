 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;
using RDM = UnityEngine.Random;

public class FiveZeroOne : MonoBehaviour
{
    public KMBombModule Module;
    public KMAudio Audio;
    public AudioSource BruhSFX2;
    public KMBombInfo Bomb;
    public KMBossModule Boss;

    public TextMesh BigText;
    public Color[] RandomColors;
    public AudioClip[] SoundEffex;
    public AudioClip[] YouDidOkay;
    public AudioClip[] YouFuckedUp;

    public KMSelectable Button;
    bool Holding;
    int HoldLength;

    bool Testable;
    int TestStage;

    int NumberCycle1; //Who is integer array and why are they in my code
    int NumberCycle2;
    int NumberCycle3;
    int NumberCycle4;
    int NumberCycle5;
    int NumberCycle6;
    int NumberCycle7;
    int NumberCycle8;
    int NumberCycle9;
    int NumberCycle10;
    int CurrentNumber;
    int CorrectNumber; //Helps with dynamic scoring for TP
    bool Strikeable;
    bool Solveable;
    bool SafeRelease;

    bool Warioware;
    int Speed;
    public AudioClip[] Correct;
    public AudioClip[] Incorrect;
    public AudioClip[] Begin;
    public AudioClip Faster;
    bool NoHolding;

    int GarunteedCorrect; //501 is much more rare than 42, so this will gradually increase the chances of getting one
    private float TPScoring = 0.5f;

    int Stage;
    int MaxStage;
    private string[] Ignored = { "14", "42", "501", "A>N<D", "Bamboozling Time Keeper", "Brainf---", "Busy Beaver", "Don't Touch Anything", "Forget Enigma", "Forget Everything", "Forget It Not", "Forget Me Later", "Forget Me Not", "Forget Perspective", "Forget The Colors", "Forget Them All", "Forget This", "Forget Us Not", "Iconic", "Kugelblitz", "Multitask", "OmegaForget", "Organization", "Password Destroyer", "Purgatory", "RPS Judging", "Simon Forgets", "Simon's Stages", "Souvenir", "Tallordered Keys", "The Time Keeper", "The Troll", "The Twin", "The Very Annoying Button", "Timing Is Everything", "Turn The Key", "Ultimate Custom Night", "Übermodule" };

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool ModuleSolved;

    //Mod Settings
    public AudioClip[] SevenTwentySeven; //Startup, Strike, Solve

    private bool _aprilFoolsOn, _aprilFoolsChecked;
    private bool AprilFoolsOn
    {
        get
        {
            if(!_aprilFoolsChecked)
            {
                _aprilFoolsOn = !Application.isEditor && SettingsHelper.ReadSettings(GetComponent<KMModSettings>());
                _aprilFoolsChecked = true;
            }
            return _aprilFoolsOn;
        }
    }

    private int CorrectInt
    {
        get
        {
            if(AprilFoolsOn)
                return 727;
            else
                return 501;
        }
    }

    void Awake()
    {
        BruhSFX2.volume = 0.6f;
        moduleId = moduleIdCounter++;
        BigText.text = "";
        if (Boss.GetIgnoredModules(Module, Ignored) != null)
            Ignored = Boss.GetIgnoredModules(Module, Ignored);
        Button.OnInteract += delegate () { Pressed(); return false; };
        Button.OnInteractEnded += delegate () { Unpressed(); };
    }

    void Start()
    {
        StartCoroutine(INeedThisForStartup());
        MaxStage = Bomb.GetSolvableModuleNames().Where(a => !Ignored.Contains(a)).Count();
        if (!Application.isEditor)
        {
            if (MaxStage == Stage)
            {
                Module.HandlePass();
                BigText.text = "WOO";
                BigText.color = RandomColors[1];
                ModuleSolved = true;
                Debug.LogFormat("[501 #{0}] No stages were able to be generated. Autosolving.", moduleId);
            }
            else
                Debug.LogFormat("[501 #{0}] Welcome to {2}. The maximum amount of stages possible is {1}.", moduleId, MaxStage, CorrectInt);
        }
        else
        {
            MaxStage = 1000;
            NumberCycle1 = RDM.Range(0, 1000); //debug code
            if(NumberCycle1 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle2 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle2 == CorrectInt)
                {
                    NumberCycle2 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle2 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle3 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle3 == CorrectInt)
                {
                    NumberCycle3 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle3 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle4 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle4 == CorrectInt)
                {
                    NumberCycle4 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle4 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle5 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle5 == CorrectInt)
                {
                    NumberCycle5 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle5 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle6 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle6 == CorrectInt)
                {
                    NumberCycle6 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle6 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle7 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle7 == CorrectInt)
                {
                    NumberCycle7 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle7 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle8 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle8 == CorrectInt)
                {
                    NumberCycle8 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle8 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle9 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle9 == CorrectInt)
                {
                    NumberCycle9 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle9 == CorrectInt)
            {
                Strikeable = true;
            }
            NumberCycle10 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle10 == CorrectInt)
                {
                    NumberCycle10 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle10 == CorrectInt)
            {
                Strikeable = true;
            }
            Debug.LogFormat("[501 #{0}] Stage 0: All numbers are zeroes for convenience.", moduleId);
        }
    }

    void Pressed()
    {
        if (!ModuleSolved && !NoHolding)
        {
            Button.AddInteractionPunch();
            StopCoroutine(SuccessfulSafety());
            BigText.text = "";
            BigText.characterSize = 1f;
            BruhSFX2.PlayOneShot(SoundEffex[0]);
            Holding = true;
        }
    }

    void Unpressed()
    {
        if (Stage >= MaxStage)
            Solveable = true;
        if (!ModuleSolved && Holding)
        {
            if (Solveable == true)
            {
                HoldLength = 1000;
                BigText.characterSize = 0.7f;
                if(AprilFoolsOn)
                    BruhSFX2.PlayOneShot(SevenTwentySeven[2]);
                else
                    BruhSFX2.PlayOneShot(SoundEffex[3]);
                BigText.text = "NOT";
                BigText.text += System.Environment.NewLine + "BAD";
                BigText.text += System.Environment.NewLine + "KID";
                Module.HandlePass();
                ModuleSolved = true;
                Debug.LogFormat("[501 #{0}] You released on {1} (Or we ran out of stages to generate). Congratulations, you solved the module.", moduleId, CorrectInt);
                StartCoroutine(WowYouSolvedIt());
            }
            else if (CurrentNumber == 6 && Stage == 0)
            {
                Warioware = true;
                BigText.characterSize = 0.7f;
                BigText.text = "GOO" + System.Environment.NewLine + "D L" + System.Environment.NewLine + "UCK";
                HoldLength = 16;
                Debug.LogFormat("[501 #{0}] Speedup mode activated!", moduleId);
                BruhSFX2.PlayOneShot(Faster);
            }
            else if (CurrentNumber == 1 && Stage == 0)
            {
                BruhSFX2.volume = 0f;
                BigText.text = "SHH";
                HoldLength = 14;
            }
            else if (SafeRelease == false)
            {
                HoldLength = 0;
                if(AprilFoolsOn)
                    BruhSFX2.PlayOneShot(SevenTwentySeven[1]);
                else
                    BruhSFX2.PlayOneShot(SoundEffex[4]);
                BigText.text = "";
                Module.HandleStrike();
                Debug.LogFormat("[501 #{0}] You released on an invalid number. Strike.", moduleId);
            }
            else if (SafeRelease == true)
            {
                HoldLength = 0;
                Debug.LogFormat("[501 #{0}] You released when the module was blank, cancelling submission.", moduleId);
                BruhSFX2.PlayOneShot(SoundEffex[1]);
                StartCoroutine(SuccessfulSafety());
            }
        }
        Holding = false;
    }

    IEnumerator INeedThisForStartup()
    {
        yield return new WaitForSeconds(0.6f);
        if(AprilFoolsOn)
            BruhSFX2.PlayOneShot(SevenTwentySeven[0]);
        else
            BruhSFX2.PlayOneShot(SoundEffex[5]);
    }

    IEnumerator INeedThisToSpaceOutSoundsOnWarioware()
    {
        NoHolding = true;
        HoldLength = 19;
        while (BruhSFX2.isPlaying)
            yield return new WaitForSeconds(0.01f);
        if (Stage % 10 == 0 && Speed < 10)
        {
            BruhSFX2.PlayOneShot(Faster);
            yield return new WaitForSeconds(0.6f / (1+(0.1f*Speed)));
            Debug.LogFormat("[501 #{0}] Faster!", moduleId);
            Speed++;
            BigText.characterSize = 0.7f;
            BigText.text = "SPE" + System.Environment.NewLine + "ED " + System.Environment.NewLine + "UP!";
            while (BruhSFX2.isPlaying)
            {
                BigText.color = RandomColors[RDM.Range(0, 6)];
                yield return new WaitForSeconds(0.1f / (1+(0.1f*Speed)));
            }
            BruhSFX2.pitch = BruhSFX2.pitch + 0.1f;
            BigText.characterSize = 1f;
            BigText.text = "";
        }
        int index = RDM.Range(0, 2);
        BruhSFX2.PlayOneShot(Begin[index]);
        BigText.characterSize = 1f;
        BigText.text = (Stage - 1).ToString();
        BigText.color = RandomColors[RDM.Range(0, 6)];
        yield return new WaitForSeconds(0.5f / (1+(0.1f*Speed)));
        BigText.text = Stage.ToString();
        while (BruhSFX2.isPlaying)
            yield return new WaitForSeconds(0.01f);
        BigText.text = "";
        NoHolding = false;
    }

    IEnumerator WowYouSolvedIt()
    {
        BruhSFX2.volume = 0.6f;
        ModuleSolved = true;
        BigText.color = RandomColors[4];
        yield return new WaitForSeconds(0.1f / (1 + (0.1f * Speed)));
        BigText.color = RandomColors[5];
        yield return new WaitForSeconds(0.2f / (1 + (0.1f * Speed)));
        BigText.color = RandomColors[0];
        yield return new WaitForSeconds(0.2f / (1 + (0.1f * Speed)));
        BigText.color = RandomColors[2];
        yield return new WaitForSeconds(0.2f / (1 + (0.1f * Speed)));
        BigText.color = RandomColors[1];
    }

    IEnumerator SuccessfulSafety()
    {
        for (int i=0; i<100; i++)
        {
            BigText.text = CorrectInt.ToString();
            BigText.characterSize = BigText.characterSize - 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        BigText.text = "";
    }

    IEnumerator SoundTest()
    {
        Testable = true;
        yield return new WaitForSeconds(10f);
        TestStage++;
        Testable = false;
    }

    void FixedUpdate()
    {
        if (Application.isEditor && !Testable)
            StartCoroutine(SoundTest());
        if ((Stage < Bomb.GetSolvedModuleNames().Where(a => !Ignored.Contains(a)).Count() || (Application.isEditor && TestStage > Stage)) && !ModuleSolved)
        {
            Stage++;
            if (Stage == MaxStage)
                Debug.LogFormat("[501 #{0}] All available modules have been solved. Please ignore any logging that follows this.", moduleId);
            if (Strikeable == true)
            {
                Module.HandleStrike();
                Debug.LogFormat("[501 #{0}] The number {1} was present in the previous sequence. Strike.", moduleId, CorrectInt);
                Strikeable = false;
                if (Warioware)
                {
                    int index = RDM.Range(0, 2);
                    BruhSFX2.PlayOneShot(Incorrect[index]);
                    StartCoroutine(INeedThisToSpaceOutSoundsOnWarioware());
                }
                else
                {
                    int index = RDM.Range(0, YouFuckedUp.Length);
                    BruhSFX2.PlayOneShot(YouFuckedUp[index]);
                }
            }
            else
            {
                if (Warioware)
                {
                    int index = RDM.Range(0, 2);
                    BruhSFX2.PlayOneShot(Correct[index]);
                    StartCoroutine(INeedThisToSpaceOutSoundsOnWarioware());
                }
                else
                {
                    int index = RDM.Range(0, YouDidOkay.Length);
                    BruhSFX2.PlayOneShot(YouDidOkay[index]);
                }
            }
            if (Stage < 100)
                GarunteedCorrect = RDM.Range(0, 100);
            if(GarunteedCorrect <= (0 + Stage))
                NumberCycle1 = CorrectInt;
            else
                NumberCycle1 = RDM.Range(0, 1000);
            if(NumberCycle1 == CorrectInt)
            {
                CorrectNumber = 1;
                Strikeable = true;
            }
            NumberCycle2 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle2 == CorrectInt)
                {
                    NumberCycle2 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle2 == CorrectInt)
            {
                CorrectNumber = 2;
                Strikeable = true;
            }
            NumberCycle3 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle3 == CorrectInt)
                {
                    NumberCycle3 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle3 == CorrectInt)
            {
                CorrectNumber = 3;
                Strikeable = true;
            }
            NumberCycle4 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle4 == CorrectInt)
                {
                    NumberCycle4 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle4 == CorrectInt)
            {
                CorrectNumber = 4;
                Strikeable = true;
            }
            NumberCycle5 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle5 == CorrectInt)
                {
                    NumberCycle5 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle5 == CorrectInt)
            {
                CorrectNumber = 5;
                Strikeable = true;
            }
            NumberCycle6 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle6 == CorrectInt)
                {
                    NumberCycle6 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle6 == CorrectInt)
            {
                CorrectNumber = 6;
                Strikeable = true;
            }
            NumberCycle7 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle7 == CorrectInt)
                {
                    NumberCycle7 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle7 == CorrectInt)
            {
                CorrectNumber = 7;
                Strikeable = true;
            }
            NumberCycle8 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle8 == CorrectInt)
                {
                    NumberCycle8 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle8 == CorrectInt)
            {
                CorrectNumber = 8;
                Strikeable = true;
            }
            NumberCycle9 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle9 == CorrectInt)
                {
                    NumberCycle9 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle9 == CorrectInt)
            {
                CorrectNumber = 9;
                Strikeable = true;
            }
            NumberCycle10 = RDM.Range(0, 1000);
            if (Strikeable == true)
            {
                if(NumberCycle10 == CorrectInt)
                {
                    NumberCycle10 = RDM.Range(100, 400);
                }
            }
            else if(NumberCycle10 == CorrectInt)
            {
                CorrectNumber = 10;
                Strikeable = true;
            }
            Debug.LogFormat("[501 #{0}] Stage {11}: The sequence of numbers generated is: {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}.", moduleId, NumberCycle1, NumberCycle2, NumberCycle3, NumberCycle4, NumberCycle5, NumberCycle6, NumberCycle7, NumberCycle8, NumberCycle9, NumberCycle10, Stage);

        }
        if (!ModuleSolved)
            if (Holding && !NoHolding)
                HoldLength++;

        if (HoldLength == 60 - (3*Speed)) //This is all me being really really bad at coding efficiently :P
        {
            SafeRelease = false;
            Solveable = false;
            BigText.text = NumberCycle1.ToString();
            CurrentNumber = 1;
            if (NumberCycle1 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle1 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle1 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 105 - (6*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle2.ToString();
            CurrentNumber = 2;
            if (NumberCycle2 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle2 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle2 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 150 - (9*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle3.ToString();
            CurrentNumber = 3;
            if (NumberCycle3 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle3 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle3 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 195 - (12*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle4.ToString();
            CurrentNumber = 4;
            if (NumberCycle4 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle4 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle4 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 240 - (15*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle5.ToString();
            CurrentNumber = 5;
            if (NumberCycle5 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle5 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle5 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 285 - (18*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle6.ToString();
            CurrentNumber = 6;
            if (NumberCycle6 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle6 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle6 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 330 - (21*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle7.ToString();
            CurrentNumber = 7;
            if (NumberCycle7 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle7 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle7 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 375 - (24*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle8.ToString();
            CurrentNumber = 8;
            if (NumberCycle8 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle8 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle8 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 420 - (27*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle9.ToString();
            CurrentNumber = 9;
            if (NumberCycle9 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle9 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle9 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 465 - (30*Speed))
        {
            Solveable = false;
            BigText.text = NumberCycle10.ToString();
            CurrentNumber = 10;
            if (NumberCycle10 < 10)
            {
                BigText.text = "00" + BigText.text;
            }
            else if (NumberCycle10 < 100)
            {
                BigText.text = "0" + BigText.text;
            }
            if(NumberCycle10 == CorrectInt)
            {
                Solveable = true;
            }
            BigText.color = RandomColors[RDM.Range(0, 6)];
            
            BruhSFX2.PlayOneShot(SoundEffex[2]);
        }
        else if (HoldLength == 510 - (33*Speed))
        {
            
            CurrentNumber = 11;
            BruhSFX2.PlayOneShot(SoundEffex[2]);
            BigText.text = "";
            Solveable = false;
            SafeRelease = true;
            HoldLength = 15;
        }
    }
#pragma warning disable 414 //TWITCH PLAYYYYYYYYS
    private readonly string TwitchHelpMessage = @"!{0} hold (Starts the number cycle) | !{0} release [1-11] (Releases at the specified number (11 is blank)) | !{0} speedup (Initiates Speed Up, only available on stage 0) | !{0} quiet (Disables sound)";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] HellYeah = command.Split(' ');
        if (Regex.IsMatch(HellYeah[0], @"^\s*hold\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;

            if (HellYeah.Length != 1)
                yield return "sendtochaterror That's not how you hold.";
            if (Holding == true)
                yield return "sendtochaterror How am I supposed to hold this thing twice? FailFish";
            if (Stage == 0)
                yield return "sendtochaterror This stage is all zeroes. Ignored to help save time.";
            else
                Button.OnInteract();
        }
        else if (Regex.IsMatch(HellYeah[0], @"^\s*release\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;

            if (HellYeah.Length < 2)
                yield return "sendtochaterror You really shouldn't do that.";
            else if (HellYeah.Length > 2)
                yield return "sendtochaterror I'm getting mixed signals here. Give me a single number to release at.";
            else if (HellYeah[1] == "69" | HellYeah[1] == "420")
                yield return "sendtochaterror Not funny.";
            else if (HellYeah[1] == "0")
                yield return "sendtochaterror The release command is 11, not 0.";
            else if (!IsValid(HellYeah.ElementAt(1)))
                yield return "sendtochaterror Hey, make sure your number is between 1 and 11 so I can release!";
            else if (!Holding)
                yield return "sendtochaterror ...I'm not holding. I can't release if I'm not holding.";
            else
            {
                yield return new WaitWhile(() => HellYeah[1] != CurrentNumber.ToString());
                yield return new WaitForSeconds(0.025f);
                if (CurrentNumber == CorrectNumber || Stage >= MaxStage)
                {
                    yield return "awardpointsonsolve " + Math.Max(Mathf.CeilToInt(Stage * TPScoring + 0.5f), 1).ToString();
                    yield return "solve";
                }
                else if (HellYeah[1] == "11")
                {
                    SafeRelease = true;
                }
                Button.OnInteractEnded();
            }
        }
        else if (Regex.IsMatch(HellYeah[0], @"^\s*speedup\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (Stage != 0)
                yield return "sendtochaterror Sorry, it's too late to enable Speed Up mode.";
            else
            {
                Warioware = true;
                BigText.characterSize = 0.7f;
                BigText.text = "GOO" + System.Environment.NewLine + "D L" + System.Environment.NewLine + "UCK";
                HoldLength = 16;
                Debug.LogFormat("[501 #{0}] Speedup mode activated!", moduleId);
                BruhSFX2.PlayOneShot(Faster);
            }
        }
        else if (Regex.IsMatch(HellYeah[0], @"^\s*quiet\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            BruhSFX2.volume = 0f;
            BigText.text = "SHH";
            HoldLength = 14;
        }
        else
        {
            yield return null;
            yield return "sendtochaterror I don't understand. The only commands I know of are 'hold' and 'release'...";
        }
    }
    private bool IsValid(string par)
    {
        //range is 1-11, so we gotta set that here
        ushort s;
        return ushort.TryParse(par, out s) && s < 12;
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        Module.HandlePass();
        StartCoroutine(WowYouSolvedIt());
        Debug.LogFormat("[501 #{0}] Autosolve command received.", moduleId);
    }
}