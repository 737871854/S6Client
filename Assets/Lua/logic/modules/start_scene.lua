--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: start_scene.lua
-- author: lxt  created: 2015/12/28
-- descrip: 启动逻辑模块。因为最开始gGame的init还没有完成，不能使用。所以在
-- OnUpdate里切到其他逻辑模块，然后就可以自由的使用gGame和gControl了
--======================================================================
local Tool = Import("logic/common/tool").CTool
local SceneBase = Import("logic/modules/base_scene").CSceneBase 

CStartScene = class(SceneBase)
CStartScene.Init = function(self)
	self._hasEnterGame = false
end

CStartScene.OnLevelWasLoaded = function(self)
end

CStartScene.OnUpdate = function(self)
	if self._hasEnterGame then return end
	self._hasEnterGame = true
	print("ChangeScene to game")
	gGame:ChangeScene("game","gameScene")
end
