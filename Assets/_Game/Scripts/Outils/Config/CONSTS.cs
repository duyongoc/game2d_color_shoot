using System;
public class CONSTS
{
    public const string SCENE_KEY = "SCENE_KEY";

    #region ANIM SETTING
    public const string ANIM_SPEED_F = "Speed_f";
    public const float ANIM_SPEED_RUN = 0.5f;
    public const float ANIM_SPEED_IDLE = 0f;
    public const string ANIM_STATIC_B = "Static_b";

    public const string ANIM_POPUP_APPEAR_TR = "Popup_Appear_Tr";
    public const string ANIM_POPUP_DISAPPEAR_TR = "Popup_Disappear_Tr";
    public const float ANIM_POPUP_SPEED = 0.5f;
    public const float ANIM_DRAW_TIME = 5f;
    #endregion

    #region DICE CONST
    public const float DICE_ANIM_TIME_MAX = 3;
    public const float DICE_POPUP_CLOSE = 1.5f;
    public const int DICE_VALUE_MIN = 1;
    public const int DICE_VALUE_MAX = 6;
    public const int DICE_ANIM_TYPE_MIN = 0;
    public const int DICE_ANIM_TYPE_MAX = 2;

    public const string DICE_ANIM_PARAM_DICE_VALUE = "Dice_Value";
    public const string DICE_ANIM_PARAM_DICE_BLEND = "Dice_Blend";
    public const string DICE_ANIM_PARAM_DICE_TRIGGER_STATE_CHANGE = "ToTriggerStateChange";
    #endregion


    #region NETWORK SETTING
    // public const string HOST_ENDPOINT_SOCKET = "wss://795f-113-161-74-234.ap.ngrok.io/";
    // public const string HOST_ENDPOINT_API = "http://192.168.9.9:4000/";
    // public const string HOST_ENDPOINT_SOCKET = "wss://test-game-server.w3w.app/";
    // public const string HOST_ENDPOINT = "https://test-game-api.w3w.app/";
    public const string HOST_ENDPOINT_SOCKET = "wss://dev-game-server.w3w.app/";
    public const string HOST_ENDPOINT = "https://dev-game-api.w3w.app/";
    public const string API_GET_MAP = HOST_ENDPOINT + "api/maps";
    
    
    // game data
    public const string API_GET_GAME_CHARACTER = HOST_ENDPOINT + "api/game-characters";
    public const string API_GET_GAME_ITEM = HOST_ENDPOINT + "api/game-items";
    public const string API_GET_GAME_SPHERE = HOST_ENDPOINT + "api/game-sapphires";

    // user data
    public const string API_GET_USER_DATA = HOST_ENDPOINT + "api/users/detail";
    public const string API_GET_USER_CHARACTER = HOST_ENDPOINT + "api/user-characters";
    public const string API_GET_USER_ITEM = HOST_ENDPOINT + "api/user-items";
    public const string API_GET_USER_SPHERE = HOST_ENDPOINT + "api/user-sapphires";

    // formation
    public const string API_GET_USER_FORMATION = HOST_ENDPOINT + "api/user-characters/game";
    public const string API_FORMATION_ATK = HOST_ENDPOINT + "api/users/formation/atk"; // mapGame - atk - def
    public const string API_FORMATION_DEF = HOST_ENDPOINT + "api/users/formation/def"; // mapGame - atk - def


    public const string DEFAULT_ID = "61ee1891d1949c6bc79425a8";
    public const string DEFAULT_ADDRESS = "0x64470E5F5DD38e497194BbcAF8Daa7CA578926F6";
    public const string DEFAULT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2MzQwZTcxMDE1ZDA5OGE5Yzk2MTMzZTMiLCJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiIweDY0NDcwZTVmNWRkMzhlNDk3MTk0YmJjYWY4ZGFhN2NhNTc4OTI2ZjYiLCJpbWFnZSI6bnVsbCwiaXNSZWNlaXZlZCI6ZmFsc2UsImlzTmV3VXNlciI6ZmFsc2UsInR5cGUiOiJhY2Nlc3MiLCJpYXQiOjE2NjU1NjA3MjEsImV4cCI6MTk4MTEzNjcyMX0.wSpCoUqNdp_7b081VwhskwH_5GtBnUb1SVk8H9fkjmE";
    #endregion


    #region LOADING DETAIL MESSAGE
    public const string LOADING_DETAIL_LOADMAP = "Initialize the map...";
    #endregion
}

public enum SEND_TYPE
{
    LOBBY_SEND,
    GAMEROOM_SEND
}

public enum CYCLE_TURN
{
    START_TURN,
    WAITING_ACTION,
    START_ACTION,
    END_ACTION,
    END_TURN,
}

public enum ACTION_TYPE
{
    MOVE,
    ROLL_DICE,
    LUCKY_DRAW,
    CHANCE,
    COMBAT,
    END_TURN,

    INVALID_ACTION = 100
}

public class TEXT_UI
{
    public const string LANGUAGE = "Language";
    public const string ENGLISH = "English";
    public const string JAPANESE = "Japanese";

}

public class ROOM_TYPE
{
    public const string GAME_ROOM = "my_room";
    public const string LOBBY_ROOM = "lobby";
    public const string ARENA_ROOM = "arena";
}

#region SERVER EVENT
public enum GAMEROOM_SENT_EVENTS
{
    OTHER,
    ROLL_DICE,
    LUCKY_DRAW,
    FIGHT,
    BALANCE,
    GAME_EXIT,
    CHANCE,
    DEFAULT,
    CHECK_INTERACTED,
}

public enum GAMEROOM_RECEIVE_EVENTS
{
    ROLL_RESULT,
    LUCKY_DRAW_RESULT,
    FIGHT_RESULT,
    BALANCE_RESULT,
    CHANCE_RESULT,
    DEFAULT_RESULT,
    CHECK_INTERACTED_RESULT,
}

public enum LOBBY_SENT_EVENTS
{
    MAP_LIST,
    MAP_NODE,
};

public enum LOBBY_RECEIVE_EVENTS
{
    MAP_LIST_RESULT,
    MAP_NODE_RESULT,
};

public enum PLAYER_ERRORS
{
    GAME_SERVER_HAVE_ERROR = 4000,
    THERE_WAS_ANOTHER_CONNECTION = 4001,
    MAP_ROOM_IS_MISS = 4002,
    YOU_IN_ANOTHER_GAMEROOM = 4003,
    MAP_ROOM_NOT_FOUND = 4004,
    SENT_EVENT_WRONG = 4010,
    NOT_YOUR_TURN = 4011,
    NOT_ENOUGH_ENERGY = 4012,
    NOT_ENOUGH_YU_POINT = 4013,
    NOT_INTERACTED_WITH_NODE = 4014,
    INTERACTED_WITH_NODE = 4015,
}

public enum REQUEST_ERRORS
{
    USER_NOT_FOUND,
    INVALID_ACCESS_TOKEN,
}

public enum STATUS_BATTLE
{
    WIN = 1,
    DRAW = 0,
    LOSS = -1,
}

[Serializable]
public class Interacted_MSG
{
    public bool isInteracted;
}
#endregion



