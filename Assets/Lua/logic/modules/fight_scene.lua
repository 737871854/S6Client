local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

local Tag =
{
    Warrior = "warrior",
    Land = "land",
}

--战斗场景逻辑
CFightScene = class(SceneBase)
------------------------------------------------------------------------------------------
-- 场景及数据的初始化
------------------------------------------------------------------------------------------
CFightScene.Init = function(self)
    SceneBase.Init(self)
end

-- 进入场景调用
CFightScene.OnLevelWasLoaded = function(self)
    self:InitCamera()
    self:InitIndiData()
    -- 打开ui界面
    gGame:GetUIManager():Open("Fight", self)
    self._uiMainLogic = gGame:GetUIManager():GetCurMainLogic()
    -- 场景帮助，初始化投兵，寻路等
    --ioo.audioManager:PlayBGM(CommonVoiceEvent.FightBg)
end

--进行CFightScene的提前加载处理，所有的场景在加载之前都需要添加预加载列表。
--预加载生成对象池之后，可以提高战斗中放兵操作效率等。这里需要预加载战斗中所有角色和建筑资源;
CFightScene.SetPreLoad = function(self)
    --这里是预加载对象列表，包括建筑，小兵和英雄
    for k, v in pairs(self._preLoadObjectsList) do
        LoadSceneMgr.Instance:AddPreLoadPrefab(k, v)
    end
end

-- 初始化摄像机
CFightScene.InitCamera = function(self)
end

-- 添加与加载信息 yeah 2015-10-26
CFightScene.AddPreLoadInfo = function(self, resPath, resNum)
    if self._preLoadObjectsList[resPath] then --如果有这个信息，直接改数字
        self._preLoadObjectsList[resPath] = self._preLoadObjectsList[resPath] + resNum
    else --没有这个信息，那就是新加
        self._preLoadObjectsList[resPath] = resNum
    end
end
-- 场景及数据的初始化 End
------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------
-- 战斗逻辑及流程
------------------------------------------------------------------------------------------
CFightScene.OnUpdate = function(self)
--    if self:IsSameProcess(FightSceneCommon.FightProcess.kPostingTeam) then
--        self:UpdatePostingTeam()
--    elseif self:IsSameProcess(FightSceneCommon.FightProcess.kCountDown) then
--        self:UpdateCountDown()
--    elseif self:IsFighting() then
--        self:UpdateFighting()
--    elseif self:IsSameProcess(FightSceneCommon.FightProcess.kFightEnd) then
--        self:UpdateFightEnd()
--    end
end

CFightScene.OnApplicationQuit = function(self)
    if not self:IsFighting() then return end
    self:SetFightProcess(FightSceneCommon.FightProcess.kFightEnd)
    --self._indicatorManager:UnloadIndicators()
    --self._battleManager:EndFight()
    self:ConsumeResult()
end

CFightScene.QuitFight = function(self)
    print("Quit Fight")
    --self._indicatorManager:UnloadIndicators()
    gGame:ChangeScene("main")
end

-- 场景销毁
CFightScene.OnDestroy = function(self)
    if self._battleManager ~= nil then
        self._battleManager:EndFight()
        self._battleManager:Release()
    end
end

--计算战斗结果
CFightScene.ConsumeResult = function(self, isWin)
    print("consume result")
    self._fightInputController:LockCamera()
    -- 结果发送给服务器
    self:ConsumeDeadResult()
    self:ConsumeAliveResult()
    gGame:GetUIManager():GetCurMainLogic():ShowSwitch(false)
    gGame:GetUIManager():Open("FightResult", self, nil ,nil, "FightResult")
end
-- 战斗逻辑及流程 End
------------------------------------------------------------------------------------------