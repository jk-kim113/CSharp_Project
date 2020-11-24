﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Scientia
{
    class DefinedProtocol
    {
        public enum eFromClient
        {
            LogInTry,
            OverlapCheck_ID,
            OverlapCheck_NickName,
            EnrollTry,
            GetMyCharacterInfo,
            CreateCharacter,
            GetMyInfoData,
            AddReleaseCard,
            CreateRoom,
            GetRoomList,
            TryEnterRoom,
            InformReady,
            InformGameStart,
            FinishReadCard,
            PickCard,

            ConnectionTerminate,

            max
        }

        public enum eToClient
        {
            LogInResult,
            ResultOverlap_ID,
            ResultOverlap_NickName,
            EnrollResult,
            CharacterInfo,
            EndCharacterInfo,
            EndCreateCharacter,
            ShowMyInfo,
            CompleteAddReleaseCard,
            EnterRoom,
            ShowRoomList,
            FinishShowRoom,
            ShowReady,
            ShowMaster,
            CannotPlay,
            GameStart,
            ShowPickedCard,
            PickCard,
            ShowPickCard,
            ChooseAction,

            max
        }

        public enum eFromServer
        {
            CheckLogIn,
            OverlapCheck_ID,
            OverlapCheck_NickName,
            CheckEnroll,
            CheckCharacterInfo,
            CreateCharacter,
            UserMyInfoData,
            AddReleaseCard,
            GetBattleInfo,
            GetAllCard,

            max
        }

        public enum eToServer
        {
            LogInResult,
            OverlapResult_ID,
            OverlapResult_NickName,
            EnrollResult,
            ShowCharacterInfo,
            CompleteCharacterInfo,
            CreateCharacterResult,
            ShowMyInfoData,
            CompleteAddReleaseCard,
            ShowBattleInfo,
            ShowAllCard,

            max
        }
    }
}
