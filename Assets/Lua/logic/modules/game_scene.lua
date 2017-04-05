--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: game_scene.lua
-- author: lxt  created: 2015/12/28
-- descrip: 测试用场景模块，以后可以用来做RPG内的小游戏
--======================================================================
local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

-- local UIProgBarAndTextLogic = Import("logic/ui/prog_bar_and_text/ui_prog_bar_and_text_logic").CUIProgBarAndTextLogic 
-- local UIPnlPlayMethod = Import("logic/ui/pnl_play_method/ui_pnl_play_method_logic").CUIPnlPlayMethodLogic 
-- local UIPnlInfoSet = Import("logic/ui/pnl_info_set/ui_pnl_info_set_logic").CUIPnlInfoSetLogic 
local UIPnlResultLogic = Import("logic/ui/pnl_result/ui_pnl_result_logic").CUIPnlResultLogic

CGameScene = class(SceneBase)
CGameScene.Init = function(self)
	self._hasEnterGame = false
end

CGameScene.OnLevelWasLoaded = function(self)
    GameLogic.Instance.state = 1;
    self:InitCamera()
	print("Enter game_scene Start to Run Snail")
	--gGame:GetUIMgr():OpenFloatDialog("123456")
	--gGame:GetUIMgr():OpenAlertDialog("Title","试试Alert","diandian",nil,false)

end

CGameScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
    GameLogic.Instance:StartGame()
	--print("ChangeScene to login")
	--gGame:ChangeScene("login")
end

CGameScene.OnDestroy = function(self)
end

-- 初始化摄像机
CGameScene.InitCamera = function(self)
    self._camera = Camera.main
 	--self._camera.transform.localPosition = Vector3.New(-20, 20, -70)
	--self._camera.transform.rotation = Quaternion.Euler(45, 0, 0)
end

CGameScene.ShowBetInfo = function (self, isShow)
	if self._isShow == isShow then return end
	self._isShow = isShow
	self._pnlInfoSet:ShowSwitch(isShow)
	self._playMethod:ShowSwitch(isShow)
end

CGameScene.ShowEndResult = function (self, isShow)
	self._pnlResult = gGame:GetUIMgr():CreateWindowCustom("Result",UIPnlResultLogic)
end
