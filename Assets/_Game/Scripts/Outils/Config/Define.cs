

using System;

public static class Define
{
    public const string KEY_SETTING = "Setting";
}


public enum EGameState
{
    Loading,
    Menu,
    InGame,
    GameOver,
}


public enum ESceneName
{
    SplashScene,
    GameScene,
    BattleScene
}


public enum EView
{
    Splash,
    Loading,
    Login,
    Privacy,
    Profile,
    Lobby,
    Quest,
    Character,
    Equipment,
    Formation,
    BloodUnion,
    SkillSynthesis,
    Arena,
    ArenaConfirm,
}


[Serializable]
public class LocalView
{
    public ViewSplash splash;
    public ViewLoading loading;
    public ViewPrivacy privacy;
    public ViewLogin login;
    public ViewLobby lobby;
    public ViewQuest quest;
    public ViewProfile profile;
    public ViewFormation formation;
    public ViewEquipment equipment;
    public ViewCharacter character;
    public ViewBloodUnion bloodUnion;
    public ViewSkillSynthesis skillSynthesis;
    public ViewArena arena;
    public ViewArenaConfirm arenaConfirm;
}


public enum SFX
{
    // music
    MUSIC_BACKGROUND,

    // sfx
    SFX_CLICK,
}