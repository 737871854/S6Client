Lua代码规范
驼峰命名法(camel命名法)小写字母开头，大小写混合，每个单词第一个字母大写，如：doSomeThing
火箭命名法(Pascal命名法)大写字母开头，大小写混合,每个单词第一个字母大写，如：DoSomeThing

一、命名规范，未说明对象一律用骆驼峰命名,
   	lua目录下的文件与文件夹名，使用小写加下划线(_)命名，主要是为了避免不同系统的大小写问题。
	(其实如果大小写使用规范，也是可以使用驼峰命名法的，本项目不使用)
	如：Lua\logic\ui\modules\build_store\ui_btn_build_item_logic.lua

二、类定义
	类名称开头加上大写字符‘C’，必然让读者一眼就能理解到这是一个类，单词第一个字母用大写形式，例如：
	定义一个名叫player的类，格式如下：CPlayer

三、函数定义
为了查找函数原型方便，如下使用：
	DoSomething = function()
	end
尽量使用，动词+名词的方式，例如定义一个“处理事件的函数”:
	HandleEvent = function()
	end
注意：禁止使用handleevent或者Handleevent，handle_event之类命名。(等号两边各一个空格，不能多也不能少)

设置数据和返回数据的函数名因为lua查找特性，请使用SetXXXData 和 SetXXXID 命名。（避免查询麻烦)

四、变量定义
	类内部变量，使用‘_’开头的驼峰命名法，例如CPlayer类下的一个name变量，
	定义为CPlayer._myName
而且这样可以与Import导入文件分开，在查找时非常有用。
	禁止直接引用其它对象的内部成员变量，而应使用Get,Set方法，
	如object:GetMyName，

	全局变量使用gMyName形式。

五、常量定义
以大写字母加_下划线来表示（C,C++遗留习惯）
如local MAX_VALUE = 100

六、定义类时，请在Init函数里把所有会用到的成员变量列出来并做初始化，如
classAA = class()
classAA.Init = function(self)
	self._member1 = null;
	self._member2 = 1
	…
end
这样方便他人理解和修改代码

七、枚举定义
建议用一个table包起来
local FightProcess = {
	Loading = 1, --场景还在加载中
	CountDown = 2, --进入开始倒计时
	Fighting = 3, --开打了
	FightEnd = 4, --战斗结束，包括强制结束
}
枚举成员可以认为是常量，table如同类名，也可以用大写字母加_的方式来定义枚举值；
不过因为一些编码习惯问题，这里一般采用火箭命名法

八、添加注释
我们都希望自己的代码是自注释的，但是有时我们会很惊讶自己忘记代码的速度。
--======================================================================
--（c）copyright 2015 175game.com All Rights Reserved
--======================================================================
-- filename: GUI_Login.lua
-- author: lxt  created: 2015/5/10
-- descrip: 登陆界面输入账户密码的逻辑处理
--======================================================================

在函数前也可以添加标准注释，参考下面示例
--function: CreateMultText
--author:lxt
--descrip: 创建多行文本，同时实现了多行文本走马灯效果

九、编码文件长度控制
一个lua文件过长，需要分析原因（500行以上都看看）功能分解不合理，策划设计细节太多,或未合理利用函数

暂时就这么多，有补充或者建议请提出。
//////////////////////////////////////////////////////////////////////////////////////////////////
以下为Assets/Lua/logic下个目录模块划分基本逻辑
base 
工程基础类，一些枚举定义和本游戏专用的工具类。

common
在下一个项目还可以使用的工具类

data
玩家数据处理，数个UI模块，或者场景对象管理中用到数据需要放在这个目录中编写(例如:战斗和任务的数据)
请不要把单个UI中专用的逻辑和数据处理模块放在此处，直接放在ui->XXX模块

modules
场景逻辑模块 编写目录，同时具体场景的input输入控制也请写在此目录
暂时分为 登陆 主场景 战斗场景
--login
--main
--battle

net 
网络消息处理相关代码

objects
包括对象管理器和lua中定义的基础管理对象actor

ui
--base	UI的管理及ui_base_logic类
--common 公用ui模块，一些标准功能的ui ，通过ui_manager.lua的CUIManager直接生成对应的UI
--XXX模块 目录中包括XXX_logic.lua 、 XXX_view.lua 、 XXX_data(ui模块专用数据处理放在此模块目录中，数个模块都需要放在modules中中）


logic根目录下的game.lua是游戏逻辑入口，操作类全部继承于common/controller_base.lua然后调用gControl:SwitchController（操作类实体）既可。
各种c#中提供的类，及其方法，在生成luawarp类之后，可以在lua中直接调用。

gGame提供了全局的事件注册和发送方法FireGlobalEvent HandleGlobalEvent
 当然也包括取消事件方法 DelGlobalEvent

相关通用代码的说明
--ioo.audioManager:Play2D()  :PlayBFM() 播放各种声音

--CTool.Sec2String 将秒转化为天/时/分格式

TODO: