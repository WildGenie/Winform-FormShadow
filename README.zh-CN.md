# WinForm 投影组件 - FormShadow

[\[ENGLISH README]\](https://github.com/NetDimension/Winform-FormShadow)

__FormShadow__ 能够快速的为你的WinForm窗口绘制窗体投影，当然不是用添加 __CS_DROPSHADOW__ 样式这种又陋又难看的方式。 

其工作原理和最近几个版本的Visual Studio Shell采用的投影方式相同，在主窗体的四周创建4个LayeredWindow来承载阴影图片，并与主窗体同步事件。

![Screen Shot](http://www.ohtrip.cn/media/FormShadowDecorator.png)

之前看GitHub上有另外一个国人做的窗体投影的工具[winform.DropShadow](https://github.com/wenerme/winform.DropShadow)，
虽然实现了投影，而且投影也能够动态生成，但是有几个问题：
- 阴影呈现效果与主窗体极不协调
- 如果把主窗体弄成可以改变大小的形式，那么绘制影子的时候卡顿非常明显
- 似乎内存泄漏，内存越用越大

本人的这个库完美解决上面三个问题，不论从速度还是整体协调上，个人觉得都要更甚一筹（上述组件的作者不要打我[哭笑]）。

对于为什么要做这个组件，很明显，本人的另外一个项目[NanUI](https://github.com/NetDimension/NanUI)已经好久没有更新，之前一直有用Win7的朋友说窗口呈现有问题，而且之前的NanUI窗口影子是使用DWM来实现的，
换句话说就是Win8/Win8.1下面因为系统原因就没有窗口投影，比较难看。虽然有替代的方法为NanUI提供了另外一种画影子的方法，但是实现原理和 __winform.DropShadow__ 这个项目一样，也存在那些问题，所以为了下一版的
NanUI能有个漂亮的影子效果，这个项目就诞生啦！

# 功能
- 为WinForm窗体创建漂亮的投影效果，并且支持 __活动__/__非活动__ 状态下面的投影颜色。
- 快速绘制阴影，不闪烁，不卡顿。
- 主窗体能随意改变大小不受任何限制。



# 使用例子
下面的例子就是使用FormShadow最简单的例子，初始化一个Decorator来为主窗体添加影子效果，然后设置了拖动这些影子能改变主窗体（Borderless）的大小。

```C#
public partial class Form1 : Form
{

	protected readonly FormShadowDecorator ShadowDecorator;

	public Form1()
	{
		InitializeComponent();
		ShadowDecorator = new FormShadowDecorator(this);
		//启用窗口大小调整
		//Enable resizing form with shadows.
		ShadowDecorator.EnableResize(true);
	}
}
```


# 联系作者
有任何疑问欢迎加我的私人QQ（不一定在线，现在大家都微信咯）或者QQ群来讨论任何有关于.NET技术的话题。

__2000人QQ群:__ 241088256

__我的QQ:__ 19843266

__博客园:__ [http://www.cnblogs.com/linxuanchen/](http://www.cnblogs.com/linxuanchen/)

# 赞助作者

如果你喜欢我的工作，那么欢迎您加入到任何项目的开发中来；

当然你也可以非常直接了当的支付宝或微信扫码来请我喝咖啡：）

![Screen Shot](http://www.ohtrip.cn/media/BEG.png)

__“听说之前请我喝咖啡的朋友，最后都走上人生巅峰了呢~”__ 

—— Mr.JSON

