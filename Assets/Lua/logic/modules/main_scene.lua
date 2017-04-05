local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 
--local ObjectMgr = Import("logic/modules/object/object_mgr").CObjectMgr
--local cameraFactorInfo = Import("logic/config/info_camera_factor").Data
local UIProgBarAndTextLogic = Import("logic/ui/prog_bar_and_text/ui_prog_bar_and_text_logic").CUIProgBarAndTextLogic 
local UIPnlPlayMethod = Import("logic/ui/pnl_play_method/ui_pnl_play_method_logic").CUIPnlPlayMethodLogic 
local UIPnlInfoSet = Import("logic/ui/pnl_info_set/ui_pnl_info_set_logic").CUIPnlInfoSetLogic 
local UIPnlResultLogic = Import("logic/ui/pnl_result/ui_pnl_result_logic").CUIPnlResultLogic


--主城场景(显示下注界面，进行倒计时)
CMainScene = class(SceneBase)

CMainScene.Init = function(self)
	SceneBase.Init(self)
	self._camera = nil
	--self._camera_transform = nil

	self._playMethod = nil
	self._progBar = nil
	self._pnlInfoSet = nil
	self._hasEnterGame = false
end

CMainScene.OnLevelWasLoaded = function(self)
	self._camera = Camera.main
	print("Enter Bet Scene, ready to Bet!!!")

    --self._sceneController = MainSceneController:New(self, self._camera_transform)
    --gControl:SwitchController(self._sceneController)
    --self._sceneController:InitFactorTestUI(_uiMainLogic._view._MainCanvas.transform)
    --ioo.audioManager:PlayBGM(CommonVoiceEvent.MainBg)
end

CMainScene.OnDestroy = function(self)
	self._playMethod = nil
	self._progBar = nil
	self._pnlInfoSet = nil
	self._camera = nil
end

--估计这里需要进行主城中逻辑对象的更新
CMainScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
    GameAudio.Instance:InitBetAudios()
    GameAudio.Instance:PlayAudio(0)

  	self._playMethod = gGame:GetUIMgr():CreateWindowCustom("playMethod",UIPnlPlayMethod)
	self._progBar = gGame:GetUIMgr():CreateWindowCustom("progBar",UIProgBarAndTextLogic)
	self._pnlInfoSet = gGame:GetUIMgr():CreateWindowCustom("InfoSet",UIPnlInfoSet)
	gGame:GetDataMgr():GetBetData():InitLogic(self._playMethod)
end

CMainScene.OnApplicationQuit = function(self)
	--先将操作的对象置空，保存一次操作对象的数据
end

