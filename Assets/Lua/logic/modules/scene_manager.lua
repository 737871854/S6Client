local StartScene = Import("logic/modules/start_scene").CStartScene
local LoginScene = Import("logic/modules/login_scene").CLoginScene
local MainScene = Import("logic/modules/main_scene").CMainScene
local FightScene = Import("logic/modules/fight_scene").CFightScene
local GameScene = Import("logic/modules/game_scene").CGameScene
local scene_load_info = Import("logic/config/info_scene_load").Data

local SceneConfig = {
	["login"] = {
		["class"] = LoginScene,
	},
	["main"] = {
		["class"] = MainScene,
	},
	["fight"] = {
		["class"] = FightScene,
	},
	["game"] = {
		["class"] = GameScene,
	},
}

CSceneManager = class()

CSceneManager.Init = function(self)
	self._isLoadingScene = false
end

CSceneManager.IsLoadingScene = function(self)
	return self._isLoadingScene
end

CSceneManager.Update = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnUpdate()
	end
end

CSceneManager.FixedUpdate = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnFixedUpdate()
	end
end

CSceneManager.LateUpdate = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnLateUpdate()
	end
end

CSceneManager.OnLevelWasLoaded = function(self)
	self._isLoadingScene = false
	if self._current_scene then
		self._current_scene:OnLevelWasLoaded()
	end
end

CSceneManager.OnApplicationQuit = function(self)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnApplicationQuit()
	end
end
--调用了changeScene函数后就等着调用OnLevelWasLoaded
CSceneManager.ChangeScene = function(self, name, assetName)
	if self._isLoadingScene then return end
	if self._current_scene then
		self._current_scene:OnDestroy()
	end
    gGame:GetUIMgr():Clear()
    --gGame:GetObjMgr():Clear()
	gControl:ClearAll()
	self._isLoadingScene = true

    local classConfig = SceneConfig[name]
	local scene_cfg = scene_load_info[assetName]
	if classConfig == nil or not scene_cfg then
		warn("sceneType:"..name.." or asset:"..assetName.." not exist!")
	end

   	self._current_scene = classConfig["class"]:New()
    LoadSceneMgr.Instance:SetLoadScene(scene_cfg["path"],scene_cfg["level"])
    self._current_scene:SetPreLoad()
    LoadSceneMgr.Instance:StartLoad()
	collectgarbage("collect")
end

CSceneManager.GetCurrentScene = function(self)
	return self._current_scene
end
