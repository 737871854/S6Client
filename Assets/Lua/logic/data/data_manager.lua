--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: data_manager.lua
-- author: lxt  created: 2015/10/29
-- descrip: 服务器下发的用户数据处理中心，公用数据处理模块全部添加到此处
--======================================================================
-- local CPlayerData = Import("logic/data/player_data").CPlayerData
-- local CItemData = Import("logic/data/item_data").CItemData
-- local CTaskData = Import("logic/data/task_data").CTaskData

--Import各个模块的数据接口--BEGIN
local CLoginData = Import("logic/data/login_data").CLoginData
local CBetData = Import("logic/data/bet_data").CBetData
--Import各个模块的数据接口--END

CDataManager = class()

CDataManager.Init = function(self)
    self._betData = CBetData:New()
    self._dataDic = {}
end

CDataManager.ClearAllData = function(self)
	-- self._playerData:Clear()
	-- self._itemData:Clear()
    self._betData:ClearAll()
    self._dataDic = {}
end

CDataManager.GetLoginData = function(self)
    if not self._dataDic["login"] then
        self._dataDic["login"] = CLoginData:New()
    end
    return self._dataDic["login"]
end

CDataManager.GetBetData = function (self)
    return self._betData
end