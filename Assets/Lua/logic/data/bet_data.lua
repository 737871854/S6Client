local EventDefine = Import("logic/base/event_define").Define
local EventSource = Import("logic/common/event_source").EventSource
-- local UIPnlSnailSelectLogic = Import("logic/ui/pnl_snail_select/ui_pnl_snail_select_logic").CUIPnlSnailSelectLogic 
-- local UIPnlPlayMethod = Import("logic/ui/pnl_play_method/ui_pnl_play_method_logic").CUIPnlPlayMethodLogic 

BetMethod = {
	NONE = 0,
	Q1 = 1,
	Q2 = 2,
	Q3 = 3,
	Q4 = 4,
	Q5 = 5,
    QEnd = 10, --前n玩法结束
	R2 = 12,
	R3 = 13,
	R4 = 14,
	R5 = 15,
}

--下注类型 单选，复选，胆拖
BetType = {
	Single = 1,	
	Multi = 2,
	DanTuo = 3,
}

--当前选择的状态
BetSelectType = {
	NONE = 0,
	Dan = 1,
	Tuo = 2,
}

CBetData = class()


CBetData.Init = function(self)
	--投注方式，
	self._betMethod = BetMethod.Q1
	self._betType = BetType.Single
	self._isDanTuo = false --当前是否为胆拖投注

	self._selectedSnail = {}
	self._DanSnail = {}
	self._TuoSnail = {}
end

--获得选择蜗牛和玩法设定的窗口逻辑对象
CBetData.InitLogic = function(self, playMethod)

    self._playMethod = playMethod
    --用这个机会来对数据再次初始化
    self:Init()
end
--选择
CBetData.SetBetMethod = function(self, betMethod)
    if self._betMethod == betMethod then
	   --gGame:GetUIMgr():OpenFloatDialog("请选择一种下注模式！")
       --self._betMethod = BetMethod.NONE
       return
    else
	   self._betMethod	= betMethod
    end
    self:ClearAll()
end

CBetData.SetDanTuo = function(self, isOn)
    self:ClearAll()
    self._isDanTuo = isOn
    if self._isDanTuo then
        self._betType = BetType.DanTuo
    else
        self._betType = BetType.Single
    end
end

--处理投注情况,必须保证这个snailIndex是被选中过，返回false代表不能选号
CBetData.SelectSnail = function(self, snailIndex)
    if self._betMethod == BetMethod.NONE then
	   gGame:GetUIMgr():OpenFloatDialog("请选择一种下注模式！")
       return false
    end

	if self._isDanTuo then
		--必须BetMethod > 11 ,为R2,R3,R4,R5
        local selectType = self._playMethod:GetSelectType()
		if selectType == BetSelectType.NONE then 
	    	gGame:GetUIMgr():OpenFloatDialog("选择状态错误！")
		end
		if selectType == BetSelectType.Dan then
            return self:SelectDanSnail(snailIndex)
		elseif selectType == BetSelectType.Tuo then
            return self:SelectTuoSnail(snailIndex)
        end
	end

    local num = #self._selectedSnail
    if num == 6 then return false end
    --判断玩法
    if self._betMethod < BetMethod.QEnd then --前X玩法
        if num >= self._betMethod then
            self._betType = BetType.Multi
        end
        self._selectedSnail[num +1] = snailIndex
    else -- self._betMethod  肯定是任意X 玩法
        if num >= self._betMethod -10 then
            self._betType = BetType.Multi
        end
        self._selectedSnail[num +1] = snailIndex
    end

    EventTrigger(EventDefine.CHANGE_BET_TYPE,self._betType)
    EventTrigger(EventDefine.ADD_SNAIL_INDEX,snailIndex)
    --self._playMethod:SetBetType(self._betType)
    --self._playMethod:AddNumber(snailIndex)
end

--选中胆号蜗牛，这判断胆的数量是否超出
CBetData.SelectDanSnail = function(self, snailIndex)
    local maxDanNum = self._betMethod - 10 - 1
    local danNum = #self._DanSnail
    local totalNum = danNum + #self._TuoSnail
    if totalNum == 6 then
        gGame:GetUIMgr():OpenFloatDialog("胆号+拖号已满")
        return false
    end

    if danNum < maxDanNum then
        self._DanSnail[danNum + 1] = snailIndex
        --self._playMethod:AddNumber(snailIndex)
        EventTrigger(EventDefine.ADD_SNAIL_INDEX,snailIndex)

        return true
    else
    	gGame:GetUIMgr():OpenFloatDialog("胆号已满")
        --自动切换为拖得Toggle
        local tuoNum = #self._TuoSnail
        self._TuoSnail[tuoNum + 1] = snailIndex
        EventTrigger(EventDefine.CHANGE_SELECT_DanTuo,BetSelectType.Tuo)
        EventTrigger(EventDefine.ADD_SNAIL_INDEX,snailIndex)
        return true
    end
end

--选中拖号蜗牛,在开始选拖号蜗牛的时候不允许再次修改胆号，不然全部重选
CBetData.SelectTuoSnail = function(self, snailIndex)
    local danNum = #self._DanSnail
    local maxTuoNum = 6 - danNum
    local tuoNum = #self._TuoSnail
    if tuoNum == 5 and danNum == 0 then
        self._DanSnail[danNum + 1] = snailIndex
        EventTrigger(EventDefine.CHANGE_SELECT_DanTuo,BetSelectType.Dan)
        EventTrigger(EventDefine.ADD_SNAIL_INDEX,snailIndex)
        return true
    end
    if tuoNum < maxTuoNum then
        self._TuoSnail[tuoNum + 1] = snailIndex
        EventTrigger(EventDefine.ADD_SNAIL_INDEX,snailIndex)
        --self._playMethod:AddNumber(snailIndex)
        return true
    else
    	gGame:GetUIMgr():OpenFloatDialog("拖号已满")
        return false
    end
end

CBetData.CanBet = function(self)
    if self._betMethod < BetMethod.QEnd then
        -- R玩法
        if #self._selectedSnail >= self._betMethod then
            return true
        else
            return false
        end
    else
        local num = self._betMethod - 10
        if self._playMethod:GetIsDanTuo() then
            --warn("CanBet in DanTuo")
            -- 胆拖玩法
            local total = #self._TuoSnail + #self._DanSnail
            --warn("DanTuo:"..total.." num:"..num)
            if total >= num then
                return true
            else
                return false
            end
        else
            if #self._selectedSnail >= num then
                return true
            else
                return false
            end
        end
    end
end


-- 获得下注金额
CBetData.GetBetMoney = function(self)
    if self:CanBet() == false then
        return 0
    end

    if self._playMethod:GetIsDanTuo() then
        warn("计算胆拖")
        self._betType = BetType.DanTuo
        local danNum = #self._DanSnail
        local tuoNum = #self._TuoSnail
        local selectNum = self._betMethod - 10
        return 2 * UtilMath.C(tuoNum, selectNum - danNum)
    end

    if self._betType == BetType.Single then
        warn("单独下注")
        return 2
    end
    warn("计算复试注")
    local num = #self._selectedSnail
    if self._betMethod < BetMethod.QEnd then
        if num < self._betMethod then
            warn("此时无法下注,应该在界面逻辑先进行判断")
            return 0
        end
        return 2 * UtilMath.P1(num, self._betMethod)
    end
    -- 任X玩法
    return 2 * UtilMath.C(num, self._betMethod - 10)
end

--取消指定蜗牛的选中状态,这里我也很想做比较细致的判断，但是胆拖没有规则，只能先做成统一处理
--单选复选倒是可以做成这样
CBetData.CancelSnail = function(self, snailIndex)
    if self._isDanTuo then
        self:ClearAll()
        return
    end 
    for i = #self._selectedSnail, 1, -1 do
        if snailIndex ==  self._selectedSnail[i] then
            table.remove(self._selectedSnail, i)
            return
        end
    end
end

CBetData.ClearAll = function (self)
   	self._selectedSnail = {}
	self._DanSnail = {}
	self._TuoSnail = {}
    self._betType = BetType.Single
    --self._betMethod = BetMethod.Q1
    EventTrigger(EventDefine.CLEAR_BET_DATA)
end